using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _2048Game.objects
{
    public abstract class Field
    {
        public abstract (int xPosition, int yPosition) GenerationSetItemPosition();
        public abstract int CheckingForMatches();

        public abstract void ClearTheField();
    }
}
