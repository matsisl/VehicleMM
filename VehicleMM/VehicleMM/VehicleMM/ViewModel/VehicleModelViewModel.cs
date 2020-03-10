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
        int pageIndex;
        int pageSize;
        public VehicleModelViewModel(VehicleMakeModel vehicleMakeModel)
        {
            vehicleMake = vehicleMakeModel;
            vehicleModel.MakeId = vehicleMake.Id;
            container = AutofacHelper.Build();
            vms = container.ResolveNamed<VehicleModelService>("ModelService");
            VehicleModels = new ObservableCollection<VehicleModelModel>();
            pageIndex = 0;
            pageSize = 4;
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

            NextCommand = new Command(() => 
            {
                nextCommandFunction();
            });

            PreviousCommand = new Command(() =>
            {
                previousCommandFunction();
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
        public Command NextCommand { get; }
        public Command PreviousCommand { get; }

        private void getVehicleModel()
        {
            
            List<VehicleModel> models = vms.PagingByMake(vehicleMake.Id, pageIndex, pageSize);           
            if (models==null)
            {
                pageIndex--;
            }
            else if (models.Count ==0)
            {
                pageIndex--;
            }
            else
            {
                VehicleModels.Clear();
                List<VehicleModelModel> vehicleMakeModels = new List<VehicleModelModel>();
                foreach (VehicleModel item in models)
                {
                    VehicleModels.Add(mapper.Map<VehicleModelModel>(item));
                }
            }

        }

        private void createCommandFunction()
        {
            int i = vms.Add(mapper.Map<VehicleModel>(vehicleModel));
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
                VehicleModels.Clear();
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
            pageIndex = 0;
        }
        
        private void nextCommandFunction()
        {
            pageIndex++;
            getVehicleModel();
        }

        private void previousCommandFunction()
        {
            if (pageIndex > 0)
            {
                pageIndex--;
                getVehicleModel();
            }
        }

    }
}

