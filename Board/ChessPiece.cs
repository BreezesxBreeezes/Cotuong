using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace Board
{
    class ChessPiece
    {
        public int Row;
        public int Column;
        public string Name;
        public int Party;   //Phe=0 || Phe=1 || Phe=2 (Tại ô đó không có quân cờ)
        public string Order="";//VD: quân mã phía trái, phải
        public int Status;  // Status = 1: quân cờ còn sống, Status = 0: quân cờ đã chết hoặc bị ăn.
        public PictureBox picChessPiece = new PictureBox();
        public bool isLocked = false;

        public ChessPiece()
        {
            Row = 10;
            Column = 10;
            Name = "";
            Party = 2;
            Order = "";
            Status = 0;
            isLocked = true;
        }
        
        //Khởi tạo quân cờ
        public void Initialize(int phe, string name, string thutu, int stt, bool khoa, int x, int y) 
        {
            Row = x;
            Column = y;
            Name = name;
            Status = stt;
            Party = phe;
            Order = thutu;
            isLocked = khoa;
            Boad_Game.Position[x, y].Row = x;
            Boad_Game.Position[x, y].Column = y;
            Boad_Game.Position[x, y].Name = name;
            Boad_Game.Position[x, y].Order = thutu;
            Boad_Game.Position[x, y].Party = phe;
            Boad_Game.Position[x, y].isEmpty = false;
            picChessPiece.MouseClick += new MouseEventHandler(picChessPiece_MouseClick);        // xử dụng 
            
        }
        
        //Hàm vẽ quân cờ
        public void draw()
        {
            if (Party == 0)
            {
                if (Name == "tuong") picChessPiece.Image = Board.Properties.Resources._1tuong;
                if (Name == "sy") picChessPiece.Image = Board.Properties.Resources._1sy;
                if (Name == "tinh") picChessPiece.Image = Board.Properties.Resources._1tinh;
                if (Name == "xe") picChessPiece.Image = Board.Properties.Resources._1xe;
                if (Name == "phao") picChessPiece.Image = Board.Properties.Resources._1phao;
                if (Name == "ma") picChessPiece.Image = Board.Properties.Resources._1ma;
                if (Name == "chot") picChessPiece.Image = Board.Properties.Resources._1chot;
            }
            if (Party == 1)
            {
                if (Name == "tuong") picChessPiece.Image = Board.Properties.Resources._2tuong;
                if (Name == "sy") picChessPiece.Image = Board.Properties.Resources._2sy;
                if (Name == "tinh") picChessPiece.Image = Board.Properties.Resources._2tinh;
                if (Name == "xe") picChessPiece.Image = Board.Properties.Resources._2xe;
                if (Name == "phao") picChessPiece.Image = Board.Properties.Resources._2phao;
                if (Name == "ma") picChessPiece.Image = Board.Properties.Resources._2ma;
                if (Name == "chot") picChessPiece.Image = Board.Properties.Resources._2chot;
            }

            //Vẽ quân cờ
            picChessPiece.Width = 42;
            picChessPiece.Height = 42;
            picChessPiece.Cursor = Cursors.Hand;
            picChessPiece.Top = Row * 53 + 80;
            picChessPiece.Left = Column * 53 + 61;
            picChessPiece.BackColor = Color.Transparent;
            
            //Thiết lập quân cờ trên Bàn Cờ
            Boad_Game.Position[Row, Column].Row = Row;
            Boad_Game.Position[Row, Column].Column = Column;
            Boad_Game.Position[Row, Column].isEmpty = false;
            Boad_Game.Position[Row, Column].Name = Name;
            Boad_Game.Position[Row, Column].Order = Order;
            Boad_Game.Position[Row, Column].Party = Party;            
        }


        public virtual int Move_Test(int i,int j)
        {
            return 0;
        }

        public virtual int SafeGeneral_Test(int i, int j)
        {
            return 0;
        }     
       
        //------------Thao tác trên quân cờ-------------------------//
        private void picChessPiece_MouseClick(Object sender, MouseEventArgs e)
        {
            if (Management.isCondescend)
            {
                if (this.Name != "tuong") Management.Destroy_Chess(this);
                Boad_Game.Position[this.Row, this.Column].isEmpty = true;
                Boad_Game.Position[this.Row, this.Column].Party = 2;
                Boad_Game.Position[this.Row, this.Column].Order = "";
                Boad_Game.Position[this.Row, this.Column].Name = "";

            }
            else
            {
                switch (Management.Marked)
                {
                    case true:
                        if (this.Status == 1)
                        {
                            if (this.Party == Management.ChessMarked.Party)
                            {
                                Management.Marked = false;
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
                                Boad_Game.ResetCanMove();
                            }
                            if (this.Party != Management.ChessMarked.Party)
                            {
                                if (Management.ChessMarked.Move_Test(this.Row, this.Column) == 1 && Management.ChessMarked.SafeGeneral_Test(this.Row, this.Column) == 1)//Nếu nước đi hợp lệ
                                {
                                    //Ghi thông tin nước đi vào GameLog
                                    Management.LuuVaoGameLog(sender, this);

                                    //Ăn quân cờ của đối phương
                                    Management.Destroy_Chess(this);

                                    //Trả lại ô cờ trống
                                    Management.Blank_FlagPoint(Management.ChessMarked.Row, Management.ChessMarked.Column);

                                    //Thiết lập quân cờ đã chọn vào bàn cờ
                                    Management.SetChess(sender, Management.ChessMarked, this.Row, this.Column);


                                    //Tiếng động
                                    if (Management.isSound) Management.ClickSound(Management.ChessMarked.Name);

                                    //Kiểm tra chiếu tướng
                                    Management.Checkmate_Test();

                                    //Thay đổi lượt đi                            
                                    Management.ChangeMove();

                                    //Kiểm tra quân Tướng của đối phương đã bị ăn chưa
                                    Management.CheckmateEnd_Test();
                                    if (Management.Winner != 2)
                                    {
                                        Management.picNotifyCheckmate.Visible = false;
                                        Management.pnlCheckMate.Visible = true;
                                    }
                                    else Management.pnlCheckMate.Visible = false;


                                    Boad_Game.ResetCanMove();
                                }
                                Management.Marked = false;

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
                                Boad_Game.ResetCanMove();
                            }
                        }
                        break;
                    case false:
                        if (this.Status == 1)
                        {
                            if (this.isLocked == false)
                            {
                                //Đánh dấu quân cờ được chọn
                                Management.Marked = true;
                                Management.ChessMarked = new ChessPiece();
                                Management.ChessMarked = this;

                                if (Party == 0)
                                {
                                    if (Name == "tuong") picChessPiece.Image = Board.Properties.Resources._1tuong_marked;
                                    if (Name == "sy") picChessPiece.Image = Board.Properties.Resources._1sy_marked;
                                    if (Name == "tinh") picChessPiece.Image = Board.Properties.Resources._1tinh_marked;
                                    if (Name == "xe") picChessPiece.Image = Board.Properties.Resources._1xe_marked;
                                    if (Name == "phao") picChessPiece.Image = Board.Properties.Resources._1phao_marked;
                                    if (Name == "ma") picChessPiece.Image = Board.Properties.Resources._1ma_marked;
                                    if (Name == "chot") picChessPiece.Image = Board.Properties.Resources._1chot_marked;
                                }
                                if (Party == 1)
                                {
                                    if (Name == "tuong") picChessPiece.Image = Board.Properties.Resources._2tuong_marked;
                                    if (Name == "sy") picChessPiece.Image = Board.Properties.Resources._2sy_marked;
                                    if (Name == "tinh") picChessPiece.Image = Board.Properties.Resources._2tinh_marked;
                                    if (Name == "xe") picChessPiece.Image = Board.Properties.Resources._2xe_marked;
                                    if (Name == "phao") picChessPiece.Image = Board.Properties.Resources._2phao_marked;
                                    if (Name == "ma") picChessPiece.Image = Board.Properties.Resources._2ma_marked;
                                    if (Name == "chot") picChessPiece.Image = Board.Properties.Resources._2chot_marked;
                                }
                                for (int i = 0; i <= 9; i++)
                                {
                                    for (int j = 0; j <= 8; j++)
                                    {
                                        if (i != Row || j != Column)
                                            if (this.Move_Test(i, j) == 1 && this.SafeGeneral_Test(i, j) == 1) Boad_Game.Position[i, j].CanMove.Visible = true;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
