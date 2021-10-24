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

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для Food.xaml
    /// </summary>
    public partial class Food : UserControl
    {
        static public double Width = 20;
        static public double Height = 20;
        public static int count=0;
        public double FoodPointX { get; private set; }
        public double FoodPointY { get; private set; }
        static public List<Point> FoodsPoints=new List<Point>();
        
        public Food(double X,double Y)
        {
            InitializeComponent();
            FoodPointX = X;
            FoodPointY = Y;
            FoodsPoints.Add(new Point(FoodPointX, FoodPointY));
            count++;       
        }
     
    }
}
