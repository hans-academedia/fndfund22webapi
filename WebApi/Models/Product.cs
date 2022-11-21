using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
