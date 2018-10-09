using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knightTour.Algorith_Design.Chess.Class
{
    public class Position
    {
        public int xPos;
        public int yPos;
    }

    class Horse
    {
        private int x;
        private int y;
        private int matrixX;
        private int matrixY;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public int MatrixX { get { return matrixX; } set { matrixX = value; } }
        public int MatrixY { get { return matrixY; } set { matrixY = value; } }

        public Horse(int W,int H)
        {
            MatrixX = W;
            MatrixY = H;
        }

        public void Move(Position p)
        {
            X = p.xPos;
            Y = p.yPos;
        }
        private Position Move(int _x, int _y)
        {
            if ((0 < _x && _x <= matrixX) && (0 < _y && _y <= matrixY))
            {
                X = _x;
                Y = _y;
                return new Position() { xPos = x, yPos = y };
            }
            return null;
        }

        public Position MoveRU()
        {
            int _x = X;
            int _y = Y;
            _x += 2;
            _y++;
            return Move(_x, _y);
        }
        public Position MoveRD()
        {
            int _x = X;
            int _y = Y;
            _x += 2;
            _y--;
            return Move(_x, _y);
        }
        public Position MoveLU()
        {
            int _x = X;
            int _y = Y;
            _x -= 2;
            _y++;
            return Move(_x, _y);
        }
        public Position MoveLD()
        {
            int _x = X;
            int _y = Y;
            _x -= 2;
            _y--;
            return Move(_x, _y);
        }
        public Position MoveUR()
        {
            int _x = X;
            int _y = Y;
            _x++;
            _y += 2;
            return Move(_x, _y);
        }
        public Position MoveUL()
        {
            int _x = X;
            int _y = Y;
            _x--;
            _y += 2;
            return Move(_x, _y);
        }
        public Position MoveDR()
        {
            int _x = X;
            int _y = Y;
            _x++;
            _y -= 2;
            return Move(_x, _y);
        }
        public Position MoveDL()
        {
            int _x = X;
            int _y = Y;
            _x--;
            _y -= 2;
            return Move(_x, _y);
        }
        public Position GetPosition()
        {
            return new Position() { xPos = X, yPos = Y };
        }
        public List<Position> MoveCount()
        {
            Position tmpPosition = new Position();
            List<Position> lstPosition = new List<Position>();
            Horse h = new Horse(MatrixX,MatrixY);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition=h.MoveRD();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveRU();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveUR();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveUL();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveLU();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveLD();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveDL();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveDR();
            if (tmpPosition != null)
                lstPosition.Add(tmpPosition);

            return lstPosition;
        }
    }
}
