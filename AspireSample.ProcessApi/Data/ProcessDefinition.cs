using AspireSample.ProcessApi.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.Data
{
    public class ProcessDefinition : ITimestamped
    {
        [Key, Required, MaxLength(64)]
        public string? Id { get; set; }

        [Required, MaxLength(128)]
        public string? Description { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [Required]
        public List<ProcessDefinitionTask>? Tasks { get; set; }

        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public DateTime? Modified { get; set; }
    }
}
