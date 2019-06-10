using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Shape
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int xSpeed { get; set; }
        public int ySpeed { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle(x, y, width, height);
            }
        }
        public Shape(int x, int y, int width, int height, int xSpeed, int ySpeed)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }

        public void Draw(Brush brush, Graphics gfx)
        {
            gfx.FillRectangle(brush, new Rectangle(x, y, width, height));
        }

        public void Update(int widthSize, int heightSize)
        {
            x += xSpeed;
            y += ySpeed;

            if(x < 0)
            {
                xSpeed = Math.Abs(xSpeed);
            }

            else if( x + width > widthSize)
            {
                xSpeed = -Math.Abs(xSpeed);
            }

            if(y < 0)
            {
                ySpeed = Math.Abs(ySpeed);
            }

            else if( y + height > heightSize)
            {
                ySpeed = -Math.Abs(ySpeed);
            }
        }
    }
}
