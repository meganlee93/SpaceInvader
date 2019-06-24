using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Enemy : Shape
    {
        public Bullet bullet { get; set; }
        public Enemy(Bullet bullet, int x, int y, int width, int height, int xSpeed, int ySpeed = 0) : base(x,y,width,height,xSpeed,ySpeed)
        {
            this.bullet = bullet;
        }


        public void Update()
        {
            x += xSpeed;
        }

        public void UpdateLast(int ClientSize, int ClientHeight)
        {
            x += xSpeed;
            if(x +  width > ClientSize)
            {
                xSpeed = -Math.Abs(xSpeed);
            }
        }

        public bool checkLast(int ClientSize)
        {
            if(x + width > ClientSize)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
