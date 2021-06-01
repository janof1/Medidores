using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel
{
    public class Mensaje
    {
        private int nromedidor;
        private string fecha;
        private Decimal valorconsumo;

        public int NroMedidor { get => nromedidor; set => nromedidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public Decimal ValorConsumo{ get => valorconsumo; set => valorconsumo = value; }

        public override string ToString()
        {
            return nromedidor
                + " "
                + fecha + " "
                + valorconsumo;
        }
    }
}
