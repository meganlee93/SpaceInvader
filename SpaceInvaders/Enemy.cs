using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Enemy : Shape
    {
        public Enemy(int x, int y, int width, int height, int xSpeed, int ySpeed = 0) : base(x,y,width,height,xSpeed,ySpeed)
        {

        }
    }
}
