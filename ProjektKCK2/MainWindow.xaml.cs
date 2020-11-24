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
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        Random rand = new Random();

        int Window_Width = 1272;
        int Window_Height = 720;
        int speed = 10;
        int SteeringSpeed = 20;
        int carNum;
        int powerModeCounter = 200;
        int NewCar = 800;
        List<Rectangle> itemRemover = new List<Rectangle>();

        int CoinTemp = 30;
        int score;
        //Rect Colission;
        bool gameOver, moveLeft, moveRight, powerMode;

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
            // increase the score by .5 each tick of the timer

            //starCounter -= 1; // reduce 1 from the star counter each tick

            scoreText.Content = "Coins=" + score.ToString(); // this line will show the seconds passed in decimal numbers in the score text label

            playerHitBox = new Rect(Canvas.GetLeft(e90_car), Canvas.GetTop(e90_car), e90_car.Width, e90_car.Height); // assign the player hit box to the player

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



            // if the star counter integer goes below 1 then we run the make star function and also generate a random number inside of the star counter integer


            // below is the main game loop, inside of this loop we will go through all of the rectangles available in this game
            foreach (var x in MainCanvas.Children.OfType<Rectangle>())
            {
                // first we search through all of the rectangles in this game

                // then we check if any of the rectangles has a tag called road marks
                if ((string)x.Tag == "Mark1")
                {
                    // if we find any of the rectangles with the road marks tag on it then 

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed); // move it down using the speed variable

                    // if the road marks goes below the screen then move it back up top of the screen
                    if (Canvas.GetTop(x) > 800)
                    {
                        Canvas.SetTop(x, -152);
                    }

                } // end of the road marks if statement

                // if we find a rectangle with the car tag on it
                if ((string)x.Tag == "Car")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed); // move the rectangle down using the speed variable

                    Cars_Actions(x, false);

                } // end of car if statement

                if ((string)x.Tag == "Car2")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + speed / 2); // move the rectangle down using the speed variable
                    Cars_Actions(x, true);


                }



                // if we find a rectangle with the star tag on it
                if ((string)x.Tag == "coin")
                {
                    // move it down the screen 5 pixels at a time
                    Canvas.SetTop(x, Canvas.GetTop(x) + 5);

                    // create a new rect with for the star and pass in the star X values inside of it
                    Rect coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // if the player and the star collide then
                    if (playerHitBox.IntersectsWith(coinHitBox))
                    {
                        // add the star to the item remover list
                        itemRemover.Add(x);
                        score++;
                        // set power mode to true
                        powerMode = true;

                        // set power mode counter to 200
                        powerModeCounter = 200;

                    }

                    // if the star goes beyon 400 pixels then add it to the item remover list
                    if (Canvas.GetTop(x) > Window_Height)
                    {
                        itemRemover.Add(x);

                    }

                } // end of start if statement
            } // end of for each loop

            // if the power mode is true
            if (powerMode == true)
            {
                powerModeCounter -= 1; // reduce 1 from the power mode counter 
                // run the power up function
                //PowerUp();
                // if the power mode counter goes below 1 
                if (powerModeCounter < 1)
                {
                    // set power mode to false
                    powerMode = false;
                }
            }
            else
            {
                // if the mode is false then change the player car back to default and also set the background to gray
                //playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/playerImage.png"));
                MainCanvas.Background = Brushes.Gray;
            }

            // each item we find inside of the item remove we will remove it from the canvas
            foreach (Rectangle y in itemRemover)
            {
                MainCanvas.Children.Remove(y);
            }

            // below are the score and speed configurations for the game
            // as you progress in the game you will score higher and traffic speed will go up

            if (score >= 10 && score < 20)
            {
                speed = 12;
            }

            if (score >= 20 && score < 30)
            {
                speed = 14;
            }
            if (score >= 30 && score < 40)
            {
                speed = 16;
            }
            if (score >= 40 && score < 50)
            {
                speed = 18;
            }
            if (score >= 50 && score < 80)
            {
                speed = 22;
            }


        }

        private void Cars_Actions(Rectangle x, bool direction)
        {
            // if the car has left the scene then run then run the change cars function with the current x rectangle inside of it
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

            // create a new rect called car hit box and assign it to the x which is the cars rectangle
            Rect carHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


            if (playerHitBox.IntersectsWith(carHitBox))
            {
                // if the power is OFF and car and the player collide then

                gameTimer.Stop(); // stop the game timer
                EndLabel.Content = "GAMEOVER!\n" + "Press R to Restart Game\n" + "Press Esc to Back Menu"; // add this text to the existing text on the label
                gameOver = true; // set game over boolean to true
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
            powerMode = false;
            score = 0;
            scoreText.Content = "Coins = 0";

            coinImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/coin.png"));


            //dodany z naszej strony
            SR_Cars("Car");
            SR_Cars("Car2");
        }

        private void SR_Cars(string car)
        {
            foreach (var x in MainCanvas.Children.OfType<Rectangle>())
            {
                // if we find any rectangle with the car tag on it then we will
                if ((string)x.Tag == car)
                {
                    // set a random location to their top and left position
                    Canvas.SetTop(x, (rand.Next(100, 400) * -1));
                    Canvas.SetLeft(x, rand.Next(0, 500));
                    // run the change cars function
                    if (car == "Car")
                    {
                        ChangeCars(x, false);
                    }


                    else
                    {
                        ChangeCars(x, true);
                    }



                }

                // if we find a star in the beginning of the game then we will add it to the item remove list
                itemRemover.Clear();

            }
        }

        private void ChangeCars(Rectangle car, bool OurDirection)
        {

            // we want the game to change the traffic car images as they leave the scene and come back to it again

            carNum = rand.Next(1, 3); // to start lets generate a random number between 1 and 6

            ImageBrush carImage = new ImageBrush(); // create a new image brush for the car image 
            Rect NewCarHitBox = new Rect(Canvas.GetLeft(car), Canvas.GetTop(car), car.Width, car.Height);

            // the switch statement below will see what number have generated for the car num integer and 
            // based on that number it will assign a different image to the car rectangle
            //tyly aut
            if (OurDirection == true)
            {
                int NewHeight = rand.Next(100, 400) * -1;
                int NewWidth = (rand.Next(0, 50) % 3) * 205 + 664;
                switch (carNum)
                {
                    case 1:
                        carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/mk4_rear.png"));
                        break;
                    case 2:
                        carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/passat_rear.png"));
                        break;

                }

                car.Fill = carImage; // assign the chosen car image to the car rectangle

                // set a random top and left position for the traffic car
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


                switch (carNum)
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

            // in this case we will listen for the enter key aswell but for this to execute we will need the game over boolean to be true
            if (e.Key == Key.R && gameOver == true)
            {
                // if both of these conditions are true then we will run the start game function
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

