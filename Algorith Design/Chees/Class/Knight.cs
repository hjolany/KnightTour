using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightTour.Algorith_Design.Chees.Class
{ 
    public class Position
    {
        public int xPos;
        public int yPos;
    }

    class Knight
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

        IPiececs mPiece;
        public Knight(IPiececs sender)
        {
            mPiece = sender;
            matrixX = ((ChessBoard)sender).Width;
            matrixY = ((ChessBoard)sender).Height;
        }
        public Knight(int Width,int Height)
        {
            matrixX = Width;
            matrixY = Height;
        }
 
        public Position Move(int _x, int _y)
        {
            if ((0 <= _x && _x < matrixX) && (0 <= _y && _y < matrixY))
            {
                X = _x;
                Y = _y;
                if (mPiece != null)
                    mPiece.onMove(new Position() { xPos = x, yPos = y });
                return new Position() { xPos = x, yPos = y };
            }
            return null;
        }

        Position MoveRU()
        {
            int _x = X;
            int _y = Y;
            _x += 2;
            _y--;
            return Move(_x, _y);
        }
        Position MoveRD()
        {
            int _x = X;
            int _y = Y;
            _x += 2;
            _y++;
            return Move(_x, _y);
        }
        Position MoveLU()
        {
            int _x = X;
            int _y = Y;
            _x -= 2;
            _y--;
            return Move(_x, _y);
        }
        Position MoveLD()
        {
            int _x = X;
            int _y = Y;
            _x -= 2;
            _y++;
            return Move(_x, _y);
        }
        Position MoveUR()
        {
            int _x = X;
            int _y = Y;
            _x++;
            _y -= 2;
            return Move(_x, _y);
        }
        Position MoveUL()
        {
            int _x = X;
            int _y = Y;
            _x--;
            _y -= 2;
            return Move(_x, _y);
        }
        Position MoveDR()
        {
            int _x = X;
            int _y = Y;
            _x++;
            _y += 2;
            return Move(_x, _y);
        }
        Position MoveDL()
        {
            int _x = X;
            int _y = Y;
            _x--;
            _y += 2;
            return Move(_x, _y);
        }
        Position GetPosition()
        {
            return new Position() { xPos = X, yPos = Y };
        }
        public List<Position> Movements()
        {
            Position tmpPosition = new Position();
            List<Position> lstPosition = new List<Position>();
            Knight h = new Knight(matrixX,matrixY);

            h.X = this.X;
            h.Y = this.Y;
            tmpPosition = h.MoveRD();
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
