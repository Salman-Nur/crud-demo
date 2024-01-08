
using Autofac;
using Exam1.Application.Features.Admission.Services;

namespace Exam1.Application
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
