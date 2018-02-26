using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class Familiares
    {
        public Familiares() { }

        public Familiares(int idEmpleado)
        {
            IdEmpleado = idEmpleado;
        }

        public int IdEmpleado { get; set; }
        public int IdFamiliar { get; set; }
        public string Relacion { get; set; }

        public Empleado Empleado { get; set; }
        public Persona Familiar { get; set; }
    }
}
