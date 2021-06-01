﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel.DAL
{
    public class MensajeDALArchivos : IMensajesDAL
    {
        //para implementar Singleton
        //1. El contructor tiene que ser private
        private MensajeDALArchivos()
        {

        }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static MensajeDALArchivos instancia;
        //3. Tener un metodo GetIntance, que devuelve una referencia al atributo
        public static IMensajesDAL GetInstancia()
        {
            if(instancia == null)
            {
                instancia = new MensajeDALArchivos();
            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";
        public void AgregarMensaje(Mensaje mensaje)
        {
           try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(mensaje.NroMedidor + "|" + mensaje.Fecha + "|" + mensaje.ValorConsumo);
                    writer.Flush();
                }
            }catch(Exception ex)
            {

            }
        }

        public List<Mensaje> ObtenerMensajes()
        {
            List<Mensaje> lista = new List<Mensaje>();
            try
            {
                using(StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            Mensaje mensaje = new Mensaje()
                            {
                                NroMedidor = Convert.ToInt32(arr[0]),
                                Fecha = arr[1],
                                ValorConsumo = Convert.ToDecimal(arr[2])
                            };
                            lista.Add(mensaje);
                        }

                    } while (texto != null);
                }

            }catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
