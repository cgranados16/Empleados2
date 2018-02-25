using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models;
using EmpleadosUWP.Helpers;
using Microsoft.Toolkit.Uwp.Helpers;

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
                throw new EmployeeSavingException("Unable to save. There might have been a problem " +
                    "connecting to the database. Please try again.", ex);
            }

            if (result != null)
            {
                Debug.Print("Yea");
            }
            else
            {
                await DispatcherHelper.ExecuteOnUIThreadAsync(() => new EmployeeSavingException(
                    "Unable to save. There might have been a problem " +
                    "connecting to the database. Please try again."));
            }
        }

        public class EmployeeSavingException : Exception
        {

            public EmployeeSavingException() : base("Error saving an order.")
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
