using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Correos = new HashSet<Correos>();
            Familiares = new HashSet<Familiares>();
            Telefonos = new HashSet<Telefonos>();
        }

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
        public string EstadoCivil { get; set; }
        public string Direccion { get; set; }

        public Empleado Empleado { get; set; }
        public ICollection<Correos> Correos { get; set; }
        public ICollection<Familiares> Familiares { get; set; }
        public ICollection<Telefonos> Telefonos { get; set; }
    }
}
