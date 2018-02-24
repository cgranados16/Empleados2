using System;
using System.Collections.Generic;

namespace Empleados.Models { 

    public partial class Correos
    {
        public int IdPersona { get; set; }
        public string Correo { get; set; }

        public Persona IdPersonaNavigation { get; set; }
    }
}
