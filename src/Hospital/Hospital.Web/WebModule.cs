using Autofac;
using Hospital.Web.Areas.Admin.Models;

namespace Hospital.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PatientCreateModel>().AsSelf();
            builder.RegisterType<PatientListModel>().AsSelf();
            builder.RegisterType<PatientUpdateModel>().AsSelf();
        }
    }
}
