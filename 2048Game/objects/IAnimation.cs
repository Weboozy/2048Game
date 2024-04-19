using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace _2048Game.objects
{
    public interface IAnimation
    {
        void AnimationColorSetPosition(Grid tile);
        void AnimationColorAddition(Grid tile, int random);

        void AnimationDropSnow(Ellipse elliepse);
    }
}
