using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Bullet : Shape
    {
        public Bullet(int x, int y, int width, int height, int xSpeed, int ySpeed) : base(x,y,width,height,xSpeed, ySpeed)
        {

        }
    }
}
