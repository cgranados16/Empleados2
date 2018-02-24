using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class Permisos
    {
        public int IdEmpleado { get; set; }
        public string Permiso { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
    }
}
