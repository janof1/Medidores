using MedidoresModel;
using MedidoresModel.DAL;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidores.Comunicacion
{
    class HebraCliente
    {
        private ClienteCom clienteCom;
        private IMensajesDAL mensajesDAL = MensajeDALArchivos.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese Nro Medidor: ");
            int nromedidor = Convert.ToInt32(clienteCom.Leer());
            clienteCom.Escribir(DateTime.Now.ToString("yyyy-MM-dd-HH-mmm-ss"));
            string fecha = DateTime.Now.ToString("yyyy-MM-dd-HH-mmm-ss");
            clienteCom.Escribir("Ingrese Valor Consumo: ");
            decimal valorconsumo = Convert.ToDecimal(clienteCom.Leer());

            Mensaje mensaje = new Mensaje()
            {
                NroMedidor = nromedidor,
                Fecha = fecha,
                ValorConsumo = (decimal)valorconsumo
            };

            lock (mensajesDAL)
            {
                mensajesDAL.AgregarMensaje(mensaje);
            }

            clienteCom.Desconectar();
        }
    }
}
