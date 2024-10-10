using AspireSample.ProcessApi.Data.Enums;
using AspireSample.ProcessApi.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.Data
{
    public class ProcessTask : ITimestamped
    {
        [Required]
        public Guid? Id { get; set; }

        [Required, MaxLength(64)]
        public string? TaskId { get; set; }

        [Required, MaxLength(128)]
        public string? Description { get; set; }
        
        [Required, MaxLength(96)]
        public string? TopicName { get; set; }

        [Required]
        public string Status { get; set; } = Enums.Status.PENDING.ToString();

        [Required]
        public int Version { get; set; } = 1;

        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public DateTime? Modified { get; set; }
    }
}
