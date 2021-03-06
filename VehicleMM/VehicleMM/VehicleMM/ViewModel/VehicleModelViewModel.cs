﻿using Autofac;
using AutoMapper;
using Service;
using System;
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
        IMapper mapper;
        VehicleModelService vms;
        public VehicleMakeModel vehicleMake;
        VehicleModelModel vehicleModel;
        int pageIndex;
        int pageSize;
        public VehicleModelViewModel(VehicleMakeModel vehicleMakeModel, VehicleModelService vehicleModelService, IMapper m)
        {
            vms = vehicleModelService;
            mapper = m;
            vehicleMake = vehicleMakeModel;
            vehicleModel = AutofacHelper.GetInstance().GetContainer().Resolve<VehicleModelModel>();
            vehicleModel.MakeId = vehicleMake.Id;
            VehicleModels = new ObservableCollection<VehicleModelModel>();
            pageIndex = 0;
            pageSize = 4;
            GetVehicleModel();

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

            NextCommand = new Command(() => 
            {
                NextCommandFunction();
            });

            PreviousCommand = new Command(() =>
            {
                PreviousCommandFunction();                
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

        private async void GetVehicleModel()
        {
            try {
                List<VehicleModel> models = await vms.PagingByMake(vehicleMake.Id, pageIndex, pageSize);
                if (models == null)
                {
                    pageIndex--;
                }
                else if (models.Count == 0)
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
            catch (ServiceException ex)
            {
                DisplayAlert(ex.Message);
            }

        }

        private async void CreateCommandFunction()
        {
            try
            {
                int vehicleModelCreated = await vms.Add(mapper.Map<VehicleModel>(vehicleModel));
                if (vehicleModelCreated != 0)
                {
                    DisplayAlert("New vehicle make is created!");
                    ResetEnterData();
                    GetVehicleModel();
                }
                else
                {
                    DisplayAlert("Creating failed!");
                }
            }
            catch (ServiceException ex)
            {
                DisplayAlert(ex.Message);
            }

        }

        private async void DeleteCommandFunction()
        {
            try
            {
                int vehicleModelDeleted = await vms.Delete(mapper.Map<VehicleModel>(vehicleModel));
                if (vehicleModelDeleted != 0)
                {
                    DisplayAlert("Vehicle make " + vehicleModel.Id + " is deleted!");
                    VehicleModels.Clear();
                    ResetEnterData();
                    GetVehicleModel();
                }
                else
                {
                    DisplayAlert("Deleting failed!");
                }
            }
            catch (ServiceException ex)
            {
                DisplayAlert(ex.Message);
            }

        }

        private async void UpdateCommandFunction()
        {
            try
            {
                int vehickeModelUpdated = await vms.Update(mapper.Map<VehicleModel>(vehicleModel));
                if (vehickeModelUpdated != 0)
                {
                    DisplayAlert("Vehicle make " + vehicleModel.Id + " is updated!");
                    ResetEnterData();
                    GetVehicleModel();
                }
                else
                {
                    DisplayAlert("Updating failed!");
                }
            }
            catch (ServiceException ex)
            {
                DisplayAlert(ex.Message);
            }

        }

        private void ResetEnterData()
        {
            Id = 0;
            Name = "";
            Abrv = "";
            pageIndex = 0;
        }
        
        private void NextCommandFunction()
        {
            pageIndex++;
            GetVehicleModel();
        }

        private void PreviousCommandFunction()
        {
            if (pageIndex > 0)
            {
                pageIndex--;
                GetVehicleModel();
            }
        }

        private void DisplayAlert(string message)
        {
            Application.Current.MainPage.DisplayAlert("Message", message, "Ok");
        }
    }
}

