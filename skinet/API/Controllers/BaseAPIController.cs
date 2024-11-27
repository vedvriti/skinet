using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        public async Task<IActionResult> CreatePagedResult<T>(IGenericRepository<T> repo, 
        ISpecification<T> spec, int PageIndex, int PageSize) where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);
            var pagination = new Pagination<T>(
               PageIndex,PageSize,count,items);
            return Ok(pagination);
        }
    }
}
