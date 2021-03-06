using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Board
{
    class Management
    {
        #region Khai Báo 
        public struct NuocDi
        {
            public ChessPiece Head;
            public ChessPiece Tail;
            //----------------
            public int headRow;
            public int headColumn;
            //----------------
            public int tailRow;
            public int tailColumn;
        }
        public struct DeathChess    // quân đã chết  - bị ăn
        {
            public int Row;
            public int Column;
            public PictureBox picChessPiece;
        }
        public static Player[] Gamer = new Player[2];
        public static string nameOfGamer1;
        public static string nameOfGamer2;
        public static bool isPlaying = false;
        public static int Turn = 0; //lượt đi
        public static int Winner = 2;
        public static bool isCondescend = false;

        //Thông báo chiếu tướng, chiếu bí, kết quả, chọn cờ chấp
        public static PictureBox picNotifyCheckmate = new PictureBox();
        //--------------------------------
        public static Panel pnlCheckMate = new Panel();
        public static PictureBox picReMove = new PictureBox();
        public static PictureBox picSurrender = new PictureBox();
        //--------------------------------
        public static Panel pnlResult = new Panel();
        public static Label lblWinner = new Label();
        public static PictureBox picRePlay = new PictureBox();
        public static PictureBox picExit = new PictureBox();
        //--------------------------------
        public static Panel pnlCondescend = new Panel();
        public static PictureBox picCondescended = new PictureBox();
        public static PictureBox picTurnOfGamer1 = new PictureBox();
        public static PictureBox picTurnOfGamer2 = new PictureBox();
        public static Bitmap BackBuffer = null;

        //Chọn quân cờ 
        public static bool Marked = false; //Kiểm tra đã có quân cờ nào được chọn chưa
        public static ChessPiece ChessMarked; //Quân cờ DanhDau tham chiếu đến quân cờ được chọn trong 1 nước đi
        //Nhật kí các nước đi (dùng cho Undo)
        public static NuocDi[] GameLog;
        public static int turnCount = 0;//Lưu tổng số lượt đi của ván cờ        
        //Các quân cờ bị ăn
        public static DeathChess[] Death_Red;
        public static int count_Red = 0;
        public static DeathChess[] Death_Black;
        public static int count_Black = 0;
        //Bộ đếm thời gian & chấp cờ
        public static bool isTimeCal = false;
        public static int minuteOfGamer1, secondsOfGamer1 = 0;
        public static Label lblTimeOfGamer1 = new Label();
        public static int minuteOfGamer2, secondsOfGamer2 = 0;
        public static Label lblTimeOfGamer2 = new Label();
        //Options
        public static bool isSound = true;
        public static bool isSoundTrack = true;
        public static string Path_NhacNen = @Application.StartupPath + "\\a.mp3";
        #endregion

        //Play mp3 files
        [DllImport("winmm.dll")]
        public static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        static Management()
        {
            //Khởi tạo 2 người chơi: NguoiChoi[0] phe 0, NguoiChoi[1] phe 1;
            Gamer[0] = new Player(0);
            Gamer[1] = new Player(1);
            // 
            // ThongBaoChieuTuong
            // 
            picNotifyCheckmate.BackColor = Color.Transparent;
            picNotifyCheckmate.Image = Board.Properties.Resources.ChieuTuong;
            picNotifyCheckmate.Width = 266;
            picNotifyCheckmate.Height = 93;
            picNotifyCheckmate.Top = 9;
            picNotifyCheckmate.Left = 551;
            picNotifyCheckmate.Visible = false;
            // 
            // P1_Turn
            // 
            picTurnOfGamer1.BackColor = Color.Transparent;
            picTurnOfGamer1.Width = 41;
            picTurnOfGamer1.Height = 41;
            picTurnOfGamer1.Top = 154;
            picTurnOfGamer1.Left = 757;
            picTurnOfGamer1.Image = Board.Properties.Resources.Turning;
            // 
            // P2_Turn
            // 
            picTurnOfGamer2.BackColor = Color.Transparent;
            picTurnOfGamer2.Width = 41;
            picTurnOfGamer2.Height = 41;
            picTurnOfGamer2.Top = 414;
            picTurnOfGamer2.Left = 757;
            picTurnOfGamer2.Image = Board.Properties.Resources.NotTurn;
            // 
            // ChieuBi
            // 
            pnlCheckMate.BackColor = System.Drawing.Color.Transparent;
            pnlCheckMate.BackgroundImage = Board.Properties.Resources.ChieuBi;
            pnlCheckMate.Controls.Add(picSurrender);
            pnlCheckMate.Controls.Add(picReMove);
            pnlCheckMate.Top = 9;
            pnlCheckMate.Left = 551;
            pnlCheckMate.Size = new System.Drawing.Size(266, 93);
            pnlCheckMate.Visible = false;
            // 
            // DiLai
            // 
            picReMove.Image = Board.Properties.Resources.DiLai;
            picReMove.Location = new System.Drawing.Point(67, 60);
            picReMove.Size = new System.Drawing.Size(68, 20);
            // 
            // ChiuThua
            // 
            picSurrender.Image = Board.Properties.Resources.ChiuThua;
            picSurrender.Location = new System.Drawing.Point(149, 60);
            picSurrender.Size = new System.Drawing.Size(68, 20);
            // 
            // KetQua
            // 
            pnlResult.BackColor = System.Drawing.Color.Transparent;
            pnlResult.BackgroundImage = Board.Properties.Resources.Ketquabg;
            pnlResult.Controls.Add(lblWinner);
            pnlResult.Controls.Add(picRePlay);
            pnlResult.Controls.Add(picExit);
            pnlResult.Top = 9;
            pnlResult.Left = 551;
            pnlResult.Size = new System.Drawing.Size(266, 93);
            pnlResult.Visible = false;
            // 
            // NguoiThang
            // 
            lblWinner.Text = "";
            lblWinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblWinner.Location = new System.Drawing.Point(130, 20);
            // 
            // ChoiLai
            // 
            picRePlay.Image = Board.Properties.Resources.ChoiLai;
            picRePlay.Location = new System.Drawing.Point(75, 65);
            picRePlay.Size = new System.Drawing.Size(54, 20);
            // 
            // Thoat
            // 
            picExit.Image = Board.Properties.Resources.Thoat;
            picExit.Location = new System.Drawing.Point(160, 65);
            picExit.Size = new System.Drawing.Size(45, 20);
            // 
            // ChapCo
            // 
            pnlCondescend.BackColor = System.Drawing.Color.Transparent;
            pnlCondescend.BackgroundImage = Board.Properties.Resources.Chap;
            pnlCondescend.Controls.Add(picCondescended);
            pnlCondescend.Top = 9;
            pnlCondescend.Left = 551;
            pnlCondescend.Size = new System.Drawing.Size(266, 93);
            pnlCondescend.Visible = false;
            // 
            // ChapXong
            // 
            picCondescended.Image = Board.Properties.Resources.ChapXong;
            picCondescended.Location = new System.Drawing.Point(105, 55);
            picCondescended.Size = new System.Drawing.Size(81, 20);
            // 
            // ThGian_NguoiChoi1
            // 
            lblTimeOfGamer1.BackColor = System.Drawing.Color.Transparent;
            lblTimeOfGamer1.Top = 9;
            lblTimeOfGamer1.Left = 551;
            lblTimeOfGamer1.Size = new System.Drawing.Size(300, 100);
            lblTimeOfGamer1.Text = "Time: " + Convert.ToString(minuteOfGamer1) + " : 00";
            lblTimeOfGamer1.Location = new System.Drawing.Point(597, 203);
            lblTimeOfGamer1.ForeColor = System.Drawing.Color.Maroon;
            lblTimeOfGamer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ThGian_NguoiChoi2
            // 
            lblTimeOfGamer2.BackColor = System.Drawing.Color.Transparent;
            lblTimeOfGamer2.Top = 9;
            lblTimeOfGamer2.Left = 551;
            lblTimeOfGamer2.Size = new System.Drawing.Size(300, 100);
            lblTimeOfGamer2.Text = "Time: " + Convert.ToString(minuteOfGamer2) + " : 00";
            lblTimeOfGamer2.Location = new System.Drawing.Point(597, 462);
            lblTimeOfGamer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }        

        public static void PlaySoundTrack(bool nhacnen)
        {

            if (nhacnen)
            {
                mciSendString("open \"" + Path_NhacNen + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile REPEAT", null, 0, IntPtr.Zero);
            }
            else mciSendString("close MediaFile", null, 0, IntPtr.Zero);

        }
        #region Chức Năng của button
        public static void NewGame()
        {
            switch (isPlaying)
            {
                case true:
                    //Xóa các quân cờ trên bàn cờ
                    Gamer[0].qGeneral.picChessPiece.Visible = false;
                    Gamer[0].qAdvisor[0].picChessPiece.Visible = false;
                    Gamer[0].qAdvisor[1].picChessPiece.Visible = false;
                    Gamer[0].qElephant[0].picChessPiece.Visible = false;
                    Gamer[0].qElephant[1].picChessPiece.Visible = false;
                    Gamer[0].qChariot[0].picChessPiece.Visible = false;
                    Gamer[0].qChariot[1].picChessPiece.Visible = false;
                    Gamer[0].qCannon[0].picChessPiece.Visible = false;
                    Gamer[0].qCannon[1].picChessPiece.Visible = false;
                    Gamer[0].qHorse[0].picChessPiece.Visible = false;
                    Gamer[0].qHorse[1].picChessPiece.Visible = false;
                    Gamer[0].qSoldier[0].picChessPiece.Visible = false;
                    Gamer[0].qSoldier[1].picChessPiece.Visible = false;
                    Gamer[0].qSoldier[2].picChessPiece.Visible = false;
                    Gamer[0].qSoldier[3].picChessPiece.Visible = false;
                    Gamer[0].qSoldier[4].picChessPiece.Visible = false;
                    Gamer[1].qGeneral.picChessPiece.Visible = false;
                    Gamer[1].qAdvisor[0].picChessPiece.Visible = false;
                    Gamer[1].qAdvisor[1].picChessPiece.Visible = false;
                    Gamer[1].qElephant[0].picChessPiece.Visible = false;
                    Gamer[1].qElephant[1].picChessPiece.Visible = false;
                    Gamer[1].qChariot[0].picChessPiece.Visible = false;
                    Gamer[1].qChariot[1].picChessPiece.Visible = false;
                    Gamer[1].qCannon[0].picChessPiece.Visible = false;
                    Gamer[1].qCannon[1].picChessPiece.Visible = false;
                    Gamer[1].qHorse[0].picChessPiece.Visible = false;
                    Gamer[1].qHorse[1].picChessPiece.Visible = false;
                    Gamer[1].qSoldier[0].picChessPiece.Visible = false;
                    Gamer[1].qSoldier[1].picChessPiece.Visible = false;
                    Gamer[1].qSoldier[2].picChessPiece.Visible = false;
                    Gamer[1].qSoldier[3].picChessPiece.Visible = false;
                    Gamer[1].qSoldier[4].picChessPiece.Visible = false;

                    //Xóa 2 người chơi 
                    Array.Resize<Board.Player>(ref Gamer, 0);

                    //Khởi tạo 2 người chơi mới
                    Array.Resize<Board.Player>(ref Gamer, 2);
                    Gamer[0] = new Player(0);
                    Gamer[1] = new Player(1);

                    //Khởi tạo bàn cờ mới
                    Boad_Game.ResetBoard_Game();
                    Winner = 2;
                    Turn = 0;
                    turnCount = 0;
                    count_Black = 0;
                    count_Red = 0;
                    picNotifyCheckmate.Visible = false;
                    pnlCheckMate.Visible = false;
                    picTurnOfGamer1.Image = Board.Properties.Resources.Turning;
                    picTurnOfGamer2.Image = Board.Properties.Resources.NotTurn;


                    //Đặt các quân cờ của Người Chơi 1
                    Gamer[0].qGeneral.draw();
                    Gamer[0].qAdvisor[0].draw();
                    Gamer[0].qAdvisor[1].draw();
                    Gamer[0].qElephant[0].draw();
                    Gamer[0].qElephant[1].draw();
                    Gamer[0].qChariot[0].draw();
                    Gamer[0].qChariot[1].draw();
                    Gamer[0].qCannon[0].draw();
                    Gamer[0].qCannon[1].draw();
                    Gamer[0].qHorse[0].draw();
                    Gamer[0].qHorse[1].draw();
                    Gamer[0].qSoldier[0].draw();
                    Gamer[0].qSoldier[1].draw();
                    Gamer[0].qSoldier[2].draw();
                    Gamer[0].qSoldier[3].draw();
                    Gamer[0].qSoldier[4].draw();

                    //Đặt các quân cờ của Người Chơi 2
                    Gamer[1].qGeneral.draw();
                    Gamer[1].qAdvisor[0].draw();
                    Gamer[1].qAdvisor[1].draw();
                    Gamer[1].qElephant[0].draw();
                    Gamer[1].qElephant[1].draw();
                    Gamer[1].qChariot[0].draw();
                    Gamer[1].qChariot[1].draw();
                    Gamer[1].qCannon[0].draw();
                    Gamer[1].qCannon[1].draw();
                    Gamer[1].qHorse[0].draw();
                    Gamer[1].qHorse[1].draw();
                    Gamer[1].qSoldier[0].draw();
                    Gamer[1].qSoldier[1].draw();
                    Gamer[1].qSoldier[2].draw();
                    Gamer[1].qSoldier[3].draw();
                    Gamer[1].qSoldier[4].draw();
                    if(Management.isSound) ClickSound("ready");
                    break;

                case false:
                    //Tạo bàn cờ trống
                    Boad_Game.ResetBoard_Game();
                    Management.isPlaying = true;

                    //Đặt các quân cờ của Người Chơi 1
                    Gamer[0].qGeneral.draw();
                    Gamer[0].qAdvisor[0].draw();
                    Gamer[0].qAdvisor[1].draw();
                    Gamer[0].qElephant[0].draw();
                    Gamer[0].qElephant[1].draw();
                    Gamer[0].qChariot[0].draw();
                    Gamer[0].qChariot[1].draw();
                    Gamer[0].qCannon[0].draw();
                    Gamer[0].qCannon[1].draw();
                    Gamer[0].qHorse[0].draw();
                    Gamer[0].qHorse[1].draw();
                    Gamer[0].qSoldier[0].draw();
                    Gamer[0].qSoldier[1].draw();
                    Gamer[0].qSoldier[2].draw();
                    Gamer[0].qSoldier[3].draw();
                    Gamer[0].qSoldier[4].draw();

                    //Đặt các quân cờ của Người Chơi 2
                    Gamer[1].qGeneral.draw();
                    Gamer[1].qAdvisor[0].draw();
                    Gamer[1].qAdvisor[1].draw();
                    Gamer[1].qElephant[0].draw();
                    Gamer[1].qElephant[1].draw();
                    Gamer[1].qChariot[0].draw();
                    Gamer[1].qChariot[1].draw();
                    Gamer[1].qCannon[0].draw();
                    Gamer[1].qCannon[1].draw();
                    Gamer[1].qHorse[0].draw();
                    Gamer[1].qHorse[1].draw();
                    Gamer[1].qSoldier[0].draw();
                    Gamer[1].qSoldier[1].draw();
                    Gamer[1].qSoldier[2].draw();
                    Gamer[1].qSoldier[3].draw();
                    Gamer[1].qSoldier[4].draw();
                    picTurnOfGamer1.Image = Board.Properties.Resources.Turning;
                    picTurnOfGamer2.Image = Board.Properties.Resources.NotTurn;
                    if(Management.isSound) ClickSound("ready");
                    break;
            }
        }

        public static void ChangeMove()
        {
            if (Turn == 0) Turn = 1;
            else Turn = 0;

            if (Management.Turn == 0)
            {
                Management.Gamer[0].qGeneral.isLocked = false;
                Management.Gamer[0].qAdvisor[0].isLocked = false;
                Management.Gamer[0].qAdvisor[1].isLocked = false;
                Management.Gamer[0].qElephant[0].isLocked = false;
                Management.Gamer[0].qElephant[1].isLocked = false;
                Management.Gamer[0].qChariot[0].isLocked = false;
                Management.Gamer[0].qChariot[1].isLocked = false;
                Management.Gamer[0].qCannon[0].isLocked = false;
                Management.Gamer[0].qCannon[1].isLocked = false;
                Management.Gamer[0].qHorse[0].isLocked = false;
                Management.Gamer[0].qHorse[1].isLocked = false;
                Management.Gamer[0].qSoldier[0].isLocked = false;
                Management.Gamer[0].qSoldier[1].isLocked = false;
                Management.Gamer[0].qSoldier[2].isLocked = false;
                Management.Gamer[0].qSoldier[3].isLocked = false;
                Management.Gamer[0].qSoldier[4].isLocked = false;

                Management.Gamer[1].qGeneral.isLocked = true;
                Management.Gamer[1].qAdvisor[0].isLocked = true;
                Management.Gamer[1].qAdvisor[1].isLocked = true;
                Management.Gamer[1].qElephant[0].isLocked = true;
                Management.Gamer[1].qElephant[1].isLocked = true;
                Management.Gamer[1].qChariot[0].isLocked = true;
                Management.Gamer[1].qChariot[1].isLocked = true;
                Management.Gamer[1].qCannon[0].isLocked = true;
                Management.Gamer[1].qCannon[1].isLocked = true;
                Management.Gamer[1].qHorse[0].isLocked = true;
                Management.Gamer[1].qHorse[1].isLocked = true;
                Management.Gamer[1].qSoldier[0].isLocked = true;
                Management.Gamer[1].qSoldier[1].isLocked = true;
                Management.Gamer[1].qSoldier[2].isLocked = true;
                Management.Gamer[1].qSoldier[3].isLocked = true;
                Management.Gamer[1].qSoldier[4].isLocked = true;

                Management.picTurnOfGamer1.Image = Board.Properties.Resources.Turning;
                Management.picTurnOfGamer2.Image = Board.Properties.Resources.NotTurn;

                if (Management.isTimeCal == true)
                {
                    ChessBoard.Timer_NguoiChoi1.Enabled = true;
                    ChessBoard.Timer_NguoiChoi2.Enabled = false;
                }
            }
            if (Management.Turn == 1)
            {
                Management.Gamer[0].qGeneral.isLocked = true;
                Management.Gamer[0].qAdvisor[0].isLocked = true;
                Management.Gamer[0].qAdvisor[1].isLocked = true;
                Management.Gamer[0].qElephant[0].isLocked = true;
                Management.Gamer[0].qElephant[1].isLocked = true;
                Management.Gamer[0].qChariot[0].isLocked = true;
                Management.Gamer[0].qChariot[1].isLocked = true;
                Management.Gamer[0].qCannon[0].isLocked = true;
                Management.Gamer[0].qCannon[1].isLocked = true;
                Management.Gamer[0].qHorse[0].isLocked = true;
                Management.Gamer[0].qHorse[1].isLocked = true;
                Management.Gamer[0].qSoldier[0].isLocked = true;
                Management.Gamer[0].qSoldier[1].isLocked = true;
                Management.Gamer[0].qSoldier[2].isLocked = true;
                Management.Gamer[0].qSoldier[3].isLocked = true;
                Management.Gamer[0].qSoldier[4].isLocked = true;

                Management.Gamer[1].qGeneral.isLocked = false;
                Management.Gamer[1].qAdvisor[0].isLocked = false;
                Management.Gamer[1].qAdvisor[1].isLocked = false;
                Management.Gamer[1].qElephant[0].isLocked = false;
                Management.Gamer[1].qElephant[1].isLocked = false;
                Management.Gamer[1].qChariot[0].isLocked = false;
                Management.Gamer[1].qChariot[1].isLocked = false;
                Management.Gamer[1].qCannon[0].isLocked = false;
                Management.Gamer[1].qCannon[1].isLocked = false;
                Management.Gamer[1].qHorse[0].isLocked = false;
                Management.Gamer[1].qHorse[1].isLocked = false;
                Management.Gamer[1].qSoldier[0].isLocked = false;
                Management.Gamer[1].qSoldier[1].isLocked = false;
                Management.Gamer[1].qSoldier[2].isLocked = false;
                Management.Gamer[1].qSoldier[3].isLocked = false;
                Management.Gamer[1].qSoldier[4].isLocked = false;

                Management.picTurnOfGamer2.Image = Board.Properties.Resources.Turning;
                Management.picTurnOfGamer1.Image = Board.Properties.Resources.NotTurn;

                if (Management.isTimeCal == true)
                {
                    ChessBoard.Timer_NguoiChoi2.Enabled = true;
                    ChessBoard.Timer_NguoiChoi1.Enabled = false;
                }

            }
        }

        public static void Undo()
        {
            int t;
            ChessPiece temp_d, temp_c;
            temp_d = new ChessPiece();
            temp_c = new ChessPiece();

            if (!Management.Marked)
                if (Management.turnCount > 0)
                {
                    //Kiểm tra -> tham chiếu temp_d đến quân cờ trên bàn cờ
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "tuong") 
                        temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qGeneral;
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "sy")
                    {
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "0") 
                            temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qAdvisor[0];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "1") 
                            temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qAdvisor[1];
                    }
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "tinh")
                    {
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "0") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qElephant[0];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "1") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qElephant[1];
                    }
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "xe")
                    {
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "0") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qChariot[0];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "1") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qChariot[1];
                    }
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "phao")
                    {
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "0") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qCannon[0];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "1") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qCannon[1];
                    }
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "ma")
                    {
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "0") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qHorse[0];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "1") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qHorse[1];
                    }
                    if (Management.GameLog[Management.turnCount - 1].Head.Name == "chot")
                    {
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "0") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qSoldier[0];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "1") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qSoldier[1];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "2") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qSoldier[2];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "3") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qSoldier[3];
                        if (Management.GameLog[Management.turnCount - 1].Head.Order == "4") temp_d = Management.Gamer[Management.GameLog[Management.turnCount - 1].Head.Party].qSoldier[4];
                    }

                    //Kiểm tra nước đi có phải là nước đi ăn hay không
                    if (Management.GameLog[Management.turnCount - 1].Tail == null) 
                        t = 0;
                    else 
                        t = 1;

                    switch (t)
                    {
                        //Nếu là nước đi không ăn quân cờ của đối phương
                        case 0:
                            //Trả lại ô cờ trống cho vị trí vừa đi đến
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].isEmpty = true;
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].Party = 2;
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].Name = "";
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].Order = "";

                            //Đặt quân cờ vừa đi trở lại vị trí cũ
                            temp_d.Row = Management.GameLog[Management.turnCount - 1].headRow;
                            temp_d.Column = Management.GameLog[Management.turnCount - 1].headColumn;
                            temp_d.picChessPiece.Top = temp_d.Row * 53 + 80;
                            temp_d.picChessPiece.Left = temp_d.Column * 53 + 61;

                            //Thiết lập quân cờ tại vị trí cũ vừa đặt ở trên
                            Boad_Game.Position[temp_d.Row, temp_d.Column].isEmpty = false;
                            Boad_Game.Position[temp_d.Row, temp_d.Column].Party = temp_d.Party;
                            Boad_Game.Position[temp_d.Row, temp_d.Column].Order = temp_d.Order;
                            Boad_Game.Position[temp_d.Row, temp_d.Column].Name = temp_d.Name;

                            //Xóa nước đi cuối cùng khỏi GameLog
                            if (Management.turnCount >= 1) Management.turnCount--;
                            Array.Resize<Management.NuocDi>(ref Management.GameLog, Management.turnCount);

                            //Kiểm tra chiếu tướng
                            if (Management.isSound) ClickSound("0");
                            Management.Checkmate_Test();

                            if (Management.pnlCheckMate.Visible == true) Management.pnlCheckMate.Visible = false;

                            //Trả lại lượt đi                        
                            Management.ChangeMove();
                            break;

                        //Nếu là nước đi ăn quân cờ của đối phương
                        case 1:
                            //Kiểm tra -> tham chiếu temp_c đến quân cờ trên bàn cờ
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "tuong") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qGeneral;
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "sy")
                            {
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "0") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qAdvisor[0];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "1") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qAdvisor[1];
                            }
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "tinh")
                            {
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "0") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qElephant[0];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "1") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qElephant[1];
                            }
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "xe")
                            {
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "0") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qChariot[0];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "1") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qChariot[1];
                            }
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "phao")
                            {
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "0") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qCannon[0];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "1") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qCannon[1];
                            }
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "ma")
                            {
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "0") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qHorse[0];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "1") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qHorse[1];
                            }
                            if (Management.GameLog[Management.turnCount - 1].Tail.Name == "chot")
                            {
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "0") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qSoldier[0];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "1") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qSoldier[1];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "2") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qSoldier[2];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "3") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qSoldier[3];
                                if (Management.GameLog[Management.turnCount - 1].Tail.Order == "4") temp_c = Management.Gamer[Management.GameLog[Management.turnCount - 1].Tail.Party].qSoldier[4];
                            }

                            //Thiết lập lại quân cờ ở vị trí vừa bị ăn trên Bàn Cờ
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].isEmpty = false;
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].Party = Management.GameLog[Management.turnCount - 1].Tail.Party;
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].Name = Management.GameLog[Management.turnCount - 1].Tail.Name;
                            Boad_Game.Position[Management.GameLog[Management.turnCount - 1].Head.Row, Management.GameLog[Management.turnCount - 1].Head.Column].Order = Management.GameLog[Management.turnCount - 1].Tail.Order;


                            //Đặt quân cờ bị ăn vào vị trí ở trên
                            temp_c.Status = 1;
                            temp_c.picChessPiece.Top = temp_c.Row * 53 + 80;
                            temp_c.picChessPiece.Left = temp_c.Column * 53 + 61;
                            temp_c.picChessPiece.Width = 42;
                            temp_c.picChessPiece.Height = 42;
                            temp_c.picChessPiece.Cursor = Cursors.Hand;
                            if (temp_c.Party == 0)
                            {
                                
                                count_Red--;
                                if (temp_c.Name == "tuong") temp_c.picChessPiece.Image = Board.Properties.Resources._1tuong;
                                if (temp_c.Name == "sy") temp_c.picChessPiece.Image = Board.Properties.Resources._1sy;
                                if (temp_c.Name == "tinh") temp_c.picChessPiece.Image = Board.Properties.Resources._1tinh;
                                if (temp_c.Name == "xe") temp_c.picChessPiece.Image = Board.Properties.Resources._1xe;
                                if (temp_c.Name == "phao") temp_c.picChessPiece.Image = Board.Properties.Resources._1phao;
                                if (temp_c.Name == "ma") temp_c.picChessPiece.Image = Board.Properties.Resources._1ma;
                                if (temp_c.Name == "chot") temp_c.picChessPiece.Image = Board.Properties.Resources._1chot;
                            }
                            if (temp_c.Party == 1)
                            {
                                count_Black--;
                                if (temp_c.Name == "tuong") temp_c.picChessPiece.Image = Board.Properties.Resources._2tuong;
                                if (temp_c.Name == "sy") temp_c.picChessPiece.Image = Board.Properties.Resources._2sy;
                                if (temp_c.Name == "tinh") temp_c.picChessPiece.Image = Board.Properties.Resources._2tinh;
                                if (temp_c.Name == "xe") temp_c.picChessPiece.Image = Board.Properties.Resources._2xe;
                                if (temp_c.Name == "phao") temp_c.picChessPiece.Image = Board.Properties.Resources._2phao;
                                if (temp_c.Name == "ma") temp_c.picChessPiece.Image = Board.Properties.Resources._2ma;
                                if (temp_c.Name == "chot") temp_c.picChessPiece.Image = Board.Properties.Resources._2chot;
                            }

                            //Đặt quân cờ vừa đi trở lại vị trí cũ
                            temp_d.Row = Management.GameLog[Management.turnCount - 1].headRow;
                            temp_d.Column = Management.GameLog[Management.turnCount - 1].headColumn;
                            temp_d.picChessPiece.Top = temp_d.Row * 53 + 80;
                            temp_d.picChessPiece.Left = temp_d.Column * 53 + 61;

                            //Thiết lập quân cờ tại vị trí vừa đặt ở trên
                            Boad_Game.Position[temp_d.Row, temp_d.Column].isEmpty = false;
                            Boad_Game.Position[temp_d.Row, temp_d.Column].Party = temp_d.Party;
                            Boad_Game.Position[temp_d.Row, temp_d.Column].Order = temp_d.Order;
                            Boad_Game.Position[temp_d.Row, temp_d.Column].Name = temp_d.Name;

                            //Xóa nước đi cuối cùng khỏi GameLog
                            if (Management.turnCount >= 1) Management.turnCount--;
                            Array.Resize<Management.NuocDi>(ref Management.GameLog, Management.turnCount);

                            //Trả lại VanCo.winner=2 nếu nước đi ăn Tướng đối phương
                            if (Management.Winner != 2) Management.Winner = 2;

                            //Kiểm tra chiếu tướng
                            if (Management.isSound) ClickSound("0");
                            Management.Checkmate_Test();

                            //Trả lại lượt đi                            
                            Management.ChangeMove();

                            break;
                    }
                }
        }

        public static void Save()
        {
            FileStream saveFile;
            //FileInfo file = new FileInfo(Application.StartupPath + "\\save\\" + TenNguoiChoi1 + "_vs_" + TenNguoiChoi2 + "_" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Year) + ".ccb");
            //MessageBox.Show(file.Name);
            //if (file.Exists) saveFile = File.Create(Application.StartupPath + "\\save\\" + TenNguoiChoi1 + "_vs_" + TenNguoiChoi2 + "_1.ccb");
            saveFile = File.Create(Application.StartupPath + "\\save\\" + nameOfGamer1 + "_vs_" + nameOfGamer2 + "__" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Year) + "__" + Convert.ToString(DateTime.Now.Hour) + "." + Convert.ToString(DateTime.Now.Minute)+ "." + Convert.ToString(DateTime.Now.Second)+ ".ccb");

            StreamWriter fileWriter = new StreamWriter(saveFile);

            //Ghi lượt đi vào dòng đầu tiên
            fileWriter.WriteLine(Convert.ToString(Management.Turn));
            //Tính thời gian (0 hoặc 1)
            if (isTimeCal == true) fileWriter.WriteLine("1");
            else fileWriter.WriteLine("0");
            //Ghi tên 2 người chơi vào 2 dòng tiếp theo
            fileWriter.WriteLine(Management.nameOfGamer1);
            fileWriter.WriteLine(Management.nameOfGamer2);
            //Ghi thời gian còn lại
            fileWriter.WriteLine(Convert.ToString(Management.minuteOfGamer1));
            fileWriter.WriteLine(Convert.ToString(Management.secondsOfGamer1));
            fileWriter.WriteLine(Convert.ToString(Management.minuteOfGamer2));
            fileWriter.WriteLine(Convert.ToString(Management.secondsOfGamer2));


            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (Boad_Game.Position[i, j].isEmpty == false)
                    {
                        fileWriter.WriteLine(Convert.ToString(Boad_Game.Position[i, j].Row) + Convert.ToString(Boad_Game.Position[i, j].Column) + Convert.ToString(Boad_Game.Position[i, j].Party) + Boad_Game.Position[i, j].Order + Boad_Game.Position[i, j].Name);
                    }
                }
            }
            fileWriter.Close();
            saveFile.Close();
            MessageBox.Show("Ván cờ đã được lưu");
        }

        public static void Open(string path)
        {
            Management.Gamer[0].qGeneral.picChessPiece.Visible = false;
            Management.Gamer[0].qAdvisor[0].picChessPiece.Visible = false;
            Management.Gamer[0].qAdvisor[1].picChessPiece.Visible = false;
            Management.Gamer[0].qElephant[0].picChessPiece.Visible = false;
            Management.Gamer[0].qElephant[1].picChessPiece.Visible = false;
            Management.Gamer[0].qChariot[0].picChessPiece.Visible = false;
            Management.Gamer[0].qChariot[1].picChessPiece.Visible = false;
            Management.Gamer[0].qCannon[0].picChessPiece.Visible = false;
            Management.Gamer[0].qCannon[1].picChessPiece.Visible = false;
            Management.Gamer[0].qHorse[0].picChessPiece.Visible = false;
            Management.Gamer[0].qHorse[1].picChessPiece.Visible = false;
            Management.Gamer[0].qSoldier[0].picChessPiece.Visible = false;
            Management.Gamer[0].qSoldier[1].picChessPiece.Visible = false;
            Management.Gamer[0].qSoldier[2].picChessPiece.Visible = false;
            Management.Gamer[0].qSoldier[3].picChessPiece.Visible = false;
            Management.Gamer[0].qSoldier[4].picChessPiece.Visible = false;

            Management.Gamer[1].qGeneral.picChessPiece.Visible = false;
            Management.Gamer[1].qAdvisor[0].picChessPiece.Visible = false;
            Management.Gamer[1].qAdvisor[1].picChessPiece.Visible = false;
            Management.Gamer[1].qElephant[0].picChessPiece.Visible = false;
            Management.Gamer[1].qElephant[1].picChessPiece.Visible = false;
            Management.Gamer[1].qChariot[0].picChessPiece.Visible = false;
            Management.Gamer[1].qChariot[1].picChessPiece.Visible = false;
            Management.Gamer[1].qCannon[0].picChessPiece.Visible = false;
            Management.Gamer[1].qCannon[1].picChessPiece.Visible = false;
            Management.Gamer[1].qHorse[0].picChessPiece.Visible = false;
            Management.Gamer[1].qHorse[1].picChessPiece.Visible = false;
            Management.Gamer[1].qSoldier[0].picChessPiece.Visible = false;
            Management.Gamer[1].qSoldier[1].picChessPiece.Visible = false;
            Management.Gamer[1].qSoldier[2].picChessPiece.Visible = false;
            Management.Gamer[1].qSoldier[3].picChessPiece.Visible = false;
            Management.Gamer[1].qSoldier[4].picChessPiece.Visible = false;
            Boad_Game.ResetBoard_Game();

            ChessPiece temp;
            temp = new ChessPiece();
            int hang, cot, phe, luotdi, nc1_phut, nc1_giay, nc2_phut, nc2_giay;
            string ld = "", ten = "", thutu = "", ten1, ten2, thgian;
            StreamReader fileReader = File.OpenText(path);

            //Đọc vào giá trị luotdi
            luotdi = Convert.ToInt32(ld = fileReader.ReadLine());
            //Tính thời gian
            thgian = fileReader.ReadLine();
            //Đọc vào tên 2 người chơi
            ten1 = fileReader.ReadLine();
            ten2 = fileReader.ReadLine();
            //Đọc vào thời gian còn lại
            nc1_phut = Convert.ToInt32(fileReader.ReadLine());
            nc1_giay = Convert.ToInt32(fileReader.ReadLine());
            nc2_phut = Convert.ToInt32(fileReader.ReadLine());
            nc2_giay = Convert.ToInt32(fileReader.ReadLine());

            while (!fileReader.EndOfStream)
            {
                string Line = fileReader.ReadLine();
                hang = Convert.ToInt32(Convert.ToString(Line[0]));
                cot = Convert.ToInt32(Convert.ToString(Line[1]));
                phe = Convert.ToInt32(Convert.ToString(Line[2]));
                thutu = Convert.ToString(Line[3]);
                for (int i = 4; i < Line.Length; i++)
                {
                    ten += Line[i];
                }

                //Kiểm tra quân cờ để tham chiếu
                if (ten == "tuong") temp = Management.Gamer[phe].qGeneral;
                if (ten == "sy")
                {
                    if (thutu == "0") temp = Management.Gamer[phe].qAdvisor[0];
                    if (thutu == "1") temp = Management.Gamer[phe].qAdvisor[1];
                }
                if (ten == "tinh")
                {
                    if (thutu == "0") temp = Management.Gamer[phe].qElephant[0];
                    if (thutu == "1") temp = Management.Gamer[phe].qElephant[1];
                }
                if (ten == "xe")
                {
                    if (thutu == "0") temp = Management.Gamer[phe].qChariot[0];
                    if (thutu == "1") temp = Management.Gamer[phe].qChariot[1];
                }
                if (ten == "phao")
                {
                    if (thutu == "0") temp = Management.Gamer[phe].qCannon[0];
                    if (thutu == "1") temp = Management.Gamer[phe].qCannon[1];
                }
                if (ten == "ma")
                {
                    if (thutu == "0") temp = Management.Gamer[phe].qHorse[0];
                    if (thutu == "1") temp = Management.Gamer[phe].qHorse[1];
                }
                if (ten == "chot")
                {
                    if (thutu == "0") temp = Management.Gamer[phe].qSoldier[0];
                    if (thutu == "1") temp = Management.Gamer[phe].qSoldier[1];
                    if (thutu == "2") temp = Management.Gamer[phe].qSoldier[2];
                    if (thutu == "3") temp = Management.Gamer[phe].qSoldier[3];
                    if (thutu == "4") temp = Management.Gamer[phe].qSoldier[4];
                }

                //Thiết lập quân cờ trên bàn cờ
                Boad_Game.Position[hang, cot].isEmpty = false;
                Boad_Game.Position[hang, cot].Party = phe;
                Boad_Game.Position[hang, cot].Name = ten;
                Boad_Game.Position[hang, cot].Order = thutu;

                //Đặt quân cờ vào vị trí
                if (luotdi != phe) temp.isLocked = true;
                else temp.isLocked = false;
                temp.picChessPiece.Visible = true;
                temp.Row = hang;
                temp.Column = cot;
                temp.Status = 1;
                temp.draw();

                //Trả lại giá trị null để tiếp tục lấy thông tin quân cờ khác
                ten = "";
                thutu = "";
            }
            //Thiết lập lượt đi, tên người chơi, thời gian
            Management.nameOfGamer1 = ten1;
            Management.nameOfGamer2 = ten2;
            if (thgian == "0") Management.isTimeCal = false;
            if (thgian == "1") Management.isTimeCal = true;
            Management.minuteOfGamer1 = nc1_phut;
            Management.secondsOfGamer1 = nc1_giay;
            Management.minuteOfGamer2 = nc2_phut;
            Management.secondsOfGamer2 = nc2_giay;
            Management.Turn = luotdi;
            turnCount = 0;
            isPlaying = true;
            Winner = 2;
            count_Black = 0;
            count_Red = 0;
            //Kiểm tra chiếu tướng
            Management.Checkmate_Test();

            fileReader.Close();
        }
        #endregion

        public static void Blank_FlagPoint(int i, int j)        // định nghĩa điểm cờ rỗng.
        {
            Boad_Game.Position[i, j].isEmpty = true;
            Boad_Game.Position[i, j].Name = "";
            Boad_Game.Position[i, j].Order = "";
            Boad_Game.Position[i, j].Party = 2;
        }

        public static void SetChess(Object sender, ChessPiece q, int i, int j)
        {
            if (sender.GetType() == typeof(Board.ChessBoard))
            {
                q.Row = i;
                q.Column = j;
                q.draw();
            }
            if (sender.GetType() == typeof(System.Windows.Forms.PictureBox))
            {
                Boad_Game.Position[i, j].isEmpty = false;
                Boad_Game.Position[i, j].Party = Management.ChessMarked.Party;
                Boad_Game.Position[i, j].Name = Management.ChessMarked.Name;
                Boad_Game.Position[i, j].Order = Management.ChessMarked.Order;
                Management.ChessMarked.Row = i;
                Management.ChessMarked.Column = j;
                Management.ChessMarked.picChessPiece.Top = i * 53 + 80;
                Management.ChessMarked.picChessPiece.Left = j * 53 + 61;
            }

        }

        public static void LuuVaoGameLog(Object sender, ChessPiece q)
        {
            if (sender.GetType() == typeof(Board.ChessBoard))
            {
                Management.turnCount++;
                Array.Resize<Management.NuocDi>(ref Management.GameLog, Management.turnCount);
                Management.GameLog[Management.turnCount - 1].Head = Management.ChessMarked;
                Management.GameLog[Management.turnCount - 1].headRow = q.Row;
                Management.GameLog[Management.turnCount - 1].headColumn = q.Column;
            }
            if (sender.GetType() == typeof(System.Windows.Forms.PictureBox))
            {
                Management.turnCount++;
                Array.Resize<Management.NuocDi>(ref Management.GameLog, Management.turnCount);
                Management.GameLog[Management.turnCount - 1].Head = Management.ChessMarked;
                Management.GameLog[Management.turnCount - 1].headRow = Management.ChessMarked.Row;
                Management.GameLog[Management.turnCount - 1].headColumn = Management.ChessMarked.Column;
                Management.GameLog[Management.turnCount - 1].Tail = q;
                Management.GameLog[Management.turnCount - 1].tailRow = q.Row;
                Management.GameLog[Management.turnCount - 1].tailColumn = q.Column;
            }
        }

        public static bool Checkmate_Next(ChessPiece tuong)
        {
            string kt = "";
            bool chieu = false;
            
            if (tuong.Party == 0)
            {
                int xe0 = 0, xe1 = 0, phao0 = 0, phao1 = 0, ma0 = 0, ma1 = 0, chot0 = 0, chot1 = 0, chot2 = 0, chot3 = 0, chot4 = 0;

                if (Management.Gamer[1].qChariot[0].Status == 1)
                    if (Management.Gamer[1].qChariot[0].Move_Test(tuong.Row, tuong.Column) == 1) xe0 = 1;
                if (Management.Gamer[1].qChariot[1].Status == 1)
                    if (Management.Gamer[1].qChariot[1].Move_Test(tuong.Row, tuong.Column) == 1) xe1 = 1;
                if (Management.Gamer[1].qCannon[0].Status == 1)
                    if (Management.Gamer[1].qCannon[0].Move_Test(tuong.Row, tuong.Column) == 1) phao0 = 1;
                if (Management.Gamer[1].qCannon[1].Status == 1)
                    if (Management.Gamer[1].qCannon[1].Move_Test(tuong.Row, tuong.Column) == 1) phao1 = 1;
                if (Management.Gamer[1].qHorse[0].Status == 1)
                    if (Management.Gamer[1].qHorse[0].Move_Test(tuong.Row, tuong.Column) == 1) ma0 = 1;
                if (Management.Gamer[1].qHorse[1].Status == 1)
                    if (Management.Gamer[1].qHorse[1].Move_Test(tuong.Row, tuong.Column) == 1) ma1 = 1;
                if (Management.Gamer[1].qSoldier[0].Status == 1)
                    if (Management.Gamer[1].qSoldier[0].Move_Test(tuong.Row, tuong.Column) == 1) chot0 = 1;
                if (Management.Gamer[1].qSoldier[1].Status == 1)
                    if (Management.Gamer[1].qSoldier[1].Move_Test(tuong.Row, tuong.Column) == 1) chot1 = 1;
                if (Management.Gamer[1].qSoldier[2].Status == 1)
                    if (Management.Gamer[1].qSoldier[2].Move_Test(tuong.Row, tuong.Column) == 1) chot2 = 1;
                if (Management.Gamer[1].qSoldier[3].Status == 1)
                    if (Management.Gamer[1].qSoldier[3].Move_Test(tuong.Row, tuong.Column) == 1) chot3 = 1;
                if (Management.Gamer[1].qSoldier[4].Status == 1)
                    if (Management.Gamer[1].qSoldier[4].Move_Test(tuong.Row, tuong.Column) == 1) chot4 = 1;

                if (xe0 == 1) kt += " xe0";
                if (xe0 == 1) kt += " xe1";
                if (phao0 == 1) kt += " phao0";
                if (phao1 == 1) kt += " phao1";
                if (ma0 == 1) kt += " ma0";
                if (ma1 == 1) kt += " ma1";
                if (chot0 == 1) kt += " chot0";
                if (chot1 == 1) kt += " chot1";
                if (chot2 == 1) kt += " chot2";
                if (chot3 == 1) kt += " chot3";
                if (chot4 == 1) kt += " chot4";

                //if(kt!="") MessageBox.Show(kt);

                if (xe0 != 1 &&
                    xe1 != 1 &&
                    phao0 != 1 &&
                    phao1 != 1 &&
                    ma0 != 1 &&
                    ma1 != 1 &&
                    chot0 != 1 &&
                    chot1 != 1 &&
                    chot2 != 1 &&
                    chot3 != 1 &&
                    chot4 != 1)
                    chieu = false;
                else chieu = true;

            }
            if (tuong.Party == 1)
            {
                int xe0 = 0, xe1 = 0, phao0 = 0, phao1 = 0, ma0 = 0, ma1 = 0, chot0 = 0, chot1 = 0, chot2 = 0, chot3 = 0, chot4 = 0;

                if (Management.Gamer[0].qChariot[0].Status == 1)
                    if (Management.Gamer[0].qChariot[0].Move_Test(tuong.Row, tuong.Column) == 1) xe0 = 1;
                if (Management.Gamer[0].qChariot[1].Status == 1)
                    if (Management.Gamer[0].qChariot[1].Move_Test(tuong.Row, tuong.Column) == 1) xe1 = 1;
                if (Management.Gamer[0].qCannon[0].Status == 1)
                    if (Management.Gamer[0].qCannon[0].Move_Test(tuong.Row, tuong.Column) == 1) phao0 = 1;
                if (Management.Gamer[0].qCannon[1].Status == 1)
                    if (Management.Gamer[0].qCannon[1].Move_Test(tuong.Row, tuong.Column) == 1) phao1 = 1;
                if (Management.Gamer[0].qHorse[0].Status == 1)
                    if (Management.Gamer[0].qHorse[0].Move_Test(tuong.Row, tuong.Column) == 1) ma0 = 1;
                if (Management.Gamer[0].qHorse[1].Status == 1)
                    if (Management.Gamer[0].qHorse[1].Move_Test(tuong.Row, tuong.Column) == 1) ma1 = 1;
                if (Management.Gamer[0].qSoldier[0].Status == 1)
                    if (Management.Gamer[0].qSoldier[0].Move_Test(tuong.Row, tuong.Column) == 1) chot0 = 1;
                if (Management.Gamer[0].qSoldier[1].Status == 1)
                    if (Management.Gamer[0].qSoldier[1].Move_Test(tuong.Row, tuong.Column) == 1) chot1 = 1;
                if (Management.Gamer[0].qSoldier[2].Status == 1)
                    if (Management.Gamer[0].qSoldier[2].Move_Test(tuong.Row, tuong.Column) == 1) chot2 = 1;
                if (Management.Gamer[0].qSoldier[3].Status == 1)
                    if (Management.Gamer[0].qSoldier[3].Move_Test(tuong.Row, tuong.Column) == 1) chot3 = 1;
                if (Management.Gamer[0].qSoldier[4].Status == 1)
                    if (Management.Gamer[0].qSoldier[4].Move_Test(tuong.Row, tuong.Column) == 1) chot4 = 1;

                if (xe0 == 1) kt += " xe0";
                if (xe0 == 1) kt += " xe1";
                if (phao0 == 1) kt += " phao0";
                if (phao1 == 1) kt += " phao1";
                if (ma0 == 1) kt += " ma0";
                if (ma1 == 1) kt += " ma1";
                if (chot0 == 1) kt += " chot0";
                if (chot1 == 1) kt += " chot1";
                if (chot2 == 1) kt += " chot2";
                if (chot3 == 1) kt += " chot3";
                if (chot4 == 1) kt += " chot4";

                //if (kt != "") MessageBox.Show(kt);

                if (xe0 != 1 &&
                    xe1 != 1 &&
                    phao0 != 1 &&
                    phao1 != 1 &&
                    ma0 != 1 &&
                    ma1 != 1 &&
                    chot0 != 1 &&
                    chot1 != 1 &&
                    chot2 != 1 &&
                    chot3 != 1 &&
                    chot4 != 1)
                    chieu = false;
                else chieu = true;

            }
            return chieu;
        }

        public static void Checkmate_Test()
        {
            int t = 0;
            if (Management.Checkmate_Next(Management.Gamer[1].qGeneral) == true && Management.Checkmate_Next(Management.Gamer[0].qGeneral) == false) t = 1;
            if (Management.Checkmate_Next(Management.Gamer[1].qGeneral) == false && Management.Checkmate_Next(Management.Gamer[0].qGeneral) == true) t = 0;
            if (Management.Checkmate_Next(Management.Gamer[1].qGeneral) == false && Management.Checkmate_Next(Management.Gamer[0].qGeneral) == false) t = 2;
            switch (t)
            {
                case 1:
                    Management.Gamer[1].qGeneral.picChessPiece.Image = Board.Properties.Resources._2tuong_bichieu;
                    Management.Gamer[0].qGeneral.picChessPiece.Image = Board.Properties.Resources._1tuong;
                    Management.picNotifyCheckmate.Visible = true;
                    if (Management.isSound) ClickSound("chieu");
                    break;
                case 0:
                    Management.Gamer[0].qGeneral.picChessPiece.Image = Board.Properties.Resources._1tuong_bichieu;
                    Management.Gamer[1].qGeneral.picChessPiece.Image = Board.Properties.Resources._2tuong;
                    Management.picNotifyCheckmate.Visible = true;
                    if (Management.isSound) ClickSound("chieu");
                    break;
                case 2:
                    Management.Gamer[0].qGeneral.picChessPiece.Image = Board.Properties.Resources._1tuong;
                    Management.Gamer[1].qGeneral.picChessPiece.Image = Board.Properties.Resources._2tuong;
                    Management.picNotifyCheckmate.Visible = false;
                    break;
            }
        }

        public static void Destroy_Chess(ChessPiece q)
        {
            int hang = 0;
            int cot = 0;
            q.Status = 0;
            if (q.Party == 1)
            {
                if (count_Black >= 0 && count_Black <= 5)
                {
                    hang = 0;
                    cot = count_Black;
                }
                if (count_Black >= 6 && count_Black <= 11)
                {
                    hang = 1;
                    cot = count_Black - 6;
                }
                if (count_Black >= 12 && count_Black <= 15)
                {
                    hang = 2;
                    cot = count_Black - 12;
                }
                count_Black++;
                Array.Resize<DeathChess>(ref Death_Black, count_Black);
                Management.Death_Black[count_Black - 1].Row = hang;
                Management.Death_Black[count_Black - 1].Column = cot;
                Management.Death_Black[count_Black - 1].picChessPiece = q.picChessPiece;
                if (q.Name == "tuong") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2tuong_bian;
                if (q.Name == "sy") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2sy_bian;
                if (q.Name == "tinh") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2tinh_bian;
                if (q.Name == "xe") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2xe_bian;
                if (q.Name == "phao") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2phao_bian;
                if (q.Name == "ma") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2ma_bian;
                if (q.Name == "chot") Management.Death_Black[count_Black - 1].picChessPiece.Image = Board.Properties.Resources._2chot_bian;
                Management.Death_Black[count_Black - 1].picChessPiece.Width = 31;
                Management.Death_Black[count_Black - 1].picChessPiece.Height = 31;
                Management.Death_Black[count_Black - 1].picChessPiece.BackColor = Color.Transparent;
                Management.Death_Black[count_Black - 1].picChessPiece.Cursor = Cursors.Arrow;
                Management.Death_Black[count_Black - 1].picChessPiece.Top = Management.Death_Black[count_Black - 1].Row * 33 + 244;
                Management.Death_Black[count_Black - 1].picChessPiece.Left = Management.Death_Black[count_Black - 1].Column * 33 + 596;
            }
            if (q.Party == 0)
            {
                if (count_Red >= 0 && count_Red <= 5)
                {
                    hang = 0;
                    cot = count_Red;
                }
                if (count_Red >= 6 && count_Red <= 11)
                {
                    hang = 1;
                    cot = count_Red - 6;
                }
                if (count_Red >= 12 && count_Red <= 15)
                {
                    hang = 2;
                    cot = count_Red - 12;
                }
                count_Red++;
                Array.Resize<DeathChess>(ref Death_Red, count_Red);
                Management.Death_Red[count_Red - 1].Row = hang;
                Management.Death_Red[count_Red - 1].Column = cot;
                Management.Death_Red[count_Red - 1].picChessPiece = q.picChessPiece;
                if (q.Name == "tuong") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1tuong_bian;
                if (q.Name == "sy") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1sy_bian;
                if (q.Name == "tinh") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1tinh_bian;
                if (q.Name == "xe") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1xe_bian;
                if (q.Name == "phao") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1phao_bian;
                if (q.Name == "ma") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1ma_bian;
                if (q.Name == "chot") Management.Death_Red[count_Red - 1].picChessPiece.Image = Board.Properties.Resources._1chot_bian;
                Management.Death_Red[count_Red - 1].picChessPiece.Width = 31;
                Management.Death_Red[count_Red - 1].picChessPiece.Height = 31;
                Management.Death_Red[count_Red - 1].picChessPiece.Cursor = Cursors.Arrow;
                Management.Death_Red[count_Red - 1].picChessPiece.BackColor = Color.Transparent;
                Management.Death_Red[count_Red - 1].picChessPiece.Top = Management.Death_Red[count_Red - 1].Row * 33 + 503;
                Management.Death_Red[count_Red - 1].picChessPiece.Left = Management.Death_Red[count_Red - 1].Column * 33 + 596;
            }
        }

        public static void CheckmateEnd_Test()
        {
            bool cuu = false;
            int tuong = 0, sy0 = 0, sy1 = 0, tinh0 = 0, tinh1 = 0, xe0 = 0, xe1 = 0, phao0 = 0, phao1 = 0, ma0 = 0, ma1 = 0, chot0 = 0, chot1 = 0, chot2 = 0, chot3 = 0, chot4 = 0;
            if (Management.Turn == 0)
            {
                if (Checkmate_Next(Gamer[0].qGeneral) == true)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (Gamer[0].qGeneral.Status == 1)
                                if (Gamer[0].qGeneral.Move_Test(i, j) == 1 && Gamer[0].qGeneral.SafeGeneral_Test(i, j) == 1) tuong = 1;
                            if (Gamer[0].qAdvisor[0].Status == 1)
                                if (Gamer[0].qAdvisor[0].Move_Test(i, j) == 1 && Gamer[0].qAdvisor[0].SafeGeneral_Test(i, j) == 1) sy0 = 1;
                            if (Gamer[0].qAdvisor[1].Status == 1)
                                if (Gamer[0].qAdvisor[1].Move_Test(i, j) == 1 && Gamer[0].qAdvisor[1].SafeGeneral_Test(i, j) == 1) sy1 = 1;
                            if (Gamer[0].qElephant[0].Status == 1)
                                if (Gamer[0].qElephant[0].Move_Test(i, j) == 1 && Gamer[0].qElephant[0].SafeGeneral_Test(i, j) == 1) tinh0 = 1;
                            if (Gamer[0].qElephant[1].Status == 1)
                                if (Gamer[0].qElephant[1].Move_Test(i, j) == 1 && Gamer[0].qElephant[1].SafeGeneral_Test(i, j) == 1) tinh1 = 1;
                            if (Gamer[0].qChariot[0].Status == 1)
                                if (Gamer[0].qChariot[0].Move_Test(i, j) == 1 && Gamer[0].qChariot[0].SafeGeneral_Test(i, j) == 1) xe0 = 1;
                            if (Gamer[0].qChariot[1].Status == 1)
                                if (Gamer[0].qChariot[1].Move_Test(i, j) == 1 && Gamer[0].qChariot[1].SafeGeneral_Test(i, j) == 1) xe1 = 1;
                            if (Gamer[0].qCannon[0].Status == 1)
                                if (Gamer[0].qCannon[0].Move_Test(i, j) == 1 && Gamer[0].qCannon[0].SafeGeneral_Test(i, j) == 1) phao0 = 1;
                            if (Gamer[0].qCannon[1].Status == 1)
                                if (Gamer[0].qCannon[1].Move_Test(i, j) == 1 && Gamer[0].qCannon[1].SafeGeneral_Test(i, j) == 1) phao1 = 1;
                            if (Gamer[0].qHorse[0].Status == 1)
                                if (Gamer[0].qHorse[0].Move_Test(i, j) == 1 && Gamer[0].qHorse[0].SafeGeneral_Test(i, j) == 1) ma0 = 1;
                            if (Gamer[0].qHorse[1].Status == 1)
                                if (Gamer[0].qHorse[1].Move_Test(i, j) == 1 && Gamer[0].qHorse[1].SafeGeneral_Test(i, j) == 1) ma1 = 1;
                            if (Gamer[0].qSoldier[0].Status == 1)
                                if (Gamer[0].qSoldier[0].Move_Test(i, j) == 1 && Gamer[0].qSoldier[0].SafeGeneral_Test(i, j) == 1) chot0 = 1;
                            if (Gamer[0].qSoldier[1].Status == 1)
                                if (Gamer[0].qSoldier[1].Move_Test(i, j) == 1 && Gamer[0].qSoldier[1].SafeGeneral_Test(i, j) == 1) chot1 = 1;
                            if (Gamer[0].qSoldier[2].Status == 1)
                                if (Gamer[0].qSoldier[2].Move_Test(i, j) == 1 && Gamer[0].qSoldier[2].SafeGeneral_Test(i, j) == 1) chot2 = 1;
                            if (Gamer[0].qSoldier[3].Status == 1)
                                if (Gamer[0].qSoldier[3].Move_Test(i, j) == 1 && Gamer[0].qSoldier[3].SafeGeneral_Test(i, j) == 1) chot3 = 1;
                            if (Gamer[0].qSoldier[4].Status == 1)
                                if (Gamer[0].qSoldier[4].Move_Test(i, j) == 1 && Gamer[0].qSoldier[4].SafeGeneral_Test(i, j) == 1) chot4 = 1;

                            if ((tuong == 1) ||
                                (sy0 == 1) ||
                                (sy1 == 1) ||
                                (tinh0 == 1) ||
                                (tinh1 == 1) ||
                                (xe0 == 1) ||
                                (xe1 == 1) ||
                                (phao0 == 1) ||
                                (phao1 == 1) ||
                                (ma0 == 1) ||
                                (ma1 == 1) ||
                                (chot0 == 1) ||
                                (chot1 == 1) ||
                                (chot2 == 1) ||
                                (chot3 == 1) ||
                                (chot4 == 1))
                            {
                                cuu = true;
                                break;
                            }
                        }
                    }
                }
                else cuu = true;
                if (!cuu) Winner = 1;
            }
            if (Management.Turn == 1)
            {
                if (Checkmate_Next(Gamer[1].qGeneral) == true)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (Gamer[1].qGeneral.Status == 1)
                                if (Gamer[1].qGeneral.Move_Test(i, j) == 1 && Gamer[1].qGeneral.SafeGeneral_Test(i, j) == 1) tuong = 1;
                            if (Gamer[1].qAdvisor[0].Status == 1)
                                if (Gamer[1].qAdvisor[0].Move_Test(i, j) == 1 && Gamer[1].qAdvisor[0].SafeGeneral_Test(i, j) == 1) sy0 = 1;
                            if (Gamer[1].qAdvisor[1].Status == 1)
                                if (Gamer[1].qAdvisor[1].Move_Test(i, j) == 1 && Gamer[1].qAdvisor[1].SafeGeneral_Test(i, j) == 1) sy1 = 1;
                            if (Gamer[1].qElephant[0].Status == 1)
                                if (Gamer[1].qElephant[0].Move_Test(i, j) == 1 && Gamer[1].qElephant[0].SafeGeneral_Test(i, j) == 1) tinh0 = 1;
                            if (Gamer[1].qElephant[1].Status == 1)
                                if (Gamer[1].qElephant[1].Move_Test(i, j) == 1 && Gamer[1].qElephant[1].SafeGeneral_Test(i, j) == 1) tinh1 = 1;
                            if (Gamer[1].qChariot[0].Status == 1)
                                if (Gamer[1].qChariot[0].Move_Test(i, j) == 1 && Gamer[1].qChariot[0].SafeGeneral_Test(i, j) == 1) xe0 = 1;
                            if (Gamer[1].qChariot[1].Status == 1)
                                if (Gamer[1].qChariot[1].Move_Test(i, j) == 1 && Gamer[1].qChariot[1].SafeGeneral_Test(i, j) == 1) xe1 = 1;
                            if (Gamer[1].qCannon[0].Status == 1)
                                if (Gamer[1].qCannon[0].Move_Test(i, j) == 1 && Gamer[1].qCannon[0].SafeGeneral_Test(i, j) == 1) phao0 = 1;
                            if (Gamer[1].qCannon[1].Status == 1)
                                if (Gamer[1].qCannon[1].Move_Test(i, j) == 1 && Gamer[1].qCannon[1].SafeGeneral_Test(i, j) == 1) phao1 = 1;
                            if (Gamer[1].qHorse[0].Status == 1)
                                if (Gamer[1].qHorse[0].Move_Test(i, j) == 1 && Gamer[1].qHorse[0].SafeGeneral_Test(i, j) == 1) ma0 = 1;
                            if (Gamer[1].qHorse[1].Status == 1)
                                if (Gamer[1].qHorse[1].Move_Test(i, j) == 1 && Gamer[1].qHorse[1].SafeGeneral_Test(i, j) == 1) ma1 = 1;
                            if (Gamer[1].qSoldier[0].Status == 1)
                                if (Gamer[1].qSoldier[0].Move_Test(i, j) == 1 && Gamer[1].qSoldier[0].SafeGeneral_Test(i, j) == 1) chot0 = 1;
                            if (Gamer[1].qSoldier[1].Status == 1)
                                if (Gamer[1].qSoldier[1].Move_Test(i, j) == 1 && Gamer[1].qSoldier[1].SafeGeneral_Test(i, j) == 1) chot1 = 1;
                            if (Gamer[1].qSoldier[2].Status == 1)
                                if (Gamer[1].qSoldier[2].Move_Test(i, j) == 1 && Gamer[1].qSoldier[2].SafeGeneral_Test(i, j) == 1) chot2 = 1;
                            if (Gamer[1].qSoldier[3].Status == 1)
                                if (Gamer[1].qSoldier[3].Move_Test(i, j) == 1 && Gamer[1].qSoldier[3].SafeGeneral_Test(i, j) == 1) chot3 = 1;
                            if (Gamer[1].qSoldier[4].Status == 1)
                                if (Gamer[1].qSoldier[4].Move_Test(i, j) == 1 && Gamer[1].qSoldier[4].SafeGeneral_Test(i, j) == 1) chot4 = 1;

                            if ((tuong == 1) ||
                                (sy0 == 1) ||
                                (sy1 == 1) ||
                                (tinh0 == 1) ||
                                (tinh1 == 1) ||
                                (xe0 == 1) ||
                                (xe1 == 1) ||
                                (phao0 == 1) ||
                                (phao1 == 1) ||
                                (ma0 == 1) ||
                                (ma1 == 1) ||
                                (chot0 == 1) ||
                                (chot1 == 1) ||
                                (chot2 == 1) ||
                                (chot3 == 1) ||
                                (chot4 == 1))
                            {
                                cuu = true;
                                break;
                            }
                        }
                    }
                }
                else cuu = true;
                if (!cuu) Winner = 0;
            }
        }

        public static void ClickSound(string s)
        {
            if (s == "chieu")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.Chieu);
                sound.Play();
            }
            if (s == "ready")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.Ready);
                sound.Play();
            }
            if (s == "0")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.Mark);
                sound.Play();
            }
            if (s == "tuong")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.TuongAn);
                sound.Play();
            }
            if (s == "sy")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.SyAn);
                sound.Play();
            }
            if (s == "tinh")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.TinhAn);
                sound.Play();
            }
            if (s == "xe")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.XeAn);
                sound.Play();
            }
            if (s == "phao")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.PhaoAn);
                sound.Play();
            }
            if (s == "ma")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.MaAn);
                sound.Play();
            }
            if (s == "chot")
            {
                SoundPlayer sound = new SoundPlayer(Board.Properties.Resources.ChotAn);
                sound.Play();
            }
        }
    }
}
