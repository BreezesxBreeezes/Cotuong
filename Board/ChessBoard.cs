using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Board
{
    public partial class ChessBoard : Form
    {
        #region Khởi tạo formMain
        private bool dangkeo;
        private Point diemkeo;
        private int new_t = 0;//Kiểm tra NewGame đc click chưa
        private int open_t = 0;//Kiểm tra Open đc click chưa
        private int exit_t = 0;//Kiểm tra Exit đã đc click chưa
        private int save_t = 0;
        NewGame form_NewGame = new NewGame();
        private void ChessBoard_Load(object sender, EventArgs e)
        {
            //Đọc dữ liệu đã lưu vào Options
            StreamReader OptionsReader = File.OpenText("Options.cco");
            if (OptionsReader.ReadLine() == "1") Management.isSound = true;
            else Management.isSound = false;
            if (OptionsReader.ReadLine() == "1") Management.isSoundTrack = true;
            else Management.isSoundTrack = false;
            Management.Path_NhacNen = OptionsReader.ReadLine();
            OptionsReader.Close();

            //Kiểm tra và chơi nhạc nền
            Management.PlaySoundTrack(Management.isSoundTrack);

            Management.BackBuffer = new Bitmap(this.Width, this.Height);
            Bitmap bg = new Bitmap(Board.Properties.Resources.bg1);
            Graphics g = Graphics.FromImage(Management.BackBuffer);
            g.Clear(this.BackColor);
            g.DrawImage(bg, 0, 0);
            g.Dispose();

            this.LuuVanCo.Visible = false;

            //Thiết lập cho Timer
            Timer_NguoiChoi1.Tick += new EventHandler(Timer_NguoiChoi1_Tick);
            Timer_NguoiChoi2.Tick += new EventHandler(Timer_NguoiChoi2_Tick);
            Timer_NguoiChoi1.Interval = 1000;
            Timer_NguoiChoi2.Interval = 1000;

            //Flash screen
            FlashScreen FlashScr = new FlashScreen();
            FlashScr.ShowDialog();
            this.Refresh();
        }

        //Khai báo Timer đếm thời gian cho 2 người chơi
        public static Timer Timer_NguoiChoi1 = new Timer();
        public static Timer Timer_NguoiChoi2 = new Timer();
        

        public ChessBoard()
        {           
            InitializeComponent();
        }

        #region Dùng chuột di chuyển form
        private void ChessBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dangkeo = true;
                diemkeo = new Point(e.X, e.Y);
            }
            else dangkeo = false;            
        }

        private void ChessBoard_MouseUp(object sender, MouseEventArgs e)
        {
            dangkeo = false;            
        }

        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (dangkeo)
            {
                Point diemden;
                diemden = this.PointToScreen(new Point(e.X, e.Y));
                diemden.Offset(-diemkeo.X, -diemkeo.Y);
                this.Location = diemden;
            }
        }
        #endregion

        #region Vẽ lại form background từ buffer
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do nothing             
        }
        protected override void OnPaint(PaintEventArgs e)
        {            
            e.Graphics.DrawImage(Management.BackBuffer, 0, 0, this.Width, this.Height);
        }
        #endregion
        #endregion

        #region Menu Ok, Cancel, Close panel của panel LuuVanCo
        private void Ok_MouseEnter(object sender, EventArgs e)
        {
            Ok.Image = Board.Properties.Resources.Ok_MouseOver;
        }
        private void Ok_MouseLeave(object sender, EventArgs e)
        {
            Ok.Image = Board.Properties.Resources.Ok;
        }
        private void Ok_MouseClick(object sender, MouseEventArgs e)
        {

            if (new_t == 1 && open_t == 0 && exit_t == 0 && save_t == 0)
            {
                new_t = 0;
                this.LuuVanCo.Visible = false;
                Save_MouseClick(sender, e);
                form_NewGame.ShowDialog(this);
                TenQuanDo.Text = Management.nameOfGamer1;
                TenQuanDen.Text = Management.nameOfGamer2;
                AddQuanCo();
            }
            if (new_t == 0 && open_t == 1 && exit_t == 0 && save_t == 0)
            {
                open_t = 0;
                this.LuuVanCo.Visible = false;
                Management.Save();

                //Mở OpenDialog
                /*string SourcePath;
                openFileDialog1.Filter = "Chinese Chess Board file (*.ccb)|*.ccb";
                openFileDialog1.Title = "Load ván cờ";
                openFileDialog1.Multiselect = false;
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName != "")
                {
                    //Khởi tạo bàn cờ trống
                    BanCo.ResetBanCo();

                    SourcePath = openFileDialog1.FileName;
                    VanCo.Open(SourcePath);
                    TenQuanDo.Text = VanCo.TenNguoiChoi1;
                    TenQuanDen.Text = VanCo.TenNguoiChoi2;
                }*/
                Open form_Open = new Open();
                form_Open.ShowDialog(this);
                TenQuanDo.Text = Management.nameOfGamer1;
                TenQuanDen.Text = Management.nameOfGamer2;
                if (Management.minuteOfGamer1 >= 10)
                {
                    if (Management.secondsOfGamer1 >= 10)
                    {
                        Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                    }
                    else Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                }
                if (Management.minuteOfGamer1 < 10)
                {
                    if (Management.secondsOfGamer1 >= 10) Management.lblTimeOfGamer1.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                    else Management.lblTimeOfGamer1.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                }

                if (Management.minuteOfGamer2 >= 10)
                {
                    if (Management.secondsOfGamer2 >= 10)
                    {
                        Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                    }
                    else Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                }
                if (Management.minuteOfGamer2 < 10)
                {
                    if (Management.secondsOfGamer2 >= 10) Management.lblTimeOfGamer2.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                    else Management.lblTimeOfGamer2.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                }

                //Mở lại bộ đếm thời gian
                DemThoiGian();

                //Mở lại bàn cờ
                MoBanCo();
            }
            if (new_t == 0 && open_t == 0 && exit_t == 1 && save_t == 0)
            {
                exit_t = 0;
                this.LuuVanCo.Visible = false;
                Management.Save();
                this.Close();
            }
            if (new_t == 0 && open_t == 0 && exit_t == 0 && save_t == 1)
            {
                save_t = 0;
                this.LuuVanCo.Visible = false;
                Management.Save();
                //Mở lại bộ đếm thời gian
                DemThoiGian();

                //Mở lại bàn cờ
                MoBanCo();
            }
        }

        private void Cancel_MouseEnter(object sender, EventArgs e)
        {
            Cancel.Image = Board.Properties.Resources.Cancel_MouseOver;
        }
        private void Cancel_MouseLeave(object sender, EventArgs e)
        {
            Cancel.Image = Board.Properties.Resources.Cancel;
        }
        private void Cancel_MouseClick(object sender, MouseEventArgs e)
        {

            if (new_t == 1 && open_t == 0 && exit_t == 0 && save_t == 0)
            {
                this.LuuVanCo.Visible = false;               
                new_t = 0;          
                form_NewGame.ShowDialog(this);  
                TenQuanDo.Text = Management.nameOfGamer1;
                TenQuanDen.Text = Management.nameOfGamer2;
                AddQuanCo();                
            }
            if (new_t == 0 && open_t == 1 && exit_t == 0 && save_t == 0)
            {
                this.LuuVanCo.Visible = false;
                open_t = 0;

                //Mở OpenDialog
                /*string SourcePath;
                openFileDialog1.Filter = "Chinese Chess Board file (*.ccb)|*.ccb";
                openFileDialog1.Title = "Load ván cờ";
                openFileDialog1.Multiselect = false;
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName != "")
                {
                    //Khởi tạo bàn cờ trống
                    BanCo.ResetBanCo();

                    SourcePath = openFileDialog1.FileName;
                    VanCo.Open(SourcePath);
                    TenQuanDo.Text = VanCo.TenNguoiChoi1;
                    TenQuanDen.Text = VanCo.TenNguoiChoi2;
                }*/
                Open form_Open = new Open();
                form_Open.ShowDialog(this);
                TenQuanDo.Text = Management.nameOfGamer1;
                TenQuanDen.Text = Management.nameOfGamer2;
                if (Management.minuteOfGamer1 >= 10)
                {
                    if (Management.secondsOfGamer1 >= 10)
                    {
                        Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                    }
                    else Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                }
                if (Management.minuteOfGamer1 < 10)
                {
                    if (Management.secondsOfGamer1 >= 10) Management.lblTimeOfGamer1.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                    else Management.lblTimeOfGamer1.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                }

                if (Management.minuteOfGamer2 >= 10)
                {
                    if (Management.secondsOfGamer2 >= 10)
                    {
                        Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                    }
                    else Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                }
                if (Management.minuteOfGamer2 < 10)
                {
                    if (Management.secondsOfGamer2 >= 10) Management.lblTimeOfGamer2.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                    else Management.lblTimeOfGamer2.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                }

                //Mở lại bộ đếm thời gian
                DemThoiGian();

                //Mở lại bàn cờ
                MoBanCo();
            }
            if (new_t == 0 && open_t == 0 && exit_t == 1 && save_t == 0)
            {
                exit_t = 0;
                this.LuuVanCo.Visible = false;                
                this.Close();
            }
            if (new_t == 0 && open_t == 0 && exit_t == 0 && save_t == 1)
            {
                save_t = 0;
                this.LuuVanCo.Visible = false;
                
                //Mở lại bộ đếm thời gian
                DemThoiGian();

                //Mở lại bàn cờ
                MoBanCo();
            }
        }

        private void Closepnl_MouseEnter(object sender, EventArgs e)
        {
            Closepnl.Image = Board.Properties.Resources.Close_MouseOver;
        }
        private void Closepnl_MouseLeave(object sender, EventArgs e)
        {
            Closepnl.Image = Board.Properties.Resources.Close;
        }
        private void Closepnl_MouseClick(object sender, MouseEventArgs e)
        {
            this.LuuVanCo.Visible = false;
            new_t = 0;
            open_t = 0;
            exit_t = 0;

            //Mở lại bộ đếm thời gian
            DemThoiGian();

            //Mở lại bàn cờ
            MoBanCo();
        }
        #endregion        

        #region Menu Xin đi lại, Chịu thua của panel Management->ChieuBi và Chơi lại, Thoát của panel KetQua, Chấp cờ
        private void ChapXong_MouseEnter(object sender, EventArgs e)
        {
            Management.picCondescended.Image = Board.Properties.Resources.ChapXong_MouseOver;
        }

        private void ChapXong_MouseLeave(object sender, EventArgs e)
        {
            Management.picCondescended.Image = Board.Properties.Resources.ChapXong;
        }

        private void ChapXong_MouseClick(object sender, MouseEventArgs e)
        {
            Management.isCondescend = false;
            Management.pnlCondescend.Visible = false;
            DemThoiGian();
        }

        private void DiLai_MouseEnter(object sender, EventArgs e)
        {
            Management.picReMove.Image = Board.Properties.Resources.DiLai_MouseOver;
        }
        private void DiLai_MouseLeave(object sender, EventArgs e)
        {
            Management.picReMove.Image = Board.Properties.Resources.DiLai;
        }
        private void DiLai_MouseClick(object sender, MouseEventArgs e)
        {
            Management.Undo();
            Management.Undo();
            Management.pnlCheckMate.Visible = false;
            Management.Winner = 2;
        }

        private void ChiuThua_MouseEnter(object sender, EventArgs e)
        {
            Management.picSurrender.Image = Board.Properties.Resources.ChiuThua_MouseOver;
        }
        private void ChiuThua_MouseLeave(object sender, EventArgs e)
        {
            Management.picSurrender.Image = Board.Properties.Resources.ChiuThua;
        }
        private void ChiuThua_MouseClick(object sender, MouseEventArgs e)
        {
            if (Management.Winner == 0) Management.lblWinner.Text = Management.nameOfGamer1;
            if (Management.Winner == 1) Management.lblWinner.Text = Management.nameOfGamer2;
            Management.pnlCheckMate.Visible = false;
            Management.pnlResult.Visible = true;
            KhoaBanCo();
        }

        private void ChoiLai_MouseEnter(object sender, EventArgs e)
        {
            Management.picRePlay.Image = Board.Properties.Resources.ChoiLai_MouseOver;
        }
        private void ChoiLai_MouseLeave(object sender, EventArgs e)
        {
            Management.picRePlay.Image = Board.Properties.Resources.ChoiLai;
        }
        private void ChoiLai_MouseClick(object sender, MouseEventArgs e)
        {
            Management.pnlResult.Visible = false;
            //reset thông số trên bàn cờ      
            Management.minuteOfGamer1 = 15;
            Management.secondsOfGamer1 = 0;
            Management.minuteOfGamer2 = 15;
            Management.secondsOfGamer2 = 0;
            Management.lblTimeOfGamer1.Text = "Time: " + Convert.ToString(Management.minuteOfGamer2) + " : 00";
            Management.lblTimeOfGamer2.Text = "Time: " + Convert.ToString(Management.minuteOfGamer2) + " : 00";
            ChessBoard.Timer_NguoiChoi1.Enabled = false;
            ChessBoard.Timer_NguoiChoi2.Enabled = false;

            //tạo ván cờ mới
            Management.NewGame();
            AddQuanCo();
            if (Management.isTimeCal) ChessBoard.Timer_NguoiChoi1.Enabled = true;
        }

        private void Thoat_MouseEnter(object sender, EventArgs e)
        {
            Management.picExit.Image = Board.Properties.Resources.Thoat_MouseOver;
        }
        private void Thoat_MouseLeave(object sender, EventArgs e)
        {
            Management.picExit.Image = Board.Properties.Resources.Thoat;
        }
        private void Thoat_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Menu NewGame, Undo, Save, Open, Options, Exit, Minimize của form
        private void NewGame_MouseEnter(object sender, EventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                NewGame.Image = Board.Properties.Resources.Newgame_MouseOver;
            }
        }
        private void NewGame_MouseLeave(object sender, EventArgs e)
        {
            NewGame.Image = Board.Properties.Resources.Newgame;
        }
        private void NewGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                switch (Management.isPlaying)
                {
                    case false:
                        form_NewGame.ShowDialog(this);
                        TenQuanDo.Text = Management.nameOfGamer1;
                        TenQuanDen.Text = Management.nameOfGamer2;
                        break;
                    case true:
                        this.LuuVanCo.Visible = true;
                        //Tạm ngưng đếm ngược
                        Timer_NguoiChoi1.Enabled = false;
                        Timer_NguoiChoi2.Enabled = false;

                        KhoaBanCo();
                        new_t = 1;
                        break;
                }
                if (Management.isPlaying == true)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        for (int j = 0; j <= 8; j++)
                        {
                            this.Controls.Add(Boad_Game.Position[i, j].CanMove);
                            Boad_Game.Position[i, j].CanMove.MouseClick += new MouseEventHandler(CanMove_MouseClick);
                        }
                    }
                    AddQuanCo();
                }
            }
        }

        private void Undo_MouseEnter(object sender, EventArgs e)
        {
            //if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlChapCo.Visible == false)
            //{
            //    this.Undo.Image = Board.Properties.Resources.Undo_MouseOver;
            //}
        }
        private void Undo_MouseLeave(object sender, EventArgs e)
        {
           //đổi image khi kích 
            //this.undo.image = board.properties.resources.undo;
        }
        private void Undo_MouseClick(object sender, MouseEventArgs e)
        {
            //if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlChapCo.Visible == false)
            //{
            //    //Hoàn lại nước đi
            //    Management.Undo();
            //}
        }

        private void Save_MouseEnter(object sender, EventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                this.Save.Image = Board.Properties.Resources.Save_MouseOver;
            }
        }
        private void Save_MouseLeave(object sender, EventArgs e)
        {
            this.Save.Image = Board.Properties.Resources.Save;
        }
        private void Save_MouseClick(object sender, MouseEventArgs e)
        {
            if (Management.isPlaying == true && Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                /*string SourcePath;
                saveFileDialog1.Filter = "Chinese Chess Board file (*.ccb)|*.ccb";
                saveFileDialog1.Title = "Save ván cờ";
                if (VanCo.DangChoi == true)
                {
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        SourcePath = saveFileDialog1.FileName;
                        VanCo.Save(SourcePath);
                    }
                }*/
                //VanCo.Save();
                //Tạm ngưng đếm ngược
                Timer_NguoiChoi1.Enabled = false;
                Timer_NguoiChoi2.Enabled = false;
                KhoaBanCo();
                save_t = 1;
                this.LuuVanCo.Visible = true;
            }
        }

        private void Open_MouseEnter(object sender, EventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                this.Open.Image = Board.Properties.Resources.Open_MouseOver;
            }
        }
        private void Open_MouseLeave(object sender, EventArgs e)
        {
            this.Open.Image = Board.Properties.Resources.Open;
        }
        private void Open_MouseClick(object sender, MouseEventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                /*string SourcePath;
                openFileDialog1.Filter = "Chinese Chess Board file (*.ccb)|*.ccb";
                openFileDialog1.Title = "Load ván cờ";
                openFileDialog1.Multiselect = false;*/

                switch (Management.isPlaying)
                {
                    case false:
                        /*openFileDialog1.ShowDialog();
                        if (openFileDialog1.FileName != "")
                        {
                            //Khởi tạo bàn cờ trống
                            BanCo.ResetBanCo();

                            SourcePath = openFileDialog1.FileName;
                            VanCo.Open(SourcePath);*/
                        Open form_Open = new Open();
                        form_Open.ShowDialog(this);
                        TenQuanDo.Text = Management.nameOfGamer1;
                        TenQuanDen.Text = Management.nameOfGamer2;

                        if (Management.isPlaying)
                        {
                            if (Management.Turn == 0)
                            {
                                Management.picTurnOfGamer1.Image = Board.Properties.Resources.Turning;
                                Management.picTurnOfGamer2.Image = Board.Properties.Resources.NotTurn;
                            }
                            else
                            {
                                Management.picTurnOfGamer1.Image = Board.Properties.Resources.NotTurn;
                                Management.picTurnOfGamer2.Image = Board.Properties.Resources.Turning;
                            }
                            if (Management.isTimeCal == true)
                            {
                                if (Management.minuteOfGamer1 >= 10)
                                {
                                    if (Management.secondsOfGamer1 >= 10)
                                    {
                                        Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                                    }
                                    else Management.lblTimeOfGamer1.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                                }
                                if (Management.minuteOfGamer1 < 10)
                                {
                                    if (Management.secondsOfGamer1 >= 10) Management.lblTimeOfGamer1.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                                    else Management.lblTimeOfGamer1.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                                }

                                if (Management.minuteOfGamer2 >= 10)
                                {
                                    if (Management.secondsOfGamer2 >= 10)
                                    {
                                        Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                                    }
                                    else Management.lblTimeOfGamer2.Text = "Thời gian còn: " + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                                }
                                if (Management.minuteOfGamer2 < 10)
                                {
                                    if (Management.secondsOfGamer2 >= 10) Management.lblTimeOfGamer2.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                                    else Management.lblTimeOfGamer2.Text = "Thời gian còn: 0" + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                                }

                                if (Management.Turn == 0)
                                {
                                    Timer_NguoiChoi1.Enabled = true;
                                    Timer_NguoiChoi2.Enabled = false;
                                }
                                if (Management.Turn == 1)
                                {
                                    Timer_NguoiChoi2.Enabled = true;
                                    Timer_NguoiChoi1.Enabled = false;
                                }
                            }
                            for (int i = 0; i <= 9; i++)
                            {
                                for (int j = 0; j <= 8; j++)
                                {
                                    this.Controls.Add(Boad_Game.Position[i, j].CanMove);
                                    Boad_Game.Position[i, j].CanMove.MouseClick += new MouseEventHandler(CanMove_MouseClick);
                                }
                            }
                            AddQuanCo();
                        }
                        break;
                    case true:
                        this.LuuVanCo.Visible = true;
                        //Tạm ngưng đếm ngược
                        Timer_NguoiChoi1.Enabled = false;
                        Timer_NguoiChoi2.Enabled = false;
                        KhoaBanCo();
                        open_t = 1;
                        break;
                }
            }
        }

        private void Options_MouseEnter(object sender, EventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                this.Options.Image = Board.Properties.Resources.Options_MouseOver;
            }
        }
        private void Options_MouseLeave(object sender, EventArgs e)
        {
            this.Options.Image = Board.Properties.Resources.Options;
        }
        private void Options_MouseClick(object sender, MouseEventArgs e)
        {
            if (Management.Marked == false)
            {
                if (this.LuuVanCo.Visible == false)
                {
                    Sound_Options opt = new Sound_Options();
                    opt.ShowDialog(this);
                }
            }
        }

        private void Exit_MouseEnter(object sender, EventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                this.Exit.Image = Board.Properties.Resources.Exit_MouseOver;
            }
        }
        private void Exit_MouseLeave(object sender, EventArgs e)
        {
            this.Exit.Image = Board.Properties.Resources.Exit;
        }
        private void Exit_MouseClick(object sender, MouseEventArgs e)
        {
            if (Management.Marked == false && this.LuuVanCo.Visible == false && Management.pnlResult.Visible == false && Management.pnlCondescend.Visible == false)
            {
                switch (Management.isPlaying)
                {
                    case false:
                        this.Close();
                        break;
                    case true:
                        this.LuuVanCo.Visible = true;
                        //Tạm ngưng đếm ngược
                        Timer_NguoiChoi1.Enabled = false;
                        Timer_NguoiChoi2.Enabled = false;

                        exit_t = 1;
                        KhoaBanCo();
                        break;
                }
            }
        }

        private void Mini_MouseEnter(object sender, EventArgs e)
        {
            this.Mini.Image = Board.Properties.Resources.Mini_MouseOver;
        }
        private void Mini_MouseLeave(object sender, EventArgs e)
        {
            this.Mini.Image = Board.Properties.Resources.Mini;
        }
        private void Mini_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Phương thức di chuyển quân cờ
        private void ChessBoard_MouseClick(Object sender, MouseEventArgs e)
        {
            ChessPiece temp;
            temp = new ChessPiece();
            switch (Management.Marked)
            {
                case true:
                    Management.Marked = false;

                    //Kiểm tra, tham chiếu temp đến quân cờ được Đánh Dấu
                    if (Management.ChessMarked.Name == "tuong") temp = Management.Gamer[Management.ChessMarked.Party].qGeneral;
                    if (Management.ChessMarked.Name == "sy")
                    {
                        if (Management.ChessMarked.Order == "0") temp = Management.Gamer[Management.ChessMarked.Party].qAdvisor[0];
                        if (Management.ChessMarked.Order == "1") temp = Management.Gamer[Management.ChessMarked.Party].qAdvisor[1];
                    }
                    if (Management.ChessMarked.Name == "tinh")
                    {
                        if (Management.ChessMarked.Order == "0") temp = Management.Gamer[Management.ChessMarked.Party].qElephant[0];
                        if (Management.ChessMarked.Order == "1") temp = Management.Gamer[Management.ChessMarked.Party].qElephant[1];
                    }
                    if (Management.ChessMarked.Name == "xe")
                    {
                        if (Management.ChessMarked.Order == "0") temp = Management.Gamer[Management.ChessMarked.Party].qChariot[0];
                        if (Management.ChessMarked.Order == "1") temp = Management.Gamer[Management.ChessMarked.Party].qChariot[1];
                    }
                    if (Management.ChessMarked.Name == "phao")
                    {
                        if (Management.ChessMarked.Order == "0") temp = Management.Gamer[Management.ChessMarked.Party].qCannon[0];
                        if (Management.ChessMarked.Order == "1") temp = Management.Gamer[Management.ChessMarked.Party].qCannon[1];
                    }
                    if (Management.ChessMarked.Name == "ma")
                    {
                        if (Management.ChessMarked.Order == "0") temp = Management.Gamer[Management.ChessMarked.Party].qHorse[0];
                        if (Management.ChessMarked.Order == "1") temp = Management.Gamer[Management.ChessMarked.Party].qHorse[1];
                    }
                    if (Management.ChessMarked.Name == "chot")
                    {
                        if (Management.ChessMarked.Order == "0") temp = Management.Gamer[Management.ChessMarked.Party].qSoldier[0];
                        if (Management.ChessMarked.Order == "1") temp = Management.Gamer[Management.ChessMarked.Party].qSoldier[1];
                        if (Management.ChessMarked.Order == "2") temp = Management.Gamer[Management.ChessMarked.Party].qSoldier[2];
                        if (Management.ChessMarked.Order == "3") temp = Management.Gamer[Management.ChessMarked.Party].qSoldier[3];
                        if (Management.ChessMarked.Order == "4") temp = Management.Gamer[Management.ChessMarked.Party].qSoldier[4];
                    }

                    if (temp.Party == 0)
                    {
                        if (temp.Name == "tuong") temp.picChessPiece.Image = Board.Properties.Resources._1tuong;
                        if (temp.Name == "sy") temp.picChessPiece.Image = Board.Properties.Resources._1sy;
                        if (temp.Name == "tinh") temp.picChessPiece.Image = Board.Properties.Resources._1tinh;
                        if (temp.Name == "xe") temp.picChessPiece.Image = Board.Properties.Resources._1xe;
                        if (temp.Name == "phao") temp.picChessPiece.Image = Board.Properties.Resources._1phao;
                        if (temp.Name == "ma") temp.picChessPiece.Image = Board.Properties.Resources._1ma;
                        if (temp.Name == "chot") temp.picChessPiece.Image = Board.Properties.Resources._1chot;
                    }
                    if (temp.Party == 1)
                    {
                        if (temp.Name == "tuong") temp.picChessPiece.Image = Board.Properties.Resources._2tuong;
                        if (temp.Name == "sy") temp.picChessPiece.Image = Board.Properties.Resources._2sy;
                        if (temp.Name == "tinh") temp.picChessPiece.Image = Board.Properties.Resources._2tinh;
                        if (temp.Name == "xe") temp.picChessPiece.Image = Board.Properties.Resources._2xe;
                        if (temp.Name == "phao") temp.picChessPiece.Image = Board.Properties.Resources._2phao;
                        if (temp.Name == "ma") temp.picChessPiece.Image = Board.Properties.Resources._2ma;
                        if (temp.Name == "chot") temp.picChessPiece.Image = Board.Properties.Resources._2chot;
                    }
                    
                    Boad_Game.ResetCanMove();
                    break;
                case false:
                    break;
            }
        }
        private void CanMove_MouseClick(Object sender, MouseEventArgs e)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (sender.Equals(Boad_Game.Position[i, j].CanMove)) 
                    {
                        if (Management.Marked)
                        {
                            switch (Boad_Game.Position[i, j].isEmpty)
                            {
                                case true:
                                    if (Management.ChessMarked.Party == 0)
                                    {
                                        if (Management.ChessMarked.Name == "tuong") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1tuong;
                                        if (Management.ChessMarked.Name == "sy") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1sy;
                                        if (Management.ChessMarked.Name == "tinh") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1tinh;
                                        if (Management.ChessMarked.Name == "xe") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1xe;
                                        if (Management.ChessMarked.Name == "phao") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1phao;
                                        if (Management.ChessMarked.Name == "ma") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1ma;
                                        if (Management.ChessMarked.Name == "chot") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1chot;
                                    }
                                    if (Management.ChessMarked.Party == 1)
                                    {
                                        if (Management.ChessMarked.Name == "tuong") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2tuong;
                                        if (Management.ChessMarked.Name == "sy") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2sy;
                                        if (Management.ChessMarked.Name == "tinh") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2tinh;
                                        if (Management.ChessMarked.Name == "xe") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2xe;
                                        if (Management.ChessMarked.Name == "phao") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2phao;
                                        if (Management.ChessMarked.Name == "ma") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2ma;
                                        if (Management.ChessMarked.Name == "chot") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2chot;
                                    }
                                    //Bỏ chọn quân cờ
                                    Management.Marked = false;

                                    //Ghi thông tin nước đi vào GameLog
                                    Management.LuuVaoGameLog(this, Management.ChessMarked);

                                    //Ô cờ trống tại ví trí ban đầu                
                                    Management.Blank_FlagPoint(Management.ChessMarked.Row, Management.ChessMarked.Column);

                                    //Đặt quân cờ đã chọn vào vị trí mới [i,j]
                                    Management.SetChess(sender, Management.ChessMarked, i, j);

                                    //Tiếng động
                                    if (Management.isSound) Management.ClickSound("0");

                                    //Kiểm tra chiếu tướng
                                    Management.Checkmate_Test();       

                                    //Thay đổi lượt đi                        
                                    Management.ChangeMove();                                    

                                    //Kiểm tra chiếu bí
                                    Management.CheckmateEnd_Test();
                                    if (Management.Winner != 2)
                                    {
                                        Management.picNotifyCheckmate.Visible = false;
                                        Management.pnlCheckMate.Visible = true;
                                    }
                                    else Management.pnlCheckMate.Visible = false;                           

                                    Boad_Game.ResetCanMove();
                                    break;

                                case false:
                                    if (Management.ChessMarked.Party == 0)
                                    {
                                        if (Management.ChessMarked.Name == "tuong") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1tuong;
                                        if (Management.ChessMarked.Name == "sy") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1sy;
                                        if (Management.ChessMarked.Name == "tinh") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1tinh;
                                        if (Management.ChessMarked.Name == "xe") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1xe;
                                        if (Management.ChessMarked.Name == "phao") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1phao;
                                        if (Management.ChessMarked.Name == "ma") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1ma;
                                        if (Management.ChessMarked.Name == "chot") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._1chot;
                                    }
                                    if (Management.ChessMarked.Party == 1)
                                    {
                                        if (Management.ChessMarked.Name == "tuong") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2tuong;
                                        if (Management.ChessMarked.Name == "sy") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2sy;
                                        if (Management.ChessMarked.Name == "tinh") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2tinh;
                                        if (Management.ChessMarked.Name == "xe") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2xe;
                                        if (Management.ChessMarked.Name == "phao") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2phao;
                                        if (Management.ChessMarked.Name == "ma") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2ma;
                                        if (Management.ChessMarked.Name == "chot") Management.ChessMarked.picChessPiece.Image = Board.Properties.Resources._2chot;
                                    }

                                    int phekia = 2;
                                    if (Management.ChessMarked.Party == 0) phekia = 1;
                                    else phekia = 0;
                                    ChessPiece temp_c;
                                    temp_c = new ChessPiece();

                                    if (Boad_Game.Position[i, j].Name == "tuong") temp_c = Management.Gamer[phekia].qGeneral;
                                    if (Boad_Game.Position[i, j].Name == "sy")
                                    {
                                        if (Boad_Game.Position[i, j].Order == "0") temp_c = Management.Gamer[phekia].qAdvisor[0];
                                        if (Boad_Game.Position[i, j].Order == "1") temp_c = Management.Gamer[phekia].qAdvisor[1];
                                    }
                                    if (Boad_Game.Position[i, j].Name == "tinh")
                                    {
                                        if (Boad_Game.Position[i, j].Order == "0") temp_c = Management.Gamer[phekia].qElephant[0];
                                        if (Boad_Game.Position[i, j].Order == "1") temp_c = Management.Gamer[phekia].qElephant[1];
                                    }
                                    if (Boad_Game.Position[i, j].Name == "xe")
                                    {
                                        if (Boad_Game.Position[i, j].Order == "0") temp_c = Management.Gamer[phekia].qChariot[0];
                                        if (Boad_Game.Position[i, j].Order == "1") temp_c = Management.Gamer[phekia].qChariot[1];
                                    }
                                    if (Boad_Game.Position[i, j].Name == "phao")
                                    {
                                        if (Boad_Game.Position[i, j].Order == "0") temp_c = Management.Gamer[phekia].qCannon[0];
                                        if (Boad_Game.Position[i, j].Order == "1") temp_c = Management.Gamer[phekia].qCannon[1];
                                    }
                                    if (Boad_Game.Position[i, j].Name == "ma")
                                    {
                                        if (Boad_Game.Position[i, j].Order == "0") temp_c = Management.Gamer[phekia].qHorse[0];
                                        if (Boad_Game.Position[i, j].Order == "1") temp_c = Management.Gamer[phekia].qHorse[1];
                                    }
                                    if (Boad_Game.Position[i, j].Name == "chot")
                                    {
                                        if (Boad_Game.Position[i, j].Order == "0") temp_c = Management.Gamer[phekia].qSoldier[0];
                                        if (Boad_Game.Position[i, j].Order == "1") temp_c = Management.Gamer[phekia].qSoldier[1];
                                        if (Boad_Game.Position[i, j].Order == "2") temp_c = Management.Gamer[phekia].qSoldier[2];
                                        if (Boad_Game.Position[i, j].Order == "3") temp_c = Management.Gamer[phekia].qSoldier[3];
                                        if (Boad_Game.Position[i, j].Order == "4") temp_c = Management.Gamer[phekia].qSoldier[4];
                                    }

                                    //Bỏ chọn quân cờ
                                    Management.Marked = false;

                                    //Ghi thông tin nước đi vào GameLog
                                    Management.LuuVaoGameLog(sender, temp_c);

                                    //Ăn quân cờ của đối phương
                                    Management.Destroy_Chess(temp_c);

                                    //Trả lại ô cờ trống
                                    Management.Blank_FlagPoint(Management.ChessMarked.Row, Management.ChessMarked.Column);

                                    //Thiết lập quân cờ đã chọn vào bàn cờ
                                    Management.SetChess(sender, Management.ChessMarked, i, j);
                                    
                                    //Tiếng động
                                    if (Management.isSound) Management.ClickSound(Management.ChessMarked.Name);

                                    //Kiểm tra chiếu tướng
                                    Management.Checkmate_Test();          

                                    //Thay đổi lượt đi                            
                                    Management.ChangeMove();
                                    
                                    //Kiểm tra chiếu bí
                                    Management.CheckmateEnd_Test();
                                    if (Management.Winner != 2)
                                    {
                                        Management.picNotifyCheckmate.Visible = false;
                                        Management.pnlCheckMate.Visible = true;
                                    }
                                    else Management.pnlCheckMate.Visible = false;

                                    Boad_Game.ResetCanMove();
                                    break;
                            }
                        }
                    }
                }

            }
        }
        #endregion

        #region Khóa Bàn Cờ, Add Quân Cờ, TimeTick
        private void Timer_NguoiChoi1_Tick(object sender, EventArgs e)
        {
            Management.secondsOfGamer1--;
            if (Management.secondsOfGamer1 < 0) 
            {
                Management.minuteOfGamer1--;
                Management.secondsOfGamer1=59;
            }
            if (Management.minuteOfGamer1 >= 0)
            {
                if (Management.minuteOfGamer1 >= 10)
                {
                    if (Management.secondsOfGamer1 >= 10)
                    {
                        Management.lblTimeOfGamer1.Text = "Time: " + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                    }
                    else Management.lblTimeOfGamer1.Text = "Time: " + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                }
                if (Management.minuteOfGamer1 < 10)
                {
                    if (Management.secondsOfGamer1 >= 10) Management.lblTimeOfGamer1.Text = "Time: 0" + Convert.ToString(Management.minuteOfGamer1) + " : " + Convert.ToString(Management.secondsOfGamer1);
                    else Management.lblTimeOfGamer1.Text = "Time: 0" + Convert.ToString(Management.minuteOfGamer1) + " : 0" + Convert.ToString(Management.secondsOfGamer1);
                }
            }
            else
            {
                Timer_NguoiChoi1.Enabled = false;
                KhoaBanCo();
                Management.lblWinner.Text = Management.nameOfGamer2;
                Management.Winner = 1;
                Management.pnlResult.Visible = true;
            }
        }
        private void Timer_NguoiChoi2_Tick(object sender, EventArgs e)
        {
            Management.secondsOfGamer2--;
            if (Management.secondsOfGamer2 < 0)
            {
                Management.minuteOfGamer2--;
                Management.secondsOfGamer2 = 59;
            }
            if (Management.minuteOfGamer2 >= 0)
            {
                if (Management.minuteOfGamer2 >= 10)
                {
                    if (Management.secondsOfGamer2 >= 10) Management.lblTimeOfGamer2.Text = "Time: " + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                    else Management.lblTimeOfGamer2.Text = "Time: " + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                }
                if (Management.minuteOfGamer2 < 10)
                {
                    if (Management.secondsOfGamer2 >= 10) Management.lblTimeOfGamer2.Text = "Time: 0" + Convert.ToString(Management.minuteOfGamer2) + " : " + Convert.ToString(Management.secondsOfGamer2);
                    else Management.lblTimeOfGamer2.Text = "Time: 0" + Convert.ToString(Management.minuteOfGamer2) + " : 0" + Convert.ToString(Management.secondsOfGamer2);
                }
            }
            else
            {
                Timer_NguoiChoi2.Enabled = false;
                KhoaBanCo();
                Management.lblWinner.Text = Management.nameOfGamer1;
                Management.Winner = 0;
                Management.pnlResult.Visible = true;
            }
        }

        private void DemThoiGian()
        {
            if (Management.isTimeCal)
            {
                if (Management.Turn == 0)
                {
                    Timer_NguoiChoi1.Enabled = true;
                    Timer_NguoiChoi2.Enabled = false;
                }
                if (Management.Turn == 1)
                {
                    Timer_NguoiChoi2.Enabled = true;
                    Timer_NguoiChoi1.Enabled = false;
                }
            }
        }

        private void MoBanCo()
        {
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
        }

        private void KhoaBanCo()
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
        }
        private void AddQuanCo()
        {
            this.Controls.Add(Management.Gamer[0].qGeneral.picChessPiece);
            this.Controls.Add(Management.Gamer[0].qAdvisor[0].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qAdvisor[1].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qElephant[0].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qElephant[1].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qChariot[0].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qChariot[1].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qCannon[0].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qCannon[1].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qHorse[0].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qHorse[1].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qSoldier[0].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qSoldier[1].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qSoldier[2].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qSoldier[3].picChessPiece);
            this.Controls.Add(Management.Gamer[0].qSoldier[4].picChessPiece);

            this.Controls.Add(Management.Gamer[1].qGeneral.picChessPiece);
            this.Controls.Add(Management.Gamer[1].qAdvisor[0].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qAdvisor[1].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qElephant[0].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qElephant[1].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qChariot[0].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qChariot[1].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qCannon[0].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qCannon[1].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qHorse[0].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qHorse[1].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qSoldier[0].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qSoldier[1].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qSoldier[2].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qSoldier[3].picChessPiece);
            this.Controls.Add(Management.Gamer[1].qSoldier[4].picChessPiece);

            this.Controls.Add(Management.pnlCondescend);
            this.Controls.Add(Management.pnlCheckMate);
            this.Controls.Add(Management.picNotifyCheckmate);
            this.Controls.Add(Management.pnlResult);
            this.Controls.Add(Management.picTurnOfGamer1);
            this.Controls.Add(Management.picTurnOfGamer2);
            this.Controls.Add(Management.lblTimeOfGamer1);
            this.Controls.Add(Management.lblTimeOfGamer2);
            
            Management.picCondescended.MouseEnter+=new EventHandler(ChapXong_MouseEnter);
            Management.picCondescended.MouseLeave+=new EventHandler(ChapXong_MouseLeave);
            Management.picCondescended.MouseClick+=new MouseEventHandler(ChapXong_MouseClick);
            Management.picReMove.MouseEnter += new System.EventHandler(DiLai_MouseEnter);
            Management.picReMove.MouseLeave += new System.EventHandler(DiLai_MouseLeave);
            Management.picReMove.MouseClick += new MouseEventHandler(DiLai_MouseClick);
            Management.picSurrender.MouseEnter += new System.EventHandler(ChiuThua_MouseEnter);
            Management.picSurrender.MouseLeave += new System.EventHandler(ChiuThua_MouseLeave);
            Management.picSurrender.MouseClick += new MouseEventHandler(ChiuThua_MouseClick);

            Management.picRePlay.MouseEnter+=new EventHandler(ChoiLai_MouseEnter);
            Management.picRePlay.MouseLeave+=new EventHandler(ChoiLai_MouseLeave);
            Management.picRePlay.MouseClick+=new MouseEventHandler(ChoiLai_MouseClick);
            Management.picExit.MouseEnter+=new EventHandler(Thoat_MouseEnter);
            Management.picExit.MouseLeave+=new EventHandler(Thoat_MouseLeave);
            Management.picExit.MouseClick+=new MouseEventHandler(Thoat_MouseClick);
        }        
        #endregion

        #region Load Form - Set giá trị ban đầu
        
        #endregion

    }
}