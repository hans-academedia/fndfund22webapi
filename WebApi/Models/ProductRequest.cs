using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ProductRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
    }
}
