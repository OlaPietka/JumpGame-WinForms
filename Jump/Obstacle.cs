using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jump
{
    class Obstacle
    {
        public int xPos { get; set; }
        public int yPos { get; set; }
        public int maxPos { get; }
        public int width { get; }
        public int height { get; }   

        public Obstacle()
        {
            this.xPos = new DefaultData().obstacleX;
            this.yPos = new DefaultData().obstacleY;
            this.maxPos = new DefaultData().obstacleMaxX;
            this.width = new DefaultData().obstacleWidth;
            this.height = new DefaultData().obstacleHeight;
        }
    }
}
