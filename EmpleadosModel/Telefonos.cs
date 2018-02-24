using System;
using System.Collections.Generic;

namespace Empleados.Models
{
    public partial class Telefonos
    {
        public int IdPersona { get; set; }
        public decimal Telefono { get; set; }

        public Persona IdPersonaNavigation { get; set; }
    }
}
