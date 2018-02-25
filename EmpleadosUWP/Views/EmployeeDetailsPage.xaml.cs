using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Empleados.Models;
using EmpleadosUWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static EmpleadosUWP.ViewModels.EmployeeDetailsViewModel;

namespace EmpleadosUWP.Views
{
    public sealed partial class EmployeeDetailsPage : Page, INotifyPropertyChanged
    {

        public EmployeeDetailsPage()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Stores the view model. 
        /// </summary>
        private EmployeeDetailsViewModel _viewModel;

        /// <summary>
        /// We use this object to bind the UI to our data.
        /// </summary>
        public EmployeeDetailsViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                if (_viewModel != value)
                {
                    _viewModel = value;
                    OnPropertyChanged();
                }
            }
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Determine whether a valid order was provided.
            var employee = e.Parameter as EmployeeViewModel;
            if (employee != null)
            {
                ViewModel = new EmployeeDetailsViewModel(employee);
            }
            else
            {
                ViewModel = new EmployeeDetailsViewModel(new EmployeeViewModel(new Empleado()));
                CedulaNumericBox.IsEnabled = true;
            }
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Navigates from the current page.
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

            base.OnNavigatingFrom(e);
        }

        private async void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                await ViewModel.SaveEmployee();
            }
            catch (EmployeeSavingException ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Unable to save",
                    Content = $"There was an error saving your order:\n{ex.Message}",
                    PrimaryButtonText = "OK"
                };

                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Fired when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value changed. 
        /// </summary>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


     
    }
}
