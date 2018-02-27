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
        /// Creates a CustomerDetailPageViewModel that wraps the specified customer.
        /// </summary>
        public EmployeeDetailsViewModel(EmployeeViewModel empleado)
        {
            _employee = empleado;
            Task.Run(LoadPagosRealizados);
        }

        private ObservableCollection<PagosRealizados> _payments = new ObservableCollection<PagosRealizados>();
        /// <summary>
        /// The collection of the customer's orders.
        /// </summary>
        public ObservableCollection<PagosRealizados> Payments
        {
            get => _payments;
            set => SetProperty(ref _payments, value);
        }

        private bool _isLoading;
        /// <summary>
        /// Indicates whether to show the loading icon. 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isNewEmployee;
        /// <summary>
        /// Indicates whether this is a new customer.
        /// </summary>
        public bool IsNewEmployee
        {
            get => _isNewEmployee;
            set => SetProperty(ref _isNewEmployee, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the user has changed the order. 
        /// </summary>
        bool _hasChanges = false;
        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                if (value != _hasChanges)
                {
                    // Only record changes after the order has loaded. 
                    if (IsLoaded)
                    {
                        _hasChanges = value;
                        OnPropertyChanged(nameof(HasChanges));
                    }
                }
            }
        }

        public bool IsLoaded => _employee != null && (IsNewEmployee || _employee.Persona != null);

        public bool IsNotLoaded => !IsLoaded;

        private EmployeeViewModel _employee;
        /// <summary>
        /// Gets and sets the current customer values.
        /// </summary>
        public EmployeeViewModel Employee
        {
            get => _employee;

            set
            {
                SetProperty(ref _employee, value);
                HasChanges = true;
            }
        }



        private string _errorText = null;
        /// <summary>
        /// Gets and sets the relevant error text.
        /// </summary>
        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value);
        }


        public async Task LoadPagosRealizados()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);
            var payments = await App.Repository.Payments.GetEmployeePaymentsAsync(_employee.Model.IdEmpleado);
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Payments.Clear();
                foreach (var p in payments)
                {
                    Payments.Add(p);
                }
                IsLoading = false;
            });

        }



        /// <summary>
        /// Saves the current order to the database. 
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

        public async void ShowSaveDialogMessage(bool success)
        {

            if (success)
            {
                var dialog = new ContentDialog()
                {
                    Title = App.resourceLoader.GetString("SuccessSaveDialogTitle"),
                    Content = App.resourceLoader.GetString("SuccessSaveDialogContent"),
                    PrimaryButtonText = "OK"
                };

                await dialog.ShowAsync();
            }
            else
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() => new EmployeeSavingException(
                    "No se pudo guardar. Hubo un problema" +
                    "con la conexión. Intente de nuevo."));
            }
        }


        /// <summary>
        /// Adds a payment to the user. 
        /// </summary>
        public async Task AddPayment(double monto){
            PagosRealizados result;
            try
            {
                PagosRealizados payment = new PagosRealizados();
                payment.IdEmpleado = _employee.Model.IdEmpleado;
                payment.Monto = (decimal)monto;
                payment.Fecha = DateTime.Now;
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
