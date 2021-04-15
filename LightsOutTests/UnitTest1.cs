using AllPayInterviewLightsOut;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LightsOutTests
{
    public class Tests
    {
        private Form1 f;
        [SetUp]
        public void Setup()
        {
            f = new Form1();
        }

        /// <summary>
        /// A test to make sure that a grid of green buttons is not counted as a win state
        /// </summary>
        [Test]
        public void GameIsNotInWinStateAllGreenButtons()
        {
            List<Button> l = new List<Button>();
            for (var i = 0; i < 25; i++)
            {
                Button b = new Button();
                b.BackColor = Color.Green;
                l.Add(b);
            }
            Assert.AreEqual(f.checkWinState(l), false);
        }

        /// <summary>
        /// A test to make sure that a grid of white buttons, but one green button, is not counted as a win state
        /// </summary>
        [Test]
        public void GameIsNotInWinStateOneGreenButton()
        {
            List<Button> l = new List<Button>();
            for (var i = 0; i < 25; i++)
            {
                Button b = new Button();
                b.BackColor = Color.White;
                l.Add(b);
            }
            l[13].BackColor = Color.Green;
            Assert.AreEqual(f.checkWinState(l), false);
        }


        /// <summary>
        /// A test to make sure that a grid of white buttons is counted as a win state
        /// </summary>
        [Test]
        public void GameIsInWinState()
        {
            List<Button> l = new List<Button>();
            for (var i = 0; i < 25; i++)
            {
                Button b = new Button();
                b.BackColor = Color.White;
                l.Add(b);
            }
            Assert.AreEqual(f.checkWinState(l), true);
        }


        /// <summary>
        /// Test to make sure that a button that is green is successfully toggled to white
        /// </summary>
        [Test]
        public void ButtonToggleToWhiteTest()
        {
            Button myButton = new Button();
            myButton.BackColor = Color.Green;
            f.toggleColour(myButton);
            Assert.AreEqual(myButton.BackColor, Color.White);
        }

        /// <summary>
        /// Test to make sure that a button that is white is successfully toggled to green
        /// </summary>
        [Test]
        public void ButtonToggleToGreenTest()
        {
            Button myButton = new Button();
            myButton.BackColor = Color.White;
            f.toggleColour(myButton);

            Assert.AreEqual(myButton.BackColor, Color.Green);
        }
    }
}