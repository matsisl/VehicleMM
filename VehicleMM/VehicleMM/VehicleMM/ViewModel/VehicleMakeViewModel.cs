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

namespace VehicleMM.ViewModel
{
    public class VehicleMakeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Autofac.IContainer container;
        private IMapper mapper = AutoMapperHelper.Maps();
        VehicleMakeService vms;
        VehicleMakeModel model = new VehicleMakeModel();
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
        }

        public int Id
        {
            get => model.Id;
            set
            {
                model.Id = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(model.Id)));
            }
        }
        public string Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(model.Name)));
            }
        }
        public string Abrv
        {
            get => model.Abrv;
            set
            {
                model.Abrv = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(model.Abrv)));
            }
        }

        public ObservableCollection<VehicleMakeModel> VehicleMakes { get; }

        public Command CreateComand { get; }
        public Command UpdateCommand { get; }
        public Command DeleteComand { get; }

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
            int i = vms.Create(mapper.Map<VehicleMake>(model));
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
            int i=vms.Delete(mapper.Map<VehicleMake>(model));
            if (i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Vehicle make " + model.Id+" is deleted!", "Ok");
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
            int i = vms.Update(mapper.Map<VehicleMake>(model));
            if(i != 0)
            {
                Application.Current.MainPage.DisplayAlert("Message", "Vehicle make " + model.Id + " is updated!", "Ok");
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
    }
}
