using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace _2048Game.objects
{
    public class PlayingField:Field
    {
        private Random randomPositon = new Random();
        public Tile[,] Field = new Tile[4, 4]
        {
         {new Tile(),new Tile(),new Tile(),new Tile()},
         {new Tile(),new Tile(),new Tile(),new Tile()},
         {new Tile(),new Tile(),new Tile(),new Tile()},
         {new Tile(),new Tile(),new Tile(),new Tile()}
        };
        public override (int xPosition, int yPosition) GenerationSetItemPosition() {
            int countFreeTile = 0;
            //cortage
            var orientation = (xPosition: 0, yPosition: 0);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Field[i,j].Value == 0)
                    {
                        countFreeTile++;
                    }
                }
            }
            if (countFreeTile > 0)
            {
                int x = randomPositon.Next(0, 4);
                int y = randomPositon.Next(0, 4);
                while (true)
                {
                    if (Field[x, y].Value == 0)
                    {
                        Field[x, y].Value = new Tile().TileNumbers[0];
                        Field[x, y].X = x;
                        Field[x, y].Y = y;
                        orientation = (xPosition: x, yPosition: y);
                        break;
                    }
                    else if (Field[x, y].Value != 0)
                    {
                        x = randomPositon.Next(0, 4);
                        y = randomPositon.Next(0, 4);
                        continue;
                    }
                }
                return orientation;
            }
            else {
                //value 50(no space in arr Field)
                return orientation = (xPosition: 50, yPosition: 50);
            }
            
        }
        public override int CheckingForMatches() {
            int[] arr = new int[4];
            int coincidence = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    arr[j] = Field[i, j].Value;
                }
                for (int arrI = 0; arrI < arr.Length - 1; arrI++)
                {
                    if (arr[arrI] == 0)
                    {
                        coincidence ++;
                        break;
                    }
                    if (arr[arrI] == arr[arrI + 1] && arr[arrI] != 0 && arr[arrI + 1] != 0) {
                        coincidence++;
                        break;
                    }
                }
            }
            if (coincidence == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        arr[j] = Field[j, i].Value;
                    }
                    for (int arrI = 0; arrI < arr.Length - 1; arrI++)
                    {
                        if (arr[arrI] == 0)
                        {
                            coincidence ++;
                            break;
                        }
                        if (arr[arrI] == arr[arrI + 1] && arr[arrI] != 0 && arr[arrI + 1] != 0) {
                            coincidence++;
                            break;
                        }
                    }
                }
            }
            if (coincidence == 0) return 0;
            else return 1;

        }

        public void WritinScoreInFile(int score) {
            using (var streamWriter = new StreamWriter("CurrentScore.txt",false) ) {
                streamWriter.Write(score);
            }
        }
        public override void ClearTheField() {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Field[i, j].Value = 0;
                }
            }
        }
    }
}
