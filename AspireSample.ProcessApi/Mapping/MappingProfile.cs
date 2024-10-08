using AspireSample.ProcessApi.Data;
using AspireSample.ProcessApi.DTO.Read;
using AspireSample.ProcessApi.DTO.Write;
using AutoMapper;

namespace AspireSample.ProcessApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<AddProcessDefinitionDTO, ProcessDefinition>();
            CreateMap<ProcessTaskIdDTO, ProcessDefinitionTask>();
            CreateMap<ProcessTaskDefinition, ProcessDefinitionTask>();
            CreateMap<AddProcessTaskDefinitionDTO, ProcessTaskDefinition>();
            CreateMap<ProcessDefinition, Process>()
                .ForMember(dest => dest.ProcessDefinitionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProcessDefinitionTask, ProcessTask>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.Modified, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Process, ProcessReadDTO>();
            CreateMap<ProcessTask, ProcessTaskReadDTO>();
            CreateMap<ProcessDefinition, ProcessDefinitionReadDTO>();
            CreateMap<ProcessDefinitionTask, ProcessTaskDefinitionReadDTO>();
        }
    }
}
