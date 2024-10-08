using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Write
{
    public class AddProcessTaskDefinitionDTO
    {
        [Required, MaxLength(64)]
        public string? Id { get; set; }

        [Required, MaxLength(128)]
        public string? Description { get; set; }

        [Required, MaxLength(96)]
        public string? TopicName { get; set; }
    }
}
