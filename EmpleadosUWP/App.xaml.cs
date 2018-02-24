using System;
using System.Data.SqlClient;
using DatabaseRepository;
using DatabaseRepository.Sql;
using EmpleadosUWP.Services;
using Microsoft.EntityFrameworkCore;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace EmpleadosUWP
{
    public sealed partial class App : Application
    {
        /// <summary>
        /// Pipeline for interacting with backend service or database.
        /// </summary>
        public static IEmpleadosRepository Repository { get; private set; }

        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
           
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
            UseSqlServer();


        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.EmployeesGridPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        
        public static void UseSqlServer()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "tarea03.database.windows.net";
            builder.UserID = "Carlos";
            builder.Password = "Caca1234";
            builder.InitialCatalog = "Tarea3";
            var dbOptions = new DbContextOptionsBuilder<Tarea3Context>().UseSqlServer(builder.ConnectionString);

            Repository = new SqlEmpleadosRepository(dbOptions);
        }
    }
}
