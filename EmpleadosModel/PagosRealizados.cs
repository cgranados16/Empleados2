using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class PagosRealizados
    {
        public PagosRealizados(){}

        public PagosRealizados(int IdEmpleado, decimal Monto, DateTime Fecha)
        {
            this.IdEmpleado = IdEmpleado;
            this.Monto = Monto;
            this.Fecha = Fecha;
        }
       
        public int IdEmpleado { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
    }
}
