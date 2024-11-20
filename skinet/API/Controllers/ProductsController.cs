using System.Linq;

using Azure.Core.Pipeline;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{
    //Attributes
    //Server will know where to send the incoming http requests
    [Route("api/[controller]")]
    [ApiController]
    //ProductsController is derived from the . net framework class we are going to derive from a class named ControllerBase
    //public class ProductsController(IProductRepository repo) : ControllerBase
        public class ProductsController(IGenericRepository<Product> repo) : ControllerBase

      {
                   //     private readonly StoreContext context;
                   //     public ProductsController(StoreContext context)
                  //     {
                  //         this.context = context;
                  //     }
        [HttpGet]
        //1.Returning an asynchronous response
        //2.Action Result allow us to return HTTP type of responses
        //3.Returning a type of list IEnumerable of type product from this requset a list of products

        //This method is responsible to get the list of products - inorder to get them from the databse we need to inject the store context in our controller
        // public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand,string? type,string? sort)
            public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
            //When we are pssing object in the parameter we need to specify [FromQuery]
             {
            //Controller
            //return await context.Products.ToListAsync();
            //Repository Pattern
            //return Ok(await repo.GetProductAsync(brand,type,sort));
            //Generic repository
            //  return Ok(await repo.ListAllAsync());

            //Using Specification Pattern
            //Creating a new instance of product specification
            // var spec = new ProductSpecification(brand,type,sort);
            // var products = await repo.ListAsync(spec);
            // return Ok(products);

            var spec = new ProductSpecification(specParams);
            var products = await repo.ListAsync(spec);
            return Ok(products);
            // var spec = new ProductSpecification(specParams);
            // var products = await repo.ListAsync(spec);
            // var count = await repo.CountAsync(spec);

            // var pagination = new Pagination<Product>(specParams.PageIndex,specParams.PageSize,count,products);
            // return Ok(pagination);
           
           

        }

        [HttpGet("{id:int}")] //api/products/2
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            // var product = await context.Products.FindAsync(id);
            // if(product == null) return NotFound();
            // return product;

            // var product = await repo.GetProductByIdAsync(id);
            // if(product == null) return NotFound();
            // return Ok(product);

            var product = await repo.GetByIdAsync(id);
            if(product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            //  context.Products.Add(product);
            // await context.SaveChangesAsync();
            // return product;

            //Add the product
            // repo.AddProduct(product);
            // if(await repo.SaveChangesAsync())
            // {
            //     return CreatedAtAction("GetProducts",new{id = product.Id},product);
            // }
            // return BadRequest("Problem creating product");

            repo.Add(product);
            if(await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProducts",new{id = product.Id},product);
            }
            return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> updateProduct(int id, Product product)
        {
            // if(product.Id != id || !productExists(id))
            //     return BadRequest("Cannot update this product");
             //Modifying the product - Telling entity framework about what we are going to update
             //Entity framework will track the products that we retrieve from the databse

             //context.Entry(product).State = EntityState.Modified;
            // repo.UpdateProduct(product);


             //await context.SaveChangesAsync();
            //  if(await repo.SaveChangesAsync())
            //  {
            //     return NoContent();
            //  }
            ///return NoContent();
            // return BadRequest("Problem in updating the product");

             if(product.Id != id || !productExists(id))
                return BadRequest("Cannot update this product");
            //Modifying the product - Telling entity framework about what we are going to update
            //Entity framework will track the products that we retrieve from the databse

            //context.Entry(product).State = EntityState.Modified;
            repo.Update(product);


            //await context.SaveChangesAsync();
             if(await repo.SaveAllAsync())
             {
                return NoContent();
             }
            //return NoContent();
            return BadRequest("Problem in updating the product");
        }

        [HttpDelete("{id:int}")]
        public async  Task<ActionResult> DeleteProduct(int id)
        {
            //   var product = await context.Products.FindAsync(id);
            //   if(product == null) return NotFound();
            //   context.Products.Remove(product);
            //   await context.SaveChangesAsync();
            //   return NoContent();

            // var product = await repo.GetProductByIdAsync(id);
            // if(product == null) return NotFound();
            // repo.DeleteProduct(product);
            // if(await repo.SaveChangesAsync())
            //  {
            //     return NoContent();
            //  }
            // return BadRequest("Problem in deleting the product");

              var product = await repo.GetByIdAsync(id);
            if(product == null) return NotFound();
            repo.Remove(product);
            if(await repo.SaveAllAsync())
             {
                return NoContent();
             }
            return BadRequest("Problem in deleting the product");
        }
        // [HttpGet("brands")]
        // public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        // {
        //     return Ok(await repo.GetBrandAsync());
        // }
        
         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            //TODO: IMPLEMENT METHOD
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }
        
        // [HttpGet("Types")]
        // public async Task<ActionResult<IReadOnlyList<Strings>>> GetTypes()
        // {
        //     return Ok(await repo.GetTypesAsync());
        // }
         [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<Strings>>> GetTypes()
        {
            //TODO: IMPLEMENT METHOD
            var spec = new TypeListSpecification();
            return Ok( await repo.ListAsync(spec));
        }
        //Checking if product exists or not in the database
        private bool productExists(int id)
        {
            //return context.Products.Any(x=>x.Id == id);
            return repo.Exists(id);
        }

    }
}
