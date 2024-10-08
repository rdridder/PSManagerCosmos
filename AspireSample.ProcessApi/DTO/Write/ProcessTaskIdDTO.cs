using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Write
{
    public class ProcessTaskIdDTO
    {
        [Required, MaxLength(64)]
        public string? Id { get; set; }
    }
}
