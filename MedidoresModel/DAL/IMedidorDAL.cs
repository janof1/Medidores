using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel.DAL
{
    public interface IMedidorDAL
    {
        void IngresarMedidor(Medidor medidor);

        List<Medidor> ObtenerMedidores();

        List<Medidor> FiltrarMedidores(int nromedidor);
    }
}
