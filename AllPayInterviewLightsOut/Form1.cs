using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllPayInterviewLightsOut
{
    public partial class Form1 : Form
    {
        private List<Button> Buttons { get; set; }
        private int moveCount { get; set; }
        public Form1()
        {
            InitializeComponent();
            setUpGame();
        }

        /// <summary>
        /// The event that fires whenever a button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button_Click(object sender, EventArgs e)
        {
            this.moveCount++;
            Control n = this.Controls.Find("MoveCount", true).FirstOrDefault();
            n.Text = "Number of moves made: " + this.moveCount;
            Button button = (Button)sender;
            //Get the position of the button that has been seleted
            int index = this.Buttons.IndexOf(button);
            toggleColour(button);
            int listLength = this.Buttons.Count() - 1;
            //If the button isn't on the left side, toggle the left button
            if (index % 5 != 0)
            {
                toggleColour(this.Buttons[index - 1]);
            }
            //If the button isn't on the right side, toggle the right button
            if ((index + 1) % 5 != 0)
            {
                toggleColour(this.Buttons[index + 1]);
            }
            //If the button isn't on the top, toggle the button above
            if (index - 5 >= 0)
            {
                toggleColour(this.Buttons[index - 5]);
            }
            //If the button isn't on the bottom, toggle the button below
            if (index + 5 < this.Buttons.Count)
            {
                toggleColour(this.Buttons[index + 5]);
            }

            //If a button has been turned from green to white, check to make sure that the user has won the game.
            if (button.BackColor == Color.White)
            {
                bool hasWon = checkWinState(this.Buttons);
                //If they have won, remove all the buttons from the view, set up a congratulatory message and give the user the option to play again
                if (hasWon == true)
                {
                    Control[] playButtons = this.Controls.Find("PlayButton", true);
                    foreach (var playButton in playButtons)
                    {
                        this.Controls.Remove(playButton);
                    }
                    Label winLabel = new Label();
                    winLabel.Text = "Congratulations!";
                    winLabel.Location = new Point(330, 200);
                    this.Controls.Add(winLabel);
                    Button resetButton = new Button();
                    resetButton.Text = "Play again?";
                    resetButton.Location = new Point(340, 245);
                    resetButton.Click += new EventHandler(mybutton_Click);
                    this.Controls.Add(resetButton);
                }
            }

        }



        public void mybutton_Click(object sender, EventArgs e)
        {
            setUpGame();
        }

        public void setUpGame()
        {
            this.moveCount = 0;

            this.Controls.Clear();


            Label moveCountLabel = new Label();
            moveCountLabel.Text = "Number of moves made: " + this.moveCount;
            moveCountLabel.Width = 200;
            moveCountLabel.Name = "MoveCount";
            moveCountLabel.Location = new Point(300, 400);
            this.Controls.Add(moveCountLabel);

            this.Buttons = new List<Button>();
            //The x position for each button
            int xPos = 250;
            //The y position for each button
            int yPos = 40;
            //Getting a random pattern for the user to play on
            int[] pattern = getPattern();
            //Generating all the buttons that will be used in the game
            for (var i = 0; i < 25; i++)
            {
                //Start a new row by resetting the x position and incrementing the y position
                if (i % 5 == 0)
                {
                    xPos = 250;
                    yPos += 50;
                }
                Button myButton = new Button();
                myButton.Name = "PlayButton";
                myButton.Height = 50;
                myButton.Width = 50;
                if (pattern[i] == 1)
                {
                    myButton.BackColor = Color.White;
                }
                else
                {
                    myButton.BackColor = Color.Green;
                }


                myButton.Location = new Point(xPos, yPos);
                myButton.Click += new EventHandler(button_Click);
                xPos += 50;
                this.Buttons.Add(myButton);
                this.Controls.Add(myButton);
            }
        }

        /// <summary>
        /// Selects a random pattern for the game to use. 1s become white buttons and 0s become green buttons
        /// </summary>
        /// <returns>The randomly selected pattern that the game will use </returns>
        public int[] getPattern()
        {
            List<int[]> patterns = new List<int[]>();
            int[] a = new int[] { 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1 };
            patterns.Add(a);
            int[] b = new int[] { 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0 };
            patterns.Add(b);
            int[] c = new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1 };
            patterns.Add(c);

            int[] d = new int[] { 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
            patterns.Add(d);
            int[] e = new int[] { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
            patterns.Add(e);

            int[] result = patterns[new Random().Next(0, 5)];

            return result;
        }



        /// <summary>
        /// Check every button. If one of them is green, it isn't a winning case. Otherwise, it is a winning case.
        /// </summary>
        /// <returns>boolean value of whether or not the player has won the game</returns>
        public bool checkWinState(List<Button> myButtons)
        {
            foreach (Button button in myButtons)
            {
                //If one of the buttons is still green, the game isn't in a winning state and it returns false
                if (button.BackColor == Color.Green)
                {
                    return false;
                }
            }
            //If none of the buttons are still green, the game is in a winning state and it returns true
            return true;
        }

        /// <summary>
        /// This method toggles the colour between white and green for the given button
        /// </summary>
        /// <param name="button">The button that needs to be toggled</param>
        public void toggleColour(Button button)
        {
            if (button.BackColor == Color.Green)
            {
                button.BackColor = Color.White;
            }
            else
            {
                button.BackColor = Color.Green;
            }
        }

    }
}
