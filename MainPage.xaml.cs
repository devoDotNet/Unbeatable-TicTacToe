using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        #region
        public static bool Draw { get; set; } = false;
        public static bool MachineWins { get; set; } = false;
        public static int PlayerMoveCount { get; set; } = 0;
        public bool PlayerIsFirst { get; set; } = true;

        Random random = new Random();
        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        void OnSizeChanged(object sender, EventArgs e) //controls the positioning of the game grid
        {
            oneButton.HeightRequest = oneButton.Width;
            oneButton.WidthRequest = oneButton.Height;

            twoButton.HeightRequest = twoButton.Width;
            twoButton.WidthRequest = twoButton.Height;

            threeButton.HeightRequest = threeButton.Width;
            threeButton.WidthRequest = threeButton.Height;
        } 

        private void OnButton_Clicked(object sender, EventArgs e)
        {
            PlayerMoveCount++;
            
            (sender as Button).IsEnabled = false;
            (sender as Button).BackgroundColor = Color.Green;
            (sender as Button).Text = "X";
            
            if (!CheckForWinMove())
            {
                if (!CheckForBlockMove())
                {
                    if (!CheckForSmartMove())
                    {
                        FiatMove();
                    }
                }
            }
            
            CheckMachineWins();
            CheckDraw();
            EndGameIf();
        }

        /*private void MachineActsSecond()
        {
            //[checks for tricks and avoids them]
            
            //if players first move is center than machine chooses corner
            
            if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.Green)
            {
                int randomInt = random.Next(4);

                switch (randomInt)
                {
                    case 0:
                        oneButton.BackgroundColor = Color.IndianRed;
                        oneButton.IsEnabled = false;
                        oneButton.Text = "0";
                        break;
                    case 1:
                        threeButton.BackgroundColor = Color.IndianRed;
                        threeButton.IsEnabled = false;
                        threeButton.Text = "0";
                        break;
                    case 2:
                        sevenButton.BackgroundColor = Color.IndianRed;
                        sevenButton.IsEnabled = false;
                        sevenButton.Text = "0";
                        break;
                    case 3:
                        nineButton.BackgroundColor = Color.IndianRed;
                        nineButton.IsEnabled = false;
                        nineButton.Text = "0";
                        break;
                }
            }
            //if players first move is corner or middle, than machine chooses center
            else if ((PlayerMoveCount == 1) && (oneButton.BackgroundColor == Color.Green || threeButton.BackgroundColor == Color.Green || sevenButton.BackgroundColor == Color.Green || nineButton.BackgroundColor == Color.Green || 
                    twoButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green || sixButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if ((PlayerMoveCount == 2 && fiveButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green) ||
                     (PlayerMoveCount == 2 && fiveButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green))
            {
                int randomInt = random.Next(2);

                switch (randomInt)
                {
                    case 0:
                        threeButton.BackgroundColor = Color.IndianRed;
                        threeButton.IsEnabled = false;
                        threeButton.Text = "0";
                        break;
                    case 1:
                        sevenButton.BackgroundColor = Color.IndianRed;
                        sevenButton.IsEnabled = false;
                        sevenButton.Text = "0";
                        break;
                }
            }
            else if ((PlayerMoveCount == 2 && fiveButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green) ||
                     (PlayerMoveCount == 2 && fiveButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green))
            {
                int randomInt = random.Next(2);

                switch (randomInt)
                {
                    case 0:
                        oneButton.BackgroundColor = Color.IndianRed;
                        oneButton.IsEnabled = false;
                        oneButton.Text = "0";
                        break;
                    case 1:
                        nineButton.BackgroundColor = Color.IndianRed;
                        nineButton.IsEnabled = false;
                        nineButton.Text = "0";
                        break;
                }
            }
            //trick: if player plays corner and machine plays center and player plays opposite corner, then machine chooses random middle space.
            else if ((PlayerMoveCount == 2) && ((oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green)))
            {
                int randomInt = random.Next(4);

                switch (randomInt)
                {
                    case 0:
                        twoButton.BackgroundColor = Color.IndianRed;
                        twoButton.IsEnabled = false;
                        twoButton.Text = "0";
                        break;
                    case 1:
                        fourButton.BackgroundColor = Color.IndianRed;
                        fourButton.IsEnabled = false;
                        fourButton.Text = "0";
                        break;
                    case 2:
                        sixButton.BackgroundColor = Color.IndianRed;
                        sixButton.IsEnabled = false;
                        sixButton.Text = "0";
                        break;
                    case 3:
                        eightButton.BackgroundColor = Color.IndianRed;
                        eightButton.IsEnabled = false;
                        eightButton.Text = "0";
                        break;
                }
            }

            //[checks for win opportunity and takes it] 

            //good
            else if ((twoButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed ||
               fourButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
               fiveButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (oneButton.BackgroundColor == Color.Default))
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed ||
                     fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) && (twoButton.BackgroundColor == Color.Default))
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.IndianRed ||
                     fiveButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                     sixButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (threeButton.BackgroundColor == Color.Default))
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                     fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) && (fourButton.BackgroundColor == Color.Default))
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed ||
                     twoButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed ||
                     threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                     fourButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) && (fiveButton.BackgroundColor == Color.Default))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if ((threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed ||
                     fourButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed) && (sixButton.BackgroundColor == Color.Default))
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.IndianRed ||
                     threeButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed ||
                     eightButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (sevenButton.BackgroundColor == Color.Default))
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if ((twoButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed ||
                     sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (eightButton.BackgroundColor == Color.Default))
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed ||
                     threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed ||
                     sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) && (nineButton.BackgroundColor == Color.Default))
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }

            //[checks for compulsory move and takes it]

            //good
            else if ((twoButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.Green ||
                    fourButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                    fiveButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (oneButton.BackgroundColor == Color.Default)) 
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.Green ||
                        fiveButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green) && (twoButton.BackgroundColor == Color.Default))
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.Green ||
                        fiveButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                        sixButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (threeButton.BackgroundColor == Color.Default))
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                        fiveButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green) && (fourButton.BackgroundColor == Color.Default))
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green ||
                        twoButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green ||
                        threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                        fourButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green) && (fiveButton.BackgroundColor == Color.Default))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if ((threeButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green ||
                        fourButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green) && (sixButton.BackgroundColor == Color.Default))
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.Green ||
                        threeButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green ||
                        eightButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (sevenButton.BackgroundColor == Color.Default))
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if ((twoButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green ||
                        sevenButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (eightButton.BackgroundColor == Color.Default))
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green ||
                threeButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green ||
                sevenButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green) && (nineButton.BackgroundColor == Color.Default))
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }

            //[If it does not matter it takes a move in order from one to nine] 
            //need to randomize this
            else if (oneButton.BackgroundColor == Color.Default)
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if (twoButton.BackgroundColor == Color.Default)
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if (threeButton.BackgroundColor == Color.Default)
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if (fourButton.BackgroundColor == Color.Default)
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if (fiveButton.BackgroundColor == Color.Default)
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if (sixButton.BackgroundColor == Color.Default)
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if (sevenButton.BackgroundColor == Color.Default)
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if (eightButton.BackgroundColor == Color.Default)
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }
            else if (nineButton.BackgroundColor == Color.Default)
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }
        }*/ //old version

        /*private void MachineActsFirst()
        {
            //[when the reset button is pressed the machine chooses center or chooses a random corner]
            //to keep the tricks played at an average equal rate demands balancing the odds. this demands using a "coin flip" to decide center or corner, then if corner, to use a random to choose which corner.

            if (PlayerMoveCount == 0)
            {
                int randomInt = random.Next(2);

                if (randomInt == 0)
                {
                    fiveButton.BackgroundColor = Color.IndianRed;
                    fiveButton.IsEnabled = false;
                    fiveButton.Text = "0";
                }
                else if (randomInt == 1)
                {
                    int anotherRandomInt = random.Next(4);

                    switch (anotherRandomInt)
                    {
                        case 0:
                            oneButton.BackgroundColor = Color.IndianRed;
                            oneButton.IsEnabled = false;
                            oneButton.Text = "0";
                            break;
                        case 1:
                            threeButton.BackgroundColor = Color.IndianRed;
                            threeButton.IsEnabled = false;
                            threeButton.Text = "0";
                            break;
                        case 2:
                            sevenButton.BackgroundColor = Color.IndianRed;
                            sevenButton.IsEnabled = false;
                            sevenButton.Text = "0";
                            break;
                        case 3:
                            nineButton.BackgroundColor = Color.IndianRed;
                            nineButton.IsEnabled = false;
                            nineButton.Text = "0";
                            break;
                    }
                }
            }

            //[checks for tricks to perform]

            //center trick
            
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green)
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green)
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green)
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green)
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }

            //corner trick

            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed)
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed)
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed)
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if (PlayerMoveCount == 1 && fiveButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed)
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }

            //checks for win move.. make own method

            else if ((twoButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed ||
                    fourButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                    fiveButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (oneButton.BackgroundColor == Color.Default))
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed ||
                     fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) && (twoButton.BackgroundColor == Color.Default))
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.IndianRed ||
                     fiveButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                     sixButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (threeButton.BackgroundColor == Color.Default))
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                     fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) && (fourButton.BackgroundColor == Color.Default))
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed ||
                     twoButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed ||
                     threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed ||
                     fourButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) && (fiveButton.BackgroundColor == Color.Default))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if ((threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed ||
                     fourButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed) && (sixButton.BackgroundColor == Color.Default))
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.IndianRed ||
                     threeButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed ||
                     eightButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (sevenButton.BackgroundColor == Color.Default))
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if ((twoButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed ||
                     sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (eightButton.BackgroundColor == Color.Default))
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed ||
                     threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed ||
                     sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) && (nineButton.BackgroundColor == Color.Default))
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }
            
            //checks for compulsory move. should be own method

            else if ((twoButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.Green ||
                    fourButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                    fiveButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (oneButton.BackgroundColor == Color.Default))
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.Green ||
                        fiveButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green) && (twoButton.BackgroundColor == Color.Default))
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.Green ||
                        fiveButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                        sixButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (threeButton.BackgroundColor == Color.Default))
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                        fiveButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green) && (fourButton.BackgroundColor == Color.Default))
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green ||
                        twoButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green ||
                        threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green ||
                        fourButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green) && (fiveButton.BackgroundColor == Color.Default))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if ((threeButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green ||
                        fourButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green) && (sixButton.BackgroundColor == Color.Default))
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.Green ||
                        threeButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green ||
                        eightButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (sevenButton.BackgroundColor == Color.Default))
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if ((twoButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green ||
                        sevenButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (eightButton.BackgroundColor == Color.Default))
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }
            else if ((oneButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green ||
                threeButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green ||
                sevenButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green) && (nineButton.BackgroundColor == Color.Default))
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }

            //executes plan move; a lax move is one which is not for-win or defense/compulsory.

            //choose center first.... this may be redundant. 
            else if (fiveButton.BackgroundColor == Color.Default)
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }

            //choose an unoccupied corner

            else if (oneButton.BackgroundColor == Color.Default)
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if (threeButton.BackgroundColor == Color.Default)
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if (sevenButton.BackgroundColor == Color.Default)
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if (nineButton.BackgroundColor == Color.Default)
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }

            //choose an unoccupied middle

            else if (twoButton.BackgroundColor == Color.Default)
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if (fourButton.BackgroundColor == Color.Default)
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if (sixButton.BackgroundColor == Color.Default)
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if (eightButton.BackgroundColor == Color.Default)
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }

        }*/ //old version

        private void CheckDraw()
        {
            if (oneButton.IsEnabled == false &&
                twoButton.IsEnabled == false &&
                threeButton.IsEnabled == false &&
                fourButton.IsEnabled == false &&
                fiveButton.IsEnabled == false &&
                sixButton.IsEnabled == false &&
                sevenButton.IsEnabled == false &&
                eightButton.IsEnabled == false &&
                nineButton.IsEnabled == false)
            {
                Draw = true;
            }
        }

        private void CheckMachineWins()
        {
            if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed) ||
                (fourButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) ||
                (sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) ||
                (oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed) ||
                (twoButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) ||
                (threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) ||
                (oneButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) ||
                (threeButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed))
            {
                MachineWins = true;
            }
        }

        private void EndGameIf()
        {
            if (MachineWins == true)
            {
                DisableButtons();
                displayLabel.Text = "you lose.";
            }
            else if (Draw == true)
            {
                DisableButtons();
                displayLabel.Text = "it's a draw.";
            }
        }

        private void DisableButtons()
        {
            oneButton.IsEnabled = false;
            twoButton.IsEnabled = false;
            threeButton.IsEnabled = false;
            fourButton.IsEnabled = false;
            fiveButton.IsEnabled = false;
            sixButton.IsEnabled = false;
            sevenButton.IsEnabled = false;
            eightButton.IsEnabled = false;
            nineButton.IsEnabled = false;
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            PlayerMoveCount = 0;
            MachineWins = false;
            Draw = false;

            displayLabel.Text = "resistance is futile...";
            
            oneButton.BackgroundColor = Color.Default;
            oneButton.IsEnabled = true;
            oneButton.Text = "T";

            twoButton.BackgroundColor = Color.Default;
            twoButton.IsEnabled = true;
            twoButton.Text = "I";

            threeButton.BackgroundColor = Color.Default;
            threeButton.IsEnabled = true;
            threeButton.Text = "C";

            fourButton.BackgroundColor = Color.Default;
            fourButton.IsEnabled = true;
            fourButton.Text = "T";

            fiveButton.BackgroundColor = Color.Default;
            fiveButton.IsEnabled = true;
            fiveButton.Text = "A";

            sixButton.BackgroundColor = Color.Default;
            sixButton.IsEnabled = true;
            sixButton.Text = "C";

            sevenButton.BackgroundColor = Color.Default;
            sevenButton.IsEnabled = true;
            sevenButton.Text = "T";

            eightButton.BackgroundColor = Color.Default;
            eightButton.IsEnabled = true;
            eightButton.Text = "O";

            nineButton.BackgroundColor = Color.Default;
            nineButton.IsEnabled = true;
            nineButton.Text = "E";

            if (!PlayerIsFirst)
            {
                if (!CheckForWinMove())
                {
                    if (!CheckForBlockMove())
                    {
                        if (!CheckForSmartMove())
                        {
                            FiatMove();
                        }
                    }
                }
            }
        }

        private void PriorityButton_Clicked(object sender, EventArgs e)
        {
            if (PlayerIsFirst)
            {
                PlayerIsFirst = false;
                priorityButton.Text = "act first";
            }
            else
            {
                PlayerIsFirst = true;
                priorityButton.Text = "act second";
            }

            ResetButton_Clicked(sender, e);
        }

        private bool CheckForSmartMove()
        {
            bool smartMoveExecuted = false;
            switch (PlayerMoveCount)
            {
                case 0: //machine randomly chooses to start center or corner
                    switch (random.Next(3))
                    {
                        case 0: //machine chooses center
                            fiveButton.BackgroundColor = Color.IndianRed;
                            fiveButton.IsEnabled = false;
                            fiveButton.Text = "0";
                            smartMoveExecuted = true;
                            break;
                        case 1:
                        case 2: //machine chooses a random corner
                            switch (random.Next(4))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 2:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 3:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                            break;
                    }
                    break;
                case 1:
                    if (PlayerIsFirst)
                    {
                        if (fiveButton.IsEnabled == true) //it chooses center
                        {
                            fiveButton.BackgroundColor = Color.IndianRed;
                            fiveButton.IsEnabled = false;
                            fiveButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else //if center is taken it chooses a random corner
                        {
                            switch (random.Next(4))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 2:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 3:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                    }
                    else //(player is not first) machine makes its second move acting first
                    {
                        if (fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            //center > corner
                            //if machine has center and player has a corner, machine chooses opposite corner
                            if (oneButton.BackgroundColor == Color.Green)
                            {
                                nineButton.BackgroundColor = Color.IndianRed;
                                nineButton.IsEnabled = false;
                                nineButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            else if (threeButton.BackgroundColor == Color.Green)
                            {
                                sevenButton.BackgroundColor = Color.IndianRed;
                                sevenButton.IsEnabled = false;
                                sevenButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            else if (sevenButton.BackgroundColor == Color.Green)
                            {
                                threeButton.BackgroundColor = Color.IndianRed;
                                threeButton.IsEnabled = false;
                                threeButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            else if (nineButton.BackgroundColor == Color.Green)
                            {
                                oneButton.BackgroundColor = Color.IndianRed;
                                oneButton.IsEnabled = false;
                                oneButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            //center > middle
                            else if (twoButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green || sixButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green)
                            {
                                switch (random.Next(3))
                                {
                                    case 0:
                                        //if machine has center and player has a middle, than the machine chooses a corner opposite the player's middle
                                        if (twoButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    sevenButton.BackgroundColor = Color.IndianRed;
                                                    sevenButton.IsEnabled = false;
                                                    sevenButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    nineButton.BackgroundColor = Color.IndianRed;
                                                    nineButton.IsEnabled = false;
                                                    nineButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (fourButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    threeButton.BackgroundColor = Color.IndianRed;
                                                    threeButton.IsEnabled = false;
                                                    threeButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    nineButton.BackgroundColor = Color.IndianRed;
                                                    nineButton.IsEnabled = false;
                                                    nineButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (sixButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    oneButton.BackgroundColor = Color.IndianRed;
                                                    oneButton.IsEnabled = false;
                                                    oneButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    sevenButton.BackgroundColor = Color.IndianRed;
                                                    sevenButton.IsEnabled = false;
                                                    sevenButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (eightButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    oneButton.BackgroundColor = Color.IndianRed;
                                                    oneButton.IsEnabled = false;
                                                    oneButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    threeButton.BackgroundColor = Color.IndianRed;
                                                    threeButton.IsEnabled = false;
                                                    threeButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 1:
                                        //if machine has center and player has a middle, than the machine chooses a middle adjacent to player
                                        if (twoButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    fourButton.BackgroundColor = Color.IndianRed;
                                                    fourButton.IsEnabled = false;
                                                    fourButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    sixButton.BackgroundColor = Color.IndianRed;
                                                    sixButton.IsEnabled = false;
                                                    sixButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (fourButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    twoButton.BackgroundColor = Color.IndianRed;
                                                    twoButton.IsEnabled = false;
                                                    twoButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    eightButton.BackgroundColor = Color.IndianRed;
                                                    eightButton.IsEnabled = false;
                                                    eightButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (sixButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    twoButton.BackgroundColor = Color.IndianRed;
                                                    twoButton.IsEnabled = false;
                                                    twoButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    eightButton.BackgroundColor = Color.IndianRed;
                                                    eightButton.IsEnabled = false;
                                                    eightButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (eightButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    fourButton.BackgroundColor = Color.IndianRed;
                                                    fourButton.IsEnabled = false;
                                                    fourButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    sixButton.BackgroundColor = Color.IndianRed;
                                                    sixButton.IsEnabled = false;
                                                    sixButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 2:
                                        //if machine has center and player has middle, machine chooses corner adjacent to the player
                                        if (twoButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    oneButton.BackgroundColor = Color.IndianRed;
                                                    oneButton.IsEnabled = false;
                                                    oneButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    threeButton.BackgroundColor = Color.IndianRed;
                                                    threeButton.IsEnabled = false;
                                                    threeButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (fourButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    oneButton.BackgroundColor = Color.IndianRed;
                                                    oneButton.IsEnabled = false;
                                                    oneButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    sevenButton.BackgroundColor = Color.IndianRed;
                                                    sevenButton.IsEnabled = false;
                                                    sevenButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (sixButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    threeButton.BackgroundColor = Color.IndianRed;
                                                    threeButton.IsEnabled = false;
                                                    threeButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    nineButton.BackgroundColor = Color.IndianRed;
                                                    nineButton.IsEnabled = false;
                                                    nineButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        else if (eightButton.BackgroundColor == Color.Green)
                                        {
                                            switch (random.Next(2))
                                            {
                                                case 0:
                                                    sevenButton.BackgroundColor = Color.IndianRed;
                                                    sevenButton.IsEnabled = false;
                                                    sevenButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                                case 1:
                                                    nineButton.BackgroundColor = Color.IndianRed;
                                                    nineButton.IsEnabled = false;
                                                    nineButton.Text = "0";
                                                    smartMoveExecuted = true;
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        //corner > center
                        //machine has corner and player has center, machine chooses opposite corner
                        //if the middle buttton is green than this implies the player chose it, implying the space was freed up, implying the machine must have chosen the only other alternative of center when acting first
                        else if (fiveButton.BackgroundColor == Color.Green)
                        {
                            if (oneButton.BackgroundColor == Color.IndianRed)
                            {
                                nineButton.BackgroundColor = Color.IndianRed;
                                nineButton.IsEnabled = false;
                                nineButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            else if (threeButton.BackgroundColor == Color.IndianRed)
                            {
                                sevenButton.BackgroundColor = Color.IndianRed;
                                sevenButton.IsEnabled = false;
                                sevenButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            else if (sevenButton.BackgroundColor == Color.IndianRed)
                            {
                                threeButton.BackgroundColor = Color.IndianRed;
                                threeButton.IsEnabled = false;
                                threeButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                            else if (nineButton.BackgroundColor == Color.IndianRed)
                            {
                                oneButton.BackgroundColor = Color.IndianRed;
                                oneButton.IsEnabled = false;
                                oneButton.Text = "0";
                                smartMoveExecuted = true;
                            }
                        }
                        //corner > adjacent corner 
                        else if ((oneButton.BackgroundColor == Color.IndianRed && (threeButton.BackgroundColor == Color.Green || sevenButton.BackgroundColor == Color.Green)) || (threeButton.BackgroundColor == Color.IndianRed && (oneButton.BackgroundColor == Color.Green || nineButton.BackgroundColor == Color.Green)) || (sevenButton.BackgroundColor == Color.IndianRed && (oneButton.BackgroundColor == Color.Green || nineButton.BackgroundColor == Color.Green)) || (nineButton.BackgroundColor == Color.IndianRed && (threeButton.BackgroundColor == Color.Green || sevenButton.BackgroundColor == Color.Green)))
                        {
                            switch (random.Next(3))
                            {
                                case 0:
                                    //if machine has corner and player has an adjacent corner, machine chooses middle adjacent to itself and opposite player
                                    if ((oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green))
                                    {
                                        twoButton.BackgroundColor = Color.IndianRed;
                                        twoButton.IsEnabled = false;
                                        twoButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green))
                                    {
                                        fourButton.BackgroundColor = Color.IndianRed;
                                        fourButton.IsEnabled = false;
                                        fourButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((threeButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green))
                                    {
                                        sixButton.BackgroundColor = Color.IndianRed;
                                        sixButton.IsEnabled = false;
                                        sixButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((sevenButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green))
                                    {
                                        eightButton.BackgroundColor = Color.IndianRed;
                                        eightButton.IsEnabled = false;
                                        eightButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                                case 1:
                                    //if machine has corner and player has a adjacent corner, machine chooses corner opposite of player
                                    if ((sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green))
                                    {
                                        oneButton.BackgroundColor = Color.IndianRed;
                                        oneButton.IsEnabled = false;
                                        oneButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green))
                                    {
                                        threeButton.BackgroundColor = Color.IndianRed;
                                        threeButton.IsEnabled = false;
                                        threeButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green))
                                    {
                                        sevenButton.BackgroundColor = Color.IndianRed;
                                        sevenButton.IsEnabled = false;
                                        sevenButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((sevenButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green))
                                    {
                                        nineButton.BackgroundColor = Color.IndianRed;
                                        nineButton.IsEnabled = false;
                                        nineButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                                case 2:
                                    //if machine has corner and player has adjacent corner, machine chooses corner opposite itself
                                    if ((nineButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green))
                                    {
                                        oneButton.BackgroundColor = Color.IndianRed;
                                        oneButton.IsEnabled = false;
                                        oneButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((sevenButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green))
                                    {
                                        threeButton.BackgroundColor = Color.IndianRed;
                                        threeButton.IsEnabled = false;
                                        threeButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((threeButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green))
                                    {
                                        sevenButton.BackgroundColor = Color.IndianRed;
                                        sevenButton.IsEnabled = false;
                                        sevenButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green) || (oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green))
                                    {
                                        nineButton.BackgroundColor = Color.IndianRed;
                                        nineButton.IsEnabled = false;
                                        nineButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                            }
                        }
                        //corner > opposite corner
                        //if machine has corner and player has opposite corner, machine chooses remaining corner at random
                        else if (oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (sevenButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (nineButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        //corner > adjacent middle
                        else if ((oneButton.BackgroundColor == Color.IndianRed && (twoButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green)) || (threeButton.BackgroundColor == Color.IndianRed && (twoButton.BackgroundColor == Color.Green || sixButton.BackgroundColor == Color.Green)) || (sevenButton.BackgroundColor == Color.IndianRed && (fourButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green)) || (nineButton.BackgroundColor == Color.IndianRed && (sixButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green)))
                        {
                            switch (random.Next(3))
                            {
                                case 0:
                                    //if machine has corner and player has an adjacent middle, machine chooses adjacent middle to itself
                                    if ((oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green))
                                    {
                                        twoButton.BackgroundColor = Color.IndianRed;
                                        twoButton.IsEnabled = false;
                                        twoButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green))
                                    {
                                        fourButton.BackgroundColor = Color.IndianRed;
                                        fourButton.IsEnabled = false;
                                        fourButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((threeButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green))
                                    {
                                        sixButton.BackgroundColor = Color.IndianRed;
                                        sixButton.IsEnabled = false;
                                        sixButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((sevenButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green))
                                    {
                                        eightButton.BackgroundColor = Color.IndianRed;
                                        eightButton.IsEnabled = false;
                                        eightButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                                case 1:
                                    //if machine has corner and player has an adjacent middle, machine chooses center
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 2:
                                    //if machine has corner and player has an adjacent middle, machine chooses corner adjacent to itself and opposite player
                                    if ((threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green))
                                    {
                                        oneButton.BackgroundColor = Color.IndianRed;
                                        oneButton.IsEnabled = false;
                                        oneButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    if ((oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green))
                                    {
                                        threeButton.BackgroundColor = Color.IndianRed;
                                        threeButton.IsEnabled = false;
                                        threeButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green))
                                    {
                                        sevenButton.BackgroundColor = Color.IndianRed;
                                        sevenButton.IsEnabled = false;
                                        sevenButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    if ((threeButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green))
                                    {
                                        nineButton.BackgroundColor = Color.IndianRed;
                                        nineButton.IsEnabled = false;
                                        nineButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                            }
                        }
                        //corner > opposite middle
                        else if ((oneButton.BackgroundColor == Color.IndianRed && (sixButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green)) || (threeButton.BackgroundColor == Color.IndianRed && (fourButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green)) || (sevenButton.BackgroundColor == Color.IndianRed && (twoButton.BackgroundColor == Color.Green || sixButton.BackgroundColor == Color.Green)) || (nineButton.BackgroundColor == Color.IndianRed && (twoButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green)))
                        {
                            switch (random.Next(3))
                            {
                                //if machine has corner and player has opposite middle, machine chooses corner adjacent to itself and opposite player
                                case 0:
                                    if ((threeButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green))
                                    {
                                        oneButton.BackgroundColor = Color.IndianRed;
                                        oneButton.IsEnabled = false;
                                        oneButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green))
                                    {
                                        threeButton.BackgroundColor = Color.IndianRed;
                                        threeButton.IsEnabled = false;
                                        threeButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green))
                                    {
                                        sevenButton.BackgroundColor = Color.IndianRed;
                                        sevenButton.IsEnabled = false;
                                        sevenButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((threeButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green))
                                    {
                                        nineButton.BackgroundColor = Color.IndianRed;
                                        nineButton.IsEnabled = false;
                                        nineButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                                //if machine has corner and player has opposite middle, machine chooses corner adjacent to both itself and player
                                case 1:
                                    if ((threeButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green))
                                    {
                                        oneButton.BackgroundColor = Color.IndianRed;
                                        oneButton.IsEnabled = false;
                                        oneButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green))
                                    {
                                        threeButton.BackgroundColor = Color.IndianRed;
                                        threeButton.IsEnabled = false;
                                        threeButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((oneButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green) || (nineButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green))
                                    {
                                        sevenButton.BackgroundColor = Color.IndianRed;
                                        sevenButton.IsEnabled = false;
                                        sevenButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    else if ((threeButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green) || (sevenButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green))
                                    {
                                        nineButton.BackgroundColor = Color.IndianRed;
                                        nineButton.IsEnabled = false;
                                        nineButton.Text = "0";
                                        smartMoveExecuted = true;
                                    }
                                    break;
                                //if machine has corner and player has opposite middle, machine chooses center
                                case 2:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }  
                        }
                    }
                    break;
                case 2:
                    if (PlayerIsFirst) //machine makes second move acting second
                    {
                        //avoids corner > center
                        //if player has corner and machine has center and player has corner opposite themself, machine chooses random middle
                        if ((oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) || (threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green))
                        {
                            switch (random.Next(4))
                            {
                                case 0:
                                    twoButton.BackgroundColor = Color.IndianRed;
                                    twoButton.IsEnabled = false;
                                    twoButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    fourButton.BackgroundColor = Color.IndianRed;
                                    fourButton.IsEnabled = false;
                                    fourButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 2:
                                    sixButton.BackgroundColor = Color.IndianRed;
                                    sixButton.IsEnabled = false;
                                    sixButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 3:
                                    eightButton.BackgroundColor = Color.IndianRed;
                                    eightButton.IsEnabled = false;
                                    eightButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        //avoids center > corner
                        //if player has center and machine has corner and player has corner opposite machine corner, machine randomly chooses remaining corner
                        else if ((oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                    }
                    else //machine makes third move acting first, this move sets win pin
                    {
                        //corner > adjacent corner
                        //if machine has corner and player has adjacent corner and machine has corner opposite of player, machine chooses remaining corner
                        if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            nineButton.BackgroundColor = Color.IndianRed;
                            nineButton.IsEnabled = false;
                            nineButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else if ((threeButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed) || (threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed))
                        {
                            sevenButton.BackgroundColor = Color.IndianRed;
                            sevenButton.IsEnabled = false;
                            sevenButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else if ((sevenButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed))
                        {
                            threeButton.BackgroundColor = Color.IndianRed;
                            threeButton.IsEnabled = false;
                            threeButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else if ((nineButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            oneButton.BackgroundColor = Color.IndianRed;
                            oneButton.IsEnabled = false;
                            oneButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        //corner > opposite corner
                        //if machine has corner and player has opposite corner and machine has corner, machine chooses remaining corner
                        else if ((oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            sevenButton.BackgroundColor = Color.IndianRed;
                            sevenButton.IsEnabled = false;
                            sevenButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else if ((oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && oneButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed))
                        {
                            threeButton.BackgroundColor = Color.IndianRed;
                            threeButton.IsEnabled = false;
                            threeButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else if ((threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed))
                        {
                            nineButton.BackgroundColor = Color.IndianRed;
                            nineButton.IsEnabled = false;
                            nineButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        else if ((threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed))
                        {
                            oneButton.BackgroundColor = Color.IndianRed;
                            oneButton.IsEnabled = false;
                            oneButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        //corner > adjacent middle
                        //if machine has corner and player has adjacent middle and machine has middle adjacent to itself, machine chooses center
                        else if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.IndianRed) || (oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.IndianRed) || (threeButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.IndianRed) || (threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.IndianRed))
                        {
                            fiveButton.BackgroundColor = Color.IndianRed;
                            fiveButton.IsEnabled = false;
                            fiveButton.Text = "0";
                            smartMoveExecuted = true;
                        }
                        //if machine has corner and player has adjacent middle and machine has center, machine randomly chooses corner opposite the player's middle OR middle adjacent to its own corner
                        else if (oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    fourButton.BackgroundColor = Color.IndianRed;
                                    fourButton.IsEnabled = false;
                                    fourButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    twoButton.BackgroundColor = Color.IndianRed;
                                    twoButton.IsEnabled = false;
                                    twoButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (threeButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sixButton.BackgroundColor = Color.IndianRed;
                                    sixButton.IsEnabled = false;
                                    sixButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    twoButton.BackgroundColor = Color.IndianRed;
                                    twoButton.IsEnabled = false;
                                    twoButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (sevenButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    eightButton.BackgroundColor = Color.IndianRed;
                                    eightButton.IsEnabled = false;
                                    eightButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    fourButton.BackgroundColor = Color.IndianRed;
                                    fourButton.IsEnabled = false;
                                    fourButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (nineButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    eightButton.BackgroundColor = Color.IndianRed;
                                    eightButton.IsEnabled = false;
                                    eightButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (nineButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sixButton.BackgroundColor = Color.IndianRed;
                                    sixButton.IsEnabled = false;
                                    sixButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        //if machine has corner and player has adjacent middle and machine has corner adjacent to itself and opposite player's middle, machine randomly chooses center OR corner opposite bother player's middles.
                        else if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((threeButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed) || (threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((nineButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        //corner > opp middle
                        //if machine has corner and player has opposite middle and machine has corner adjacent to itself and the player's middle, machine randomly chooses: center OR corner opposite both of opponent's middles
                        else if ((threeButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed) || (sevenButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((oneButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((oneButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed) || (nineButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((sevenButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed) || (threeButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    fiveButton.BackgroundColor = Color.IndianRed;
                                    fiveButton.IsEnabled = false;
                                    fiveButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        //center > middle
                        //if machine has center and player has middle and machine has adjacent corner to player, machine randomly chooses: corner opposite player's middle and adjacent to its own corner OR middle adjacent to its own corner
                        else if (fiveButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    fourButton.BackgroundColor = Color.IndianRed;
                                    fourButton.IsEnabled = false;
                                    fourButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sixButton.BackgroundColor = Color.IndianRed;
                                    sixButton.IsEnabled = false;
                                    sixButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && oneButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    twoButton.BackgroundColor = Color.IndianRed;
                                    twoButton.IsEnabled = false;
                                    twoButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    eightButton.BackgroundColor = Color.IndianRed;
                                    eightButton.IsEnabled = false;
                                    eightButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    twoButton.BackgroundColor = Color.IndianRed;
                                    twoButton.IsEnabled = false;
                                    twoButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    eightButton.BackgroundColor = Color.IndianRed;
                                    eightButton.IsEnabled = false;
                                    eightButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    fourButton.BackgroundColor = Color.IndianRed;
                                    fourButton.IsEnabled = false;
                                    fourButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if (fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.IndianRed)
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sixButton.BackgroundColor = Color.IndianRed;
                                    sixButton.IsEnabled = false;
                                    sixButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        //if machine has center and player has middle and machine has middle adjacent to player's middle, machine randomly chooses corner opposite both of player's middles OR corner adjacent to its middle
                        else if ((fiveButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.IndianRed) || (fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((fiveButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.IndianRed) || (fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((fiveButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.IndianRed) || (fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    threeButton.BackgroundColor = Color.IndianRed;
                                    threeButton.IsEnabled = false;
                                    threeButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    oneButton.BackgroundColor = Color.IndianRed;
                                    oneButton.IsEnabled = false;
                                    oneButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                        else if ((fiveButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.IndianRed) || (fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.IndianRed))
                        {
                            switch (random.Next(2))
                            {
                                case 0:
                                    nineButton.BackgroundColor = Color.IndianRed;
                                    nineButton.IsEnabled = false;
                                    nineButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                                case 1:
                                    sevenButton.BackgroundColor = Color.IndianRed;
                                    sevenButton.IsEnabled = false;
                                    sevenButton.Text = "0";
                                    smartMoveExecuted = true;
                                    break;
                            }
                        }
                    }
                    break;
            }
            return smartMoveExecuted;
        }

        private bool CheckForWinMove()
        {
            bool winMoveExecuted = false;
            //[checks for win opportunity and takes it] 
            if ((twoButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed || fourButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed || fiveButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (oneButton.IsEnabled == true))
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && threeButton.BackgroundColor == Color.IndianRed || fiveButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) && (twoButton.IsEnabled == true))
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && twoButton.BackgroundColor == Color.IndianRed || fiveButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed || sixButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (threeButton.IsEnabled == true))
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed || fiveButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) && (fourButton.IsEnabled == true))
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed || twoButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed || threeButton.BackgroundColor == Color.IndianRed && sevenButton.BackgroundColor == Color.IndianRed || fourButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed) && (fiveButton.IsEnabled == true))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((threeButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed || fourButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed) && (sixButton.IsEnabled == true))
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && fourButton.BackgroundColor == Color.IndianRed || threeButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed || eightButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (sevenButton.IsEnabled == true))
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((twoButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed || sevenButton.BackgroundColor == Color.IndianRed && nineButton.BackgroundColor == Color.IndianRed) && (eightButton.IsEnabled == true))
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
                winMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.IndianRed && fiveButton.BackgroundColor == Color.IndianRed || threeButton.BackgroundColor == Color.IndianRed && sixButton.BackgroundColor == Color.IndianRed || sevenButton.BackgroundColor == Color.IndianRed && eightButton.BackgroundColor == Color.IndianRed) && (nineButton.IsEnabled == true))
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
                winMoveExecuted = true;
            }
            return winMoveExecuted;
        }

        private bool CheckForBlockMove()
        {
            bool blockMoveExecuted = false;
            //[checks for compulsory block move and takes it]
            if ((twoButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green || fiveButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (oneButton.IsEnabled == true))
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.Green && threeButton.BackgroundColor == Color.Green || fiveButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green) && (twoButton.IsEnabled == true))
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.Green && twoButton.BackgroundColor == Color.Green || fiveButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green || sixButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (threeButton.IsEnabled == true))
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green || fiveButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green) && (fourButton.IsEnabled == true))
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green || twoButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green || threeButton.BackgroundColor == Color.Green && sevenButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green) && (fiveButton.IsEnabled == true))
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((threeButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green || fourButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green) && (sixButton.IsEnabled == true))
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.Green && fourButton.BackgroundColor == Color.Green || threeButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green || eightButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (sevenButton.IsEnabled == true))
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((twoButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green || sevenButton.BackgroundColor == Color.Green && nineButton.BackgroundColor == Color.Green) && (eightButton.IsEnabled == true))
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
                blockMoveExecuted = true;
            }
            else if ((oneButton.BackgroundColor == Color.Green && fiveButton.BackgroundColor == Color.Green || threeButton.BackgroundColor == Color.Green && sixButton.BackgroundColor == Color.Green || sevenButton.BackgroundColor == Color.Green && eightButton.BackgroundColor == Color.Green) && (nineButton.IsEnabled == true))
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
                blockMoveExecuted = true;
            }
            return blockMoveExecuted;
        }

        private void FiatMove()
        {
            if (fiveButton.IsEnabled == true)
            {
                fiveButton.BackgroundColor = Color.IndianRed;
                fiveButton.IsEnabled = false;
                fiveButton.Text = "0";
            }
            else if (oneButton.IsEnabled == true)
            {
                oneButton.BackgroundColor = Color.IndianRed;
                oneButton.IsEnabled = false;
                oneButton.Text = "0";
            }
            else if (threeButton.IsEnabled == true)
            {
                threeButton.BackgroundColor = Color.IndianRed;
                threeButton.IsEnabled = false;
                threeButton.Text = "0";
            }
            else if (sevenButton.IsEnabled == true)
            {
                sevenButton.BackgroundColor = Color.IndianRed;
                sevenButton.IsEnabled = false;
                sevenButton.Text = "0";
            }
            else if (nineButton.IsEnabled == true)
            {
                nineButton.BackgroundColor = Color.IndianRed;
                nineButton.IsEnabled = false;
                nineButton.Text = "0";
            }
            else if (twoButton.IsEnabled == true)
            {
                twoButton.BackgroundColor = Color.IndianRed;
                twoButton.IsEnabled = false;
                twoButton.Text = "0";
            }
            else if (fourButton.IsEnabled == true)
            {
                fourButton.BackgroundColor = Color.IndianRed;
                fourButton.IsEnabled = false;
                fourButton.Text = "0";
            }
            else if (sixButton.IsEnabled == true)
            {
                sixButton.BackgroundColor = Color.IndianRed;
                sixButton.IsEnabled = false;
                sixButton.Text = "0";
            }
            else if (eightButton.IsEnabled == true)
            {
                eightButton.BackgroundColor = Color.IndianRed;
                eightButton.IsEnabled = false;
                eightButton.Text = "0";
            }
        }
    }
}
