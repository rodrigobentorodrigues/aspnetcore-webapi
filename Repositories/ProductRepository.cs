using ASPNET_Core.Data;
using ASPNET_Core.Interfaces;
using ASPNET_Core.Models;
using ASPNET_Core.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly StoreDataContext context;

        public ProductRepository(StoreDataContext context)
        {
            this.context = context;
        }

        public async Task<List<ListProductViewModel>> ListAll()
        {
            List<ListProductViewModel> products = null;
            try
            {
                products = await context.Products.Include((product) => product.Category).
                Select((productAux) => new ListProductViewModel
                {
                    Id = productAux.Id,
                    Title = productAux.Title,
                    Price = productAux.Price,
                    Category = productAux.Category.Title,
                    CategoryID = productAux.CategoryId
                }).AsNoTracking().ToListAsync();
            } catch (Exception ex)
            {
                Console.WriteLine("List All -> " + ex.Message);
            }
            return products;
        }

        public async Task<Product> Get(int id)
        {
            Product product = null;
            try
            {
                product = await context.Products.FindAsync(id);
            } catch (Exception ex)
            {
                Console.WriteLine("Get -> " + ex.Message);
            }
            return product;
        }

        public async Task<bool> Save(Product product)
        {
            bool resultSave = false;
            try
            {
                context.Products.Add(product);
                int result = await context.SaveChangesAsync();
                resultSave = result > 0 ? true : false;
            } catch (Exception ex)
            {
                Console.WriteLine("Save -> " + ex.Message);
            }
            return resultSave;
        }

        public async Task<bool> Put(Product product)
        {
            bool resultUpdate = false;
            try
            {
                context.Entry<Product>(product).State = EntityState.Modified;
                int resultLine = await context.SaveChangesAsync();
                resultUpdate = resultLine > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put (UPDATE) -> " + ex.Message);
            }
            return resultUpdate;
        }

        public async Task<bool> Delete(Product product)
        {
            bool resultUpdate = false;
            try
            {
                context.Products.Remove(product);
                int resultLine = await context.SaveChangesAsync();
                resultUpdate = resultLine > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete -> " + ex.Message);
            }
            return resultUpdate;
        }

    }
}
