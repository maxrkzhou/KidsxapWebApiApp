using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IList<Product>> FindAllAsync();
        Task<Product> FindByNameAsync(string name);
        Task DeleteAsync(Guid id);
    }
}
