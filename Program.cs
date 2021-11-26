using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Title = "Portal de Autogestión - Clientes Corporativos";

            SolicitudServicio.CargarSolicitudes();
            Cliente.CargarClientes();
            CuentaCorriente.CargarCuentas();
            Tarifa.CargarTarifario();
            Localidad.CargarLocalidades();

            var idcliente = ValidarUsuario();
            
            bool salir = false;

            do
            {
                Console.WriteLine(Environment.NewLine + "MENÚ PRINCIPAL");
                Console.WriteLine("--------------");

                Console.WriteLine("1- Solicitar servicio de correspondencia o encomienda.");
                Console.WriteLine("2- Consultar el estado de un servicio.");
                Console.WriteLine("3- Consultar el estado de cuenta.");
                Console.WriteLine("4- Finalizar.");

                Console.WriteLine(Environment.NewLine + "Ingrese una opción y presione [Enter]");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        SolicitarServicio(idcliente);
                        break;

                    case "2":
                        ConsultarEstadoServicio();
                        break;

                    case "3":
                        ConsultarEstadoCuenta();
                        break;

                    case "4":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine(Environment.NewLine + "No ha ingresado una opción del menú. Por favor, intente de nuevo." + Environment.NewLine);
                        break;
                }

            } while (!salir);
        }

        private static void SolicitarServicio(int idcliente)
        {
            var solicitud = SolicitudServicio.IngresarSolicitud(idcliente);
            Console.WriteLine(Environment.NewLine + "Solicitud creada con éxito.");
            Console.WriteLine(Environment.NewLine + "Presione ENTER para volver al menú principal.");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ConsultarEstadoServicio()
        {
            var solicitud = SolicitudServicio.SeleccionarSolicitud();

            if(solicitud == null)
            {
                goto salida;
            }

            bool flag = false;
            Console.WriteLine(Environment.NewLine + "La solicitud seleccionada es:");
            solicitud.MostrarSolicitud();

            do
            {
                Console.WriteLine(Environment.NewLine + "¿Está seguro de que desea continuar? (S/N)");
                var key = Console.ReadKey(intercept: true);
                
                if (key.Key == ConsoleKey.S)
                {
                    solicitud.MostrarEstadoSolicitud();
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine(Environment.NewLine + "Operación cancelada.");
                    flag = true;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Usted ingresó un valor incorrecto, intente de nuevo.");
                    flag = false;
                }

            } while (flag == false);

            salida:
            Console.WriteLine(Environment.NewLine + "Presione ENTER para volver al menú principal.");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ConsultarEstadoCuenta()
        {
            var cuenta = CuentaCorriente.SeleccionarCuenta();

            if(cuenta == null)
            {
                goto salida;
            }

            bool flag = false;
            Console.WriteLine(Environment.NewLine + "La cuenta seleccionada es:");
            cuenta.MostrarCuenta();

            do
            {
                Console.WriteLine(Environment.NewLine + "¿Está seguro de que desea continuar? (S/N)");
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.S)
                {
                    cuenta.MostrarEstadoCuenta();
                    flag = true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine(Environment.NewLine + "Operación cancelada.");
                    flag = true;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine +"Usted ingresó un valor incorrecto, intente de nuevo.");
                    flag = false;
                }

            } while (flag == false);

            salida:
            Console.WriteLine(Environment.NewLine + "Presione ENTER para volver al menú principal.");
            Console.ReadKey();
            Console.Clear();
        }

        private static int ValidarUsuario()
        {
            Console.WriteLine("Bienvenido al Portal de Clientes Corporativos." + Environment.NewLine);
            bool flag = false;
            int idcliente = 0;

            Console.WriteLine("Por favor, ingrese su número de cliente.");
            do
            {
                var ingreso = Console.ReadLine();

                if(!int.TryParse(ingreso, out var salida))
                {
                    Console.WriteLine(Environment.NewLine + "No ha ingresado un número de cliente válido. Intente nuevamente.");
                    continue;
                }

                bool existe = Cliente.ExisteID(salida);

                if(!existe)
                {
                    Console.WriteLine(Environment.NewLine + "El ID ingresado no pertenece a ningún cliente registrado. Intente nuevamente.");
                    continue;
                }

                if(existe)
                {
                    flag = true;
                }

                idcliente = salida;

            } while (!flag);

            Console.Clear();
            return idcliente;
        }

    }
}
