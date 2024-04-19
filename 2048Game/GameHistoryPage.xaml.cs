using _2048Game.objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Messaging;

namespace _2048Game
{
    /// <summary>
    /// Логика взаимодействия для GameHistoryPage.xaml
    /// </summary>
    public partial class GameHistoryPage : UserControl
    {
        public int IdPlayer = 0;
        public GameHistoryPage()
        {
            InitializeComponent();
            FillHistoryGamesList();
        }
        public void OnClickAdd(Object obj, EventArgs arg) {
            if (FieldEnterName.Text != "")
            {
                var player = new Player(FieldEnterName.Text, DateTime.Now);
                string infoPlayer = " Player name: " + player.Name + " Score: " + player.GetScore() + " Data game: " + player.Data;
                ListHistoryGames.Items.Add(infoPlayer);
                using (var streamWriter = new StreamWriter("HistoryGamesList.txt", true))
                {
                    streamWriter.WriteLine(infoPlayer);
                }
                FieldEnterName.Text = "";
            }
        }
        public void FillHistoryGamesList() {
            string player = "";
            using (var reader = new StreamReader("HistoryGamesList.txt")) {
                
                while ((player = reader.ReadLine())!=null)
                {
                    ListHistoryGames.Items.Add(player);
                }
            }
        }

        public void OnClickBestScore(Object obj, EventArgs arg) {

            int size = File.ReadAllLines("HistoryGamesList.txt").Length;
            int[] arrScore = new int[size];
            Regex regex = new Regex(@"\s{1}\d+\s{1}");
            string str = "";
            using (var reader = new StreamReader("HistoryGamesList.txt"))
            {
                for (int i = 0; i < arrScore.Length; i++)
                {
                    Match match = regex.Match(reader.ReadLine());
                    str = match.Value;
                    str.Remove(0);
                    str.Remove(str.Length - 1);
                    arrScore[i] = int.Parse(str);
                }
            }
            string[] arrBestPlayers = new string[size];
            arrBestPlayers = SortBestPlayers(arrScore, size);
            for (int i = 0; i < arrBestPlayers.Length; i++)
            {
                ListHistoryGames.Items[i] = arrBestPlayers[i];
            }
        }
        public string [] SortBestPlayers(int[] arrScore, int size)
        {
            string[] arrPlayers = new string[size];
            string[] arrBestPlayers = new string[size];
            int coup = 0;
            string str = "";
            for (int i = 0; i < arrScore.Length; i++)
            {
                for (int j = arrScore.Length - 1; j > i; j--)
                {
                    if (arrScore[j - 1] > arrScore[j])
                    {
                        coup = arrScore[j];
                        arrScore[j] = arrScore[j - 1];
                        arrScore[j - 1] = coup;
                    }
                }
            }
            Array.Reverse(arrScore);
            using (var reader = new StreamReader("HistoryGamesList.txt")) {
                for (int i = 0; i < size; i++)
                {
                    arrPlayers[i] = reader.ReadLine(); 
                }
            }  
            Regex regex = new Regex(@"\s{1}\d+\s{1}");
            for (int i = 0; i < arrPlayers.Length; i++)
            {
                for (int j = 0; j < arrPlayers.Length; j++)
                {
                    Match match = regex.Match(arrPlayers[j]);
                    str = match.Value;
                    str.Remove(0);
                    str.Remove(str.Length - 1);
                    if (arrScore[i] ==int.Parse(str))
                    {
                        arrBestPlayers[i] = arrPlayers[j];
                    }
                }
            }
            return arrBestPlayers;
        }

        public void OnClickDefault(Object obj, EventArgs arg) {
            int size = File.ReadAllLines("HistoryGamesList.txt").Length;
            using (var reader = new StreamReader("HistoryGamesList.txt"))
            {
                for (int i = 0; i <size ; i++)
                {
                    ListHistoryGames.Items[i] = reader.ReadLine();
                }
            }
        }

    }
}
