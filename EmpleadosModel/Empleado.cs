using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Empleados.Models
{
    public partial class Empleado
    {
        
        public Empleado()
        {
            Familiares = new HashSet<Familiares>();
            HistorialVacaciones = new HashSet<HistorialVacaciones>();
            PagosRealizados = new HashSet<PagosRealizados>();
            Permisos = new HashSet<Permisos>();
            Persona = new Persona();
        }

        [Display(Name = "Number", GroupName = "Job Description")]
        public int IdEmpleado { get; set; }
        
        public string PuestoTrabajo { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public decimal? Salario { get; set; }

        //public Persona IdEmpleadoNavigation { get; set; }
        public virtual Persona Persona { get; set; }
        public ICollection<Familiares> Familiares { get; set; }
        public ICollection<HistorialVacaciones> HistorialVacaciones { get; set; }
        public ICollection<PagosRealizados> PagosRealizados { get; set; }
        public ICollection<Permisos> Permisos { get; set; }
    }
}
