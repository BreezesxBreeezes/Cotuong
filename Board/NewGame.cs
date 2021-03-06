using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Board
{
    public partial class NewGame : Form
    {
        public NewGame()
        {
            InitializeComponent();
        }

        private void BatDau_MouseLeave(object sender, EventArgs e)
        {
            BatDau.Image = Board.Properties.Resources.BatDau;
        }

        private void BatDau_MouseEnter(object sender, EventArgs e)
        {
            BatDau.Image = Board.Properties.Resources.BatDau_MouseOver;
        }

        private void BatDau_Click(object sender, EventArgs e)
        {
            if (TenQuanDo.Text != "" && TenQuanDen.Text != "")
            {
                Management.nameOfGamer1 = TenQuanDo.Text;
                Management.nameOfGamer2 = TenQuanDen.Text;
                TenQuanDo.Text = "";
                TenQuanDen.Text = "";

                //Thiết lập lại bộ đếm thời gian
                Management.minuteOfGamer1 = 15;
                Management.secondsOfGamer1 = 0;
                Management.minuteOfGamer2 = 15;
                Management.secondsOfGamer2 = 0;
                
                if (TinhThoiGian.Checked)
                {
                    Management.isTimeCal = true;
                    if(ChonThoiGian.SelectedIndex==0)
                    {
                        Management.minuteOfGamer1 = 5;
                        Management.minuteOfGamer2 = 5;
                    }
                    if (ChonThoiGian.SelectedIndex == 1)
                    {
                        Management.minuteOfGamer1 = 10;
                        Management.minuteOfGamer2 = 10;
                    }
                    if (ChonThoiGian.SelectedIndex == 2)
                    {
                        Management.minuteOfGamer1 = 15;
                        Management.minuteOfGamer2 = 15;
                    }
                }
                else Management.isTimeCal = false;
                Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : 00";
                Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : 00";
                ChessBoard.Timer_NguoiChoi1.Enabled = false;
                ChessBoard.Timer_NguoiChoi2.Enabled = false;
                
                //tạo ván cờ mới
                Management.NewGame();                
                
                if (Chap.Checked) Management.isCondescend = true;
                else Management.isCondescend = false;
                if (Management.isCondescend) Management.pnlCondescend.Visible = true;
                else if (Management.isTimeCal == true) ChessBoard.Timer_NguoiChoi1.Enabled = true;                
                this.Close();
            }
            else MessageBox.Show("Nhập tên 2 người chơi");
        }

        private void Close_NewGame_Form_MouseEnter(object sender, EventArgs e)
        {
            Close_NewGame_Form.Image = Board.Properties.Resources.cCancel_MouseOver;
        }

        private void Close_NewGame_Form_MouseLeave(object sender, EventArgs e)
        {
            Close_NewGame_Form.Image = Board.Properties.Resources.cCancel;
        }

        private void Close_NewGame_Form_Click(object sender, EventArgs e)
        {
            //Mở lại bộ đếm thời gian
            if (Management.isTimeCal)
            {
                if (Management.Turn == 0)
                {
                    ChessBoard.Timer_NguoiChoi1.Enabled = true;
                    ChessBoard.Timer_NguoiChoi2.Enabled = false;
                }
                if (Management.Turn == 1)
                {
                    ChessBoard.Timer_NguoiChoi2.Enabled = true;
                    ChessBoard.Timer_NguoiChoi1.Enabled = false;
                }
            }

            //Mở lại bàn cờ
            if (Management.Turn == 0)
            {
                Management.Turn = 1;
                Management.ChangeMove();
            }
            else
            {
                Management.Turn = 0;
                Management.ChangeMove();
            }
            this.Close();
        }

        private void TinhThoiGian_CheckedChanged(object sender, EventArgs e)
        {
            if (TinhThoiGian.Checked)
            {
                ChonThoiGian.Enabled = true;
            }
            else ChonThoiGian.Enabled = false;
        }            
    }
}