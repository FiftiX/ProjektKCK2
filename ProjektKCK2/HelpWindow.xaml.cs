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
using System.Windows.Shapes;

namespace ProjektKCK2
{
    /// <summary>
    /// Logika interakcji dla klasy HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            WriteInstruction();
        }

        private void WriteInstruction()
        {
            HelpText.Text =
"W grze poruszamy się drogą dwukierunkową za pomocą strzałek w lewo i prawo. (Left Arrow i Right Arrow).\n" +
"W poruszaniu przeszkdza ruch uliczny. Przybliżamy się wolniej do aut jadących zgodnie z naszym kierunkiem jazdy.\n" +
"Najeżdżając na gwiazdkę dostajemy 1 punkt. Co 10 zebranych gwiazdek, poziom się zwiększa, co oznacza zwiększenie prędkości ruchy ulicznego \n" +
            "Jeśli chcesz wrócić do menu wciśnij ESC.";

        }




        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MenuWindow MenuWindow = new MenuWindow();
                MenuWindow.Show();
                Close();

            }


        }


    }
}


