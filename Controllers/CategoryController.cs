using ASPNET_Core.Interfaces;
using ASPNET_Core.Models;
using ASPNET_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] ICategoryRepository repository)
        {
            var categories = await repository.ListAll();
            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetByID([FromServices] ICategoryRepository repository, int id)
        {
            var category = await repository.GetByID(id);
            return category;
        }

        //[HttpGet]
        //[Route("{id:int}/products")]
        //public async Task<ActionResult<List<Product>>> GetProducts([FromServices] ICategoryRepository repository, int id)
        //{
        //    var products = await context.Products.AsNoTracking()
        //        .Include((product) => product.Category.Id == id).ToListAsync();
        //    return products;
        //}

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Post([FromServices] ICategoryRepository repository, 
            [FromBody] Category model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a categoria!",
                    Data = model.Notifications
                };
            } else
            {
                await repository.Save(model);
                return new ResultViewModel
                {
                    Success = true,
                    Message = "Categoria cadastrada com sucesso!",
                    Data = model
                };
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Put([FromServices] ICategoryRepository repository, 
            [FromBody] Category model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar a categoria!",
                    Data = model.Notifications
                };
            }
            else
            {
                await repository.Put(model);
                return new ResultViewModel
                {
                    Success = true,
                    Message = "Categoria alterada com sucesso!",
                    Data = model
                };
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ResultViewModel>> Delete([FromServices] ICategoryRepository repository,
            int id)
        {
            Category model = await repository.GetByID(id);
            if (model == null)
            {
                return NotFound();
            }

            await repository.Remove(model);
            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria removida com sucesso!",
                Data = model
            };
        }

    }
}
