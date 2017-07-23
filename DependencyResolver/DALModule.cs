using EntityModel;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public class DALModule : NinjectModule
    {
        public override void Load()
        {
            //add all your relations here
            Bind<MyDbContext>().ToSelf();
        }
    }
}
