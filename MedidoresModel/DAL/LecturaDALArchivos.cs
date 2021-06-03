using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MedidoresModel.DAL
{
    public class LecturaDALArchivos : ILecturaDAL
    {   //para implementar Singleton
        //1. El contructor tiene que ser private
        private LecturaDALArchivos()
        {

        }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static LecturaDALArchivos instancia;
        //3. Tener un metodo GetIntance, que devuelve una referencia al atributo
        public static ILecturaDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturaDALArchivos();
            }
            return instancia;
        }

        private static string archivo = "lecturas.txt";
        private static string ruta = Directory.GetCurrentDirectory() + "/" + archivo;

        public void IngresarLectura(Lectura lectura)
        {
            //1. Crear el StreamWriter
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta, true))
                {
                    //1. recupero fecha y la formateo , para enviar a servidor
                    string[] fechaArr = lectura.Fecha.ToString().Split(' ');//separo fecha de la hora por el espacio
                    string[] tfecha = fechaArr[0].Split('/'); //separo fecha en dia-mes-año fecha formato yyyy-MM-dd
                    string thora = fechaArr[1].Replace(':', '-'); //guardo hora formato HH:mmm:ss y reemplazo los : por -
                    string fechaformateada = tfecha[2] + "-" + tfecha[1] + "-" + tfecha[0] + "-" + thora;//fecha se unen los resultados y se genera el formato pedido yyyy-MM-dd-HH-mmm-ss
                    //2. Agrear una linea al archivo
                    writer.WriteLine(lectura.NroMedidor + "|" + fechaformateada + "|" + lectura.Valor);
                    // 3.Cerrar el StreamWriter osea confirmar la escritura
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir en archivo" + ex.Message);
            }
        }

        public List<Lectura> FiltrarLecturas(int nromedidor)
        {
            return ObtenerLecturas().FindAll(p => p.NroMedidor == nromedidor);
        }
        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lecturas = new List<Lectura>();
            using (StreamReader reader = new StreamReader(ruta))
            {
                string texto;
                do
                {
                    texto = reader.ReadLine(); //error NULL
                    if (texto != null)
                    {
                        string[] textoArr = texto.Trim().Split('|');
                        int nromedidor = Convert.ToInt32(textoArr[0]);
                        string[] fechaArr = textoArr[1].Split('-');
                        DateTime fecha = new DateTime(int.Parse(fechaArr[0]),int.Parse(fechaArr[1]), int.Parse(fechaArr[2]), int.Parse(fechaArr[3]), int.Parse(fechaArr[4]), int.Parse(fechaArr[5])); 
                        decimal valor = Convert.ToDecimal(textoArr[2]);
                        // crear una lectura
                        Lectura l = new Lectura()
                        {
                            NroMedidor = nromedidor,
                            Fecha = fecha,
                            Valor = valor
                        };
                        lecturas.Add(l);
                    }
                } while (texto != null);
            }
            return lecturas;
        }
    }
}
