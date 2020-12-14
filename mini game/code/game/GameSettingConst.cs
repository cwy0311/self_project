
using System.Collections.Generic;

namespace esports
{
    public static class GameSettingConst
    {
        //datetime
        public static float SECONDS_PER_DAY = 1f;
        public static float NORMAL_GAME_SPEED = 1f;

        //competition
        public static int MAX_COMPETITION_RATE = 5;
        public static int MAX_COMPETITION_DIFFICULTY = 5;
        public static int COMPETITION_STAMINA_COST = 5;
        public static float COMPETITION_INTERVAL_TIME = 2f;

        //esports game
        public static int MAX_GAME_HIT_RATE = 5;

        //player
        public static float MAX_PLAYER_STATUS = 9999f;
        public static int MAX_PLAYER_STAMINA = 20;
        public static int MAX_PLAYER_SALARY = 1000000;
        public static int MAX_PLAYER_GAME_EXPERIENCE = 100;
        public static int MAX_PLAYER_LEVEL = 5;

        //recruitment/team
        public static int RECRUITMENT_QUOTA = 6;
        public static int MAX_TEAM_COUNT = 7;
        public static List<int> UNLOCK_TEAMMATE_SLOT_COST = new List<int> { 0, 0, 0, 1000, 5000, 25000, 50000 };
        public static int MAX_TEAM_NAME_LENGTH = 20;
        public static int MAX_GAME_FAME = 100;
        public static int MONTHLY_BONUS_GOLD_LIMIT = 4;
        public static int START_GOLD = 500;

        //setting
        public static string VOLUME_ON_ICON = "fa-volume-up";
        public static string VOLUME_OFF_ICON = "fa-volume-mute";


        //PlayerPrefs
        public static string PLAYERPREFS_VOLUME = "volume";


        //view
        public static int COMPETITION_SHOW_COUNT = 4;

        //team
        public static float RESTORE_STAMINA_RATE = 5f/28;
        public static float PLAYING_GAME_INCREASE_RATE = 100f/112;  //around 4 months for max exp
        public static float NOT_PLAYING_GAME_DECREASE_RATE = 100f/336; //imply players can only max up 3 games total


    }
}
