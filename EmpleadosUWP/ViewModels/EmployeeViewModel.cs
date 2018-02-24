using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Core;
using Empleados.Models;

namespace EmpleadosUWP.ViewModels
{
    public class EmployeeViewModel : ValidateViewModelBase
    {
        /// <summary>
        /// Creates a new customer model.
        /// </summary>
        public EmployeeViewModel(Empleado model)
        {
            Model = model ?? new Empleado();
        }

        /// <summary>
        /// The underlying customer model. Internal so it is 
        /// not visible to the RadDataGrid. 
        /// </summary>
        internal Empleado Model { get; set; }

        /// <summary>
        /// Gets or sets whether the underlying model has been modified. 
        /// This is used when sync'ing with the server to reduce load
        /// and only upload the models that changed.
        /// </summary>
        internal bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets whether to validate model data. 
        /// </summary>
        internal bool Validate { get; set; }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string PuestoTrabajo
        {
            get => Model.PuestoTrabajo;
            set
            {
                if (value != Model.PuestoTrabajo)
                {
                    Model.PuestoTrabajo = value;
                    IsModified = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's last name.
        /// </summary>
        public decimal? Salario
        {
            get => Model.Salario;
            set
            {
                if (value != Model.Salario)
                {
                    Model.Salario = value;
                    IsModified = true;
                }
            }
        }

       
    }
}
