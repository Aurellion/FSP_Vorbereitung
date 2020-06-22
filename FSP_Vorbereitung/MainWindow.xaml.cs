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
using System.Windows.Threading;

namespace FSP_Vorbereitung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Zahl;
        Random rnd = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        List<Körper> alleKörper = new List<Körper>();
        public MainWindow()
        {
            InitializeComponent();
            Zahl = 0;
            timer.Interval = TimeSpan.FromMilliseconds(17);
            timer.Tick += Animiere;
        }

        private void Animiere(object sender, EventArgs e)
        {
            Zeichenfläche.Children.Clear();
            foreach (Körper item in alleKörper)
            {
                item.Bewegen(timer.Interval, Zeichenfläche);
                item.Zeichnen(Zeichenfläche);
            }
        }

        private void BT_Umkehren_Click(object sender, RoutedEventArgs e)
        {
            string str = TB_Eingabe.Text;
            string rev = "";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                rev += str[i];
            }
            TB_Ausgabe.Text = rev;
        }

        private void BT_rnd_Click(object sender, RoutedEventArgs e)
        {
            Zahl += 1;
            if (Zahl % 7 == 0 || Zahl.ToString().Contains('7'))
            {
                TB_zahl.Text = "---" + Zahl.ToString() + "---\n";
            }
            else TB_zahl.Text = Zahl.ToString() + "\n";
        }

        private void BTN_Start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            BTN_Start.IsEnabled = false;
            BTN_New.IsEnabled = true;
            BTN_rem.IsEnabled = true;

            for (int i = 0; i < 20; i++)
            {
                alleKörper.Add(new Körper(rnd.NextDouble()*Zeichenfläche.ActualWidth, rnd.NextDouble()*Zeichenfläche.ActualHeight, (rnd.NextDouble()-0.5)*50, (rnd.NextDouble() - 0.5) * 50));
            }
        }

        private void BTN_New_Click(object sender, RoutedEventArgs e)
        {
            alleKörper.Add(new Körper(rnd.NextDouble() * Zeichenfläche.ActualWidth, rnd.NextDouble() * Zeichenfläche.ActualHeight, (rnd.NextDouble() - 0.5) * 50, (rnd.NextDouble() - 0.5) * 50));
            alleKörper.Last().FarbeÄndern();
        }

        private void BTN_rem_Click(object sender, RoutedEventArgs e)
        {
            int r = rnd.Next(0, alleKörper.Count);
            alleKörper.RemoveAt(r);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        alleKörper.ForEach(k => k.Beschleunigen(true));
                        break;

                    case Key.Down:
                        alleKörper.ForEach(k => k.Beschleunigen(false));
                        break;
                }
            }
        }

        private void BTN_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled) timer.Stop();
            else if (!timer.IsEnabled) timer.Start();
        }
    }
}
