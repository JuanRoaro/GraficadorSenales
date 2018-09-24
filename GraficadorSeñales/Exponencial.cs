using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Exponencial : Señal
    {
        double alpha { get; set; }

        public Exponencial(double alpha)
        {
            muestras = new List<Muestra>();
            this.alpha = alpha;
        }

        public override double evaluar(double tiempo)
        {
            return Math.Exp(alpha * tiempo);
        }
    }
}
