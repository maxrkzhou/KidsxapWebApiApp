using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLogic.TestMock
{
    public class ProductServiceManagerMock : IProductServiceManager
    {
        private IList<Product> TestProducts;

        public ProductServiceManagerMock()
        {
            TestProducts = new List<Product>()
            {
                new Product(){Id = new Guid("1D9701B6-94D2-47B0-B09C-E3865383CB37"), Name = "Product1", ExpirayDate = new DateTime(2017, 7,20),  isDeleted = false },
                new Product(){Id = new Guid("DCC058BE-7EF8-45D5-8D87-942B90770A7D"), Name = "Product2", ExpirayDate = new DateTime(2017, 8,20),  isDeleted = false },
                new Product(){Id = new Guid("FC6155D5-6C33-485F-B404-D2593BA93A51"), Name = "Product3", ExpirayDate = new DateTime(2017, 9,20),  isDeleted = false },
                new Product(){Id = new Guid("1B16518B-C6FE-44B5-9228-936E90FB6AB1"), Name = "Product4", ExpirayDate = new DateTime(2017, 10,20),  isDeleted = false },
                new Product(){Id = new Guid("C779B4DE-D4F2-4C6C-BFDB-96486D1B013C"), Name = "Product5", ExpirayDate = new DateTime(2017, 11,20),  isDeleted = true }
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            TestProducts.Where(p => p.Id == id).FirstOrDefault().isDeleted = true;
        }

        public async Task<IList<Product>> FindAllAsync()
        {
            return TestProducts.Where(p=>p.isDeleted == false).ToList();
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            return TestProducts.Where(p => p.Name == name && p.isDeleted == false).FirstOrDefault();
        }
    }
}
