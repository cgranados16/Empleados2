using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosUWP.Helpers;
using Microsoft.Toolkit.Uwp.Helpers;

namespace EmpleadosUWP.ViewModels
{
    public class EmployeeDetailsViewModel : BindableBase
    {
        public EmployeeDetailsViewModel()
        {
            Task.Run(GetCustomerListAsync);   
        }

        private ObservableCollection<EmployeeViewModel> _employees = new ObservableCollection<EmployeeViewModel>();
        /// <summary>
        /// The collection of customers in the list. 
        /// </summary>
        public ObservableCollection<EmployeeViewModel> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }

        private EmployeeViewModel _selectedCustomer;
        /// <summary>
        /// Gets or sets the selected customer, or null if no customer is selected. 
        /// </summary>
        public EmployeeViewModel SelectedEmployee
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        private string _errorText = null;
        /// <summary>
        /// Gets or sets the error text.
        /// </summary>
        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value);
        }

        private bool _isLoading = false;
        /// <summary>
        /// Gets or sets whether to show the data loading progress wheel. 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public async Task GetCustomerListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            var customers = await App.Repository.Employees.GetAsync();
            if (customers == null)
            {
                return;
            }
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Employees.Clear();
                foreach (var c in customers)
                {
                    Employees.Add(new EmployeeViewModel(c) { Validate = true });
                }
                IsLoading = false;
            });
        }

        /// <summary>
        /// Queries the database for a current list of customers.
        /// </summary>
        private void OnSync()
        {
            Task.Run(async () =>
            {
                IsLoading = true;
                foreach (var modifiedEmployee in Employees
                    .Where(x => x.IsModified).Select(x => x.Model))
                {
                    await App.Repository.Employees.UpsertAsync(modifiedEmployee);
                }
                await GetCustomerListAsync();
                IsLoading = false;
            });
        }

    }
}
