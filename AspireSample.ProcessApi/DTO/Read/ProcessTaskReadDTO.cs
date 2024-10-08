using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Read
{
    public class ProcessTaskReadDTO
    {
        public Guid? Id { get; set; }

        public string? TaskId { get; set; }

        public string? Description { get; set; }
        
        public string? TopicName { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
