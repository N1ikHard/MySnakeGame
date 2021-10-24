using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.Snake_s_classes
{
    class Snake
    {
        public double SnakePointX { get; set; }
        public double SnakePointY { get; set; }

        public Ellipse Head;
        public List<Body> Bodies;
        public bool TurnUp { get; set; }
        public bool TurnDown { get;set; }
        public bool TurnLeft { get;set; }
        public bool TurnRight { get;set;}
        private Ellipse CreateHead()
        {
            return new Ellipse { Width = 20, Height = 20, Fill = new SolidColorBrush(Colors.Black) };
        }
        public void AddBody(double x , double y)
        {
            Body PartOfBody = new Body(x,y);
            Bodies.Add(PartOfBody);
        }
        public Snake()
        {
            Head = CreateHead();
            Bodies = new List<Body>();
            Bodies.Add(new Body(SnakePointX+Head.Width/2,  SnakePointY+Head.Height/2)); //Раньше прибавлял +Head.Width u Head.Height
            TurnUp = true;
            TurnDown = true;
            TurnLeft = true;
            TurnRight = true;
        }

    }
}
