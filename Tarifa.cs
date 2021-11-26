using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Tarifa
    {
        public Tarifa(string linea)
        {
            var datos = linea.Split(';');
            LocalCorrespondencia = int.Parse(datos[0]);
            Local10kg = int.Parse(datos[1]);
            Local20kg = int.Parse(datos[2]);
            Local30kg = int.Parse(datos[3]);
            ProvincialCorrespondencia = int.Parse(datos[4]);
            Provincial10kg = int.Parse(datos[5]);
            Provincial20kg = int.Parse(datos[6]);
            Provincial30kg = int.Parse(datos[7]);
            RegionalCorrespondencia = int.Parse(datos[8]);
            Regional10kg = int.Parse(datos[9]);
            Regional20kg = int.Parse(datos[10]);
            Regional30kg = int.Parse(datos[11]);
            NacionalCorrespondencia = int.Parse(datos[12]);
            Nacional10kg = int.Parse(datos[13]);
            Nacional20kg = int.Parse(datos[14]);
            Nacional30kg = int.Parse(datos[15]);
            InternacionalCorrespondencia = int.Parse(datos[16]);
            Internacional10kg = int.Parse(datos[17]);
            Internacional20kg = int.Parse(datos[18]);
            Internacional30kg = int.Parse(datos[19]);
        }

        public static int LocalCorrespondencia { get; set; }
        private static int Local10kg { get; set; }
        private static int Local20kg { get; set; }
        private static int Local30kg { get; set; }
        private static int ProvincialCorrespondencia { get; set; }
        private static int Provincial10kg { get; set; }
        private static int Provincial20kg { get; set; }
        private static int Provincial30kg { get; set; }
        private static int RegionalCorrespondencia { get; set; }
        private static int Regional10kg { get; set; }
        private static int Regional20kg { get; set; }
        private static int Regional30kg { get; set; }
        private static int NacionalCorrespondencia { get; set; }
        private static int Nacional10kg { get; set; }
        private static int Nacional20kg { get; set; }
        private static int Nacional30kg { get; set; }
        private static int InternacionalCorrespondencia { get; set; }
        private static int Internacional10kg { get; set; }
        private static int Internacional20kg { get; set; }
        private static int Internacional30kg { get; set; }

        public static float CalcularPrecio(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.LocalidadOrigen == solicitud.LocalidadDestino)
            {
                precio = CalcularPrecioLocal(solicitud);
            }

            if (solicitud.LocalidadOrigen != solicitud.LocalidadDestino && solicitud.ProvinciaOrigen == solicitud.ProvinciaDestino)
            {
                precio = CalcularPrecioProvincial(solicitud);
            }

            if (solicitud.LocalidadOrigen != solicitud.LocalidadDestino && solicitud.ProvinciaOrigen != solicitud.ProvinciaDestino && solicitud.RegionOrigen == solicitud.RegionDestino)
            {
                precio = CalcularPrecioRegional(solicitud);
            }

            if (solicitud.RegionOrigen != solicitud.RegionDestino && solicitud.PaisDestino == "Argentina")
            {
                precio = CalcularPrecioNacional(solicitud);
            }

            if(solicitud.PaisDestino != "Argentina")
            {
                precio = CalcularPrecioInternacional(solicitud);
            }

            if(solicitud.EsUrgente == "Si")
            {
                CalcularPrecioEnvíoUrgente(precio);
            }

            if(solicitud.EntregaEnPuerta == "Si")
            {
                CalcularPrecioEnvioRetiroPuerta(precio);
            }

            if(solicitud.RetiroEnPuerta == "Si")
            {
                CalcularPrecioEnvioRetiroPuerta(precio);
            }

            return precio;
        }

        private static float CalcularPrecioLocal(SolicitudServicio solicitud)
        {
            float precio = 0;

            if(solicitud.PesoKg == 0.5)
            {
                precio = Tarifa.LocalCorrespondencia;
            }

            if(solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = Tarifa.Local10kg;
            }

            if(solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = Tarifa.Local20kg;
            }

            if(solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = Tarifa.Local30kg;
            }

            return precio;
        }

        private static float CalcularPrecioProvincial(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = Tarifa.ProvincialCorrespondencia;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = Tarifa.Provincial10kg;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = Tarifa.Provincial20kg;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = Tarifa.Provincial30kg;
            }

            return precio;
        }

        private static float CalcularPrecioRegional(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = Tarifa.RegionalCorrespondencia;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = Tarifa.Regional10kg;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = Tarifa.Regional20kg;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = Tarifa.Regional30kg;
            }

            return precio;
        }

        private static float CalcularPrecioNacional(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = Tarifa.NacionalCorrespondencia;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = Tarifa.Nacional10kg;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = Tarifa.Nacional20kg;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = Tarifa.Nacional30kg;
            }

            return precio;
        }

        private static float CalcularPrecioInternacional(SolicitudServicio solicitud)
        {
            float precio = 0;

            if (solicitud.PesoKg == 0.5)
            {
                precio = Tarifa.InternacionalCorrespondencia;
            }

            if (solicitud.PesoKg > 0.5 && solicitud.PesoKg <= 10)
            {
                precio = Tarifa.Internacional10kg;
            }

            if (solicitud.PesoKg > 10 && solicitud.PesoKg <= 20)
            {
                precio = Tarifa.Internacional20kg;
            }

            if (solicitud.PesoKg > 20 && solicitud.PesoKg <= 30)
            {
                precio = Tarifa.Internacional30kg;
            }

            return precio;
        }

        private static float CalcularPrecioEnvíoUrgente(float precio)
        {
            precio = precio * 1.1f;

            return precio;
        }

        private static float CalcularPrecioEnvioRetiroPuerta(float precio)
        {
            precio = precio + 100;
            return precio;
        }

        public static void CargarTarifario()
        {
            string nombreArchivo = "Tarifario.txt";
            
            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var tarifa = new Tarifa(linea);
                    }
                }
            }
        }
    }
}
