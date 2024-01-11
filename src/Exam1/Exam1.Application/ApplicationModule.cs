using Autofac;
using Exam1.Application.Features.Inventory.Services;

namespace Exam1.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManagementService>().As<IProductManagementService>()
                .InstancePerLifetimeScope();
        }
    }
}
