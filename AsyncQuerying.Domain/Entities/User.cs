using AsyncQuerying.Domain.Abstractions.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AsyncQuerying.Domain.Entities
{
    public class User : Entity
    {
        [Required]
        [MaxLength(255)]
        public String Name { get; protected set; }
    }
}
