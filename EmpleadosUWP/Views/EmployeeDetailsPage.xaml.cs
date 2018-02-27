using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Empleados.Models;
using EmpleadosUWP.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static EmpleadosUWP.ViewModels.EmployeeDetailsViewModel;

namespace EmpleadosUWP.Views
{
    public sealed partial class EmployeeDetailsPage : Page, INotifyPropertyChanged
    {
        public EmployeeDetailsPage(){
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
            get => _viewModel;
            set{
                if (_viewModel != value){
                    _viewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Binds the data to the UI.
        /// </summary>
        /// <param name="employee">The employee.</param>
        void LoadEmployee(EmployeeViewModel employee){
            if (employee != null){
                ViewModel = new EmployeeDetailsViewModel(employee);
                Bindings.Update();    
            }else{
                ViewModel = new EmployeeDetailsViewModel(new EmployeeViewModel(new Empleado()));
                ViewModel.IsNewEmployee = true;
                Bindings.Update();
            }
            App.SelectedEmployee = ViewModel.Employee.Model;
        }

        /// <summary>
        /// Sends a signal to save the changes in the database.
        /// </summary>
        private async Task SaveChanges()
        {
            try
            {
                var saveDialog = new SaveChangesDialog()
                {
                    Title = $"¿Guardar cambios a {ViewModel.Employee.Nombre}?",
                    Message = $"¿Realmente desea guardar los cambios realizados a este empleado?"
                };
                await saveDialog.ShowAsync();
                SaveChangesDialogResult result = saveDialog.Result;

                switch (result)
                {
                    case SaveChangesDialogResult.Save:
                        await ViewModel.SaveEmployee();
                        break;
                    default:
                        break;
                }
            }
            catch (EmployeeSavingException ex)
            {
                ShowErrorDialog(ex);
            }
        }

        /// <summary>
        /// Sends a signal to add a payment to the selected employee.
        /// </summary>
        private async Task AddPayment()
        {
            try{
                var addPaymentDialog = new AddPaymentDialog();
                await addPaymentDialog.ShowAsync();
                AddPaymentDialogResult result = addPaymentDialog.Result;
                switch (result)
                {
                    case AddPaymentDialogResult.Accept:
                        await ViewModel.AddPayment(addPaymentDialog.Amount);
                        break;
                    case AddPaymentDialogResult.Cancel:
                        break;
                }
            }
            catch (EmployeeSavingException ex)
            {
                ShowErrorDialog(ex);
            }
            
        }

        /// <summary>
        /// Shows an error dialog.
        /// </summary>
        /// <param name="exception">The exception who provoked the error.</param>
        private async void ShowErrorDialog(Exception exception){
            var dialog = new ContentDialog(){
                Title = "No se pudo guardar.",
                Content = $"Ocurrió un error al guardar:\n{exception.Message}",
                PrimaryButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Navigates to the current page.
        /// </summary>
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
            App.SelectedEmployee = null;
            base.OnNavigatingFrom(e);
        }

        /// <summary>
        /// Fired when the user chooses to save. 
        /// </summary>
        private async void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await SaveChanges(); 
        }

        /// <summary>
        /// Fired when the user clicks the AddPayment button. 
        /// </summary>
        private async void AddPayment_Button(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await AddPayment();
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
