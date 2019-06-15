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

        bool changeEnemyDirection;
        bool lower;

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

            changeEnemyDirection = false;
            lower = false;
        }

        public void generateEnemies()
        {
            int height = 0;
            for(int i = 0; i < 3; i++)
            {
                enemies = new List<Enemy>();
                for (int j = 10; j < ClientSize.Width; j += 70)
                {
                    Enemy newEnemy = new Enemy(j, height, 50, 25, 5);
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
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (bullets[i].HitBox.IntersectsWith(enemies[j].HitBox))
                    {
                        enemies.RemoveAt(j);
                        bullets.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(Color.Transparent);

            //draw bullet
            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(Brushes.Green, gfx);
            }

            //draw enemies
            for(int i = 0; i < totalEnemies.Count; i++)
            {
                for(int j = 0; j < enemies.Count; j++)
                {
                    totalEnemies[i][j].Draw(Brushes.Blue, gfx);
                }
            }

            //draw player
            user.Draw(Brushes.Pink, gfx);

            //update bullets -------- need to fix
            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(ClientSize.Width, ClientSize.Height);
                if (bullets[i].y < 0)
                {
                    bullets.RemoveAt(i);
                }
            }

            //update enemies
            for(int i = 0; i < totalEnemies.Count; i++)
            {
                for(int j = 0; j < enemies.Count; j++)
                {
                    totalEnemies[i][j].Update();
                }
            }

            if (lower)
            {
                for (int i = 0; i < totalEnemies.Count; i++)
                {
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        totalEnemies[i][j].y += 30;
                    }
                }
                lower = false;
            }

            //change direction of enemies based on edges
            for (int i = 0; i < totalEnemies.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if(totalEnemies[i][enemies.Count - 1].x + totalEnemies[i][enemies.Count - 1].width > ClientSize.Width)
                    {
                        totalEnemies[i][enemies.Count - 1].xSpeed = -Math.Abs(totalEnemies[i][enemies.Count - 1].xSpeed);
                        lower = true;
                    }

                    else if(totalEnemies[i][0].x < 0)
                    {
                        totalEnemies[i][enemies.Count - 1].xSpeed = Math.Abs(totalEnemies[i][enemies.Count - 1].xSpeed);
                        lower = true;
                    }

                    totalEnemies[i][j].xSpeed = totalEnemies[i][enemies.Count - 1].xSpeed;
                }
            }

            user.updatePlayer(ClientSize.Width);

            if(shoot == true)
            { 
                bullets.Add(new Bullet(user.x + (user.width / 2) - 5, user.y - user.height - 5,5, 5, 0, -5));

                //attack = true;
                shoot = false;
            }

            if(attack == true)
            {
                for(int i =0; i < enemyBullets.Count; i++)
                {
                    enemyBullets[i].Draw(Brushes.Red, gfx);
                    enemyBullets[i].Update(ClientSize.Width, ClientSize.Height);
                    if(enemyBullets[i].y > ClientSize.Height)
                    {
                        enemyBullets.RemoveAt(i);
                    }

                }
               // enemyBullet.Draw(Brushes.Red, gfx);
               //enemyBullet.Update(ClientSize.Width, ClientSize.Height);
            }

            bulletCheck();
            pictureBox1.Image = map;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                user.xSpeed = -10;
            }

            else if(e.KeyCode == Keys.Right)
            {
                user.xSpeed = 10;
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
                enemyBullet = new Bullet(enemies[0].x + (enemies[0].width / 2), enemies[0].y + enemies[0].height, 5, 5, 0, 10);
                enemyBullets.Add(enemyBullet);
                attack = true;
            }
        }
    }
}
