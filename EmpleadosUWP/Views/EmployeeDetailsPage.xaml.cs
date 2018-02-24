using System;
using Empleados.Models;
using EmpleadosUWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EmpleadosUWP.Views
{
    public sealed partial class EmployeeDetailsPage : Page
    {
        public EmployeeDetailsViewModel ViewModel { get; set; } = new EmployeeDetailsViewModel();

        public EmployeeDetailsPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        /// <summary>
        /// Displays the selected customer data.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EmployeeViewModel customer = e.Parameter as EmployeeViewModel;
            if (customer == null)
            {
                ViewModel = new EmployeeDetailsViewModel();
                ViewModel.Employee = new EmployeeViewModel(new Empleado()) { Validate = false };
                Bindings.Update();
                TitlePage.Text = "New customer";
            }
            else if (ViewModel.Employee != customer)
            {
                ViewModel = new EmployeeDetailsViewModel();
                ViewModel.Employee = customer;
                ViewModel.Employee.Validate = false;
                Bindings.Update();
            }
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Navigates from the current page.
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.IsInEdit = false;

            base.OnNavigatingFrom(e);
        }
    }
}
