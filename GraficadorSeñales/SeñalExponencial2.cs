using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalExponencial2 : Señal
    {
        public double alpha { get; set; }

        public SeñalExponencial2(double alpha2)
        {
            this.alpha = alpha2;
            muestras = new List<Muestra>();
        }

        public override double evaluar(double tiempo)
        {
            double resp = Math.Exp(alpha * tiempo);

            return resp;
        }
    }
}
