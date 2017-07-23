using BusinessLogic.Implementations;
using BusinessLogic.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public class PLModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductServiceManager>().To<ProductServiceManager>();
        }
    }
}
