using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jump
{
    class DefaultData
    {
        public int playerX;
        public int playerY;
        public int playerMaxY;
        public int playerWidth;
        public int playerHeight;

        public int obstacleX;
        public int obstacleY;
        public int obstacleMaxX;
        public int obstacleWidth;
        public int obstacleHeight;

        public DefaultData()
        {
            this.playerX = 70;
            this.playerY = 138;
            this.playerWidth = 60;
            this.playerHeight = 100;
            this.playerMaxY = 20;

            this.obstacleX = 816;
            this.obstacleY = 188;
            this.obstacleWidth = 52;
            this.obstacleHeight = 42;
            this.obstacleMaxX = 0 - obstacleWidth;
        }
    }
}
