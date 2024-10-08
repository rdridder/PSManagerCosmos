using AspireSample.ProcessApi.Context;
using AspireSample.ProcessApi.Data;
using AspireSample.ProcessApi.DTO;
using AspireSample.ProcessApi.DTO.Read;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AspireSample.ProcessApi.Controllers
{
    [ApiController]
    [Route("process")]
    public class ProcessReadController : ControllerBase
    {
        private readonly ProcessContext _dbContext;

        private readonly IMapper _mapper;

        private static bool _ensureCreated { get; set; } = false;

        public ProcessReadController(ProcessContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;


            if (!_ensureCreated)
            {
                _dbContext.Database.EnsureCreated();
                _ensureCreated = true;
            }
        }

        [HttpGet("processDefinition")]
        public async Task<IActionResult> GetProcessDefinition()
        {
            var items = await _dbContext.ProcessDefinition.ToListAsync();
            return Ok(_mapper.Map<List<ProcessDefinitionReadDTO>>(items));
        }

        [HttpGet("process")]
        public async Task<IActionResult> GetProcess()
        {
            var items = await _dbContext.Process.ToListAsync();
            return Ok(_mapper.Map<List<ProcessReadDTO>>(items));
        }

        [HttpGet("processTask")]
        public async Task<IActionResult> GetProcessTask()
        {
            var items = await _dbContext.ProcessTaskDefinition.ToListAsync();
            return Ok(_mapper.Map<List<ProcessTaskDefinitionReadDTO>>(items));
        }
    }
}
