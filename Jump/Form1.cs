using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Jump
{
    public partial class Form1 : Form
    {
        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

        Player player = new Player();
        Obstacle obstacle = new Obstacle();
        DefaultData data = new DefaultData();
        bool startGame = false;
        bool keyPress;
        bool jump;
        bool touch;
        bool jumpSuccess;
        int distanceHorizontal;
        int distanceVertical;
        int speed;
        int lvlSpeed;
        int score;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NewGame();

            this.Timer.Tick += new EventHandler(Timer_Tick); 
            this.Timer.Interval = 1;
            this.Timer.Enabled = true;
            this.Timer.Stop();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (player.yPos == data.playerY && startGame)
                {
                    System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Jump.Properties.Resources.jump);
                    sp.Play();

                    keyPress = true;
                    jump = true;
                }
            }
            if (e.KeyCode == Keys.Enter && this.NewGameLabel.Enabled)
                NewGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Touch();

            JumpPlayer();

            MoveObsticle();

            PlayerScore();
         }

        private void NewGameLabel_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void JumpPlayer()
        {
            if (keyPress)
            {
                if (jump) // do max wyskoku 
                {
                    player.yPos -= 8;
                    this.PlayerBox.Location = new Point(player.xPos, player.yPos);

                    if (player.yPos <= player.maxPos)
                        jump = false;
                }
                else // jak wroci na miejsce
                {
                    player.yPos += 8;
                    this.PlayerBox.Location = new Point(player.xPos, player.yPos);

                    if (player.yPos == data.playerY)
                        keyPress = false;
                }
            }
        }

        private void MoveObsticle()
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Jump.Properties.Resources.missile);

            if (obstacle.xPos != obstacle.maxPos) // Jesli nie jest poza granicami
            {
                if(data.obstacleX - speed < obstacle.xPos && obstacle.xPos < data.obstacleX + speed)
                    sp.Play();

                this.ObstacleBox.Location = new Point(obstacle.xPos, obstacle.yPos);
                obstacle.xPos -= speed;
            }
            if (obstacle.xPos <= obstacle.maxPos) // Jesli znajdzie sie poza granicami
            {
                Random random = new Random();

                int randX = random.Next(816, 1200 + speed*50);
                obstacle.xPos = randX;

                this.ObstacleBox.Location = new Point(obstacle.xPos, obstacle.yPos);

                jumpSuccess = true;

                Speed();
            }
        }

        private void Touch()
        {
            distanceHorizontal = obstacle.xPos - player.xPos;
            distanceVertical = obstacle.yPos - player.yPos;

            if (distanceHorizontal <= player.width && distanceVertical <= player.height - 30)
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Jump.Properties.Resources.die);
                sp.Play();

                GameOver();
                touch = true;
            }
        }

        private void Speed()
        {
            if (score % 2 == 1)
            {
                speed++;
                lvlSpeed++;

                this.SpeedLvl.Text = lvlSpeed.ToString();
            }
        }

        private void PlayerScore()
        {
            if (jumpSuccess && !touch)
            {
                score++;
                this.Score.Text = score.ToString();
            }

            jumpSuccess = false;
        }

        private void GameOver()
        {
            this.PlayerBox.Image = Jump.Properties.Resources.gameEnd;

            startGame = false;

            this.GameOverLabel.Visible = true;
            this.NewGameLabel.Visible = true;
            this.NewGameLabel.Enabled = true;
            this.YourScoreLabel.Visible = true;

            this.ObstacleBox.Enabled = false;
            this.ObstacleBox.Visible = false;

            this.YourScoreLabel.Text = "Your score: " + score.ToString();

            this.Timer.Stop();
            this.ObstacleBox.Enabled = false;
        }

        private void NewGame()
        {
            keyPress = false;
            jump = false;
            jumpSuccess = false;
            touch = false;
            score = 0;
            speed = 10;
            lvlSpeed = 1;

            this.PlayerBox.Image = Jump.Properties.Resources.startgame;

            this.GameOverLabel.Visible = false;
            this.NewGameLabel.Visible = false;
            this.NewGameLabel.Enabled = false;
            this.YourScoreLabel.Visible = false;
            this.PlayerBox.Visible = true;
            this.PlayerBox.Enabled = true;
            this.ObstacleBox.Visible = true;
            this.ObstacleBox.Enabled = true;

            player.xPos = data.playerX;
            player.yPos = data.playerY;

            obstacle.xPos = data.obstacleX;
            obstacle.yPos = data.obstacleY;

            this.ObstacleBox.Location = new Point(obstacle.xPos, obstacle.yPos);

            this.Score.Text = score.ToString();
            this.SpeedLvl.Text = lvlSpeed.ToString();

            Count();
        }

        async Task Count()
        {
            this.Timer.Stop();

            this.Counting.Visible = true;

            System.Media.SoundPlayer spTick = new System.Media.SoundPlayer(Jump.Properties.Resources.tick);
            System.Media.SoundPlayer spStart = new System.Media.SoundPlayer(Jump.Properties.Resources.start);

            this.Counting.ForeColor = Color.IndianRed;
            this.Counting.Text = "3";
            spTick.Play();
            await Task.Delay(250);
            this.Counting.Text = "3.";
            await Task.Delay(250);
            this.Counting.Text = "3..";
            await Task.Delay(250);
            this.Counting.Text = "3...";
            await Task.Delay(250);

            this.Counting.ForeColor = Color.Khaki;
            this.Counting.Text = "2";
            spTick.Play();
            await Task.Delay(250);
            this.Counting.Text = "2.";
            await Task.Delay(250);
            this.Counting.Text = "2..";
            await Task.Delay(250);
            this.Counting.Text = "2...";
            await Task.Delay(250);

            this.Counting.ForeColor = Color.LemonChiffon;
            this.Counting.Text = "1";
            spTick.Play();
            await Task.Delay(250);
            this.Counting.Text = "1.";
            await Task.Delay(250);
            this.Counting.Text = "1..";
            await Task.Delay(250);
            this.Counting.Text = "1...";
            await Task.Delay(250);

            this.Counting.ForeColor = Color.White;
            this.Counting.Text = "GO!";
            spStart.Play();
            await Task.Delay(1000);

            this.Counting.Visible = false;

            startGame = true;

            this.Timer.Start();
        }
    }
}
