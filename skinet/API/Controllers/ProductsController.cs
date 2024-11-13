using System.Linq;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //Attributes
    //Server will know where to send the incoming http requests
    [Route("api/[controller]")]
    [ApiController]
    //ProductsController is derived from the . net framework class we are going to derive from a class named ControllerBase
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;
        public ProductsController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet]
        //1.Returning an asynchronous response
        //2.Action Result allow us to return HTTP type of responses
        //3.Returning a type of list IEnumerable of type product from this requset a list of products

        //This method is responsible to get the list of products - inorder to get them from the databse we need to inject the store context in our controller
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        [HttpGet("{id:int}")] //api/products/2
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await context.Products.FindAsync(id);
            if(product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
             context.Products.Add(product);

            await context.SaveChangesAsync();

            return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> updateProduct(int id, Product product)
        {
            if(product.Id != id || !productExists(id))
                return BadRequest("Cannot update this product");
            //Modifying the product - Telling entity framework about what we are going to update
            //Entity framework will track the products that we retrieve from the databse

            context.Entry(product).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async  Task<ActionResult> DeleteProduct(int id)
        {
              var product = await context.Products.FindAsync(id);
              if(product == null) return NotFound();
              context.Products.Remove(product);
              await context.SaveChangesAsync();
              return NoContent();
        }
        //Checking if product exists or not in the database
        private bool productExists(int id)
        {
            return context.Products.Any(x=>x.Id == id);
        }

    }
}
