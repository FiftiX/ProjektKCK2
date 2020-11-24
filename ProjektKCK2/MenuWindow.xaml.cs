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
    /// Logika interakcji dla klasy MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            
        }

        private int menu=0;


        private void KDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Up)
            {
                if (menu == 0)
                {
                    menu = 2;
                }
                else
                {
                    menu--;
                } 
            }

            if(e.Key == Key.Down)
            {
                if (menu == 2)
                {
                    menu = 0;
                }
                else
                {
                    menu++;
                }
            }
            HeadMethod();

        }


        private void HeadMethod()
        {
            if (menu == 0)
            {
                T1.Visibility = Visibility.Visible;
                T2.Visibility = Visibility.Hidden;
                T3.Visibility = Visibility.Hidden;
            }
            if (menu == 1)
            {
                T1.Visibility = Visibility.Hidden;
                T2.Visibility = Visibility.Visible;
                T3.Visibility = Visibility.Hidden;
            }
            if (menu == 2)
            {
                T1.Visibility = Visibility.Hidden;
                T2.Visibility = Visibility.Hidden;
                T3.Visibility = Visibility.Visible;
            }
            
        }
    }
}
