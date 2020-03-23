using Autofac;
using System;
using VehicleMM.Utils;
using VehicleMM.View;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace VehicleMM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            using (var scope = AutofacHelper.GetInstance().GetContainer().BeginLifetimeScope())
            {
                var vmv = scope.Resolve<VehicleMakeView>();
                var navigationPage = new NavigationPage(vmv);
                MainPage = navigationPage;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
