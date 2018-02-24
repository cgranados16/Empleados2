using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class Familiares
    {
        public int IdEmpleado { get; set; }
        public int IdFamiliar { get; set; }
        public string Relacion { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
        public Persona IdFamiliarNavigation { get; set; }
    }
}
