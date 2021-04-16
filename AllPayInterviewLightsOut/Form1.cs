using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AllPayInterviewLightsOut
{
    public partial class Form1 : Form
    {
        //The list of buttons used in the game
        private List<Button> Buttons { get; set; }
        //The number of moves that the player has made 
        private int MoveCount { get; set; }

        /// <summary>
        /// Constructor that sets up the form component and the game
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            SetUpGame();
        }

        /// <summary>
        /// The event that fires whenever a button is clicked.
        /// </summary>
        /// <param name="sender">Button that is calling the method</param>
        /// <param name="e">Event arguments</param>
        public void PlayButtonClick(object sender, EventArgs e)
        {
            //Increment the move count and update the text
            this.MoveCount++;
            Control n = this.Controls.Find("MoveCount", true).FirstOrDefault();
            n.Text = "Number of moves made: " + this.MoveCount;
            Button button = (Button)sender;
            //Get the position of the button that has been seleted
            int index = this.Buttons.IndexOf(button);
            //Toggle the colour of the selected button
            ToggleColour(button);
            //If the button isn't on the left side, toggle the left button
            if (index % 5 != 0)
            {
                ToggleColour(this.Buttons[index - 1]);
            }
            //If the button isn't on the right side, toggle the right button
            if ((index + 1) % 5 != 0)
            {
                ToggleColour(this.Buttons[index + 1]);
            }
            //If the button isn't on the top, toggle the button above
            if (index - 5 >= 0)
            {
                ToggleColour(this.Buttons[index - 5]);
            }
            //If the button isn't on the bottom, toggle the button below
            if (index + 5 < this.Buttons.Count)
            {
                ToggleColour(this.Buttons[index + 5]);
            }

            //If a button has been turned from green to white, check to make sure that the user has won the game.
            if (button.BackColor == Color.White)
            {
                bool hasWon = CheckWinState(this.Buttons);
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
                    resetButton.Click += new EventHandler(ResetButtonClick);
                    this.Controls.Add(resetButton);
                }
            }

        }

        /// <summary>
        /// The method that is fired when the user has won the game and they want to reset the game.
        /// </summary>
        /// <param name="sender">The reset button control that has called the method</param>
        /// <param name="e">Event arguments</param>
        public void ResetButtonClick(object sender, EventArgs e)
        {
            SetUpGame();
        }


        /// <summary>
        /// The method that initialises and sets up the 
        /// </summary>
        public void SetUpGame()
        {
            this.MoveCount = 0;
            this.Controls.Clear();
            Label moveCountLabel = new Label();
            moveCountLabel.Text = "Number of moves made: " + this.MoveCount;
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
            int[] pattern = GetPattern();
            //Generate all the buttons that will be used in the game and place them on the form
            for (var i = 0; i < 25; i++)
            {
                //Start a new row by resetting the x position and incrementing the y position
                if (i % 5 == 0)
                {
                    xPos = 250;
                    yPos += 50;
                }
                //The button that is about to be added to the grid
                Button myButton = new Button();
                myButton.Name = "PlayButton";
                myButton.Height = 50;
                myButton.Width = 50;
                //If there is a 1 in the pattern, make this button white
                if (pattern[i] == 1)
                {
                    myButton.BackColor = Color.White;
                }
                //Otherwise, make it green
                else
                {
                    myButton.BackColor = Color.Green;
                }
                myButton.Location = new Point(xPos, yPos);
                myButton.Click += new EventHandler(PlayButtonClick);
                //Increment the x position for the next button
                xPos += 50;
                this.Buttons.Add(myButton);
                this.Controls.Add(myButton);
            }
        }

        /// <summary>
        /// Selects a random pattern for the game to use. 1s become white buttons and 0s become green buttons
        /// </summary>
        /// <returns>The randomly selected pattern that the game will use </returns>
        public int[] GetPattern()
        {
            List<int[]> patterns = new List<int[]>();
            //Pattern A
            int[] a = new int[] { 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1 };
            patterns.Add(a);
            //Pattern B
            int[] b = new int[] { 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0 };
            patterns.Add(b);
            //Pattern C
            int[] c = new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1 };
            patterns.Add(c);
            //Pattern D
            int[] d = new int[] { 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1 };
            patterns.Add(d);
            //Pattern E
            int[] e = new int[] { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
            patterns.Add(e);
            //Randomly select a pattern to use in the game
            int[] result = patterns[new Random().Next(0, 5)];

            return result;
        }

        /// <summary>
        /// Check every button. If one of them is green, it isn't a winning case. Otherwise, it is a winning case.
        /// </summary>
        /// <returns>boolean value of whether or not the player has won the game</returns>
        public bool CheckWinState(List<Button> myButtons)
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
        public void ToggleColour(Button button)
        {
            //If the button is green, toggle to white
            if (button.BackColor == Color.Green)
            {
                button.BackColor = Color.White;
            }
            //If the button is white, toggle to green
            else
            {
                button.BackColor = Color.Green;
            }
        }

    }
}
