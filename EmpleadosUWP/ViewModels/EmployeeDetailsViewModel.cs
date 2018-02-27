using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models;
using EmpleadosUWP.Helpers;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.ViewModels
{
    public class EmployeeDetailsViewModel : BindableBase
    {
        /// <summary>
        /// Creates a EmployeeDetailsViewModel that wraps the specified employee.
        /// </summary>
        public EmployeeDetailsViewModel(EmployeeViewModel employee){
            _employee = employee;
            Task.Run(LoadPayments);
        }

        private ObservableCollection<PagosRealizados> _payments = new ObservableCollection<PagosRealizados>();
        /// <summary>
        /// The collection of the customer's orders.
        /// </summary>
        public ObservableCollection<PagosRealizados> Payments{
            get => _payments;
            set => SetProperty(ref _payments, value);
        }

        private bool _isNewEmployee;
        /// <summary>
        /// Indicates whether this is a new customer.
        /// </summary>
        public bool IsNewEmployee{
            get => _isNewEmployee;
            set => SetProperty(ref _isNewEmployee, value);
        }

        private EmployeeViewModel _employee;
        /// <summary>
        /// Gets and sets the current customer values.
        /// </summary>
        public EmployeeViewModel Employee{
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        /// <summary>
        /// Load Payments for the selected Employee into the Payments Collection.
        /// </summary>
        public async Task LoadPayments(){
            var payments = await App.Repository.Payments.GetEmployeePaymentsAsync(_employee.Model.IdEmpleado);
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>{
                Payments.Clear();
                foreach (var p in payments){
                    Payments.Add(p);
                }
            });
        }

        /// <summary>
        /// Saves the current employee to the database. 
        /// </summary>
        public async Task SaveEmployee()
        {
            Empleado result = null;
            try
            {
                result = await App.Repository.Employees.UpsertAsync(_employee.Model);
                await App.Repository.People.UpsertAsync(_employee.Model.Persona);
            }
            catch (Exception ex)
            {
                throw new EmployeeSavingException("No se pudo guardar. Hubo un problema" +
                    "con la conexión. Intente de nuevo.", ex);
            }
            ShowSaveDialogMessage(result != null);
        }

        /// <summary>
        /// Shows a dialog whether was successfully saved or not.
        /// </summary>
        /// <param name="success">Saved successfully</param>
        public async void ShowSaveDialogMessage(bool success)
        {
            if (success){
                var dialog = new ContentDialog(){
                    Title = App.resourceLoader.GetString("SuccessSaveDialogTitle"),
                    Content = App.resourceLoader.GetString("SuccessSaveDialogContent"),
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
            }else{
                await DispatcherHelper.ExecuteOnUIThreadAsync(() => new EmployeeSavingException(
                    "No se pudo guardar. Hubo un problema" +
                    "con la conexión. Intente de nuevo."));
            }
        }

        /// <summary>
        /// Adds a payment to the selected employee. 
        /// </summary>
        /// <param name="amount">Amount of money of the payment</param>
        public async Task AddPayment(double amount){
            PagosRealizados result;
            try{
                PagosRealizados payment = new PagosRealizados(_employee.Model.IdEmpleado, (decimal)amount, DateTime.Now);
                result = await App.Repository.Payments.UpsertAsync(payment);
            }catch (Exception ex){
                throw new EmployeeSavingException("No se pudo guardar. Hubo un problema" +
                    "con la conexión. Intente de nuevo.", ex);
            }
            ShowSaveDialogMessage(result != null);
            Payments.Add(result);
        }        

        public class EmployeeSavingException : Exception
        {

            public EmployeeSavingException() : base("Error guardando el empleado.")
            {
            }

            public EmployeeSavingException(string message) : base(message)
            {
            }

            public EmployeeSavingException(string message,
                Exception innerException) : base(message, innerException)
            {

            }
        }
    }
}
