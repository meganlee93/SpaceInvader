using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Player : Shape
    {
        public Player(int x, int y, int width, int height, int xSpeed = 0, int ySpeed = 0) : base(x,y,width,height,xSpeed, ySpeed)
        {

        }

        public void updatePlayer(int playerWidth)
        {
            x += xSpeed;
            if(x < 0)
            {
                x = 0;
            }

            else if(x + width > playerWidth)
            {
                x = playerWidth - width;
            }
        }
    }
}
