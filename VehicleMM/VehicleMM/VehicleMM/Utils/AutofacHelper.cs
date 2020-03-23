using Autofac;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMM.Model;
using VehicleMM.View;
using VehicleMM.ViewModel;

namespace VehicleMM.Utils
{
    public class AutofacHelper
    {
        static IContainer Container;
        static AutofacHelper Instance;

        private AutofacHelper()
        {
            Container = BuildContainer();
        }
        public static AutofacHelper GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AutofacHelper();
            }
            return Instance;
        }

        private IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<VehicleMakeService>().SingleInstance();
            builder.RegisterType<VehicleMakeViewModel>().SingleInstance();
            builder.RegisterType<VehicleMakeView>().SingleInstance();
            builder.RegisterType<VehicleMakeModel>().InstancePerDependency();

            builder.RegisterType<VehicleModelService>().SingleInstance();
            builder.RegisterType<VehicleModelView>();
            builder.RegisterType<VehicleModelViewModel>();
            builder.RegisterType<VehicleModelModel>().InstancePerDependency();

            builder.RegisterModule<ServiceAutofacModule>();
            return builder.Build();
        }

        public IContainer GetContainer()
        {
            return Container;
        }
    }
}
