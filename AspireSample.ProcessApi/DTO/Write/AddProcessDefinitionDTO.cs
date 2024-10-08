using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Write
{
    public class AddProcessDefinitionDTO
    {
        [Required, MaxLength(64)]
        public string? Id { get; set; }

        [Required, MaxLength(128)]
        public string? Description { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [Required]
        public List<ProcessTaskIdDTO>? Tasks { get; set; }
    }
}
