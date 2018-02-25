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

        void LoadEmployee(EmployeeViewModel employee)
        {
            if (employee != null) {
                ViewModel = new EmployeeDetailsViewModel(employee);
                Bindings.Update();
                
            }
            else{
                ViewModel = new EmployeeDetailsViewModel(new EmployeeViewModel(new Empleado()));
                ViewModel.IsNewEmployee = true;
                Bindings.Update();
            }
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var employee = e.Parameter as EmployeeViewModel;
            LoadEmployee(employee);
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
                var saveDialog = new SaveChangesDialog()
                {
                    Title = $"¿Guardar cambios a {ViewModel.Employee.Nombre.ToString()}?",
                    Message = $"¿Realmente desea guardar los cambios realizados a este empleado?"                       
                };
                await saveDialog.ShowAsync();
                SaveChangesDialogResult result = saveDialog.Result;

                switch (result)
                {
                    case SaveChangesDialogResult.Save:
                        await ViewModel.SaveEmployee();
                        break;
                    case SaveChangesDialogResult.DontSave:
                        break;
                    case SaveChangesDialogResult.Cancel:
                        ViewModel.HasChanges = true;
                        break;
                }
            }
            catch (EmployeeSavingException ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "No se pudo guardar.",
                    Content = $"Ocurrió un error al guardar su orden:\n{ex.Message}",
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
