using ASPNET_Core.Data;
using ASPNET_Core.Interfaces;
using ASPNET_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly StoreDataContext context;

        public CategoryRepository(StoreDataContext context)
        {
            this.context = context;
        }

        public async Task<Category> GetByID(int id)
        {
            Category category = null;
            try
            {
                category = await context.Categories.AsNoTracking().Where(cat => cat.Id == id).FirstOrDefaultAsync();
            } catch (Exception ex)
            {
                Console.WriteLine("Get -> " + ex.Message);
            }
            return category;
        }

        public async Task<List<Category>> ListAll()
        {
            List<Category> categories = null;
            try
            {
                categories = await context.Categories.AsNoTracking().ToListAsync();
            } catch (Exception ex)
            {
                Console.WriteLine("List All -> " + ex.Message);
            }
            return categories;
        }

        public async Task<bool> Put(Category category)
        {
            bool resultUpdate = false;
            try
            {
                context.Entry<Category>(category).State = EntityState.Modified;
                int resultLine = await context.SaveChangesAsync();
                resultUpdate = resultLine > 0 ? true : false;
            } catch (Exception ex)
            {
                Console.WriteLine("Put -> " + ex.Message);
            }
            return resultUpdate;
        }

        public async Task<bool> Remove(Category category)
        {
            bool resultRemove = false;
            try
            {
                context.Categories.Remove(category);
                int resultLine = await context.SaveChangesAsync();
                resultRemove = resultLine > 0 ? true : false;
            } catch (Exception ex)
            {
                Console.WriteLine("Remove -> " + ex.Message);
            }
            return resultRemove;
        }

        public async Task<bool> Save(Category category)
        {
            bool resultInsert = false;
            try
            {
                context.Categories.Add(category);
                int resultLine = await context.SaveChangesAsync();
                resultInsert = resultLine > 0 ? true : false;
            } catch (Exception ex)
            {
                Console.WriteLine("Save -> " + ex.Message);
            }
            return resultInsert;
        }
    }
}
