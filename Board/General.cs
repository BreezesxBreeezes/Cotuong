using System;
using System.Collections.Generic;
using System.Text;

namespace Board
{
    class General:ChessPiece
    {
        
        public override int Move_Test(int i, int j)
        {
            bool turn = false;
            int ho, co;
            ChessPiece temp;
            temp = new ChessPiece();
            
            if ((i >= 0 && i <= 2 && j >= 3 && j <= 5) || (i >= 7 && i <= 9 && j >= 3 && j <= 5))
                if ((i == Row + 1 && j == Column) || (i == Row - 1 && j == Column) || (i == Row && j == Column + 1) || (i == Row && j == Column - 1))
                {
                    if (Boad_Game.Position[i, j].isEmpty == true) turn = true;
                    if (Boad_Game.Position[i, j].isEmpty == false)
                        if (Boad_Game.Position[i, j].Party != this.Party) turn = true;
                }
            

            if (Party == 0)
            {
                int ct=0;
                if (j == Management.Gamer[1].qGeneral.Column)
                {
                    if (Boad_Game.Position[i, j].isEmpty == true)
                    {
                        for (int t = Row + 1; t < Management.Gamer[1].qGeneral.Row; t++)
                        {
                            if (Boad_Game.Position[t, j].isEmpty == false) ct++;
                        }
                        if (ct == 0) turn = false;
                    }
                    else
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
                        ho = this.Row;
                        co = this.Column;
                        Management.Blank_FlagPoint(Row, Column);
                        Boad_Game.Position[i, j].isEmpty = false;
                        Boad_Game.Position[i, j].Party = Party;
                        Boad_Game.Position[i, j].Name = Name;
                        Boad_Game.Position[i, j].Order = Order;
                        this.Row = i;
                        this.Column = j;                        
                        temp.Status = 0;
                        temp.picChessPiece.Visible = false;

                        //kiem tra
                        for (int t = Row + 1; t < Management.Gamer[1].qGeneral.Row; t++)
                        {
                            if (Boad_Game.Position[t, j].isEmpty == false) ct++;
                        }
                        if (ct == 0) turn = false;

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
                }
            }
            if (Party == 1)
            {
                int ct = 0;
                if (j == Management.Gamer[0].qGeneral.Column)
                {
                    if (Boad_Game.Position[i, j].isEmpty == true)
                    {
                        for (int t = Row - 1; t > Management.Gamer[0].qGeneral.Row; t--)
                        {
                            if (Boad_Game.Position[t, j].isEmpty == false) ct++;
                        }
                        if (ct == 0) turn = false;
                    }
                    else
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
                        ho = this.Row;
                        co = this.Column;
                        Management.Blank_FlagPoint(Row, Column);
                        Boad_Game.Position[i, j].isEmpty = false;
                        Boad_Game.Position[i, j].Party = Party;
                        Boad_Game.Position[i, j].Name = Name;
                        Boad_Game.Position[i, j].Order = Order;
                        this.Row = i;
                        this.Column = j;
                        temp.Status = 0;
                        temp.picChessPiece.Visible = false;

                        //kiem tra
                        for (int t = Row - 1; t > Management.Gamer[0].qGeneral.Row; t--)
                        {
                            if (Boad_Game.Position[t, j].isEmpty == false) ct++;
                        }
                        if (ct == 0) turn = false;

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
