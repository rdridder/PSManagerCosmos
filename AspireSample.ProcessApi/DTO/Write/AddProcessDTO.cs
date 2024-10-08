using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Write
{
    public class AddProcessDTO
    {
        [Required, MaxLength(64)]
        public string? ProcessDefinitionId { get; set; }
    }
}
