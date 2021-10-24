using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public delegate void DelegateSnakeMove();
    public delegate void DelegateWall();
    class GameMechanicks
    {  
        
        public Snake_s_classes.Snake PlayersSnake;
        List<Food> foods;
        double MovePointX;
        double MovePointY;
        Random rnd;
        Snake.MainWindow mainWindow;
        bool UseWall;
        public DelegateSnakeMove Move;
        public DelegateWall Wall;
        public GameMechanicks(Snake.MainWindow mainWindow)
        {
           
            this.mainWindow = mainWindow;
            InstallSnakePosition();
            rnd = new Random();
            foods = new List<Food>();
            UseWall = true ;
            InstallGameMechanicks();
       
            
           
        }
        private void InstallSnakePosition()
        {           
                PlayersSnake = new Snake_s_classes.Snake();

                mainWindow.GameBattle.Children.Add(PlayersSnake.Head);

                PlayersSnake.SnakePointX = (mainWindow.GameBattle.Width / 2)+PlayersSnake.Head.Width/2;
                PlayersSnake.SnakePointY = (mainWindow.GameBattle.Height / 2)+PlayersSnake.Head.Height/2;

                mainWindow.SetOnCanvas(PlayersSnake.SnakePointX,
                    PlayersSnake.SnakePointY,
                    PlayersSnake.Head);
        }
        private void InstallGameMechanicks()
        {
            Move += SnakeMove;
            Wall += UseWall ? HitAboutWall : GoThroughWall;
        }

        private async void SnakeMove()
        {
            double TakeCentr = PlayersSnake.Head.Width / 8;
               
                if (!PlayersSnake.TurnDown)
                {            
                    MovePointY = -1;
                    MovePointX = 0;
                    goto label1;
                }
                if (!PlayersSnake.TurnUp)
                {
                    MovePointY = 1;
                    MovePointX = 0;
                    goto label1;
                }
                if (!PlayersSnake.TurnLeft)
                {
                    MovePointX = 1;
                    MovePointY = 0;
                    goto label1;
                }
                if (!PlayersSnake.TurnRight)
                {
                    MovePointX = -1;
                    MovePointY = 0;
                    goto label1;
                }

                 label1:
                PlayersSnake.SnakePointX += MovePointX;
                PlayersSnake.SnakePointY += MovePointY;

                PlayersSnake.Bodies[0].PointX = PlayersSnake.SnakePointX + TakeCentr;
                PlayersSnake.Bodies[0].PointY = PlayersSnake.SnakePointY + TakeCentr;
         

                mainWindow.SetOnCanvas(PlayersSnake.SnakePointX, PlayersSnake.SnakePointY, PlayersSnake.Head);

                for (int i = PlayersSnake.Bodies.Count() - 1, k = PlayersSnake.Bodies.Count() - 2; i > 0; i--, k--)
                {
                    PlayersSnake.Bodies[i].PointX = PlayersSnake.Bodies[k].PointX;
                    PlayersSnake.Bodies[i].PointY = PlayersSnake.Bodies[k].PointY;
                }

                PlayersSnake.Bodies[0].PointX = PlayersSnake.SnakePointX;
                PlayersSnake.Bodies[0].PointY = PlayersSnake.SnakePointY;

                foreach (var item in PlayersSnake.Bodies)
                    mainWindow.SetOnCanvas(item.PointX, item.PointY, item);

           
            }
        public async void SnakeEat()
        {
            Food[] array = foods.ToArray();
            foreach(Food f in array)
            {
                if (Math.Abs(f.FoodPointX - PlayersSnake.SnakePointX) < PlayersSnake.Head.Width &
                    Math.Abs(f.FoodPointY - PlayersSnake.SnakePointY) < PlayersSnake.Head.Height)
                {
                    foods.Remove(f);
                    mainWindow.GameBattle.Children.Remove(f);
                    Food.count--;
                    SnakeGrow();
                    break; //
                }          
            } 
        }
        public async void CreateFood()
        {
            if (Food.count > 15)
                return;
            else
            {
                double PointX = rnd.Next(0 + (int)Food.Width, (int)(mainWindow.Width - Food.Width));
                double PointY = rnd.Next(0 + (int)Food.Height, (int)(mainWindow.Height - Food.Height));
                Food food = new Food(PointX, PointY );
                mainWindow.GameBattle.Children.Add(food);
                mainWindow.SetOnCanvas(PointX, PointY, food);
                foods.Add(food);
            }
        }
        public async void SnakeGrow()
        {
            double X = PlayersSnake.Bodies.Last().PointX;
            double Y = PlayersSnake.Bodies.Last().PointY;

            if (!PlayersSnake.TurnUp)
               Y += 3;
            if (!PlayersSnake.TurnLeft)
               X += 3;

            PlayersSnake.AddBody(X+PlayersSnake.Bodies[0].Width/2,Y+PlayersSnake.Bodies[0].Height/2);
            mainWindow.SetOnCanvas(X,Y,PlayersSnake.Bodies.Last());
            mainWindow.GameBattle.Children.Add(PlayersSnake.Bodies.Last());

        }

        public async void HitAboutWall()
        {      

                if (PlayersSnake.SnakePointX < 0 || PlayersSnake.SnakePointX > mainWindow.GameBattle.Width - PlayersSnake.Head.Width / 2 ||
                    PlayersSnake.SnakePointY < 0 || PlayersSnake.SnakePointY > mainWindow.Height - PlayersSnake.Head.Height)
                    Move -= SnakeMove;
                                
        }
        private  void GoThroughWall()
        {   
                if (PlayersSnake.SnakePointX < 0)
                {
                    PlayersSnake.SnakePointX = mainWindow.Width;
                    goto label2;
                }
                if (PlayersSnake.SnakePointX > mainWindow.Width)
                {
                    PlayersSnake.SnakePointX = 0;
                    goto label2;
                }
                if (PlayersSnake.SnakePointY < 0)
                {
                    PlayersSnake.SnakePointY = mainWindow.Height;
                    goto label2;
                }
                if (PlayersSnake.SnakePointY > mainWindow.Height)
                {
                    PlayersSnake.SnakePointY = 0;
                    goto label2;
                }
            label2: return;
        
        }
        public async void CutBody()
        {
            Game.Snake_s_classes.Body[] bodies =  PlayersSnake.Bodies.ToArray();
            if (bodies.Length > 8)
                for (int i = 8; i < bodies.Count(); i++)
                {
                    if (Math.Abs(PlayersSnake.Bodies[i].PointX - PlayersSnake.SnakePointX) < 1 &
                        Math.Abs(PlayersSnake.Bodies[i].PointY - PlayersSnake.SnakePointY) < 1)
                    {
                        for (int k = i; k < bodies.Length; k++)
                        {
                            mainWindow.GameBattle.Children.Remove(bodies[k]);
                            PlayersSnake.Bodies.Remove(bodies[k]);
                        }
                        break;
                    }
                }
        }
    }
}
