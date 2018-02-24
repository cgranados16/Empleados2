using System;

namespace EmpleadosModel
{
    public partial class View_Empleado
    {
        public int Cédula { get; set; }
        public string Nombre { get; set; }
        public string Primer_Apellido { get; set; }
        public string Segundo_Apellido { get; set; }
        public string Puesto_de_Trabajo { get; set; }
        public DateTime? Fecha_de_Contratación { get; set; }
        public string Género { get; set; }
        public string Nacionalidad { get; set; }
        public string Estado_Civil { get; set; }
    }
}
