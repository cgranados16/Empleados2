using System;
using Empleados.Models;
using EmpleadosUWP.Models;
using EmpleadosUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.Views
{
    public sealed partial class FamiliaresDetailControl : UserControl
    {
        public FamiliarViewModel MasterMenuItem{
            get { return GetValue(MasterMenuItemProperty) as FamiliarViewModel; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(FamiliarViewModel), typeof(FamiliaresDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public FamiliaresDetailControl(){
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e){
            var control = d as FamiliaresDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
            
        }
    }
}
