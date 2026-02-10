using Demo.Data;
using Demo.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext Context;

        public ProductController(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if(Context ==  null)
            {
                return NotFound();
            }
            return await Context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            if(Context.Products == null)
            {
                return NotFound();
            }
            var product = await Context.Products.FindAsync(id);
            return Ok(product);

        }
        [HttpPost]

        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (Context == null)
            {
                return NotFound();
            }

            var product = await Context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            Context.Products.Remove(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }

    }
}
