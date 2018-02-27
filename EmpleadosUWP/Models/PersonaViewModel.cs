using Empleados.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace EmpleadosUWP.ViewModels
{
    public class PersonaViewModel : BindableBase
    {
        public PersonaViewModel(Persona model)
        {
            Model = model ?? new Persona();
            GenerosList = new ObservableCollection<string>() { "Hombre", "Mujer", };
        }

        internal Persona Model { get; set; }

        public int Cedula
        {
            get => Model.IdPersona;

            set
            {
                if (value != Model.IdPersona)
                {
                    Model.IdPersona = value;
                    OnPropertyChanged("Cedula");
                    Debug.Print(Model.IdPersona.ToString());
                }
            }
        }

        public string Nombre
        {
            get => Model.Nombre;
            set
            {
                if (value != Model.Nombre)
                {
                    Model.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Apellido1
        {
            get => Model.Apellido1;
            set
            {
                if (value != Model.Apellido1)
                {
                    Model.Apellido1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Apellido2
        {
            get => Model.Apellido2;
            set
            {
                if (value != Model.Apellido2)
                {
                    Model.Apellido2 = value;
                    OnPropertyChanged();

                }
            }
        }

        public DateTime? FechaNacimiento
        {
            get => Model.FechaNacimiento;
            set
            {
                if (value != Model.FechaNacimiento)
                {
                    Model.FechaNacimiento = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Genero
        {
            get
            {
                if (Model.Genero == null) return Model.Genero;
                return Model.Genero.Equals("M") ? "Hombre" : "Mujer";
            }
            set
            {
                if (value != null && value != Model.Genero)
                {
                    Model.Genero = value.Equals("Hombre") ? "M" : "F";
                    OnPropertyChanged();
                }
            }
        }

        public string Nacionalidad
        {
            get => Model.Nacionalidad;
            set
            {
                if (value != Model.Nacionalidad)
                {
                    Model.Nacionalidad = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EstadoCivil
        {
            get => Model.EstadoCivil;
            set
            {
                if (value != Model.EstadoCivil)
                {
                    Model.EstadoCivil = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Direccion
        {
            get => Model.Direccion;
            set
            {
                if (value != Model.Direccion)
                {
                    Model.Direccion = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> GenerosList { get; }
        public string NombreCompleto => Nombre + " " + Apellido1 + " " + Apellido2;
    }
}
