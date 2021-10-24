using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer TimerSnake;
        DispatcherTimer TimerRules;
        Game.GameMechanicks game;
        public void SetOnCanvas(double x,double y , UIElement element)
        {
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element,y);
        }
        public MainWindow()
        {
            InitializeComponent();
            CreateBattle();
            game = new Game.GameMechanicks(this);
            CreateTimers();            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            game.Move?.Invoke();
        }
        private async void CreateBattle()
        {
            for (int height = 0, k = 0; height < GameBattle.Height; height += 30 ,k+=2)
            {

                for(int width=0; width < GameBattle.Width; width += 30 , k++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 30;
                    rectangle.Height = 30;
                    rectangle.Fill = Brushes.Black;
                    rectangle.Fill = k % 2 == 0 ? Brushes.AliceBlue : Brushes.WhiteSmoke;
                    GameBattle.Children.Add(rectangle);
                    SetOnCanvas(width, height, rectangle);

                }
            }
        }
        private void CreateTimers()
        {
            TimerSnake = new DispatcherTimer();
            TimerRules = new DispatcherTimer();

            TimerSnake.Tick += Timer_Tick;
            TimerRules.Tick += TimerRules_Tick;

            TimerSnake.Interval = new TimeSpan(0, 0, 0, 0, 5);
            TimerRules.Interval = TimerSnake.Interval;

            TimerSnake.Start();
            TimerRules.Start();
           
        }

        private void TimerRules_Tick(object sender, EventArgs e)
        {
            game.CutBody();
            game.CreateFood();
            game.SnakeEat();
            game.Wall?.Invoke();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if (game.PlayersSnake.TurnUp)
                    {
                        game.PlayersSnake.TurnDown = false;
                        game.PlayersSnake.TurnLeft = true;
                        game.PlayersSnake.TurnRight = true;
                    }
                    break;
                case Key.S:
                    if (game.PlayersSnake.TurnDown)
                    {
                        game.PlayersSnake.TurnUp = false;
                        game.PlayersSnake.TurnLeft = true;
                        game.PlayersSnake.TurnRight = true;
                    }
                    break;
                case Key.A:
                    if (game.PlayersSnake.TurnLeft)
                    {
                        game.PlayersSnake.TurnRight = false;
                        game.PlayersSnake.TurnDown = true;
                        game.PlayersSnake.TurnUp = true;

                    }
                    break;
                case Key.D:
                    if (game.PlayersSnake.TurnRight)
                    {
                        game.PlayersSnake.TurnDown = true;
                        game.PlayersSnake.TurnLeft = false;
                        game.PlayersSnake.TurnUp = true;
                    }
                    break;
            }
        }
    }
}
