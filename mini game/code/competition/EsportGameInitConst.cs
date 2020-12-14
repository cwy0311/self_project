using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
    public static class EsportGameInitConst
    {

        public static EsportsGame TETRIS = new EsportsGame(1, "esportgame.tetris", 1, 1, "");
        public static EsportsGame CITY_FIGHTER = new EsportsGame(2, "esportgame.cityfighter", 3, 1, "");
        public static EsportsGame F1_LEGENDS = new EsportsGame(3, "esportgame.f1legends", 3, 2, "");
        public static EsportsGame GREAT_PIANIST = new EsportsGame(4, "esportgame.greatpianist", 2, 1, "");
        public static EsportsGame WW3 = new EsportsGame(5, "esportgame.ww3", 4, 5, "");
        public static EsportsGame ARENA_OF_GLORY = new EsportsGame(6, "esportgame.arenaofglory", 5, 5, "");
        public static EsportsGame THE_CARD_MASTER = new EsportsGame(7, "esportgame.thecardmaster", 4, 3, "");
        public static EsportsGame NBA = new EsportsGame(8, "esportgame.NBA4m", 2, 4, "");
        //TODO set iconfont


        public static EsportsGame GetEsportsGameById(int id)
        {
            switch (id)
            {
                case (1):
                    return TETRIS;
                case (2):
                    return CITY_FIGHTER;
                case (3):
                    return F1_LEGENDS;
                case (4):
                    return GREAT_PIANIST;
                case (5):
                    return WW3;
                case (6):
                    return ARENA_OF_GLORY;
                case (7):
                    return THE_CARD_MASTER;
                case (8):
                    return NBA;

            }
            return null;
        }
    }

}