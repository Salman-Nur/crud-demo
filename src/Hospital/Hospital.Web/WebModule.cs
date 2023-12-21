

using Autofac;
using Hospital.Web.Areas.Admin.Models;
using System;

namespace Hospital.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PatientCreateModel>().AsSelf();
            builder.RegisterType<PatientListModel>().AsSelf();
        }
    }
}
