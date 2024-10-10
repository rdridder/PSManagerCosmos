using AspireSample.ProcessApi.Context;
using AspireSample.ProcessApi.Data;
using AspireSample.ProcessApi.Data.Interfaces;
using AspireSample.ProcessApi.DTO.Write;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspireSample.ProcessApi.Controllers
{
    [ApiController]
    [Route("process")]
    public class ProcessInsertController : ControllerBase
    {
        private readonly ProcessContext _dbContext;

        private readonly IMapper _mapper;

        private readonly ServiceBusClient _serviceBusClient;

        private static bool _ensureCreated { get; set; } = false;

        public ProcessInsertController(ProcessContext dbContext, IMapper mapper, ServiceBusClient serviceBusClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _serviceBusClient = serviceBusClient;

            if (!_ensureCreated)
            {
                _dbContext.Database.EnsureCreated();
                _ensureCreated = true;
            }
        }

        private void SetNewlyCreated(ITimestamped item, DateTime utcDateTime) {
            item.Created = utcDateTime;
            item.Modified = utcDateTime;
        }

        private void SetUpdated(ITimestamped item, DateTime utcDateTime) {
            item.Modified = utcDateTime;
        }

        [HttpPost("processDefinition")]
        public async Task<IActionResult> CreateProcessDefinition([FromBody] AddProcessDefinitionDTO processDefinitionDTO)
        {
            if (!ModelState.IsValid) {
                return ValidationProblem();
            }
            var setOfTaskIds = processDefinitionDTO?.Tasks?.ConvertAll(x => x.Id).ToHashSet() ?? [];
            var tasksFromDb = await _dbContext.ProcessTaskDefinition.Where(x => setOfTaskIds.Contains(x.Id)).ToListAsync();
            
            if (tasksFromDb?.Count != setOfTaskIds.Count)
            {
                ModelState.AddModelError("Tasks", $"Not all tasks for the process definition are found in the database. {setOfTaskIds.Count} tasks supplied, {tasksFromDb?.Count ?? 0} found. Tasks found in the database: {string.Join(',', tasksFromDb?.ConvertAll(x => x.Id) ?? [])}");
                return ValidationProblem();
            }
            var processDefinition = _mapper.Map<ProcessDefinition>(processDefinitionDTO);
            processDefinition.Tasks = _mapper.Map<List<ProcessDefinitionTask>>(tasksFromDb);
            var addedProcessDefinition = await _dbContext.AddAsync(processDefinition);
            await _dbContext.SaveChangesAsync();
            return Ok(addedProcessDefinition.Entity);
        }

        [HttpPost("process")]
        public async Task<IActionResult> CreateProcess([FromBody] AddProcessDTO processDefinitionDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var processDefinitionFromDb = await _dbContext.ProcessDefinition.FindAsync(processDefinitionDTO.ProcessDefinitionId);

            if (processDefinitionFromDb == null)
            {
                ModelState.AddModelError("ProcessDefinitionId", $"Processdefinition with Id {processDefinitionDTO.ProcessDefinitionId} is not found in the database. Can't create a process if no definition exists.");
                return ValidationProblem();
            }
            var process = _mapper.Map<Process>(processDefinitionFromDb);
            // Add the id from the request to the process
            process.Id = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            foreach (var task in process.Tasks ?? [])
            {
                task.Id = Guid.NewGuid();
                SetNewlyCreated(task, dateTime);
            }
            SetNewlyCreated(process, dateTime);
            var addedProcess = await _dbContext.AddAsync(process);
            await _dbContext.SaveChangesAsync();
            await SendStartTaskMessage(addedProcess.Entity);
            return Ok(addedProcess.Entity);
        }

        [HttpPost("processTaskDefinition")]
        public async Task<IActionResult> CreateProcessTaskDefinition([FromBody] ProcessTaskDefinition processTaskDefinitionDTO)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var processTaskDefinition = _mapper.Map<ProcessTaskDefinition>(processTaskDefinitionDTO);
            var addedProcessTaskDefinition = await _dbContext.AddAsync(processTaskDefinition);
            await _dbContext.SaveChangesAsync();
            return Ok(addedProcessTaskDefinition.Entity);
        }

        private async Task SendStartTaskMessage(Process process) {
            // TODO, check how to use enums properly
            var task = (process.Tasks?.FirstOrDefault(x => x.Status == "PENDING")) ?? throw new InvalidOperationException($"No task with status pending found for process: {process.Id}");
            var sender = _serviceBusClient.CreateSender(task.TopicName);
            await sender.SendMessageAsync(new ServiceBusMessage("Test message"));
            await sender.DisposeAsync();
        }
    }
}
