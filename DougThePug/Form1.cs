using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DougThePug
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, hasAllCoins, pugPosition;

        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        int playerSpeed = 10;
        int backgroundSpeed = 8;

        int poopSpeed = 1;
        int poop2Speed = 1;
        int poop3Speed = 1;

        int cloudSpeed = 1;
        int cloud2Speed = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            //change the direction of pug!
            if(goRight == true)
            {
                pugPosition = true;
                pug.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                // pug.Image.RotateFlip(RotateFlipType.RotateNoneFlipX); // picture flips only once when touches boundary
                //while(pugPosition == true)
                //{
                //    pug.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                //}
            }
            else if(goLeft == true)
            {
                pugPosition = false;
                pug.Image.RotateFlip(RotateFlipType.RotateNoneFlipX); // picture flips only once when touches boundary
            }
        
            //txtScore.Text = "Score: " + score;

            //if(goLeft == true && pug.Left > 60)
            //{
            //    pug.Left -= playerSpeed;
            //}
            //if(goRight == true && pug.Left + (pug.Width) < this.ClientSize.Width)
            //{
            //    pug.Left += playerSpeed;
            //}

            //if(goLeft == true && background.Left < 0)
            //{
            //    background.Left += backgroundSpeed;
            //    MoveGameElements("forward");
            //}
            //if(goRight == true && background.Left > 1372)
            //{
            //    background.Left -= backgroundSpeed;
            //    MoveGameElements("back");
            //}
            {
                // linking the jumpspeed integer with the player picture boxes to location
                pug.Top += jumpSpeed;

                // refresh the player picture box consistently
                pug.Refresh();

                // if jumping is true and force is less than 0
                // then change jumping to false
                if (jumping && force < 0)
                {
                    jumping = false;
                }

                // if jumping is true
                // then change jump speed to -12 
                // reduce force by 1
                if (jumping)
                {
                    jumpSpeed = -12;
                    force -= 1;
                }
                else
                {
                    // else change the jump speed to 12
                    jumpSpeed = 12;
                }

                // if go left is true and players left is greater than 100 pixels
                // only then move player towards left of the 
                if (goLeft && pug.Left > 100)
                {
                    pug.Left -= playerSpeed;
                }
                // by doing the if statement above, the player picture will stop on the forms left


                // if go right Boolean is true
                // player left plus players width plus 100 is less than the forms width
                // then we move the player towards the right by adding to the players left
                if (goRight && pug.Left + (pug.Width + 100) < this.ClientSize.Width)
                {
                    pug.Left += playerSpeed;

                }
                // by doing the if statement above, the player picture will stop on the forms right


                // if go right is true and the background picture left is greater 1352
                // then we move the background picture towards the left
                if (goRight && background.Left > -1353)
                {
                    background.Left -= backgroundSpeed;

                    // the for loop below is checking to see the platforms and coins in the level
                    // when they are found it will move them towards the left
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "heart" || x is PictureBox && (string)x.Tag == "vortex" || x is PictureBox && (string)x.Tag == "poop" || x is Label && (string)x.Tag == "directions") ;
                        {
                            x.Left -= backgroundSpeed;
                        }
                    }

                }

                // if go left is true and the background pictures left is less than 2
                // then we move the background picture towards the right
                if (goLeft && background.Left < 2)
                {
                    background.Left += backgroundSpeed;

                    // below the is the for loop thats checking to see the platforms and coins in the level
                    // when they are found in the level it will move them all towards the right with the background
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "heart" || x is PictureBox && (string)x.Tag == "vortex" || x is PictureBox && (string)x.Tag == "poop")
                        {
                            x.Left += backgroundSpeed;
                        }
                    }
                }


                // below if the for loop thats checking for all of the controls in this form
                foreach (Control x in this.Controls)
                {
                    // is X is a picture box and it has a tag of platform
                    if (x is PictureBox && (string)x.Tag == "platform")
                    {
                        // then we are checking if the player is colliding with the platform
                        // and jumping is set to false
                        if (pug.Bounds.IntersectsWith(x.Bounds) && !jumping)
                        {
                            // then we do the following
                            force = 8; // set the force to 8
                            pug.Top = x.Top - pug.Height; // also we place the player on top of the picture box
                            jumpSpeed = 0; // set the jump speed to 0
                        }
                    }
                    // if the picture box found has a tag of coin
                    if (x is PictureBox && (string)x.Tag == "heart")
                    {
                        // now if the player collides with the coin picture box
                        if (pug.Bounds.IntersectsWith(x.Bounds))
                        {
                            this.Controls.Remove(x); // then we are going to remove the coin image
                            score++; // add 1 to the score
                        }
                    }
                }

                // if the player collides with the door and has key boolean is true

                //if (pug.Bounds.IntersectsWith (poop.Bounds))
                //{
                //    // then we change the image of the door to open

                //    door.Image = Properties.Resources.door_open;
                //    // and we stop the timer
                //    gameTimer.Stop();
                //    MessageBox.Show("Oh no! You Died!"); // show the message box
                //}

                // if the player collides with the key picture box

                //if (pug.Bounds.IntersectsWith(key.Bounds))
                //{

                //    // then we remove the key from the game
                //    this.Controls.Remove(key);
                //    // change the has key boolean to true
                //    hasKey = true;
                //}


                // this is where the player dies
                // if the player goes below the forms height then we will end the game
                if (pug.Top + pug.Height > this.ClientSize.Height + 60)
                {
                    gameTimer.Stop(); // stop the timer
                    MessageBox.Show("You Died!!!"); // show the message box
                }
            }
            //poop/cloud movement

            cloud.Top += cloudSpeed;
            if (cloud.Top < 53 || cloud.Top > 133)
            {
                cloudSpeed = -cloudSpeed;
            }
            poop.Top += poopSpeed;
            if (poop.Top < 336 || poop.Top > 405)
            {
                poopSpeed = -poopSpeed;
            }

            poop2.Top += poopSpeed;
            if (poop2.Top < 138 || poop2.Top > 196)
            {
                poop2Speed = -poop2Speed;
            }

            poop3.Top += poopSpeed;
            if(poop3.Top < 226 || poop3.Top > 280)
            {
                poop3Speed = -poop3Speed;
            }
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if(e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if(jumping == true)
            {
                jumping = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {

        }

        private void RestartGame()
        {

        }

        private void MoveGameElements(string direction)
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "poop" || x is PictureBox && (string)x.Tag == "sparkle" || x is PictureBox && (string)x.Tag == "vortex" || x is PictureBox && (string)x.Tag == "heart")
                {
                    if(direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }
                    if(direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }
                }
            }
        }
    }
}
