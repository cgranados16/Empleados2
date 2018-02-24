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
        /// <summary>
        /// Creates a CustomerDetailPageViewModel that wraps the specified customer.
        /// </summary>
        public EmployeeDetailsViewModel()
        {
            SaveCommand = new RelayCommand(async () => await Save());
            CancelEditsCommand = new RelayCommand(OnCancelEdits);
            StartEditCommand = new RelayCommand(OnStartEdit);
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
                if (SetProperty(ref _employee, value) == true)
                {
                    if (string.IsNullOrWhiteSpace(Employee.Nombre))
                    {
                        IsInEdit = true;
                    }
                }
            }
        }

        /// <summary>
        /// Get's the customers full (first + last) name.
        /// </summary>
        public string Name => Employee?.Nombre;

        private bool _isInEdit = false;
        /// <summary>
        /// Gets and sets the current edit mode.
        /// </summary>
        public bool IsInEdit
        {
            get => _isInEdit;
            set => SetProperty(ref _isInEdit, value);
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

        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// Saves customer data that has been edited.
        /// </summary>
        private async Task Save() => await App.Repository.Employees.UpsertAsync(_employee.Model);

        public RelayCommand CancelEditsCommand { get; private set; }

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        private void OnCancelEdits()
        {
            RefreshCommand.Execute(null);
            IsInEdit = false;
        }

        public RelayCommand StartEditCommand { get; private set; }

        /// <summary>
        /// Enables edit mode.
        /// </summary>
        private void OnStartEdit() => IsInEdit = true;

        public RelayCommand RefreshCommand { get; private set; }

    }
}
