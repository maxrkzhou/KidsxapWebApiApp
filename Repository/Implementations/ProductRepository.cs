using AutoMapper;
using EntityModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MyDbContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = from p in _context.Products
                        where p.Id == id
                        select p;
            var entity = await query.FirstOrDefaultAsync();
            entity.isDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Product>> FindAllAsync()
        {
            var query = from p in _context.Products
                        select p;
            var entityList = await query.ToListAsync();
            var modelList = _mapper.Map<IList<DtoProduct>, IList<Product>>(entityList);
            return modelList;
        }

        public async Task<Product> FindByNameAsync(string name)
        {
            var query = from p in _context.Products
                        where p.Name == name
                        select p;
            var entity = await query.FirstOrDefaultAsync();
            var model = _mapper.Map<DtoProduct, Product>(entity);
            return model;

        }
    }
}
