using ASPNET_Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core.Interfaces
{
    public interface ICategoryRepository
    {

        Task<List<Category>> ListAll();
        Task<Category> GetByID(int id);
        Task<bool> Save(Category category);
        Task<bool> Put(Category category);
        Task<bool> Remove(Category category);

    }
}
