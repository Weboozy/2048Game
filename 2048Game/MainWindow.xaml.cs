using _2048Game.objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace _2048Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Grid> ListTileGrid = new List<Grid>();
        public PlayingField playingField = new PlayingField();
        public int X;
        public int Y;
        public int AmountClickStart = 0;
        public int CurrentScore = 0;
        Tile tille = new Tile();
        public Random Random = new Random();
        public bool ClickRestart = false;
        
        public MainWindow()
        {
            InitializeComponent();
            FillListTileGird();
            PlayingField.Visibility = Visibility.Hidden;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        //Animation Drop Snow
        private void Timer_Tick(object sender, EventArgs e)
        {
            var newTile = new Tile();
            var snowFlake = new SnowFlake();
            Random random = new Random();
            int sizeSnowFlake = 0;
            for (int i = 0; i < snowFlake.arrEllieps.Length; i++)
            {
                sizeSnowFlake = random.Next(1, 5);
                CanvasField.Children.Add(snowFlake.arrEllieps[i]);
                Canvas.SetLeft(snowFlake.arrEllieps[i], random.Next(0, 407));
                snowFlake.arrEllieps[i].Height = sizeSnowFlake;
                snowFlake.arrEllieps[i].Width = sizeSnowFlake;
                newTile.AnimationDropSnow(snowFlake.arrEllieps[i]);
            }
        }
        // Da
        public void OnClickClosing(Object obj, EventArgs args)
        {
            playingField.WritinScoreInFile(0);
        }
        //Da
        public void OnClickGameHistory(Object obj, EventArgs args) {
            PlayingField.Visibility = Visibility.Hidden;
            CanvasField.Visibility = Visibility.Hidden;
            GameHistoryPage.Visibility = Visibility.Visible;
        }
        //Da
        public void OnClickStart(Object obj, EventArgs args)
        {
            AmountClickStart++;
            if (AmountClickStart < 2)
            {
                ClickRestart = false;
                PlayingField.Visibility = Visibility.Visible;
                SetPositionTile();
                SetPositionTile();
            }
            else
            {
                GameHistoryPage.Visibility = Visibility.Hidden;
                PlayingField.Visibility = Visibility.Visible; 
            }
        }
        public void FillListTileGird() {
            ListTileGrid.Add(Tile00);
            ListTileGrid.Add(Tile10);
            ListTileGrid.Add(Tile20);
            ListTileGrid.Add(Tile30);

            ListTileGrid.Add(Tile01);
            ListTileGrid.Add(Tile11);
            ListTileGrid.Add(Tile21);
            ListTileGrid.Add(Tile31);

            ListTileGrid.Add(Tile02);
            ListTileGrid.Add(Tile12);
            ListTileGrid.Add(Tile22);
            ListTileGrid.Add(Tile32);

            ListTileGrid.Add(Tile03);
            ListTileGrid.Add(Tile13);
            ListTileGrid.Add(Tile23);
            ListTileGrid.Add(Tile33);
        }
        // switch control
        public void KeyClick(Object obj, KeyEventArgs e) {
            string key = e.Key.ToString();
            if (PlayingField.Visibility != Visibility.Hidden&&ClickRestart == false)
            {
                switch (key)
                {
                    case "Up":
                        ChangePositionTileUp(playingField);
                        AdditionTileNumberUp(playingField);
                        ChangePositionTileUp(playingField);
                        UpdateTilePosition(playingField);
                        playingField.WritinScoreInFile(CurrentScore);
                        ScoreXML.Content = CurrentScore.ToString();
                        SetPositionTile();
                        GameOver(playingField.CheckingForMatches());
                        break;
                    case "Down":
                        ChangePositionTileDown(playingField);
                        AdditionTileNumberDown(playingField);
                        ChangePositionTileDown(playingField);
                        UpdateTilePosition(playingField);
                        playingField.WritinScoreInFile(CurrentScore);
                        ScoreXML.Content = CurrentScore.ToString();
                        SetPositionTile();
                        GameOver(playingField.CheckingForMatches());
                        break;
                    case "Left":
                        ChangePositionTileLeft(playingField);
                        AdditionTileNumberLeft(playingField);
                        ChangePositionTileLeft(playingField);
                        UpdateTilePosition(playingField);
                        playingField.WritinScoreInFile(CurrentScore);
                        ScoreXML.Content = CurrentScore.ToString();
                        SetPositionTile();
                        GameOver(playingField.CheckingForMatches());
                        break;
                    case "Right":
                        ChangePositionTileRight(playingField);
                        AdditionTileNumberRight(playingField);
                        ChangePositionTileRight(playingField);
                        UpdateTilePosition(playingField);
                        playingField.WritinScoreInFile(CurrentScore);
                        ScoreXML.Content = CurrentScore.ToString();
                        SetPositionTile();
                        GameOver(playingField.CheckingForMatches());

                        break;
                    default:
                        break;
                }
            }
        }
        // switch control
        public void SetPositionTile() {
            var orientation = playingField.GenerationSetItemPosition();
            string nameTileGrid = "";
            if (orientation.xPosition<50&& orientation.yPosition<50)
            {
                for (int i = 0; i < ListTileGrid.Count; i++)
                {
                    nameTileGrid = ListTileGrid[i].Name;
                    if (nameTileGrid[nameTileGrid.Length - 2].ToString() == orientation.xPosition.ToString() && nameTileGrid[nameTileGrid.Length - 1].ToString() == orientation.yPosition.ToString())
                    {
                        tille.AnimationColorSetPosition(ListTileGrid[i]);
                        var textBlock = new TextBlock();
                        textBlock = (TextBlock)ListTileGrid[i].Children[0];
                        textBlock.Text = "2";
                        textBlock.FontSize = 18;
                        textBlock.FontWeight = FontWeights.Bold;
                        textBlock.Foreground = Brushes.Black;
                    }
                }
            }
            
        }
        //control method tile
        public void ChangePositionTileUp(PlayingField playingField) {
            int[] arr = new int[4];
            int coup = 0;
            for (int j = 0; j < 4; j++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[arrC, j].Value;
                }
                for (int arrI = 0; arrI < arr.Length; arrI++)
                {
                    if (arr[arrI] == 0)
                    {
                        for (int arrJ = arrI + 1; arrJ < arr.Length; arrJ++)
                        {
                            if (arr[arrJ] > 0)
                            {
                                coup = arr[arrI];
                                arr[arrI] = arr[arrJ];
                                arr[arrJ] = coup;
                                break;
                            }
                        }
                    }
                }
                for (int arrA = 0; arrA < arr.Length; arrA++)
                {
                    playingField.Field[arrA, j].Value = arr[arrA];
                }
            }
        }
        public void ChangePositionTileDown(PlayingField playingField) {
            int[] arr = new int[4];
            int coup = 0;
            for (int j = 0; j < 4; j++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[arrC, j].Value;
                }
                Array.Reverse(arr);
                for (int arrI = 0; arrI < arr.Length; arrI++)
                {
                    if (arr[arrI] == 0)
                    {
                        for (int arrJ = arrI + 1; arrJ < arr.Length; arrJ++)
                        {
                            if (arr[arrJ] > 0)
                            {
                                coup = arr[arrI];
                                arr[arrI] = arr[arrJ];
                                arr[arrJ] = coup;
                                break;
                            }
                        }
                    }
                }
                for (int arrA = 3; arrA >= 0; arrA--)
                {
                    playingField.Field[arrA, j].Value = arr[3 - arrA];
                }
            }
        }
        public void ChangePositionTileRight(PlayingField playingField) {
            int[] arr = new int[4];
            int coup = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[i, arrC].Value;
                }
                Array.Reverse(arr);
                for (int arrI = 0; arrI < arr.Length; arrI++)
                {
                    if (arr[arrI] == 0)
                    {
                        for (int arrJ = arrI + 1; arrJ < arr.Length; arrJ++)
                        {
                            if (arr[arrJ] > 0)
                            {
                                coup = arr[arrI];
                                arr[arrI] = arr[arrJ];
                                arr[arrJ] = coup;
                                break;
                            }
                        }
                    }
                }
                for (int arrA = 3; arrA >= 0; arrA--)
                {
                    playingField.Field[i, arrA].Value = arr[3 - arrA];
                }
            }
        }
        public void ChangePositionTileLeft(PlayingField playingField){
            int[] arr = new int[4];
            int coup = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[i, arrC].Value;
                }
                for (int arrI = 0; arrI < arr.Length; arrI++)
                {
                    if (arr[arrI] == 0)
                    {
                        for (int arrJ = arrI + 1; arrJ < arr.Length; arrJ++)
                        {
                            if (arr[arrJ] > 0)
                            {
                                coup = arr[arrI];
                                arr[arrI] = arr[arrJ];
                                arr[arrJ] = coup;
                                break;
                            }
                        }
                    }
                }
                for (int arrA = 0; arrA < arr.Length; arrA++)
                {
                    playingField.Field[i, arrA].Value = arr[arrA];
                }
            }
        }
        //control method tile
        public void UpdateTilePosition(PlayingField playingField) {
            string nameTileGrid = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (playingField.Field[i,j].Value>0)
                    {
                        foreach (var tile in ListTileGrid)
                        {
                            nameTileGrid = tile.Name;
                            if (nameTileGrid[nameTileGrid.Length-2].ToString() == i.ToString()&& nameTileGrid[nameTileGrid.Length-1].ToString() ==j.ToString())
                            {
                                tille.AnimationColorAddition(tile,Random.Next(0,5));
                                var textBlock = new TextBlock();
                                textBlock = (TextBlock)tile.Children[0];
                                textBlock.Text = playingField.Field[i,j].Value.ToString();
                                textBlock.FontSize = 18;
                                textBlock.FontWeight = FontWeights.Bold;
                                textBlock.Foreground = Brushes.Black;
                            }
                        }
                    }
                    if (playingField.Field[i,j].Value == 0)
                    {
                        foreach (var tile in ListTileGrid) {
                            nameTileGrid = tile.Name;
                            if (nameTileGrid[nameTileGrid.Length - 2].ToString() == i.ToString() && nameTileGrid[nameTileGrid.Length - 1].ToString() == j.ToString())
                            {
                                tile.Background = new SolidColorBrush(Color.FromRgb(36,35,35));
                                var textBlock = new TextBlock();
                                textBlock = (TextBlock)tile.Children[0];
                                textBlock.Text = "";
                            }
                        }
                    }
                }
            }
        }

        // addition in array Tile
        public void AdditionTileNumberUp(PlayingField playingField) {
            int[] arr = new int[4];
            for (int j = 0; j < 4; j++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[arrC, j].Value;
                }
                arr = AdditionTileNumber(arr);
                for (int arrA = 0; arrA < arr.Length; arrA++)
                {
                    playingField.Field[arrA, j].Value = arr[arrA];
                }
            }
        }
        public void AdditionTileNumberDown(PlayingField playingField) {
            int[] arr = new int[4];
            for (int j = 0; j < 4; j++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[arrC, j].Value;
                }
                Array.Reverse(arr);
                arr = AdditionTileNumber(arr);
                for (int arrA = 3; arrA >= 0; arrA--)
                {
                    playingField.Field[arrA, j].Value = arr[3 - arrA];
                }
            }
        }
        public void AdditionTileNumberLeft(PlayingField playingField) {
            int[] arr = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[i, arrC].Value;
                }
                arr = AdditionTileNumber(arr);
                for (int arrA = 0; arrA < arr.Length; arrA++)
                {
                    playingField.Field[i, arrA].Value = arr[arrA];
                }
            }
        }
        public void AdditionTileNumberRight(PlayingField palyingField) {
            int[] arr = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int arrC = 0; arrC < 4; arrC++)
                {
                    arr[arrC] = playingField.Field[i, arrC].Value;
                }
                Array.Reverse(arr);
                arr = AdditionTileNumber(arr);
                for (int arrA = 3; arrA >= 0; arrA--)
                {
                    playingField.Field[i, arrA].Value = arr[3 - arrA];
                }
            }
        }
        //addition in array Tile

        //addition Tile
        public int [] AdditionTileNumber(int[] arr) {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    if (i == arr.Length - 1)
                    {
                        return arr;
                    }
                    if (i != arr.Length)
                    {
                        if (arr[i] == arr[i + 1])
                        {
                            arr[i] += arr[i];
                            CurrentScore += arr[i];
                            arr[i + 1] = 0;
                        }
                    }

                }
            }
            return arr;
        }
        //addition Tile

        public void GameOver(int value) {
            if (value ==0)
            {
                MessageBox.Show("Game Over");
            }
        }

        public void OnClickRestart(Object obj, EventArgs arg) {
            playingField.ClearTheField();
            playingField.WritinScoreInFile(0);
            AmountClickStart = 0;
            UpdateTilePosition(playingField);
            ClickRestart = true;
            CurrentScore = 0;
            ScoreXML.Content = CurrentScore;
        }

    }
}
