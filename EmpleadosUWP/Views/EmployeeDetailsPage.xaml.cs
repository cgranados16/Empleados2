using System;

using EmpleadosUWP.ViewModels;

using Windows.UI.Xaml.Controls;

namespace EmpleadosUWP.Views
{
    public sealed partial class EmployeeDetailsPage : Page
    {
        public EmployeeDetailsViewModel ViewModel { get; } = new EmployeeDetailsViewModel();

        public EmployeeDetailsPage()
        {
            InitializeComponent();
        }
    }
}
