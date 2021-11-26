using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class SolicitudServicio
    {
        const string nombreArchivo = "RegistroSolicitudes.txt";
        public SolicitudServicio()
        {
            
        }

        public SolicitudServicio(string linea)
        {
            var datos = linea.Split(';');
            IDsolicitud = int.Parse(datos[0]);
            IDcliente = int.Parse(datos[1]);
            Tiposolicitud = datos[2];
            PesoKg = double.Parse(datos[3]);
            DomicilioEntrega = datos[4];
            Receptor = datos[5];
            EstadoSolicitud = datos[6];
            PrecioAFacturar = float.Parse(datos[7]);
            LocalidadOrigen = datos[8];
            ProvinciaOrigen = datos[9];
            LocalidadDestino = datos[10];
            ProvinciaDestino = datos[11];
            PaisDestino = datos[12];
            EsUrgente = datos[13];
            RetiroEnPuerta = datos[14];
            EntregaEnPuerta = datos[15];
            DomicilioOrigen = datos[16];
            RegionOrigen = datos[17];
            RegionDestino = datos[18];
        }

        public int IDsolicitud { get; set; }
        public int IDcliente { get; set; }
        public string Tiposolicitud { get; set; }
        public double PesoKg { get; set; }
        public string DomicilioEntrega { get; set; }
        public string Receptor { get; set; }
        public string EstadoSolicitud { get; set; }
        public float PrecioAFacturar { get; set; }
        public string LocalidadOrigen { get; set; }
        public string ProvinciaOrigen { get; set; }
        public string LocalidadDestino { get; set; }
        public string ProvinciaDestino { get; set; }
        public string PaisDestino { get; set; }
        public string EsUrgente { get; set; }
        public string RetiroEnPuerta { get; set; }
        public string EntregaEnPuerta { get; set; }
        public string DomicilioOrigen { get; set; }
        public string RegionOrigen { get; set; }
        public string RegionDestino { get; set; }

        private static readonly Dictionary<int, SolicitudServicio> RegistroSolicitudes = new Dictionary<int, SolicitudServicio>();

        public string ObtenerLineaDatos()
        {
            return $"{IDsolicitud};{IDcliente};{Tiposolicitud};{PesoKg};{DomicilioEntrega};{Receptor};{EstadoSolicitud};{PrecioAFacturar};{LocalidadOrigen};{ProvinciaOrigen};{LocalidadDestino};{ProvinciaDestino};{PaisDestino};{EsUrgente};{RetiroEnPuerta};{EntregaEnPuerta};{DomicilioOrigen};{RegionOrigen};{RegionDestino}";
        }

        public static SolicitudServicio IngresarSolicitud(int idcliente)
        {
            Console.Clear();
            var solicitud = new SolicitudServicio();
            bool salir = false;
            solicitud.IDcliente = idcliente;
            solicitud.IDsolicitud = GenerarIDsolicitud();

            Console.WriteLine("Nueva solicitud de servicio." + Environment.NewLine);
            Console.WriteLine("Seleccione el tipo de servicio:");
            Console.WriteLine("1- Correspondencia (hasta 500 gramos).");
            Console.WriteLine("2- Encomienda.");

            Console.WriteLine(Environment.NewLine + "Ingrese una opción y presione [Enter]");

            do
            {    
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        solicitud.Tiposolicitud = "Correspondencia";
                        solicitud.PesoKg = 0.5;
                        salir = true;
                        break;

                    case "2":
                        solicitud.Tiposolicitud = "Encomienda";
                        bool flag = false;

                        do
                        {
                            Console.WriteLine(Environment.NewLine + "Ingrese el peso de la encomienda (en Kg).");
                            var ingreso = Console.ReadLine();

                            if (!double.TryParse(ingreso, out var pesoEncomienda))
                            {
                                Console.WriteLine(Environment.NewLine + "No ha ingresado un peso válido. Por favor, intente nuevamente.");
                                flag = true;
                                continue;
                            }

                            if (pesoEncomienda < 0.5)
                            {
                                Console.WriteLine(Environment.NewLine + "El peso de la encomienda no puede ser menor a 0.5Kg.");
                                flag = true;
                                continue;
                            }

                            if (pesoEncomienda > 30)
                            {
                                Console.WriteLine(Environment.NewLine + "El peso de la encomienda no puede ser mayor a 30Kg.");
                                flag = true;
                                continue;
                            }

                            flag = false;
                            solicitud.PesoKg = pesoEncomienda;

                        } while (flag);

                        salir = true;
                        break;

                    default:
                        Console.WriteLine(Environment.NewLine + "No ha ingresado una opción del menú. Por favor, intente de nuevo.");
                        break;
                }
            } while (!salir);

            bool check = false;
            string localidad = "";
            do
            {
                var ingreso = Ingreso("Ingrese la localidad de origen del envío.");
                
                bool validar = Localidad.ValidarLocalidad(ingreso);

                if(!validar)
                {
                    Console.WriteLine(Environment.NewLine + "La localidad ingresada no existe en nuestros registros. Por favor, intente de nuevo.");
                    check = true;
                }
                else
                {
                    check = false;
                }

                localidad = ingreso;

            } while (check);

            solicitud.LocalidadOrigen = localidad;

            var provincia = Localidad.SeleccionarLocalidad(localidad);
            solicitud.ProvinciaOrigen = provincia.Provincia;
            solicitud.RegionOrigen = provincia.Región;
            
            string destino = "";
            do
            {
                check = false;
                var ingreso = Ingreso("Ingrese la localidad de destino del envío.");

                bool validar = Localidad.ValidarLocalidad(ingreso);

                if (!validar)
                {
                    Console.WriteLine(Environment.NewLine + "La localidad ingresada no existe en nuestros registros. Por favor, intente de nuevo.");
                    check = true;
                }
                else
                {
                    check = false;
                }

                destino = ingreso;

            } while (check);

            solicitud.LocalidadDestino = destino;

            var Destino = Localidad.SeleccionarLocalidad(destino);
            solicitud.ProvinciaDestino = Destino.Provincia;
            solicitud.PaisDestino = Destino.País;
            solicitud.RegionDestino = Destino.Región;

            solicitud.Receptor = Ingreso("Ingrese el nombre y apellido del receptor del envío.");
            solicitud.EsUrgente = TarifaDiferencial("¿Desea que el envío sea con servicio urgente? S/N (El mismo cuenta con una tarifa adicional.)");
            
            var retiropuerta = TarifaDiferencial("¿Desea que el servicio incluya retiro en puerta? S/N (El mismo cuenta con una tarifa adicional).");

            if(retiropuerta == "Si")
            {
                solicitud.DomicilioOrigen = Ingreso("Ingrese el domicilio donde se retirará el envío.");              
            }
            else
            {
                solicitud.DomicilioOrigen = null;
            }

            solicitud.RetiroEnPuerta = retiropuerta;

            var entregapuerta = TarifaDiferencial("¿Desea que el servicio incluya entrega en puerta? S/N (El mismo cuenta con una tarifa adicional).");
            
            if (entregapuerta == "Si")
            {
                solicitud.DomicilioEntrega = Ingreso("Ingrese el domicilio de destino.");
            }
            else
            {
                solicitud.DomicilioEntrega = null;
            }

            solicitud.EntregaEnPuerta = entregapuerta;
            solicitud.PrecioAFacturar = Tarifa.CalcularPrecio(solicitud);
            solicitud.EstadoSolicitud = "Recibida";

            RegistroSolicitudes.Add(solicitud.IDsolicitud, solicitud);
            GrabarSolicitudes();

            CuentaCorriente.Debitar(solicitud);

            return solicitud;
        }

        public static SolicitudServicio SeleccionarSolicitud()
        {
            var modelo = SolicitudServicio.CrearModeloBusqueda();

            foreach(var solicitud in RegistroSolicitudes.Values)
            {
                if(solicitud.CoincideCon(modelo))
                {
                    return solicitud;
                }
            }

            Console.WriteLine(Environment.NewLine + "No se ha encontrado una solicitud que coincida con los criterios indicados.");
            return null;
        }

        public void MostrarEstadoSolicitud()
        {
            Console.WriteLine(Environment.NewLine + $"ID solicitud: {IDsolicitud}");
            Console.WriteLine($"Estado: {EstadoSolicitud}");
        }

        public void MostrarSolicitud()
        {
            Console.WriteLine(Environment.NewLine +  $"ID solicitud: {IDsolicitud}");
            Console.WriteLine($"Cliente: {IDcliente}");
            Console.WriteLine(Tiposolicitud);
            Console.WriteLine($"Localidad destino: {LocalidadDestino}");

            if(PaisDestino == "Argentina")
            {
                Console.WriteLine($"Provincia destino: {ProvinciaDestino}");
            }
            else
            {
                Console.WriteLine($"País destino: {PaisDestino}");
            }
            
        }

        private static string Ingreso(string titulo)
        {
            bool flag = false;
            string ingreso;
            Console.WriteLine(Environment.NewLine + titulo);

            do
            {    
                ingreso = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine(Environment.NewLine + "Este campo no puede estar vacío. Por favor, intente de nuevo.");
                    flag = false;
                    continue;
                }
                else
                {
                    flag = true;
                }

            } while (!flag);

            return ingreso;
        }

        private static string TarifaDiferencial(string titulo)
        {
            bool flag = false;
            string ingreso = "";
            
            do
            {
                Console.WriteLine(Environment.NewLine + titulo);
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.S)
                {
                    ingreso = "Si";
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    ingreso = "No";
                    flag = true;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Usted ingresó un valor incorrecto, intente de nuevo.");
                    flag = false;
                }
            } while (!flag);

            return ingreso;
        }

        public static SolicitudServicio CrearModeloBusqueda()
        {
            var modelo = new SolicitudServicio();
            bool flag = false;
            do
            {
                Console.WriteLine(Environment.NewLine + "Ingrese el número de la solicitud a buscar.");
                var solicitudABuscar = Console.ReadLine();

                if (!int.TryParse(solicitudABuscar, out var salida))
                {
                    Console.WriteLine("Usted ingreso un valor incorrecto. Intente nuevamente." + Environment.NewLine);
                    continue;
                }
                else
                {
                    modelo.IDsolicitud = salida;
                    flag = true;
                }

            } while (flag == false);

            return modelo;
        }

        public bool CoincideCon(SolicitudServicio modelo)
        {
           if(IDsolicitud != modelo.IDsolicitud)
           {
                return false;
           }
           else
           {
                return true;
           }
        }

        private static void GrabarSolicitudes()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var solicitud in RegistroSolicitudes.Values)
                {
                    var linea = solicitud.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }

        private static int GenerarIDsolicitud()
        {
            Random r = new Random();

            bool flag = true;
            var idsolicitud = 0;

            do
            {
                idsolicitud = r.Next(10000, 99999);
                bool existe = SolicitudServicio.Existe(idsolicitud);

                if (existe)
                {
                    flag = false;
                }

            } while (!flag);

            return idsolicitud;
        }

        public static bool Existe (int idsolicitud)
        {
            return RegistroSolicitudes.ContainsKey(idsolicitud);
        }

        public static void CargarSolicitudes()
        {
            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var solicitud = new SolicitudServicio(linea);
                        RegistroSolicitudes.Add(solicitud.IDsolicitud, solicitud);
                    }
                }
            }
        }

    }

    
}
