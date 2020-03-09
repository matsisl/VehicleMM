using Autofac;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AutofacContainer
    {
        internal static IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterTypes(builder);
            return builder.Build();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DataSource>().As<IDataSource>();
        }
    }
}
