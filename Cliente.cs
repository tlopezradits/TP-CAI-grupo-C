using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_CAI
{
    class Cliente
    {
        public Cliente()
        {
            
        }

        public Cliente (string linea)
        {   
            var datos = linea.Split(';');
            IDcliente = int.Parse(datos[0]);
            username = datos[1];
            direccion = datos[2];
            numtelefono = int.Parse(datos[3]);
            IDcuenta = int.Parse(datos[4]);
            RazonSocial = datos[5];
            CUIT = long.Parse(datos[6]);
        }

        public int IDcliente { get; set; }
        public string username { get; set; }
        public string direccion { get; set; }
        public int numtelefono { get; set; }
        public int IDcuenta { get; set; }
        public string RazonSocial { get; set; }
        public long CUIT { get; set; }

        static readonly Dictionary<int, Cliente> RegistroClientes = new Dictionary<int, Cliente>();

        public static bool ExisteID(int idcliente)
        {
            return RegistroClientes.ContainsKey(idcliente);
        }

        public static Cliente SeleccionarCliente(int idcliente)
        {
            var modelo = Cliente.CrearModeloBusqueda(idcliente);

            foreach (var cliente in RegistroClientes.Values)
            {
                if (cliente.Comparacion(modelo))
                {
                    return cliente;
                }
            }

            Console.WriteLine("No se ha encontrado una solicitud que coincida con los criterios indicados.");
            return null;
        }

        public static Cliente CrearModeloBusqueda(int idcliente)
        {
            var modelo = new Cliente();    
            modelo.IDcliente = idcliente;

            return modelo;
        }
        
        public bool Comparacion(Cliente modelo)
        {
            if (IDcliente != modelo.IDcliente)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void CargarClientes()
        {
            string nombreArchivo = "RegistroClientes.txt";

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cliente = new Cliente(linea);
                        RegistroClientes.Add(cliente.IDcliente, cliente);
                    }
                }
            }
        }
    }
}
