using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2048Game
{
    public class SnowFlake
    {
        private Random random = new Random();
        public  Ellipse[] arrEllieps = new Ellipse[] {
                 new Ellipse() { Height = 5, Width = 5, Fill = new SolidColorBrush(Colors.White)}
                ,new Ellipse() { Height = 5, Width = 5, Fill = new SolidColorBrush(Colors.White)}
                ,new Ellipse() { Height = 5, Width = 5, Fill = new SolidColorBrush(Colors.White)}
                ,new Ellipse() { Height = 5, Width = 5, Fill = new SolidColorBrush(Colors.White)}
                ,new Ellipse() { Height = 5, Width = 5, Fill = new SolidColorBrush(Colors.White)}
        };
    }
}
