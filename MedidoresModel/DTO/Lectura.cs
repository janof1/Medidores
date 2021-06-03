using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel
{
    public class Lectura
    {
        private int nromedidor;
        private DateTime fecha;
        private Decimal valor;

        public int NroMedidor { get => nromedidor; set => nromedidor = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public Decimal Valor { get => valor; set => valor = value; }

        public override string ToString()
        {
            return nromedidor.ToString()
                + " "
                + fecha.ToString()
                + " "
                + valor.ToString();
        }
    }
}
