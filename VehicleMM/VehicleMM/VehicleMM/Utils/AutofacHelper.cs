using Autofac;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleMM.Utils
{
    public class AutofacHelper
    {
        internal static IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterTypes(builder);
            return builder.Build();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().Named<VehicleMakeService>("MakeService").InstancePerLifetimeScope();
        }
    }
}
