using Purrfect_Blog_Starter.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Purrfect_Blog_Starter.Dtos
{
    public class SavedFactDto
    {
        public int Id { get; set; } 

        [Required]
        [MaxLength(128)]
        public string UserId { get; set; } 

        [Required]
        [MaxLength(1000)] 
        public string Text { get; set; }  

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}