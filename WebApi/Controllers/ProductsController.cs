using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _sql;
        private readonly NoSqlContext _nosql;
        private readonly FileService _fileService;

        public ProductsController(SqlContext sql, FileService fileService, NoSqlContext nosql)
        {
            _sql = sql;
            _fileService = fileService;
            _nosql = nosql;
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

            _nosql.Products.Add(product);
            await _nosql.SaveChangesAsync();

            return Created("", product);
        }
    }
}
