using System;
using System.Diagnostics;
using EmpleadosUWP.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EmpleadosUWP.Views
{
    public sealed partial class FamiliaresPage : Page
    {
        public FamiliaresViewModel ViewModel { get; } = new FamiliaresViewModel();

        public FamiliaresPage()
        {
            InitializeComponent();
            Loaded += FamiliaresPage_Loaded;
            DataContext = ViewModel;
        }

        private async void FamiliaresPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }        
    }
}
