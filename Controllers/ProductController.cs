using ASPNET_Core.Interfaces;
using ASPNET_Core.Models;
using ASPNET_Core.ViewModels;
using ASPNET_Core.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        // Não aconselhavel a itens mutaveis
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 10)]
        public async Task<ActionResult<List<ListProductViewModel>>> Get([FromServices] IProductRepository repository)
        {
            var products = await repository.ListAll();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get([FromServices] IProductRepository repository,
            int id)
        {
            var product = await repository.Get(id);
            return product;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Post([FromServices] IProductRepository repository,
            [FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto!",
                    Data = model.Notifications
                };
            }
            else 
            {
                var product = new Product();
                product.Title = model.Title;
                product.Description = model.Description;
                product.Image = model.Image;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.CreateDate = DateTime.Now;
                product.LastUpdateDate = DateTime.Now;
                await repository.Save(product);
                return new ResultViewModel
                {
                    Success = true,
                    Message = "Produto foi salvo com sucesso!",
                    Data = product
                };
            } 
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Put([FromServices] IProductRepository repository, 
            [FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto!",
                    Data = model.Notifications
                };
            }
            else
            {
                var product = await repository.Get(model.Id);
                if (product != null)
                {
                    return NotFound();
                }
                else
                {
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.Image = model.Image;
                    product.Price = model.Price;
                    product.Quantity = model.Quantity;
                    product.CategoryId = model.CategoryId;
                    product.LastUpdateDate = DateTime.Now;

                    await repository.Put(product);
                    return new ResultViewModel
                    {
                        Success = true,
                        Message = "Produto alterado com sucesso!",
                        Data = product
                    };
                }
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Delete([FromServices] IProductRepository repository, 
            [FromBody] Product model)
        {
            bool result = await repository.Delete(model);
            if (result)
            {
                return new ResultViewModel
                {
                    Success = true,
                    Message = "Produto excluído com sucesso!",
                    Data = model
                };
            } else
            {
                return BadRequest();
            }
        }
            
    }
}
