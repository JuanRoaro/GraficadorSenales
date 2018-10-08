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
    }
}