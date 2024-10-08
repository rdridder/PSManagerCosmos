using AspireSample.ProcessApi.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.Data
{
    public class ProcessDefinitionTask : ITimestamped
    {
        [Required, MaxLength(64)]
        public string? Id { get; set; }

        [Required, MaxLength(128)]
        public string? Description { get; set; }
        
        [Required, MaxLength(96)]
        public string? TopicName { get; set; }
        
        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public DateTime? Modified { get; set; }
    }
}
