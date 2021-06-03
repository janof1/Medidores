using Medidores.Comunicacion;
using MedidoresModel;
using MedidoresModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medidores
{
    class Program
    {
        private static ILecturaDAL lecturaDAL = LecturaDALArchivos.GetInstancia();
        private static IMedidorDAL medidorDAL = MedidorDALObjetos.GetInstancia();
        private static ClienteCom clienteCom;
        private static bool continuar = true;
        static bool Menu()
        {

            Console.WriteLine("Bienvenido a lectura de Medidores");
            Console.WriteLine("1. Ingresar \n 2. Mostrar \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese nuevamente");
                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            //1. Iniciar el Servidor Socket en el puerto 3000
            //2. El puerto tiene que ser configurable en el App.Config
            //3. cuando reciba un cliente, tiene que solicitar a ese cliente el 
            //nromedidor, fecha, valorconsumo, y agregar un nuevo medidor con el tipo TCP.

            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            while (Menu()) ;
        }

        static void Ingresar()
        {
            //declaro variables para recibir los datos
            int nromedidor;
            DateTime fecha;
            decimal valor;

            Console.Clear();
            Console.WriteLine("Ingreso de Datos");
            Console.WriteLine("------- -- -----");
            //valido ingreso de medidor tipo int
            bool esValido;
            do
            {
                Console.WriteLine("Ingrese Nro Medidor: ");
                esValido = int.TryParse(Console.ReadLine().Trim(), out nromedidor);
            } while (!esValido);


            esValido = false;//inicializo para volver a usar
            //valido ingreso fecha
            Console.WriteLine("Formato :" + DateTime.Now.ToString("yyyy-MM-dd HH:mmm:ss"));
            do
            {
                Console.WriteLine("Ingrese Fecha: ");
                esValido = DateTime.TryParse(Console.ReadLine().Trim(), out fecha);
            } while (!esValido);

            esValido = false;//inicializo para volver a usar 
            //valido ingreso valor
            do
            {
                Console.WriteLine("Ingrese Valor Consumo");
                esValido = Decimal.TryParse(Console.ReadLine().Trim(), out valor);
            } while (!esValido);

            Lectura lectura = new Lectura();
            {
                lectura.NroMedidor = nromedidor;
                lectura.Fecha = fecha;
                lectura.Valor = valor;
            }
            lock (lecturaDAL)
            {
                lecturaDAL.IngresarLectura(lectura);
            }
        }
        static void Mostrar()
        {
            List<Lectura> lecturas = null;
            lock (lecturaDAL)
            {
                lecturas = lecturaDAL.ObtenerLecturas();
            }
            foreach (Lectura lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }

            Console.ReadKey();//pausa antes de cerrar app

            continuar = false;//cierro aplicacion
        }
    }
}
