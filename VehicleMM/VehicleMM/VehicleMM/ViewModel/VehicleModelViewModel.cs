using Autofac;
using AutoMapper;
using Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VehicleMM.Model;
using VehicleMM.Utils;
using Xamarin.Forms;

namespace VehicleMM.ViewModel
{
    public class VehicleModelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Autofac.IContainer container;
        private IMapper mapper = AutoMapperHelper.Maps();
        VehicleModelService vms;
        public VehicleMakeModel vehicleMake;
        VehicleModelModel vehicleModel = new VehicleModelModel();

        public VehicleModelViewModel(VehicleMakeModel vehicleMakeModel)
        {
            vehicleMake = vehicleMakeModel;
            vehicleModel.MakeId = vehicleMake.Id;
            container = AutofacHelper.Build();
            vms = container.ResolveNamed<VehicleModelService>("ModelService");
            VehicleModels = new ObservableCollection<VehicleModelModel>();
            getVehicleModel();

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
        }

        public string MakeName
        {
            get => vehicleMake.Name; 
        }
        public int Id
        {
            get => vehicleModel.Id;
            set
            {
                vehicleModel.Id = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(vehicleModel.Id)));
            }
        }
        public string Name
        {
            get => vehicleModel.Name;
            set
            {
                vehicleModel.Name = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(vehicleModel.Name)));
            }
        }
        public string Abrv
        {
            get => vehicleModel.Abrv;
            set
            {
                vehicleModel.Abrv = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(vehicleModel.Abrv)));
            }
        }

        public ObservableCollection<VehicleModelModel> VehicleModels { get; }

        public Command CreateComand { get; }
        public Command UpdateCommand { get; }
        public Command DeleteComand { get; }

        private void getVehicleModel()
        {
            VehicleModels.Clear();
            List<VehicleModel> models = vms.GetVehicleModelsByMakeId(vehicleMake.Id);
            List<VehicleModelModel> vehicleMakeModels = new List<VehicleModelModel>();
            foreach (VehicleModel item in models)
            {
                VehicleModels.Add(mapper.Map<VehicleModelModel>(item));
            }
        }

        private void createCommandFunction()
        {
            int i = vms.Create(mapper.Map<VehicleModel>(vehicleModel));
            if (i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "New vehicle make is created!", "Ok");
                resetEnterData();
                getVehicleModel();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "Creating failed!", "Ok");
            }
        }

        private void deleteCommandFunction()
        {
            int i = vms.Delete(mapper.Map<VehicleModel>(vehicleModel));
            if (i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Vehicle make " + vehicleModel.Id + " is deleted!", "Ok");
                resetEnterData();
                getVehicleModel();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "Deleting failed!", "Ok");
            }
        }

        private void updateCommandFunction()
        {
            int i = vms.Update(mapper.Map<VehicleModel>(vehicleModel));
            if (i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Vehicle make " + vehicleModel.Id + " is updated!", "Ok");
                resetEnterData();
                getVehicleModel();
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
    }

}

