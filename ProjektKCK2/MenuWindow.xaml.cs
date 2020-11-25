using System;
using System.Collections.Generic;
using System.IO;
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
            Reading();
            Complet(Player.Punkty, Player.Nick);
            
        }




        private int menu=0;
        public static string[,] array = new string[10, 2];

        

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
                if (menu == 3)
                {
                    menu = 0;
                }
                else
                {
                    menu++;
                }
            }
            Visible();

            if(e.Key==Key.Enter && menu == 0)
            {
                
                NickLabel.Visibility = Visibility.Visible;
                NickText.Visibility = Visibility.Visible;
                NickText.Focus();
                

                if (e.Key == Key.Enter && NickText.Text!="")
                {
                    Player.Nick = NickText.Text;
                    MainWindow GameWindow = new MainWindow();
                    GameWindow.Show();
                    Close();
                }

                
            }

            if(e.Key==Key.Enter && menu == 1)
            {
                lvPlayers.Visibility = Visibility.Visible;
                LabelPoints.Visibility = Visibility.Visible;
                Top10();

            }
            if (e.Key == Key.Escape)
            {
                lvPlayers.Visibility = Visibility.Hidden;
                LabelPoints.Visibility = Visibility.Hidden;
                HelpText.Visibility = Visibility.Hidden;
            }

            if (e.Key==Key.Enter && menu == 2)
            {
                HelpText.Visibility = Visibility.Visible;
                WriteInstruction();
            }
            
            if (e.Key == Key.Enter && menu == 3)
            {
                Writing();
                Application.Current.Shutdown();
            }

        }


        private void Visible()
        {
            switch (menu)
            {
                case 0:
                    T1.Visibility = Visibility.Visible;
                    T2.Visibility = Visibility.Hidden;
                    T3.Visibility = Visibility.Hidden;
                    T4.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    T1.Visibility = Visibility.Hidden;
                    T2.Visibility = Visibility.Visible;
                    T3.Visibility = Visibility.Hidden;
                    T4.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    T1.Visibility = Visibility.Hidden;
                    T2.Visibility = Visibility.Hidden;
                    T3.Visibility = Visibility.Visible;
                    T4.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    T1.Visibility = Visibility.Hidden;
                    T2.Visibility = Visibility.Hidden;
                    T3.Visibility = Visibility.Hidden;
                    T4.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void Reading()
        {
            StreamReader r1 = new StreamReader(@"../../Dane/wyniki.txt");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    array[i, j] = r1.ReadLine();
                }
            }
            r1.Close();
        }

        public void Writing()
        {
            string pom;
            StreamWriter w1 = new StreamWriter(@"../../Dane/wyniki.txt");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    pom = array[i, j];
                    w1.WriteLine(pom);
                }
            }
            w1.Close();


        }

        public void Complet(int score, string nick)
        {
            if (score != 0 && nick != "")
            {
                for (int i = 0; i < 10; i++)
                {
                    if (int.Parse(array[i, 1]) < score)
                    {
                        for (int j = 10; j > i+1; j--)
                        {
                            array[j - 1, 0] = array[j - 2, 0];
                            array[j - 1, 1] = array[j - 2, 1];

                        }
                        array[i, 0] = nick;
                        array[i, 1] = score.ToString();
                        
                        break;
                    }
                }
            }
        }

        public void Top10()
        {
            List<Player> items = new List<Player>();
            for(int i = 0; i < 10; i++)
            {
                items.Add(new Player() { Place = i + 1, NickName = array[i, 0], Points = int.Parse(array[i, 1]) });
            }
            lvPlayers.ItemsSource = items;
        }

        private void WriteInstruction()
        {
            HelpText.Text =
            "W grze poruszamy się drogą dwukierunkową za pomocą strzałek w lewo i prawo. (Left Arrow i Right Arrow).\n" +
            "W poruszaniu przeszkdza ruch uliczny. Przybliżamy się wolniej do aut jadących zgodnie z naszym kierunkiem jazdy.\n" +
            "Najeżdżając na gwiazdkę dostajemy 1 punkt. Co 10 zebranych gwiazdek, poziom się zwiększa,\nco oznacza zwiększenie prędkości ruchy ulicznego \n" +
            "Jeśli chcesz wrócić do menu wciśnij ESC.";
            HelpText.FontSize = 25;
        }



    }
}
