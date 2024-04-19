using _2048Game.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace _2048Game
{
    public class Tile:IAnimation
    {
        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        } 
        public int[] TileNumbers = new[] { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };
        private int x = 0;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y = 0;
        public int Y {
            get { return y; }
            set { y = value; }
        }

        public Color[] ArrColors = new Color[5] { Colors.Violet, Colors.Peru, Colors.Purple, Colors.DeepPink, Colors.YellowGreen };
        public void AnimationColorSetPosition(Grid tile) {
            tile.Background = new SolidColorBrush(Color.FromRgb(36, 35, 35));
            var animation = new ColorAnimation();
            animation.To = Colors.BlueViolet;
            animation.Duration = TimeSpan.FromSeconds(4);
            tile.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }
        public void AnimationColorAddition(Grid tile,int random) {
            tile.Background = new SolidColorBrush(Color.FromRgb(36, 35, 35));
            var animation = new ColorAnimation();
            animation.To = ArrColors[random];
            animation.Duration = TimeSpan.FromSeconds(2);
            tile.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        public void AnimationDropSnow(Ellipse ellipse) {
            TranslateTransform transform = new TranslateTransform();
            ellipse.RenderTransform = transform;
            DoubleAnimation animationY = new DoubleAnimation(0, 410, TimeSpan.FromSeconds(6));
            transform.BeginAnimation(TranslateTransform.YProperty, animationY);
        }
    }
}
