using knightTour.Algorith_Design.Chess.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightTour.Algorith_Design
{
    public partial class frmHorse : Form
    {
        public frmHorse()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            foreach (Control ctrl in mainPage.Controls)
            {
                ctrl.Text = "";
            }
        }

        int MatrixX = 8;
        int MatrixY = 8;
        int MaxCount = 64;
        int _CurCount = 0;
        //List<List<int>> ChessBoard = new List<List<int>>();
        int[,] ChessBoard = new int[8, 8];
        private void Run()
        {
            for (int a1 = 0; a1 < MatrixX; a1++)
                for (int a2 = 0; a2 < MatrixY; a2++)
                {
                    ChessBoard[a1, a2] = 0;
                }

            Horse horse = new Horse(MatrixX, MatrixY);
            for (int i = 1; i <= MatrixX; i++)
                for (int j = MatrixY; j >= 1; j--)
                {
                    horse.X = i;
                    horse.Y = j;
                    Move(horse);
                }
            int cc = 0;
        }

        Stack<Position> _Stack = new Stack<Position>();
        private bool CheckStack(Horse H, Position p)
        {
            if (_Stack.Where(c => c.xPos == p.xPos && c.yPos == p.yPos).Count() == 0)
            {
                _Stack.Push(p); ChessBoard[p.xPos - 1, p.yPos - 1] = _Stack.Count();
                mainPage.GetControlFromPosition(p.xPos - 1, MatrixX - p.yPos).BackColor = Color.Yellow;
                mainPage.GetControlFromPosition(p.xPos - 1, MatrixX - p.yPos).ForeColor = Color.Black;
                this.Invoke(new MethodInvoker(delegate
                {
                    lstStack.Items.Add(p.xPos + "," + p.yPos);
                    mainPage.GetControlFromPosition(p.xPos - 1, MatrixX - p.yPos).Text = DeadLockProbability(H).ToString();
                    //_Stack.Count().ToString();
                }));
                return true;
            }
            else
            {
                Position _RollBack = _Stack.First();
                H.Move(_RollBack);
                return false;
            }
        }

        bool _Continue;
        private void Move(Horse H)
        {
            while (!_Continue)
            {
                System.Threading.Thread.Sleep(1);
            }
            System.Threading.Thread.Sleep(5);
            /*
            System.Threading.Thread.Sleep(10);
            while (_CurCount == _Step)
            {
                System.Threading.Thread.Sleep(1);
            }*/

            _CurCount++;
            if (_Stack.Count == MaxCount)
                return;


            if (!CheckStack(H, new Position() { xPos = H.X, yPos = H.Y }))
                return;

            if (!CheckRule(H))
                goto XX;

            //H.MoveCount

            if (H.MoveRD() != null)
            {
                Move(H);
            }
            if (H.MoveRU() != null)
            {
                Move(H);
            }
            if (H.MoveUR() != null)
            {
                Move(H);
            }
            if (H.MoveUL() != null)
            {
                Move(H);
            }
            if (H.MoveLU() != null)
            {
                Move(H);
            }
            if (H.MoveLD() != null)
            {
                Move(H);
            }
            if (H.MoveDL() != null)
            {
                Move(H);
            }
            if (H.MoveDR() != null)
            {
                Move(H);
            } 
            XX:
            Position _RollBack = _Stack.Pop(); ChessBoard[H.X - 1, H.Y - 1] = 0;
            //-----------------------------------------
             this.Invoke(new MethodInvoker(delegate
            {
                lstStack.Items.RemoveAt(lstStack.Items.Count - 1);

                if (((H.X - 1) % 2) == ((MatrixX - H.Y) % 2))
                {
                    mainPage.GetControlFromPosition(H.X - 1, MatrixX - H.Y).BackColor = Color.White;
                    mainPage.GetControlFromPosition(H.X - 1, MatrixX - H.Y).ForeColor = Color.Black;
                    mainPage.GetControlFromPosition(H.X - 1, MatrixX - H.Y).Text = "";
                }
                else
                {
                    mainPage.GetControlFromPosition(H.X - 1, MatrixX - H.Y).BackColor = Color.Black;
                    mainPage.GetControlFromPosition(H.X - 1, MatrixX - H.Y).ForeColor = Color.White;
                    mainPage.GetControlFromPosition(H.X - 1, MatrixX - H.Y).Text = "";
                }
            }));
            //-----------------------------------------
            if (_Stack.Count == 0)
                return;
            H.Move(/*_RollBack*/_Stack.First());
        }

        private bool CheckRule(Horse H)
        {
            int _Sum = 0;
            bool _Full = false;
            //R130
            //------------------------------------------------------------------------------------------
            if (H.X < 5)
            {
                _Full = true;
                for (int i = 0; i < 4; i++)
                {
                    _Sum += ChessBoard[H.Y-1, i];
                    if (ChessBoard[H.Y - 1, i] == 0)
                        _Full = false;
                }
                if (_Sum > 130)
                    return false;
                else if((_Sum == 130) && (_Full))
                        return true;
                else if ((_Sum < 130) && _Full)
                    return false;
            }
            else
            {
                _Full = true;
                for (int i = 4; i < 8; i++)
                {
                    _Sum += ChessBoard[H.Y-1, i];
                    if (ChessBoard[H.Y - 1, i] == 0)
                        _Full = false;
                }
                if (_Sum > 130)
                    return false;
                else if ((_Sum == 130) && (_Full))
                    return true;
                else if ((_Sum < 130) && _Full)
                    return false;
            }
            //C130
            //------------------------------------------------------------------------------------------
            if (H.Y < 5)
            {
                _Full = true;
                for (int j = 0; j < 4; j++)
                {
                    _Sum += ChessBoard[j, H.X-1];
                    if (ChessBoard[j, H.X - 1] == 0)
                        _Full = false;
                }
                if (_Sum > 130)
                    return false;
                else if ((_Sum == 130) && (_Full))
                    return true;
                else if ((_Sum < 130) && _Full)
                    return false;
            }
            else
            {
                _Full = true;
                for (int j = 4; j < 8; j++)
                {
                    _Sum += ChessBoard[j, H.X-1];
                    if (ChessBoard[j, H.X - 1] == 0)
                        _Full = false;
                }
                if (_Sum > 130)
                    return false;
                else if ((_Sum == 130) && (_Full))
                    return true;
                else if ((_Sum < 130) && _Full)
                    return false;
            }
            //R260
            //------------------------------------------------------------------------------------------
            _Full = true;
            for (int i = 0; i < 8; i++)
            {
                _Sum += ChessBoard[H.Y-1, i];
                if (ChessBoard[H.Y - 1, i] == 0)
                    _Full = false;
            }
            if (_Sum > 260)
                return false;
            else if ((_Sum == 260) && (_Full))
                return true;
            else if ((_Sum < 260) && _Full)
                return false;
            //C260
            //------------------------------------------------------------------------------------------
            _Full = true;
            for (int j = 0; j < 8; j++)
            {
                _Sum += ChessBoard[j, H.X-1];
                if (ChessBoard[j, H.X - 1] == 0)
                    _Full = false;
            }
            if (_Sum > 260)
                return false;
            else if ((_Sum == 260) && (_Full))
                return true;
            else if ((_Sum < 260) && _Full)
                return false;


            //Q1-520
            //------------------------------------------------------------------------------------------
            _Full = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 4; j < 8; j++)
                {
                    _Sum += ChessBoard[j, i];
                    if(ChessBoard[j, i] == 0)
                        _Full = false;
                }
            }
            if (_Sum > 520)
                return false;
            else if ((_Sum == 520) && (_Full))
                return true;
            else if ((_Sum < 520) && _Full)
                return false;

            //Q2-520
            //------------------------------------------------------------------------------------------
            _Full = true;
            for (int i = 4; i < 8; i++)
            {
                for (int j = 4; j < 8; j++)
                {
                    _Sum += ChessBoard[j, i];
                    if (ChessBoard[j, i] == 0)
                        _Full = false;
                }
            }
            if (_Sum > 520)
                return false;
            else if ((_Sum == 520) && (_Full))
                return true;
            else if ((_Sum < 520) && _Full)
                return false;

            //Q3-520
            //------------------------------------------------------------------------------------------
            _Full = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _Sum += ChessBoard[j, i];
                    if (ChessBoard[j, i] == 0)
                        _Full = false;
                }
            }
            if (_Sum > 520)
                return false;
            else if ((_Sum == 520) && (_Full))
                return true;
            else if ((_Sum < 520) && _Full)
                return false;

            //Q4-520
            //------------------------------------------------------------------------------------------
            _Full = true;
            for (int i = 4; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _Sum += ChessBoard[j, i];
                    if (ChessBoard[j, i] == 0)
                        _Full = false;
                }
            }
            if (_Sum > 520)
                return false;
            else if ((_Sum == 520) && (_Full))
                return true;
            else if ((_Sum < 520) && _Full)
                return false;


            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[7, 0] != 0) && (ChessBoard[7, 1] != 0) && (ChessBoard[6, 0] != 0) && (ChessBoard[6, 1] != 0))
            {
                if (ChessBoard[7, 0] + ChessBoard[7, 1] + ChessBoard[6, 0] + ChessBoard[6, 1] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[7, 0] + ChessBoard[7, 1] + ChessBoard[6, 0] + ChessBoard[6, 1] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[7, 2] != 0) && (ChessBoard[7, 3] != 0) && (ChessBoard[6, 2] != 0) && (ChessBoard[6, 3] != 0))
            {
                if (ChessBoard[7, 2] + ChessBoard[7, 3] + ChessBoard[6, 2] + ChessBoard[6, 3] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[7, 2] + ChessBoard[7, 3] + ChessBoard[6, 2] + ChessBoard[6, 3] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[7, 4] != 0) && (ChessBoard[7, 5] != 0) && (ChessBoard[6, 4] != 0) && (ChessBoard[6, 5] != 0))
            {
                if (ChessBoard[7, 4] + ChessBoard[7, 5] + ChessBoard[6, 4] + ChessBoard[6, 5] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[7, 4] + ChessBoard[7, 5] + ChessBoard[6, 4] + ChessBoard[6, 5] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[7, 6] != 0) && (ChessBoard[7, 7] != 0) && (ChessBoard[6, 6] != 0) && (ChessBoard[6, 7] != 0))
            {
                if (ChessBoard[7, 6] + ChessBoard[7, 7] + ChessBoard[6, 6] + ChessBoard[6, 7] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[7, 6] + ChessBoard[7, 7] + ChessBoard[6, 6] + ChessBoard[6, 7] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[5, 0] != 0) && (ChessBoard[5, 1] != 0) && (ChessBoard[4, 0] != 0) && (ChessBoard[4, 1] != 0))
            {
                if (ChessBoard[5, 0] + ChessBoard[5, 1] + ChessBoard[4, 0] + ChessBoard[4, 1] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[5, 0] + ChessBoard[5, 1] + ChessBoard[4, 0] + ChessBoard[4, 1] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[5, 2] != 0) && (ChessBoard[5, 3] != 0) && (ChessBoard[4, 2] != 0) && (ChessBoard[4, 3] != 0))
            {
                if (ChessBoard[5, 2] + ChessBoard[5, 3] + ChessBoard[4, 2] + ChessBoard[4, 3] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[5, 2] + ChessBoard[5, 3] + ChessBoard[4, 2] + ChessBoard[4, 3] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[5, 4] != 0) && (ChessBoard[5, 5] != 0) && (ChessBoard[4, 4] != 0) && (ChessBoard[4, 5] != 0))
            {
                if (ChessBoard[5, 4] + ChessBoard[5, 5] + ChessBoard[4, 4] + ChessBoard[4, 5] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[5, 4] + ChessBoard[5, 5] + ChessBoard[4, 4] + ChessBoard[4, 5] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[5, 6] != 0) && (ChessBoard[5, 7] != 0) && (ChessBoard[4, 6] != 0) && (ChessBoard[4, 7] != 0))
            {
                if (ChessBoard[5, 6] + ChessBoard[5, 7] + ChessBoard[4, 6] + ChessBoard[4, 7] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[5, 6] + ChessBoard[5, 7] + ChessBoard[4, 6] + ChessBoard[4, 7] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[3, 0] != 0) && (ChessBoard[3, 1] != 0) && (ChessBoard[2, 0] != 0) && (ChessBoard[2, 1] != 0))
            {
                if (ChessBoard[3, 0] + ChessBoard[3, 1] + ChessBoard[2, 0] + ChessBoard[2, 1] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[3, 0] + ChessBoard[3, 1] + ChessBoard[2, 0] + ChessBoard[2, 1] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[3, 2] != 0) && (ChessBoard[3, 3] != 0) && (ChessBoard[2, 2] != 0) && (ChessBoard[2, 3] != 0))
            {
                if (ChessBoard[3, 2] + ChessBoard[3, 3] + ChessBoard[2, 2] + ChessBoard[2, 3] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[3, 2] + ChessBoard[3, 3] + ChessBoard[2, 2] + ChessBoard[2, 3] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[3, 4] != 0) && (ChessBoard[3, 5] != 0) && (ChessBoard[2, 4] != 0) && (ChessBoard[2, 5] != 0))
            {
                if (ChessBoard[3, 4] + ChessBoard[3, 5] + ChessBoard[2, 4] + ChessBoard[2, 5] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[3, 4] + ChessBoard[3, 5] + ChessBoard[2, 4] + ChessBoard[2, 5] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[3, 6] != 0) && (ChessBoard[3, 7] != 0) && (ChessBoard[2, 6] != 0) && (ChessBoard[2, 7] != 0))
            {
                if (ChessBoard[3, 6] + ChessBoard[3, 7] + ChessBoard[2, 6] + ChessBoard[2, 7] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[3, 6] + ChessBoard[3, 7] + ChessBoard[2, 6] + ChessBoard[2, 7] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[1, 0] != 0) && (ChessBoard[1, 1] != 0) && (ChessBoard[0, 0] != 0) && (ChessBoard[0, 1] != 0))
            {
                if (ChessBoard[1, 0] + ChessBoard[1, 1] + ChessBoard[0, 0] + ChessBoard[0, 1] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[1, 0] + ChessBoard[1, 1] + ChessBoard[0, 0] + ChessBoard[0, 1] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[1, 2] != 0) && (ChessBoard[1, 3] != 0) && (ChessBoard[0, 2] != 0) && (ChessBoard[0, 3] != 0))
            {
                if (ChessBoard[1, 2] + ChessBoard[1, 3] + ChessBoard[0, 2] + ChessBoard[0, 3] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[1, 2] + ChessBoard[1, 3] + ChessBoard[0, 2] + ChessBoard[0, 3] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[1, 4] != 0) && (ChessBoard[1, 5] != 0) && (ChessBoard[0, 4] != 0) && (ChessBoard[0, 5] != 0))
            {
                if (ChessBoard[1, 4] + ChessBoard[1, 5] + ChessBoard[0, 4] + ChessBoard[0, 5] != 130)
                    return false;
            }
            else
            {
                if (ChessBoard[1, 4] + ChessBoard[1, 5] + ChessBoard[0, 4] + ChessBoard[0, 5] > 130)
                    return false;
            }
            //Q16-01
            //------------------------------------------------------------------------------------------
            if ((ChessBoard[1, 6] != 0) && (ChessBoard[1, 7] != 0) && (ChessBoard[0, 6] != 0) && (ChessBoard[0, 7] != 0))
            {
                if (ChessBoard[1, 6] + ChessBoard[1, 7] + ChessBoard[0, 6] + ChessBoard[0, 7] > 130)
                    return false;
            }
            else
            {
                if (ChessBoard[1, 6] + ChessBoard[1, 7] + ChessBoard[0, 6] + ChessBoard[0, 7] > 130)
                    return false;
            }

            return true;
        }

        private double DeadLockProbability(Horse H)
        {
            int x = 0;
            int y = 0;
            foreach (Control ctrl in mainPage.Controls)
            {
                for (int i = 1; i < 65; i++)
                {
                    if (ctrl.Name.ToLower() == "label" + (i).ToString())
                    {
                        x = ((i - (i / 8) * 8) == 0 ? i < 8 ? 1 : 8 : (i - (i / 8) * 8));
                        y = (i + 7) / 8;
                    }
                }
            }
            /*ToolTip tp = new ToolTip();
            tp.SetToolTip(((Label)sender), x.ToString() + ":" + y.ToString());
            mainPage.GetControlFromPosition(x - 1, y - 1).BackColor = Color.Red;*/
            int _ClosedPosition=0;
            foreach (Position pos in H.MoveCount())
            {
                if (ChessBoard[pos.xPos - 1, pos.yPos - 1] != 0)
                    _ClosedPosition++;
            }
            return ((double)_ClosedPosition / (double)H.MoveCount().Count);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _Stack.Clear();
            Thread thr = new Thread(() => Run());
            thr.Start();
            //Run();
        }

        int _Step = 0;
        private void btnStep_Click(object sender, EventArgs e)
        {
            _Step++;
            if (_Continue)
                _Continue = false;
            else
                _Continue = true;
        }

    }
}
