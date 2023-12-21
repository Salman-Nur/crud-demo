using Autofac;
using University.Application.Features.Admission;
using University.Domain.Features.Admission;


namespace University.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentManagementService>().As<IStudentManagementService>()
                .InstancePerLifetimeScope();
        }
    }
}
