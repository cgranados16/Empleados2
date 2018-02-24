using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Core;
using Empleados.Models;
using System.ComponentModel.DataAnnotations;

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

        public int Cedula => Model.IdEmpleado;

        public Persona Persona => Model.Persona;

        public string Nombre
        {
            get => Persona.Nombre;
            set
            {
                if (value != Persona.Nombre)
                {
                    Persona.Nombre = value;
                    IsModified = true;
                }
            }
        }

        public string Apellido1
        {
            get => Persona.Apellido1;
            set
            {
                if (value != Persona.Apellido1)
                {
                    Persona.Apellido1 = value;
                    IsModified = true;
                }
            }
        }

        public string Apellido2
        {
            get => Persona.Apellido2;
            set
            {
                if (value != Persona.Apellido2)
                {
                    Persona.Apellido2 = value;
                    IsModified = true;
                }
            }
        }

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

        public DateTime? FechaContratacion
        {
            get => Model.FechaContratacion;
            set
            {
                if (value != Model.FechaContratacion)
                {
                    Model.FechaContratacion = value;
                    IsModified = true;
                }
            }
        }

        public string Genero
        {
            get => Persona.Genero;
            set
            {
                if (value != Persona.Genero)
                {
                    Persona.Genero = value;
                    IsModified = true;
                }
            }
        }

        public string Nacionalidad
        {
            get => Persona.Nacionalidad;
            set
            {
                if (value != Persona.Nacionalidad)
                {
                    Persona.Nacionalidad = value;
                    IsModified = true;
                }
            }
        }

        public string EstadoCivil
        {
            get => Persona.EstadoCivil;
            set
            {
                if (value != Persona.EstadoCivil )
                {
                    Persona.EstadoCivil  = value;
                    IsModified = true;
                }
            }
        }

        public DateTime? FechaNacimiento
        {
            get => Persona.FechaNacimiento;
            set
            {
                if (value != Persona.FechaNacimiento)
                {
                    Persona.FechaNacimiento = value;
                    IsModified = true;
                }
            }
        }

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
        
        public string GeneroString => Model.Persona.Genero.Equals("M") ? "Hombre" : "Mujer";
        public string NombreCompleto => Model.Persona.Nombre + " " + Model.Persona.Apellido1 + " " + Model.Persona.Apellido2;
        public string FechaContratacionString => Model.FechaContratacion.Value.ToShortDateString();        
        
        




    }
}
