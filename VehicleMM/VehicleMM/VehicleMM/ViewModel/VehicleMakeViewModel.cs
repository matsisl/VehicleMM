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
        Autofac.IContainer container;
        IMapper mapper;
        VehicleMakeService vms;
        VehicleMakeModel vehicleMake;
        public VehicleMakeViewModel()
        {
            vehicleMake = new VehicleMakeModel();
            mapper = AutoMapperHelper.Maps();
            container = AutofacHelper.Build();
            vms = container.ResolveNamed<VehicleMakeService>("MakeService");
            VehicleMakes = new ObservableCollection<VehicleMakeModel>();
            GetVehicleMake();

            CreateComand = new Command(() =>
            {
                CreateCommandFunction();
            });

            DeleteComand = new Command(() =>
            {
                DeleteCommandFunction();
            });

            UpdateCommand = new Command(() =>
            {
                UpdateCommandFunction();
            });

            SelectedMakeCommand = new Command(() =>
            {
                SeletedMakeCommandFunction();
            });

            SearchCommand = new Command(() =>
            {
                SearchFunction();
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
        string filter = "";
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(filter)));
            }
        }

        public ObservableCollection<VehicleMakeModel> VehicleMakes { get; }

        public Command CreateComand { get; }
        public Command UpdateCommand { get; }
        public Command DeleteComand { get; }
        public Command SelectedMakeCommand { get; }
        public Command SearchCommand { get; }

        private VehicleMakeModel selectedVehicleMake;
        public VehicleMakeModel SelectedVehicleMake
        {
            get => selectedVehicleMake;
            set{
                selectedVehicleMake = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleMakeModel)));
            }
        }

        private void GetVehicleMake()
        {
            VehicleMakes.Clear();
            List<VehicleMake> makes = vms.GetAll();
            List<VehicleMakeModel> vehicleMakeModels = new List<VehicleMakeModel>();
            foreach (VehicleMake item in makes)
            {
                VehicleMakes.Add(mapper.Map<VehicleMakeModel>(item));
            }
        }

        private void CreateCommandFunction()
        {
            int vehicleMakeCreated = vms.Add(mapper.Map<VehicleMake>(vehicleMake));
            if (vehicleMakeCreated != 0)
            {
                DisplayAlert("New vehicle make is created!");
                ResetEnterData();
                GetVehicleMake();
            }
            else
            {
                DisplayAlert("Creating failed!");
            }
        }

        private void DeleteCommandFunction()
        {
            int vehicleMakeDeleted=vms.Delete(mapper.Map<VehicleMake>(vehicleMake));
            if (vehicleMakeDeleted != 0)
            {
                DisplayAlert("Vehicle make " + vehicleMake.Id+" is deleted!");
                ResetEnterData();
                GetVehicleMake();
            }
            else
            {
                DisplayAlert("Deleting failed!");
            }
        }

        private void UpdateCommandFunction()
        {
            int vehicleMakeUpdated = vms.Update(mapper.Map<VehicleMake>(vehicleMake));
            if(vehicleMakeUpdated != 0)
            {
                DisplayAlert("Vehicle make " + vehicleMake.Id + " is updated!");
                ResetEnterData();
                GetVehicleMake();
            }
            else
            {
                DisplayAlert("Updating failed!");
            }
        }

        private void ResetEnterData()
        {
            Id = 0;
            Name = "";
            Abrv = "";
        }

        private async void SeletedMakeCommandFunction()
        {
            if (!(SelectedVehicleMake is null))
            {
                var vehicleModelViewModel = new VehicleModelViewModel(SelectedVehicleMake);
                await Application.Current.MainPage.Navigation.PushAsync(new VehicleModelView(vehicleModelViewModel));
                SelectedVehicleMake = null;
            }
            

        }

        private void SearchFunction()
        {
            VehicleMakes.Clear();
            if (!Filter.Equals(""))
            {
                List<VehicleMake> makes = vms.Filter(filter);
                List<VehicleMakeModel> vehicleMakeModels = new List<VehicleMakeModel>();
                foreach (VehicleMake item in makes)
                {
                    VehicleMakes.Add(mapper.Map<VehicleMakeModel>(item));
                }
            }
            else
            {
                GetVehicleMake();
            }
        }
   
        private void DisplayAlert(string message)
        {
            Application.Current.MainPage.DisplayAlert("Message", message, "Ok");
        }
    }
}
