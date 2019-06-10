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
        List<Bullet> bullets;
        Bullet enemyBullet;
        bool shoot;
        bool attack;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(map);
            user = new Player(ClientSize.Width / 2, ClientSize.Height - 25, 50, 25);
            enemy = new Enemy(0, 0, 50, 25, 5);
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            enemies.Add(enemy);
            shoot = false;
            //enemyBullet = new Bullet();
            attack = false;
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

            //user.drawPlayer(gfx);
            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(Brushes.Green, gfx);
            }

            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(Brushes.Blue, gfx);
            }
            //enemy.Draw(Brushes.Blue, gfx);
            user.Draw(Brushes.Pink, gfx);


            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(ClientSize.Width, ClientSize.Height);
                if (bullets[i].y < 0)
                {
                    bullets.RemoveAt(i);
                }
            }

            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(ClientSize.Width, ClientSize.Height);
            }
            //enemy.Update(ClientSize.Width, ClientSize.Height);
            user.updatePlayer(ClientSize.Width);

            if(shoot == true)
            { 
                bullets.Add(new Bullet(user.x + (user.width / 2) - 5, user.y - user.height - 5,5, 5, 0, -5));

                //attack = true;
                shoot = false;
            }

            if(attack == true)
            {
                enemyBullet.Draw(Brushes.Red, gfx);
                enemyBullet.Update(ClientSize.Width, ClientSize.Height);
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
                attack = true;
            }
        }
    }
}
