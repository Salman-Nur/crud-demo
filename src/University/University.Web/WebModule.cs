using Autofac;
using University.Web.Areas.Admin.Models;

namespace University.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentCreateModel>().AsSelf();
            builder.RegisterType<StudentListModel>().AsSelf();
        }
    }
}
