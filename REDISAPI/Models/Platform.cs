using System;
using System.ComponentModel.DataAnnotations;

namespace REDISAPI.Models
{
    public class Platform{
        [Required]
        public string Id { get; set; } = $"platform:{Guid.NewGuid().ToString()}";
        
        [Required]
        public string Name {get;set;} = String.Empty;
    }
}