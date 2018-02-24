using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class HistorialVacaciones
    {
        public int IdEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
    }
}
