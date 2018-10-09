using knightTour.Algorith_Design.Chess.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightTour.Algorith_Design.Chees.Class
{
    delegate void myEvent(Hole[,] Board, Position p);
    class Hole
    {
        public int Rank = 0;
        public double lockProbability = 0;
        public int value = 0;
        public Position position;
    }
    class ChessBoard : IPiececs
    {
        public event myEvent OnMoved;

        public int Width = 8;
        public int Height = 8;
        public Knight knight;
        public Hole[,] Board;
        public ChessBoard()
        {
            knight = new Knight(this);
            initialize();
        }

        //public void Run(int x, int y)
        //{
        //    TakeTour(x, y);
        //} 

        public int GetCount()
        {
            int Count = 0;
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    if (Board[i, j].value != 0)
                        Count++;
                }
            return Count;
        }
        void initialize()
        {
            Board = new Hole[Width, Height];
            knight = new Knight(this);
            Knight tmpknight = new Knight(Width, Height);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tmpknight.Move(i, j);
                    Board[i, j] = new Hole()
                    {
                        Rank = tmpknight.Movements().Count(),
                        value = 0,
                        lockProbability = 0,
                        position = new Position() { xPos = i, yPos = j }
                    };
                }
            }
        }

        public void SetRanking(int[,] Ranks)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board[i, j].Rank = Ranks[i, j];
                }
            }
        }

        public void onMove(Position position)
        {
            Board[position.xPos, position.yPos].value = GetCount() + 1;
            CalculateLockProbability(position);
            foreach (Position p in knight.Movements())
            {
                CalculateLockProbability(p);
            }
            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
            //        CalculateLockProbability(new Position() { xPos = i, yPos = j });
            //    }
            //}
            OnMoved(Board, position);
        }
        public void CalculateLockProbability(Position position)
        {
            /*if (Board[position.xPos, position.yPos].value != 0)
            {
                Board[position.xPos, position.yPos].lockProbability = 1;
                return;
            }*/

            Knight _k = new Knight(Width, Height);
            _k.Move(position.xPos, position.yPos);
            int p = 0;
            foreach (Position pos in _k.Movements())
            {
                if (Board[pos.xPos, pos.yPos].value != 0)
                {
                    p++;
                }
            }
            Board[position.xPos, position.yPos].lockProbability = (double)p / _k.Movements().Count();
            Board[position.xPos, position.yPos].position = position;
        }
        public Boolean Locked()
        {
            int Cnt = 0;
            bool locked = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CalculateLockProbability(new Position() { xPos = i, yPos = j });
                    if ((Board[i, j].lockProbability == 1) && (Board[i, j].value == 0))
                        locked = true;
                    if (Board[i, j].value == 0)
                        Cnt++;
                }
            }
            if (locked)
            {
                if (Cnt == 1)
                    return false;
                return true;
            }
            return false;
        }
    }
}
