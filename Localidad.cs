using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Localidad
    {
        public Localidad()
        { }

        public Localidad(string linea)
        {
            var datos = linea.Split(';');
            Ciudad = datos[0];
            Provincia = datos[1];
            Región = datos[2];
            País = datos[3];
        }

        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Región { get; set; }
        public string País { get; set; }

        static readonly Dictionary<string, Localidad> Localidades = new Dictionary<string, Localidad>();

        public static void CargarLocalidades()
        {
            string nombreArchivo = "Localidades.txt";

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var localidad = new Localidad(linea);
                        Localidades.Add(localidad.Ciudad, localidad);
                    }
                }
            }
        }

        public static bool ValidarLocalidad(string ingreso)
        {
            return Localidades.ContainsKey(ingreso);
        }

        public static Localidad SeleccionarLocalidad(string ciudad)
        {
            var modelo = new Localidad();
            modelo.Ciudad = ciudad;

            foreach (var localidad in Localidades.Values)
            {
                if (localidad.Compara(modelo))
                {
                    return localidad;
                }
            }

            return null;
        }

        public bool Compara(Localidad modelo)
        {
            if (Ciudad != modelo.Ciudad)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
