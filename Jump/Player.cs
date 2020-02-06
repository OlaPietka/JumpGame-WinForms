using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jump
{
    class Player
    {
        public int xPos { get; set; }
        public int yPos { get; set; }
        public int maxPos { get;}
        public int width { get;}
        public int height { get;}

        public Player()
        {
            this.xPos = new DefaultData().playerX;
            this.yPos = new DefaultData().playerY;
            this.maxPos = new DefaultData().playerMaxY;
            this.width = new DefaultData().playerWidth;
            this.height = new DefaultData().playerHeight;
        }
    }
}
