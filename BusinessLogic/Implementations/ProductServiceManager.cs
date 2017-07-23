using BusinessLogic.Interfaces;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLogic.Implementations
{
    public class ProductServiceManager : IProductServiceManager
    {
        private readonly IProductRepository _productRepo;
        public ProductServiceManager(IProductRepository _productRepo)
        {
            this._productRepo = _productRepo;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _productRepo.DeleteAsync(id);
        }

        public async Task<IList<Product>> FindAllAsync()
        {
            return await _productRepo.FindAllAsync();
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            return await _productRepo.FindByNameAsync(name);
        }
    }
}
