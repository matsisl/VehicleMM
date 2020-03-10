using AutoMapper;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VehicleMM.Model;
using VehicleMM.Utils;
using Autofac;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using VehicleMM.View;

namespace VehicleMM.ViewModel
{
    public class VehicleMakeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Autofac.IContainer container;
        private IMapper mapper = AutoMapperHelper.Maps();
        VehicleMakeService vms;
        VehicleMakeModel vehicleMake = new VehicleMakeModel();
        public VehicleMakeViewModel()
        {
            container = AutofacHelper.Build();
            vms = container.ResolveNamed<VehicleMakeService>("MakeService");
            VehicleMakes = new ObservableCollection<VehicleMakeModel>();
            getVehicleMake();

            CreateComand = new Command(() =>
            {
                createCommandFunction();
            });

            DeleteComand = new Command(() =>
            {
                deleteCommandFunction();
            });

            UpdateCommand = new Command(() =>
            {
                updateCommandFunction();
            });

            SelectedMakeCommand = new Command(() =>
            {
                seletedMakeCommandFunction();
            });
        }

        public int Id
        {
            get => vehicleMake.Id;
            set
            {
                vehicleMake.Id = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(vehicleMake.Id)));
            }
        }
        public string Name
        {
            get => vehicleMake.Name;
            set
            {
                vehicleMake.Name = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(vehicleMake.Name)));
            }
        }
        public string Abrv
        {
            get => vehicleMake.Abrv;
            set
            {
                vehicleMake.Abrv = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(vehicleMake.Abrv)));
            }
        }

        public ObservableCollection<VehicleMakeModel> VehicleMakes { get; }

        public Command CreateComand { get; }
        public Command UpdateCommand { get; }
        public Command DeleteComand { get; }
        public Command SelectedMakeCommand { get; }

        private VehicleMakeModel selectedVehicleMake;
        public VehicleMakeModel SelectedVehicleMake
        {
            get => selectedVehicleMake;
            set{
                selectedVehicleMake = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleMakeModel)));
            }
        }

        private void getVehicleMake()
        {
            VehicleMakes.Clear();
            List<VehicleMake> makes = vms.GetAll();
            List<VehicleMakeModel> vehicleMakeModels = new List<VehicleMakeModel>();
            foreach (VehicleMake item in makes)
            {
                VehicleMakes.Add(mapper.Map<VehicleMakeModel>(item));
            }
        }

        private void createCommandFunction()
        {
            int i = vms.Add(mapper.Map<VehicleMake>(vehicleMake));
            if (i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "New vehicle make is created!", "Ok");
                resetEnterData();
                getVehicleMake();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "Creating failed!", "Ok");
            }
        }

        private void deleteCommandFunction()
        {
            int i=vms.Delete(mapper.Map<VehicleMake>(vehicleMake));
            if (i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Vehicle make " + vehicleMake.Id+" is deleted!", "Ok");
                resetEnterData();
                getVehicleMake();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "Deleting failed!", "Ok");
            }
        }

        private void updateCommandFunction()
        {
            int i = vms.Update(mapper.Map<VehicleMake>(vehicleMake));
            if(i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Vehicle make " + vehicleMake.Id + " is updated!", "Ok");
                resetEnterData();
                getVehicleMake();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "Updating failed!", "Ok");
            }
        }

        private void resetEnterData()
        {
            Id = 0;
            Name = "";
            Abrv = "";
        }

        private async void seletedMakeCommandFunction()
        {
            if (!(SelectedVehicleMake is null))
            {
                var vehicleModelViewModel = new VehicleModelViewModel(SelectedVehicleMake);
                await Application.Current.MainPage.Navigation.PushAsync(new VehicleModelView(vehicleModelViewModel));
                SelectedVehicleMake = null;
            }
            

        }
    }
}
