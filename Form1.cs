using KnightTour.Algorith_Design.Chees.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KnightTour
{
    public partial class frmKnightTour : Form
    {
        /// <summary>
        ///  Count of the valued cell in chessboard
        /// </summary>
        int _Counter = 0;
        /// <summary>
        /// the color of the cells
        /// </summary>
        Color _Color;
        /// <summary>
        /// movement position stack
        /// </summary>
        Stack<Position> _Stack = new Stack<Position>();
        /// <summary>
        /// the ChessBoard class
        /// </summary>
        ChessBoard _ChessBoard;

        public frmKnightTour()
        {
            InitializeComponent();
            _ChessBoard = new ChessBoard();
            _Color = Color.Green;
            //ShowRanking();
        }


        /// <summary>
        /// the click event for all cells, just for trace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseClick(object sender, EventArgs e)
        {
            /*if (ModifierKeys == Keys.Shift)
            {
                if (((Button)sender).Text != "")
                {
                    _Counter--;
                    ((Button)sender).Text = "";
                    ((Button)sender).BackColor = SystemColors.Control;
                }
            }
            else
            {
                if (((Button)sender).Text == "")
                {
                    ((Button)sender).Text = (++_Counter).ToString();
                    ((Button)sender).BackColor = _Color;
                }
            }*/
            int x = 0;
            int y = 0;

            for (int i = 1; i < 65; i++)
            {
                if (((Button)sender).Name == "button" + (i).ToString())
                {
                    x = ((i - (i / 8) * 8) == 0 ? i < 8 ? 1 : 8 : (i - (i / 8) * 8));
                    y = (i + 7) / 8;
                }
            }
            _Start = new Position() { xPos = x - 1, yPos = y - 1 };
            ((Button)sender).BackColor = _Color;
        }

        private void btnNoColor_Click(object sender, EventArgs e)
        {
            _Color = Color.BlueViolet;
        }

        private void btnColor1_Click(object sender, EventArgs e)
        {
            _Color = Color.Red;
        }

        private void btnColor2_Click(object sender, EventArgs e)
        {
            _Color = Color.YellowGreen;
        }

        /// <summary>
        /// reset the configuration to the initialized status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            _Stack.Clear();
            _Counter = 0;
            foreach (Control ctrl in pnlChess.Controls)
            {
                ctrl.Text = "";
                //ctrl.BackgroundImage = base.BackgroundImage;
                //ctrl.BackColor = SystemColors.Control; 
            }
            Clear();
            btnStart.Enabled = true;
            btnReset.Enabled = false;
        }

        Position _Start;
        Hole[,] ordBoard = new Hole[8, 8];
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_Start == null)
            {
                MessageBox.Show("Select Start Point","knight tour",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _ChessBoard = new ChessBoard();
            if (newOrder)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        _ChessBoard.Board[i, j].Rank = ordBoard[i, j].Rank;
                    }
                }
            }
            txtResult.AppendText("\r\n" + DateTime.Now.Ticks);
            Thread thr = new Thread(() => TakeTour(_Start.xPos, _Start.yPos));
            thr.Start();
            //chessBoard.knight.SetBasePos(new Position(){xPos=0,yPos=0});
            _ChessBoard.OnMoved += chessBoard_OnMoved;
        }
        int _CurCount = 0;

        /// <summary>
        ///  recursive main function to have a tour 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private void TakeTour(int x, int y)
        {
            //System.Threading.Thread.Sleep(100);
            if (chkStep.Checked)
            {
                while (_CurCount == _Step)
                {
                    System.Threading.Thread.Sleep(100);
                }
                _CurCount++;
            }
            if (_ChessBoard.Board[x, y].value == 0)
            {
                if (_ChessBoard.knight.Move(x, y) != null)
                {
                    if (_Stack.Count == 64)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            txtResult.AppendText("\r\n" + DateTime.Now.Ticks);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                long t1 = (long)Convert.ToDouble(txtResult.Lines[txtResult.Lines.Count() - 2]);
                                long t2 = (long)Convert.ToDouble(txtResult.Lines[txtResult.Lines.Count() - 1]);
                                txtResult.AppendText("\r\n----------------------------------------\r\n");
                                //txtResult.AppendText((t2 - t1).ToString());                                
                                DateTime dt2 = new DateTime(t2);
                                DateTime dt1 = new DateTime(t1);
                                txtResult.AppendText(dt2.Subtract(dt1).ToString()+"\r\n\r\n");
                                btnStart.Enabled = false;
                                btnReset.Enabled = true;
                            }));
                        }));
                        return;
                    }
                    if (_ChessBoard.Locked())
                    {
                        RollBack();
                        return;
                    }

                    List<Hole> AvailabeHoles = new List<Hole>();
                    foreach (Position p in _ChessBoard.knight.Movements())
                    {
                        if (_ChessBoard.Board[p.xPos, p.yPos].value == 0)
                            AvailabeHoles.Add(_ChessBoard.Board[p.xPos, p.yPos]);
                    }
                    var Q = AvailabeHoles;
                    if (_Stack.Count < 63)
                        Q = AvailabeHoles.Where(w => w.lockProbability < 1 && w.value == 0)
                            .OrderBy(o1 => o1.Rank)
                            .ThenBy(o2 => 64 - o2.position.yPos * 8 + o2.position.xPos)
                            .ThenBy(o3 => o3.lockProbability).ToList();
                    else
                    {
                        //_Color = Color.Yellow;
                        Q = AvailabeHoles.Where(w => w.value == 0).OrderBy(o1 => o1.Rank).ThenBy(o2 => o2.lockProbability).ToList();
                    }

                    foreach (Hole h in Q)
                    {
                        TakeTour(h.position.xPos, h.position.yPos);
                    }
                    if (Q.Count() == 0)
                    {
                        RollBack();
                        return;
                    }
                }
            }
            if (_Stack.Count == 64)
            {
                Application.ExitThread();
                return;
            }
            RollBack();
        }

        private void Commit(Position p)
        {
            _Stack.Push(p);
            if (!chkGraphic.Checked || _Stack.Count()==64)
            {
                foreach (Control ctrl in pnlChess.Controls)
                {
                    {
                        if (ctrl.Name == "button" + (((p.yPos) * 8) + p.xPos + 1).ToString())
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                ctrl.Text = _ChessBoard.Board[p.xPos, p.yPos].value.ToString();
                                //ctrl.BackgroundImage = Properties.Resources.knight001;
                                if (_Stack.Count() == 64)
                                    ctrl.BackColor = Color.Yellow;
                                else
                                    ctrl.BackColor = _Color;
                            }));
                        }
                    }
                }
            }
        }
        private Position RollBack()
        {
            Position CurP = _Stack.Pop();
            Position BackP = _Stack.First();
            _ChessBoard.Board[CurP.xPos, CurP.yPos].value = 0;
            _ChessBoard.CalculateLockProbability(CurP);
            _ChessBoard.CalculateLockProbability(BackP);
            if (!chkGraphic.Checked)
            {
                foreach (Control ctrl in pnlChess.Controls)
                {
                    {
                        if (ctrl.Name == "button" + (((CurP.yPos) * 8) + CurP.xPos + 1).ToString())
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                ctrl.Text = _ChessBoard.Board[CurP.xPos, CurP.yPos].Rank.ToString();
                                //ctrl.BackgroundImage = Properties.Resources.knight002;
                                ctrl.BackColor = SystemColors.Control;
                            }));
                        }
                    }
                }
            }
            return BackP;
        }

        private void ShowRanking()
        {
            foreach (Control ctrl in pnlChess.Controls)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        pnlChess.Controls.Find("button" + (((j) * 8) + i + 1).ToString(), true)[0]
                            .Text = _ChessBoard.Board[i, j].Rank.ToString();
                    }
            }
        }

        void chessBoard_OnMoved(Hole[,] Board, Position p)
        {
            Commit(p);
        }

        int _Step = 0;
        private void btnStep_Click(object sender, EventArgs e)
        {
            _Step++;
        }

        private void Clear()
        {
            foreach (Control ctrl in pnlChess.Controls)
            {
                for (int i = 1; i < 65; i++)
                {
                    if (ctrl.Name == "button" + (i).ToString())
                    {
                        ctrl.Text = "";
                        //ctrl.BackgroundImage = Properties.Resources.knight002;
                        ctrl.BackColor = SystemColors.Control;
                    }
                }
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
        }

        private void order1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        bool newOrder = false;
        private void saveCurrentOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ordBoard[i, j] = new Hole();
                    ordBoard[i, j].Rank = Convert.ToInt32(pnlChess.GetControlFromPosition(i, j).Text);
                }
            }
            newOrder = true;
        }

        private void deleteOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newOrder = true;
        }

        private void chkStep_CheckedChanged(object sender, EventArgs e)
        {
            btnStep.Enabled = chkStep.Checked;
            if (!chkStep.Checked)
                _Step++;
        }
    }
}
