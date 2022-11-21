using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _sql;
        private readonly FileService _fileService;

        public ProductsController(SqlContext sql, FileService fileService)
        {
            _sql = sql;
            _fileService = fileService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductRequest req)
        {
            var uri = await _fileService.Upload(req.ImageFile);

            var product = new Product
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                ImageUrl = uri
            };

            _sql.Products.Add(product);
            await _sql.SaveChangesAsync();

            return Created("", product);
        }
    }
}
