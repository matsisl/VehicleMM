﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VehicleMM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleMakeView : ContentPage
    {
        public VehicleMakeView(VehicleMakeViewModel vehicleMakeViewModel )
        {
            InitializeComponent();
            BindingContext = vehicleMakeViewModel;
        }
    }
}