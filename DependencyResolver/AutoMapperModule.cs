using AutoMapper;
using EntityModel;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace DependencyResolver
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
  
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

    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            CreateMap<DtoProduct, Product>();
            CreateMap<Product, DtoProduct>();
        }
    }
}
