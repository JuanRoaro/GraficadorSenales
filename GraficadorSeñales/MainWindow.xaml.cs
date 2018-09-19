using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void graficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txtAmplitud.Text);
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double fase = double.Parse(txtFase.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            Señal señal;

            switch (cbTipoSeñal.SelectedIndex)
            {
                //Seniodal
                case 0: señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                //Rampa
                case 1: señal = new Rampa();
                    break;
                default: señal = null;
                    break;
            }

            señal.tiempoFinal = tiempoFinal;
            señal.tiempoInicial = tiempoInicial;
            señal.frecuenciaMuestreo = frecuenciaMuestreo;

            señal.construirSeñalDigital();
            plnGrafica.Points.Clear();

            if (señal != null)
            {
                //recorrer una coleccion o arreglo
                foreach (Muestra muestra in señal.muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.x - tiempoInicial) * scrContenedor.Width, (muestra.y / señal.amplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
                }

                lblAmplitudMaximaY.Text = señal.amplitudMaxima.ToString();
                lblAmplitudMaximaNegativaY.Text = "-" + señal.amplitudMaxima.ToString();
            }

           

            

            plnEjeX.Points.Clear();
            //punto del principio
            plnEjeX.Points.Add(new Point(0, scrContenedor.Height / 2));
            //punto del fin
            plnEjeX.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width, scrContenedor.Height / 2));

            plnEjeY.Points.Clear();
            //punto del principio
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width, (1 * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            //punto del fin
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width, (-1 * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
        }

        private void btnGraficarRampa_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            Rampa rampa = new Rampa();
            plnGrafica.Points.Clear();

            double periodoMuestro = 1 / frecuenciaMuestreo;

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestro)
            {
                double valorMuestra = rampa.evaluar(i);
                rampa.muestras.Add(new Muestra(i, valorMuestra));
            }
            foreach (Muestra muestra in rampa.muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.x * scrContenedor.Width, (muestra.y * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
            }
        }
    }
}
