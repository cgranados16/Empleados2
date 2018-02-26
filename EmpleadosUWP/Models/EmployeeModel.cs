using Empleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosUWP.Models
{
    public class EmployeeModel
    {

        public EmployeeModel(Empleado model)
        {
            Model = model ?? new Empleado();
        }

        internal Empleado Model { get; set; }

        public int Cedula
        {
            get => Model.IdEmpleado;

            set
            {
                if (value != Model.IdEmpleado)
                {
                    Model.IdEmpleado = value;
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
                }
            }
        }

        public float Salario
        {
            get => (float)Model.Salario;
            set
            {
                if (value != Convert.ToDouble(Model.Salario))
                {
                    Model.Salario = Convert.ToDecimal(value);
                }
            }
        }
    }
}
