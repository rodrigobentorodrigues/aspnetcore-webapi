using ASPNET_Core.Models;
using ASPNET_Core.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core.Interfaces
{
    public interface IProductRepository
    {

        Task<List<ListProductViewModel>> ListAll();
        Task<Product> Get(int id);
        Task<bool> Save(Product product);
        Task<bool> Put(Product product);
        Task<bool> Delete(Product product);

    }
}
