using System;
using System.Collections.Generic;
using System.Text;

namespace Board
{
    class Chariot : ChessPiece  // Quân xe
    {
        public override int Move_Test(int i, int j)
        {
            bool turn = true;
            if (i < 0 || i > 9 || j < 0 || j > 8) turn = false;
            if (i != Row && j != Column) turn = false;
            if (i == Row && j >= 0 && j <= 8)
            {
                if (j > Column)
                    for (int k = Column + 1; k < j; k++)
                    {
                        if (Boad_Game.Position[i, k].isEmpty == false)
                        {
                            turn = false;
                            break;
                        }
                    }
                if (j < Column)
                    for (int k = j+1; k <= Column - 1; k++)
                    {
                        if (Boad_Game.Position[i, k].isEmpty == false)
                        {
                            turn = false;
                            break;
                        }
                    }
                if (Boad_Game.Position[i, j].isEmpty == false)
                    if (Boad_Game.Position[i, j].Party == this.Party) turn = false;
            }
            if (j==Column && i>=0 && i<=9)
            {
                if (i > Row)
                    for (int k = Row + 1; k < i; k++)
                    {
                        if (Boad_Game.Position[k, j].isEmpty == false)
                        {
                            turn = false;
                            break;
                        }
                    }
                if (i < Row)
                    for (int k = i+1; k <= Row - 1; k++)
                    {
                        if (Boad_Game.Position[k, j].isEmpty == false)
                        {
                            turn = false;
                            break;
                        }
                    }
                if (Boad_Game.Position[i, j].isEmpty == false)
                    if (Boad_Game.Position[i, j].Party == this.Party) turn = false;
            }            
            
            if (Management.Gamer[0].qGeneral.Column == Management.Gamer[1].qGeneral.Column && Column == Management.Gamer[1].qGeneral.Column)
            {
                int ct = 0;
                if (j != Column)
                {
                    Management.Blank_FlagPoint(Row, Column);
                    for (int t = Management.Gamer[0].qGeneral.Row + 1; t < Management.Gamer[1].qGeneral.Row; t++)
                    {
                        if (Boad_Game.Position[t, Column].isEmpty == false) ct++;
                    }
                    if (ct == 0) turn = false;
                    Boad_Game.Position[Row, Column].isEmpty = false;
                    Boad_Game.Position[Row, Column].Name = this.Name;
                    Boad_Game.Position[Row, Column].Party = this.Party;
                    Boad_Game.Position[Row, Column].Order = this.Order;
                }
            }
            //Trả về kết quả
            if (turn) return 1;
            else return 0;
        }

        public override int SafeGeneral_Test(int i, int j)
        {
            bool turn = true;

            //Kiểm tra tướng an toàn
            if (turn == true)
            {
                int ho, co;
                ChessPiece temp;
                temp = new ChessPiece();

                ho = this.Row;
                co = this.Column;

                if (Boad_Game.Position[i, j].isEmpty == false)
                {
                    if (Boad_Game.Position[i, j].Name == "tuong") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qGeneral;
                    if (Boad_Game.Position[i, j].Name == "sy")
                    {
                        if (Boad_Game.Position[i, j].Order == "0") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qAdvisor[0];
                        if (Boad_Game.Position[i, j].Order == "1") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qAdvisor[1];
                    }
                    if (Boad_Game.Position[i, j].Name == "tinh")
                    {
                        if (Boad_Game.Position[i, j].Order == "0") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qElephant[0];
                        if (Boad_Game.Position[i, j].Order == "1") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qElephant[1];
                    }
                    if (Boad_Game.Position[i, j].Name == "xe")
                    {
                        if (Boad_Game.Position[i, j].Order == "0") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qChariot[0];
                        if (Boad_Game.Position[i, j].Order == "1") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qChariot[1];
                    }
                    if (Boad_Game.Position[i, j].Name == "phao")
                    {
                        if (Boad_Game.Position[i, j].Order == "0") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qCannon[0];
                        if (Boad_Game.Position[i, j].Order == "1") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qCannon[1];
                    }
                    if (Boad_Game.Position[i, j].Name == "ma")
                    {
                        if (Boad_Game.Position[i, j].Order == "0") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qHorse[0];
                        if (Boad_Game.Position[i, j].Order == "1") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qHorse[1];
                    }
                    if (Boad_Game.Position[i, j].Name == "chot")
                    {
                        if (Boad_Game.Position[i, j].Order == "0") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qSoldier[0];
                        if (Boad_Game.Position[i, j].Order == "1") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qSoldier[1];
                        if (Boad_Game.Position[i, j].Order == "2") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qSoldier[2];
                        if (Boad_Game.Position[i, j].Order == "3") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qSoldier[3];
                        if (Boad_Game.Position[i, j].Order == "4") temp = Management.Gamer[Boad_Game.Position[i, j].Party].qSoldier[4];
                    }
                }
                //Giả sử quân cờ được đi để kiểm tra Tướng mình có bị chiếu ko
                Management.Blank_FlagPoint(Row, Column);
                Boad_Game.Position[i, j].isEmpty = false;
                Boad_Game.Position[i, j].Party = Party;
                Boad_Game.Position[i, j].Name = Name;
                Boad_Game.Position[i, j].Order = Order;
                this.Row = i;
                this.Column = j;
                if (temp.Party != 2)
                {
                    temp.Status = 0;
                    temp.picChessPiece.Visible = false;
                }

                //Kiểm tra
                if (Management.Checkmate_Next(Management.Gamer[Party].qGeneral) == true) turn = false;

                //Trả lại những gì đã giả sử ở trên ^^!
                this.Row = ho;
                this.Column = co;
                Management.Blank_FlagPoint(i, j);
                Boad_Game.Position[ho, co].isEmpty = false;
                Boad_Game.Position[ho, co].Party = Party;
                Boad_Game.Position[ho, co].Name = Name;
                Boad_Game.Position[ho, co].Order = Order;
                if (temp.Party != 2)
                {
                    temp.Status = 1;
                    temp.picChessPiece.Visible = true;
                    Boad_Game.Position[i, j].isEmpty = false;
                    Boad_Game.Position[i, j].Name = temp.Name;
                    Boad_Game.Position[i, j].Party = temp.Party;
                    Boad_Game.Position[i, j].Order = temp.Order;
                }
            } 
            //Trả về kết quả
            if (turn) return 1;
            else return 0;
        }
    }
}

