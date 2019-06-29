using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        Graphics gfx;
        Bitmap map;
        Player user;
        Enemy enemy;
        List<Enemy> enemies;
        List<List<Enemy>> totalEnemies;
        List<Bullet> bullets;
        List<Bullet> enemyBullets;
        Bullet enemyBullet;
        bool shoot;
        bool attack;

        bool lower;

        int list;
        int num;

        bool hit;

        bool delete = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(map);
            user = new Player(ClientSize.Width / 2, ClientSize.Height - 25, 50, 25);
            bullets = new List<Bullet>();
            enemyBullets = new List<Bullet>();
            
            totalEnemies = new List<List<Enemy>>();
            generateEnemies();
            shoot = false;
            attack = false;

            lower = false;
            hit = false;
        }

        public void generateEnemies()
        {
            int height = 0;
            for(int i = 0; i < 3; i++)
            {
                enemies = new List<Enemy>();
                for (int j = 10; j < ClientSize.Width; j += 70)
                {
                    enemyBullet = new Bullet(j + (50 / 2), height + 25, 5, 5, 0, 10);
                    //enemyBullets.Add(enemyBullet);
                    Enemy newEnemy = new Enemy(enemyBullet, j, height, 50, 25, 5);
                    enemies.Add(newEnemy);
                    if(enemies.Count >= 5)
                    {
                        break;
                    }
                }
                height += 30;
                totalEnemies.Add(enemies);
            }
        }

        private void bulletCheck()
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                
                bullets[i].Update(ClientSize.Width, ClientSize.Height);


                for (int j = 0; j < totalEnemies.Count; j++)
                {
                    for (int k = 0; k < totalEnemies[i].Count; k++)
                    {
                        if (bullets[i].HitBox.IntersectsWith(totalEnemies[j][k].HitBox))
                        {
                            hit = true;
                            totalEnemies[j].Remove(totalEnemies[j][k]);
                            bullets.Remove(bullets[i]);
                            if (i > 0)
                            {
                                i--;
                                
                             
                            }
                            delete = true;
                            break;
                            
                        }

                    }

                    if (delete)
                    {
                        break;
                    }
                }
                if (i >= 0 && bullets[i].y < 0 && !hit)
                {
                    bullets.Remove(bullets[i]);
                    if (i > 0)
                    {
                        i--;
                    }

                }
                hit = false;

            }            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = $"{bullets.Count}";
            gfx.Clear(Color.Transparent);

         
            

            //draw player
            user.Draw(Brushes.Pink, gfx);

            //update bullets -------- need to fix
            //for(int i = 0; i < bullets.Count; i++)
            //{
            //    bullets[i].Update(ClientSize.Width, ClientSize.Height);
            //    if (bullets[i].y < 0)
            //    {
            //        bullets.Remove(bullets[i]);
            //        i--;
            //    }
            //}

            //update enemies
            for (int i = 0; i < totalEnemies.Count; i++)
            {
                for (int j = 0; j < totalEnemies[i].Count; j++)
                {
                    totalEnemies[i][j].Update();
                }
            }

            //change direction of enemies based on edges
            //for (int i = 0; i < totalEnemies.Count; i++)
            //{
            //    for (int j = 0; j < enemies.Count; j++)
            //    {
            //        if(totalEnemies[i][enemies.Count - 1].x + totalEnemies[i][enemies.Count - 1].width > ClientSize.Width)
            //        {
            //            totalEnemies[i][enemies.Count - 1].xSpeed = -Math.Abs(totalEnemies[i][enemies.Count - 1].xSpeed);
            //            lower = true;
            //        }

            //        else if(totalEnemies[i][0].x < 0)
            //        {
            //            totalEnemies[i][enemies.Count - 1].xSpeed = Math.Abs(totalEnemies[i][enemies.Count - 1].xSpeed);
            //            lower = true;
            //        }

            //        totalEnemies[i][j].xSpeed = totalEnemies[i][enemies.Count - 1].xSpeed;
            //    }
            //}

            for (int i = 0; i < totalEnemies.Count; i++)
            {
                for (int j = 0; j < totalEnemies[i].Count; j++)
                {
                    //if (totalEnemies[i][enemies.Count - 1].x + totalEnemies[i][enemies.Count - 1].width > ClientSize.Width)
                    if(totalEnemies[i][0].x + 350 > ClientSize.Width)
                    {
                        totalEnemies[i][totalEnemies[i].Count - 1].xSpeed = -Math.Abs(totalEnemies[i][totalEnemies[i].Count - 1].xSpeed);
                        lower = true;
                    }

                    else if (totalEnemies[i][0].x < 0)
                    {
                        totalEnemies[i][totalEnemies[i].Count - 1].xSpeed = Math.Abs(totalEnemies[i][totalEnemies[i].Count - 1].xSpeed);
                        lower = true;
                    }

                    totalEnemies[i][j].xSpeed = totalEnemies[i][totalEnemies[i].Count - 1].xSpeed;
                }
            }

            if (lower)
            {
                for (int i = 0; i < totalEnemies.Count; i++)
                {
                    for (int j = 0; j < totalEnemies[i].Count; j++)
                    {
                        totalEnemies[i][j].y += 30;
                    }
                }
                lower = false;
            }

            user.updatePlayer(ClientSize.Width);

            if(shoot == true)
            { 
                bullets.Add(new Bullet(user.x + (user.width / 2) - 5, user.y - user.height - 5,5, 5, 0, -15));

                shoot = false;
            }


            if (attack == true)
            {
                //totalEnemies[list][num].bullet.Draw(Brushes.Red, gfx);
                //totalEnemies[list][num].bullet.Update(ClientSize.Height);
                for(int i = 0; i < enemyBullets.Count; i++)
                {
                    enemyBullets[i].Draw(Brushes.Red, gfx);
                    if(enemyBullets[i].Update(ClientSize.Height))
                    {
                        enemyBullets.Remove(enemyBullets[i]);
                        i--;
                        break;
                    }
                }
            }
               //draw bullet
            for(int i = 0; i<bullets.Count; i++)
            {
                bullets[i].Draw(Brushes.Green, gfx);
            }

            bulletCheck();

            //draw enemies
            for (int i = 0; i < totalEnemies.Count; i++)
            {
                for (int j = 0; j < totalEnemies[i].Count; j++)
                {
                    totalEnemies[i][j].Draw(Brushes.Blue, gfx);
                }
            }

            pictureBox1.Image = map;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                user.xSpeed = -20;
            }

            else if(e.KeyCode == Keys.Right)
            {
                user.xSpeed = 20;
            }

            if(e.KeyCode == Keys.Up)
            {
                shoot = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            user.xSpeed = 0;
        }

        private void EnemyBulletTimer_Tick(object sender, EventArgs e)
        {
            if(enemyBulletTimer.Interval >= 1500)
            {
               
                    Random random = new Random();
                    list = random.Next(0, totalEnemies.Count);
                    num = random.Next(0, totalEnemies[list].Count);

                    enemyBullet = new Bullet(totalEnemies[list][num].x + (totalEnemies[list][num].width / 2), totalEnemies[list][num].y + totalEnemies[list][num].height, 5, 5, 0, 20);
                    //totalEnemies[list][num].bullet = enemyBullet;
                    enemyBullets.Add(enemyBullet);
                    attack = true;
               
            }
        }
    }
}
