using AspireSample.ProcessApi.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.Data
{
    public class Process : ITimestamped
    {
        [Key, Required]
        public Guid Id { get; set; }

        [Required, MaxLength(64)]
        public string? ProcessDefinitionId { get; set; }

        [Required, MaxLength(128)]
        public string? Description { get; set; }

        [Required]
        public List<ProcessTask>? Tasks { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
