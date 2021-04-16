using AllPayInterviewLightsOut;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LightsOutTests
{
    public class Tests
    {
        private Form1 TestForm { get; set; }
        [SetUp]
        public void Setup()
        {
            TestForm = new Form1();
        }

        /// <summary>
        /// A test to make sure that a grid of green buttons is not counted as a win state
        /// </summary>
        [Test]
        public void GameIsNotInWinStateAllGreenButtons()
        {
            TestForm.SetUpGame();
            List<Button> list = new List<Button>();
            for (var i = 0; i < 25; i++)
            {
                Button button = new Button
                {
                    BackColor = Color.Green
                };
                list.Add(button);
            }
            Assert.AreEqual(TestForm.CheckWinState(list), false);
        }

        /// <summary>
        /// A test to make sure that a grid of white buttons, but one green button, is not counted as a win state
        /// </summary>
        [Test]
        public void GameIsNotInWinStateOneGreenButton()
        {
            List<Button> list = new List<Button>();
            for (var i = 0; i < 25; i++)
            {
                Button button = new Button
                {
                    BackColor = Color.White
                };
                list.Add(button);
            }
            list[13].BackColor = Color.Green;
            Assert.AreEqual(TestForm.CheckWinState(list), false);
        }


        /// <summary>
        /// A test to make sure that a grid of white buttons is counted as a win state
        /// </summary>
        [Test]
        public void GameIsInWinState()
        {
            List<Button> list = new List<Button>();
            for (var i = 0; i < 25; i++)
            {
                Button button = new Button
                {
                    BackColor = Color.White
                };
                list.Add(button);
            }
            Assert.AreEqual(TestForm.CheckWinState(list), true);
        }


        /// <summary>
        /// Test to make sure that a button that is green is successfully toggled to white
        /// </summary>
        [Test]
        public void ButtonToggleToWhiteTest()
        {
            Button button = new Button
            {
                BackColor = Color.Green
            };
            TestForm.ToggleColour(button);
            Assert.AreEqual(button.BackColor, Color.White);
        }

        /// <summary>
        /// Test to make sure that a button that is white is successfully toggled to green
        /// </summary>
        [Test]
        public void ButtonToggleToGreenTest()
        {
            Button button = new Button
            {
                BackColor = Color.White
            };
            TestForm.ToggleColour(button);

            Assert.AreEqual(button.BackColor, Color.Green);
        }
    }
}