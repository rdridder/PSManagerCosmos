using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Read
{
    public class ProcessDefinitionReadDTO
    {
        public string? Id { get; set; }

        public string? Description { get; set; }

        public bool Enabled { get; set; }

        public List<ProcessTaskDefinitionReadDTO>? Tasks { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
