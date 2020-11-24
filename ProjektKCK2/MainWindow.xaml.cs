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

namespace ProjektKCK2
{

    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        Random rand = new Random();

        int Window_Width = 1272;
        int Window_Height = 720;
        int speed = 10;
        int SteeringSpeed = 20;
        int CarChoose;
        int NewCar = 800;
        int Level = 0;
        List<Rectangle> itemRemover = new List<Rectangle>();

        int CoinTemp = 30;
        int score;
        bool gameOver, moveLeft, moveRight;
        Rect playerHitBox;
        ImageBrush coinImage = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30);

            StartGame();
        }



        private void GameLoop(object sender, EventArgs e)
        {
   
            scoreText.Content = "Coins=" + score.ToString();

            playerHitBox = new Rect(Canvas.GetLeft(e90_car), Canvas.GetTop(e90_car), e90_car.Width, e90_car.Height); 

            CoinTemp -= 1;

            if (moveLeft == true && Canvas.GetLeft(e90_car) > 0)
            {
                Canvas.SetLeft(e90_car, Canvas.GetLeft(e90_car) - SteeringSpeed);
            }
            if (moveRight == true && Canvas.GetLeft(e90_car) + 199 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(e90_car, Canvas.GetLeft(e90_car) + SteeringSpeed);
            }

            if (CoinTemp < 1)
            {
                Coins();
                CoinTemp = rand.Next(5, 100);
            }



            foreach (var x in MainCanvas.Children.OfType<Rectangle>())
            {

                if ((string)x.Tag == "Mark1")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed); 

                    if (Canvas.GetTop(x) > 800)
                    {
                        Canvas.SetTop(x, -152);
                    }

                } 


                if ((string)x.Tag == "Car")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed); 

                    Cars_Actions(x, false);

                } 

                if ((string)x.Tag == "Car2")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed / 2); 
                    Cars_Actions(x, true);


                }



                if ((string)x.Tag == "coin")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 5);

                    Rect coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(coinHitBox))
                    {
                        itemRemover.Add(x);
                        score++;
                        

                    }
              
                    if (Canvas.GetTop(x) > Window_Height)
                    {
                        itemRemover.Add(x);

                    }

                } 
            } 


            foreach (Rectangle y in itemRemover)
            {
                MainCanvas.Children.Remove(y);
            }

          
            if (score % 10 == 0 && score / 10 > Level)
            {
                Level++;
                speed = speed + 2;
            }

        }

        private void Cars_Actions(Rectangle x, bool direction)
        {
         
            if (Canvas.GetTop(x) > NewCar)
            {
                if (direction == true)
                {
                    ChangeCars(x, true);
                }
                else
                {
                    ChangeCars(x, false);
                }
            }

            Rect carHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


            if (playerHitBox.IntersectsWith(carHitBox))
            {
                gameTimer.Stop(); 
                EndLabel.Content = "GAMEOVER!\n" + "Press R to Restart Game\n" + "Press Esc to Back Menu"; // add this text to the existing text on the label
                gameOver = true; 
            }
        }

        private void StartGame()
        {
            Canvas.SetTop(e90_car, 550);
            Canvas.SetLeft(e90_car, 600);

            speed = 8;
            gameTimer.Start();
            moveLeft = false;
            moveRight = false;
            gameOver = false;
            score = 0;
            scoreText.Content = "Coins = 0";

            coinImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/coin.png"));

            SR_Cars("Car");
            SR_Cars("Car2");
        }

        private void SR_Cars(string car)
        {
            foreach (var x in MainCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == car)
                {
                    Canvas.SetTop(x, (rand.Next(100, 400) * -1));
                    Canvas.SetLeft(x, rand.Next(0, 500));
                    if (car == "Car")
                    {
                        ChangeCars(x, false);
                    }


                    else
                    {
                        ChangeCars(x, true);
                    }



                }
                itemRemover.Clear();
            }
        }

        private void ChangeCars(Rectangle car, bool OurDirection)
        {
            CarChoose = rand.Next(1, 3);

            ImageBrush carImage = new ImageBrush(); 
            Rect NewCarHitBox = new Rect(Canvas.GetLeft(car), Canvas.GetTop(car), car.Width, car.Height);

            if (OurDirection == true)
            {
                int NewHeight = rand.Next(100, 400) * -1;
                int NewWidth = (rand.Next(0, 50) % 3) * 205 + 664;
                switch (CarChoose)
                {
                    case 1:
                        carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/mk4_rear.png"));
                        break;
                    case 2:
                        carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/passat_rear.png"));
                        break;

                }

                car.Fill = carImage; 

                foreach (var x in MainCanvas.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "Car2")
                    {
                        if (Canvas.GetLeft(x) == NewWidth && Math.Abs(Canvas.GetTop(x) - NewHeight) <= x.Height + 10)
                        {
                            if (NewWidth < 664 + 205)
                                NewWidth = NewWidth + 205;
                            else
                                NewWidth = NewWidth - 205;
                        }
                    }
                }

                Canvas.SetLeft(car, NewWidth);
                Canvas.SetTop(car, NewHeight);





            }
            //przody
            else
            {
                int NewHeight = rand.Next(100, 400) * -1;
                int NewWidth = (rand.Next(0, 50) % 3) * 205 + 50;


                switch (CarChoose)
                {
                    case 1:
                        carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/mk4_front.png"));
                        break;
                    case 2:
                        carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/passat_front.png"));
                        break;
                }
                car.Fill = carImage; 

                foreach (var x in MainCanvas.Children.OfType<Rectangle>())
                {   
                    if ((string)x.Tag == "Car")
                    {
                        if (Canvas.GetLeft(x).Equals(NewWidth) && Math.Abs(Canvas.GetTop(x) - NewHeight) <= x.Height+10)
                        {

                            if (NewWidth < 50 + 205)
                                NewWidth = NewWidth + 205;
                            else
                                NewHeight = NewWidth - 205;
                        }
                    }

                }

                Canvas.SetLeft(car, NewWidth);
                Canvas.SetTop(car, NewHeight);
            }
        }

        private void Canvas_KeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }

            if (e.Key == Key.R && gameOver == true)
            {
                EndLabel.Content = "";
                StartGame();
            }

            if(e.Key == Key.Escape && gameOver == true)
            {
                MenuWindow men = new MenuWindow();
                men.Show();
                Close();
            }
        }


        private void Coins()
        {
            Rectangle newCoin = new Rectangle
            {
                Height = 50,
                Width = 50,
                Tag = "coin",
                Fill = coinImage
            };

            Canvas.SetLeft(newCoin, rand.Next(0, Window_Width - 50));
            Canvas.SetTop(newCoin, (rand.Next(-100, -100)));

            MainCanvas.Children.Add(newCoin);
        }







    }
}

