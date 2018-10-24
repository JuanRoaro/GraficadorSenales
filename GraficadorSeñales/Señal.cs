using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    abstract class Señal
    {
        public List<Muestra> muestras { get; set; }
        public double amplitudMaxima { get; set; }
        public double tiempoInicial { get; set; }
        public double tiempoFinal { get; set; }
        public double frecuenciaMuestreo { get; set; }

        public abstract double evaluar(double tiempo);

        public void construirSeñalDigital()
        {
            double periodoMuestro = 1 / frecuenciaMuestreo;

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestro)
            {
                double valorMuestra = this.evaluar(i);

                this.muestras.Add(new Muestra(i, valorMuestra));

            }
        }

        public void escalar(double escalar)
        {
            foreach (Muestra muestra in muestras)
            {
                muestra.y *= escalar;
            }
        }

        public void actualizarAmplitudMaxima()
        {
            foreach (Muestra muestra in muestras)
            {
                if (Math.Abs(muestra.y) > this.amplitudMaxima)
                {
                    this.amplitudMaxima = Math.Abs(muestra.y);
                }
            }

        }

        public void desplazarY(double desplazamiento)
        {
            foreach (Muestra muestra in muestras)
            {
                muestra.y += desplazamiento;
            }
        }

        public void truncar(double n)
        {
            foreach (Muestra muestra in muestras)
            {
                if (muestra.y > n)
                {
                    muestra.y = n;
                }
                else if (muestra.y < -n)
                {
                    muestra.y = -n;
                }
            }
        }

        public static Señal sumar(Señal uno, Señal dos)
        {
            SeñalPersonalizada resultado = new SeñalPersonalizada();

            resultado.tiempoFinal = uno.tiempoFinal;
            resultado.tiempoInicial = uno.tiempoInicial;
            resultado.frecuenciaMuestreo = uno.frecuenciaMuestreo;

            int c=0;
            foreach(Muestra muestra in uno.muestras)
            {
                Muestra muestra2 = new Muestra();

                muestra2.x = muestra.x;
                muestra2.y = muestra.y + dos.muestras[c++].y;

                resultado.muestras.Add(muestra2);
            }

            return resultado;
        }
    }
}