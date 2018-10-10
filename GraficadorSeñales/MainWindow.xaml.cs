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
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtFrecuenciaMuestreo.Text);

            Señal señal;

            switch (cbTipoSeñal.SelectedIndex)
            {
                //Seniodal
                case 0:
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)panelConfiguracion.Children[0]).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                //Rampa
                case 1:
                    señal = new Rampa();
                    break;
                // Exponencial
                case 2:
                    double alpha = double.Parse(((ConfiguracionSeñalExponencial)panelConfiguracion.Children[0]).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    break;
                // Exponencial2
                case 3:
                    double alpha2 = double.Parse(((ConfiguracionSeñalExponencial2)panelConfiguracion.Children[0]).txtAlpha2.Text);
                    señal = new SeñalExponencial2(alpha2);
                    break;
                default:
                    señal = null;
                    break;
            }







            plnGrafica.Points.Clear();

            if (señal != null)
            {
                señal.tiempoFinal = tiempoFinal;
                señal.tiempoInicial = tiempoInicial;
                señal.frecuenciaMuestreo = frecuenciaMuestreo;

                //contruir señal
                señal.construirSeñalDigital();

                //escalar
                if ((bool)chbEscalar.IsChecked)
                {
                    double escalar = double.Parse(txtEscalar.Text);
                    señal.escalar(escalar);
                }

                //desplazamiento
                if ((bool)chbDesplazamiento.IsChecked)
                {
                    double desplazamiento = double.Parse(txtDesplazamientoY.Text);
                    señal.desplazarY(desplazamiento);
                }

                //desplazamiento
                if ((bool)chbTruncar.IsChecked)
                {
                    double umbral = double.Parse(txtUmbral.Text);
                    señal.truncar(umbral);
                }



                //actualizar amplitud maxima
                señal.actualizarAmplitudMaxima();

                //recorrer una coleccion o arreglo
                foreach (Muestra muestra in señal.muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.x - tiempoInicial) * scrContenedor.Width, (muestra.y / señal.amplitudMaxima * ((scrContenedor.Height / 2.0) - 30) * -1) + (scrContenedor.Height / 2)));
                }

                lblAmplitudMaximaY.Text = señal.amplitudMaxima.ToString("F");
                lblAmplitudMaximaNegativaY.Text = "-" + señal.amplitudMaxima.ToString("F");
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

        private void cbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelConfiguracion != null)
            {
                panelConfiguracion.Children.Clear();
                switch (cbTipoSeñal.SelectedIndex)
                {
                    case 0: //Senoidal
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                        break;
                    case 1: //rampa
                        break;
                    case 2: //Exponencial
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial());
                        break;
                    case 3: //Exponencial2
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial2());
                        break;
                    default:
                        break;
                }
            }

        }

        private void cbTipoSeñal_SegundaSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}