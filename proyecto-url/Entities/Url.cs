using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_url.Entities
{
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //es autoincrementable
        public int Id { get; set; }
        [StringLength(50)]
        public string? ShortUrl { get; set; }

        [Required]
        [StringLength(500)]
        public string LongUrl { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitsCount { get; set; } = 1;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } //relación a una sola categoría
        
    }
}
