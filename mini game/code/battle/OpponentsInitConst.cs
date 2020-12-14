using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports {
    public static class OpponentsInitConst
    {
        //  ttlHealth += exp*3 + attitude *3 + reaction*3;     50  40  40
        //      ttlStrength += Mathf.RoundToInt((exp + cogitation + attitude) * staminaRate);
        //      ttlDefense += Mathf.RoundToInt((exp + reaction + cogitation) *staminaRate);

        //exp:60  cog:10  reaction:10  attitude:50   *4
        //360 120 80

        

        //public static EsportsGame TETRIS 1
        // public static EsportsGame CITY_FIGHTER 1;
        // public static EsportsGame F1_LEGENDS 2
        // public static EsportsGame GREAT_PIANIST 1
        // public static EsportsGame WW3 5
        //  public static EsportsGame ARENA_OF_GLORY 5
        // public static EsportsGame THE_CARD_MASTER 3
        //  public static EsportsGame NBA 4

        private static Dictionary<int, Opponents> COMPETITION_ID_TO_OPPONENTS_DICTIONARY = new Dictionary<int, Opponents>
        {
            {1,new Opponents(new SimpleBattleStatus(1000,200,200),new SimpleBattleStatus(1500,400,300),new SimpleBattleStatus(1950,600,450),43,44,45) },  //exp:100 cog:150 reaction:200 attitude: 350
            {2,new Opponents(new SimpleBattleStatus(900,400,200),new SimpleBattleStatus(1100,200,500),new SimpleBattleStatus(1200,400,400),45,46,44) }, //exp:100 cog:150 reaction:150 attitude:150
            {3,new Opponents(new SimpleBattleStatus(600,150,250),new SimpleBattleStatus(700,220,200),new SimpleBattleStatus(720,240,230),47,48,49) }, //exp:80 cog:90 reaction:80 attitude:70
            {4,new Opponents(new SimpleBattleStatus(200,50,140),new SimpleBattleStatus(320,150,70),new SimpleBattleStatus(360,120,120),44,43,45) }, //exp:20  cog:50  reaction:50  attitude:50
            {5,new Opponents(new SimpleBattleStatus(1400,400,400),new SimpleBattleStatus(1600,550,500),new SimpleBattleStatus(1800,600,600),45,46,43) }, //exp:100 cog:100 reaction:100 attitude:100 *2
            {6,new Opponents(new SimpleBattleStatus(1000,300,320),new SimpleBattleStatus(1100,400,300),new SimpleBattleStatus(1440,480,480),47,49,43) }, //exp:80 cog:80 reaction:80 attitude:80  *2
            {7,new Opponents(new SimpleBattleStatus(800,280,320),new SimpleBattleStatus(1000,350,300),new SimpleBattleStatus(1140,380,380),44,47,46) },//exp:70  cog:60  reaction:60  attitude:60   *2
            {8,new Opponents(new SimpleBattleStatus(150,40,60),new SimpleBattleStatus(210,60,60),new SimpleBattleStatus(450,120,150),44,45,46) }, //exp:100  cog:10  reaction:40  attitude:10
            {9,new Opponents(new SimpleBattleStatus(9000,3300,3500),new SimpleBattleStatus(10000,4000,3000),new SimpleBattleStatus(10000,4000,4000),48,43,45) }, //exp:100 cog:200 reaction:200 attitude:300 *5
            {10,new Opponents(new SimpleBattleStatus(4500,1300,1300),new SimpleBattleStatus(5000,1200,1400),new SimpleBattleStatus(5400,1600,1550),49,45,44) },  //exp:80 cog:100 reaction:130 attitude:140 *5      
            {11,new Opponents(new SimpleBattleStatus(3500,1100,1000),new SimpleBattleStatus(4000,1200,1100),new SimpleBattleStatus(4500,1300,1350),49,44,43) }, //exp: 70 cog:80 reaction:110 attitude:120 *5
            {12,new Opponents(new SimpleBattleStatus(2000,800,900),new SimpleBattleStatus(3000,1000,950),new SimpleBattleStatus(3750,1150,1250),48,44,43) }, //exp: 50 cog:80 reaction:100 attitude:100 *5
            {13,new Opponents(new SimpleBattleStatus(50000,2000,200),new SimpleBattleStatus(10500,3750,3500),new SimpleBattleStatus(11000,4500,4000),44,46,48) }, //exp:100 cog:300 reaction 300 attitude:300
            {14,new Opponents(new SimpleBattleStatus(4600,1650,1900),new SimpleBattleStatus(5500,1800,1800),new SimpleBattleStatus(7200,2400,2400),43,45,47) }, //exp:80 cog:200 reaction:200 attitude:200
            {15,new Opponents(new SimpleBattleStatus(4000,1700,1000),new SimpleBattleStatus(10000,1000,100),new SimpleBattleStatus(4800,1600,1850),46,47,48) }, //exp:70 cog:150 reaction:150 attitude:100
            {16,new Opponents(new SimpleBattleStatus(7400,1000,100),new SimpleBattleStatus(3400,1350,1250),new SimpleBattleStatus(4050,1550,1400),45,46,47) }, //exp:60 cog:130 reaction:90 attitude:120 *5  
            {17,new Opponents(new SimpleBattleStatus(2700,700,1300),new SimpleBattleStatus(3000,1200,1100),new SimpleBattleStatus(3450,1250,1350),44,45,46) }, //exp:50 cog:120 reaction:100 attitude:80 *5
            {18,new Opponents(new SimpleBattleStatus(10000,300,20),new SimpleBattleStatus(50000,300,20),new SimpleBattleStatus(100,1000,100000),43,44,45)}, //?
            {19,new Opponents(new SimpleBattleStatus(6300,2100,2100),new SimpleBattleStatus(4500,2400,1800),new SimpleBattleStatus(3600,3200,3000),45,47,48) }, //exp:100 cog:800 reaction: 100 attitude:200
            {20,new Opponents(new SimpleBattleStatus(1500,1300,100),new SimpleBattleStatus(2000,1200,1200),new SimpleBattleStatus(1300,1400,1500),47,44,43) }, //exp:90 cog:300 reaction:20 attitude:20*3
            {21,new Opponents(new SimpleBattleStatus(400,900,900),new SimpleBattleStatus(1000,1000,1000),new SimpleBattleStatus(1260,1230,1230),49,48,47) }, //exp:80 cog:300 reaction:30 attitude:30 *3
            {22,new Opponents(new SimpleBattleStatus(700,500,500),new SimpleBattleStatus(400,650,650),new SimpleBattleStatus(270,840,840),44,45,49) }, //exp:70 cog:200 reaction:10 attitude:10 *3
            {23,new Opponents(new SimpleBattleStatus(1800,500,800),new SimpleBattleStatus(2000,700,700),new SimpleBattleStatus(2400,800,720),49,44,45) }, //exp 100 cog:40 reaction:40 attitude:60 *4
            {24,new Opponents(new SimpleBattleStatus(1200,500,200),new SimpleBattleStatus(1500,600,300),new SimpleBattleStatus(1700,560,400),48,44,47) }, //exp:80  cog:10  reaction:10  attitude:50   *4

        };



        public static Opponents GetOpponents(int competitionId)
        {
            if (COMPETITION_ID_TO_OPPONENTS_DICTIONARY.ContainsKey(competitionId))
            {
                return COMPETITION_ID_TO_OPPONENTS_DICTIONARY[competitionId];
            }
            return null;
        }
    }

}