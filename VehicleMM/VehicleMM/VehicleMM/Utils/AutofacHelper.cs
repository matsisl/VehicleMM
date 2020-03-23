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
            builder.RegisterType<VehicleMakeService>().SingleInstance().WithParameter("m",AutoMapperHelper.GetInsance().GetMapper());
            builder.RegisterType<VehicleMakeViewModel>().SingleInstance().WithParameter("m", AutoMapperHelper.GetInsance().GetMapper());
            builder.RegisterType<VehicleMakeView>().SingleInstance();
            builder.RegisterType<VehicleMakeModel>().InstancePerDependency();

            builder.RegisterType<VehicleModelService>().SingleInstance().WithParameter("m", AutoMapperHelper.GetInsance().GetMapper());
            builder.RegisterType<VehicleModelView>();
            builder.RegisterType<VehicleModelViewModel>().WithParameter("m", AutoMapperHelper.GetInsance().GetMapper());
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
