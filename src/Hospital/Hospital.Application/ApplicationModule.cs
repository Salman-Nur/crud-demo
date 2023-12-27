using Autofac;
using Hospital.Application.Features.Account;
using Hospital.Domain.Features.Account;
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
            builder.RegisterType<PatientManagementService>().As<IPatientManagementService>()
                .InstancePerLifetimeScope();
        }
    }
}
