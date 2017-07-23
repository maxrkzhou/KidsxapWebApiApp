using AutoMapper;
using BusinessLogic.Implementations;
using BusinessLogic.Interfaces;
using EntityModel;
using Ninject;
using Ninject.Modules;
using Repository.Implementations;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    //for test purpose, include all the relations
    public class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MyDbContext>().ToSelf();
            Bind<IProductRepository>().To<ProductRepository>();
            Bind<IProductServiceManager>().To<ProductServiceManager>();
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfiles(GetType().Assembly);
                cfg.AddProfile(new RepositoryProfile());
            });

            return config;
        }
    }
}
