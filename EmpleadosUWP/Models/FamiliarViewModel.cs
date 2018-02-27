using Empleados.Models;
using EmpleadosUWP.Models;
using System.Threading.Tasks;

namespace EmpleadosUWP.ViewModels
{
    public class FamiliarViewModel : BindableBase
    {

        public FamiliarViewModel(Familiares familiar)
        {
            Model = familiar ?? new Familiares();
            Familiar = new PersonaViewModel(Model.Familiar);
            IsNewEmployee = false;
            Task.Run(LoadEmpleado);   
        }

        internal Familiares Model { get; set; }

        public EmpleadoViewModel Empleado { get; set; }

        public PersonaViewModel Familiar { get; set; }
       
        public string Relacion
        {
            get => Model.Relacion;

            set
            {
                if (value != Model.Relacion)
                {
                    Model.Relacion = value;
                    OnPropertyChanged("Relacion");
                }
            }
        }

        public int IdEmpleado
        {
            get => Model.IdEmpleado;
            set
            {
                if (value != Model.IdEmpleado)
                {
                    Model.IdEmpleado = value;
                    OnPropertyChanged("IdEmpleado");
                }
            }
        }

        public int IdFamiliar
        {
            get => Familiar.Cedula;
            set
            {
                if (value != Model.IdFamiliar)
                {
                    Model.IdFamiliar = value;
                    OnPropertyChanged("IdFamiliar");
                }
            }
        }

        private async Task LoadEmpleado()
        {
            var employee = await App.Repository.Employees.GetAsync(Model.IdEmpleado);
            if (employee != null)
            {
                Model.Empleado = employee;   
            }
            else
            {
                Model.Empleado = new Empleado();
            }
            Empleado = new EmpleadoViewModel(Model.Empleado);
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
    }
}
