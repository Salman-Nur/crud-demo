using Autofac;
using Hospital.Application.Features.Accounts;
using Hospital.Domain.Features.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PatientService>().As<IPatientService>()
                .InstancePerLifetimeScope();
        }
    }
}
