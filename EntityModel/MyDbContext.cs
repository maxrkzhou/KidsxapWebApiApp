using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=DatabaseContext")
        {
            //DbInterception.Add(new SoftDeleteInterceptor());
            Database.SetInitializer(new MyDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<DtoProduct> Products { get; set; }
    }

    public class MyDBInitializer : CreateDatabaseIfNotExists<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            IList<DtoProduct> products = new List<DtoProduct>() {
                new DtoProduct(){Id = Guid.NewGuid(), Name = "Product1", ExpirayDate = DateTime.Now.AddDays(3), isDeleted = false },
                new DtoProduct(){Id = Guid.NewGuid(), Name = "Product2", ExpirayDate = DateTime.Now.AddDays(4), isDeleted = false },
                new DtoProduct(){Id = Guid.NewGuid(), Name = "Product3", ExpirayDate = DateTime.Now.AddDays(5), isDeleted = false },
                new DtoProduct(){Id = Guid.NewGuid(), Name = "Product4", ExpirayDate = DateTime.Now.AddDays(6), isDeleted = false },
                new DtoProduct(){Id = Guid.NewGuid(), Name = "Product5", ExpirayDate = DateTime.Now.AddDays(7), isDeleted = false },
            };

            context.Products.AddRange(products);

            base.Seed(context);
        }
    }
}
