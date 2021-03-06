using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Board
{
    class Player
    {
        public int Party;       // phe của người chơi
        public General qGeneral = new General();        // quân tướng 
        public Advisor[] qAdvisor = new Advisor[2];        //2 quân sĩ
        public Elephant[] qElephant = new Elephant[2];      // 2 quân tượng
        public Chariot[] qChariot = new Chariot[2];         //2 quân xe
        public Cannon[] qCannon = new Cannon[2];        // 2 quân pháo
        public Horse[] qHorse = new Horse[2];           // 2 quân mã
        public Soldier[] qSoldier = new Soldier[5];     // 5 quân chốt
               
        public Player(int x) //Khởi tạo cho người chơi
        {
           
            if (x == 0)
            {
                qAdvisor[0] = new Advisor();
                qAdvisor[1] = new Advisor();
                qElephant[0] = new Elephant();
                qElephant[1] = new Elephant();
                qChariot[0] = new Chariot();
                qChariot[1] = new Chariot();
                qCannon[0] = new Cannon();
                qCannon[1] = new Cannon();
                qHorse[0] = new Horse();
                qHorse[1] = new Horse();
                qSoldier[0] = new Soldier();
                qSoldier[1] = new Soldier();
                qSoldier[2] = new Soldier();
                qSoldier[3] = new Soldier();
                qSoldier[4] = new Soldier();

                Party = 0;
                qGeneral.Initialize(0, "tuong", "0", 1, false, 0, 4);
                qAdvisor[0].Initialize(0, "sy", "0", 1, false, 0, 3);
                qAdvisor[1].Initialize(0, "sy", "1", 1, false, 0, 5);
                qElephant[0].Initialize(0, "tinh", "0", 1, false, 0, 2);
                qElephant[1].Initialize(0, "tinh", "1", 1, false, 0, 6);
                qChariot[0].Initialize(0, "xe", "0", 1, false, 0, 0);
                qChariot[1].Initialize(0, "xe", "1", 1, false, 0, 8);
                qCannon[0].Initialize(0, "phao", "0", 1, false, 2, 1);
                qCannon[1].Initialize(0, "phao", "1", 1, false, 2, 7);
                qHorse[0].Initialize(0, "ma", "0", 1, false, 0, 1);
                qHorse[1].Initialize(0, "ma", "1", 1, false, 0, 7);
                qSoldier[0].Initialize(0, "chot", "0", 1, false, 3, 0);
                qSoldier[1].Initialize(0, "chot", "1", 1, false, 3, 2);
                qSoldier[2].Initialize(0, "chot", "2", 1, false, 3, 4);
                qSoldier[3].Initialize(0, "chot", "3", 1, false, 3, 6);
                qSoldier[4].Initialize(0, "chot", "4", 1, false, 3, 8);
            }
            else
            {
                qAdvisor[0] = new Advisor();
                qAdvisor[1] = new Advisor();
                qElephant[0] = new Elephant();
                qElephant[1] = new Elephant();
                qChariot[0] = new Chariot();
                qChariot[1] = new Chariot();
                qCannon[0] = new Cannon();
                qCannon[1] = new Cannon();
                qHorse[0] = new Horse();
                qHorse[1] = new Horse();
                qSoldier[0] = new Soldier();
                qSoldier[1] = new Soldier();
                qSoldier[2] = new Soldier();
                qSoldier[3] = new Soldier();
                qSoldier[4] = new Soldier();

                Party = 1;
                qGeneral.Initialize(1, "tuong", "0", 1, true, 9, 4);
                qAdvisor[0].Initialize(1, "sy", "0", 1, true, 9, 3);
                qAdvisor[1].Initialize(1, "sy", "1", 1, true, 9, 5);
                qElephant[0].Initialize(1, "tinh", "0", 1, true, 9, 2);
                qElephant[1].Initialize(1, "tinh", "1", 1, true, 9, 6);
                qChariot[0].Initialize(1, "xe", "0", 1, true, 9, 0);
                qChariot[1].Initialize(1, "xe", "1", 1, true, 9, 8);
                qCannon[0].Initialize(1, "phao", "0", 1, true, 7, 1);
                qCannon[1].Initialize(1, "phao", "1", 1, true, 7, 7);
                qHorse[0].Initialize(1, "ma", "0", 1, true, 9, 1);
                qHorse[1].Initialize(1, "ma", "1", 1, true, 9, 7);
                qSoldier[0].Initialize(1, "chot", "0", 1, true, 6, 0);
                qSoldier[1].Initialize(1, "chot", "1", 1, true, 6, 2);
                qSoldier[2].Initialize(1, "chot", "2", 1, true, 6, 4);
                qSoldier[3].Initialize(1, "chot", "3", 1, true, 6, 6);
                qSoldier[4].Initialize(1, "chot", "4", 1, true, 6, 8);
            }
        }       
    }
}
