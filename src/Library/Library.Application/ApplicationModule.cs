using Autofac;
using Library.Application.Features.Catalog;
using Library.Domain.Features.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookManagementService>().As<IBookManagementService>()
                .InstancePerLifetimeScope();
        }
    }
}
