﻿using System.ComponentModel.DataAnnotations;

namespace AspireSample.ProcessApi.DTO.Read
{
    public class ProcessReadDTO
    {
        public Guid Id { get; set; }

        public string? ProcessDefinitionId { get; set; }

        public string? Description { get; set; }

        public List<ProcessTaskReadDTO>? Tasks { get; set; }

        public string? Status { get; set; }

        public int Version { get; set; } = 1;

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
