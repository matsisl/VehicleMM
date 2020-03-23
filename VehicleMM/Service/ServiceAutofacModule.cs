using Autofac;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ServiceAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataSource>().As<IDataSource>().SingleInstance().InstancePerLifetimeScope();
        }
    }
}
