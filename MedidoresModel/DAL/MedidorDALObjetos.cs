using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel.DAL
{
    public class MedidorDALObjetos : IMedidorDAL
    {
        //para implementar Singleton
        //1. El contructor tiene que ser private
        private MedidorDALObjetos()
        {

        }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static MedidorDALObjetos instancia;
        //3. Tener un metodo GetIntance, que devuelve una referencia al atributo
        public static IMedidorDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALObjetos();
            }
            return instancia;
        }

        //1. Crear una lista para guardar medidores
        private static List<Medidor> medidores = new List<Medidor>();

        //2. Crear las operaciones ingresar , mostrar y buscar

        public void IngresarMedidor(Medidor m)
        {
            medidores.Add(m);
        }

        public List<Medidor> ObtenerMedidores()
        {
            return medidores;
        }

        public List<Medidor> FiltrarMedidores(int nromedidor)
        {
            return medidores.FindAll(p => p.NroMedidor == nromedidor);
        }
    }
}
