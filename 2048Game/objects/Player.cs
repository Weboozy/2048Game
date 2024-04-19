using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2048Game.objects
{
    public class Player
    {
        private string name;
        public string Name
        {
            get { return name; } 
        }
        private string data;
        public string Data
        {
            get { return data; }
        }
        public Player(string Name, DateTime Data) {
            name = Name;
            data = Data.ToString();
        }
        public int GetScore() {
            int newScore = 0;
            using (var streamReader = new StreamReader("CurrentScore.txt")) { 
                newScore = int.Parse(streamReader.ReadLine());
            }
            return newScore;
        }
    }
}
