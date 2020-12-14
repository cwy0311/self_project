using System.Collections.Generic;

namespace esports
{
	public static class PlayerInitConst
	{
		public static Dictionary<int, Player> PLAYER_LIST = new Dictionary<int, Player>() //id->player
		{
			//TODO initialize player
			//around 34 34 34
			{1, new Player("player.name.1",1,new SkillStatus(4,12,4,14,4,18,500),10,5,1,null,null) },
			{2,new Player("player.name.2",2,new SkillStatus(6,8,6,7,3,6,500),10,5,1,null,null)},
			{3,new Player("player.name.3",3,new SkillStatus(2,5,5,4,7,18,500),10,5,1,null,null)},
			{4,new Player("player.name.4",4,new SkillStatus(2,18,3,22,1,17,300),10,5,1,null,null)},
			{5,new Player("player.name.5",5,new SkillStatus(1,19,1,21,1,20,150),10,5,1,null,null)},
			{6,new Player("player.name.6",6,new SkillStatus(1,44,2,5,3,1,300),10,5,1,null,null)},

			//around 60 60 60
			{7,new Player("player.name.7",7,new SkillStatus(8,24,8,11,8,25,500),10,5,60,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,1) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.TETRIS, 30 }})},
			{8,new Player("player.name.8",8,new SkillStatus(3,4,14,35,5,12,500),10,5,55,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,4) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.CITY_FIGHTER,15 }})},
			{9,new Player("player.name.9",9,new SkillStatus(10,15,9,15,1,22,500),10,5,55,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,4) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.CITY_FIGHTER,20 }})},
			{10,new Player("player.name.10",10,new SkillStatus(13,14,3,15,4,16,500),10,5,50,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,24) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.NBA,20 }})},
			{11,new Player("player.name.11",11,new SkillStatus(3,30,15,12,2,11,500),10,5,50,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,8) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.GREAT_PIANIST,20 }})},
			{12,new Player("player.name.12",12,new SkillStatus(1,44,1,46,2,37,100),10,5,60,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,7) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.F1_LEGENDS, 10 }})},
			
			//around 100 100 100   210/3  70/5   14   215/5  136  164  190  3
			{13,new Player("player.name.13",13,new SkillStatus(14,33,16,27,12,29,1000),12,6,200,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,4) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.CITY_FIGHTER,30 }})},
			{14,new Player("player.name.14",14,new SkillStatus(5,30,1,22,34,44,1000),15,8,250,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,8) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.GREAT_PIANIST,30 }})},
			{15,new Player("player.name.15",15,new SkillStatus(18,44,9,25,14,26,1000),12,6,200,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,24) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.NBA,25 }})},
			{16,new Player("player.name.16",16,new SkillStatus(20,51,12,15,11,16,1000),12,6,200,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,23) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.NBA,30 }})},
			{17,new Player("player.name.17",17,new SkillStatus(11,61,14,62,3,13,900),12,6,300,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,3) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.CITY_FIGHTER,20 }})},
			{18,new Player("player.name.18",18,new SkillStatus(7,24,22,82,1,1,900),11,5,300,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,22) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.THE_CARD_MASTER,20 }})},

			//around 150 150 100   120/5 = 24
			{19,new Player("player.name.19",19,new SkillStatus(14,67,12,77,33,13,2500),14,7,523,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,17) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.ARENA_OF_GLORY, 15 }})},
			{20,new Player("player.name.20",20,new SkillStatus(10,44,12,45,43,66,3000),14,7,511,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,5) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.F1_LEGENDS, 50 }})},
			{21,new Player("player.name.21",21,new SkillStatus(20,50,20,50,13,33,2000),14,7,520,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,16) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.ARENA_OF_GLORY, 10 }})},
			{22,new Player("player.name.22",22,new SkillStatus(10,34,17,103,3,12,3000),14,7,540,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,22) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.THE_CARD_MASTER, 30 }})},
			{23,new Player("player.name.23",23,new SkillStatus(19,44,8,45,23,106,3500),20,10,620,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,23) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.NBA,30 }})},
			{24,new Player("player.name.24",24,new SkillStatus(21,15,27,5,18,15,5000),16,8,111,new List<Qualification>{new TeamCompetitionQualification<TeamController>(4,8) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.GREAT_PIANIST, 5 }})},

			//around 200 200 200 200
			{25,new Player("player.name.25",25,new SkillStatus(28,60,28,59,30,50,4000),15,8,1000,new List<Qualification>{new TeamFameQualification<TeamController,EsportsGame>(10,EsportGameInitConst.ARENA_OF_GLORY) },null)},
			{26,new Player("player.name.26",26,new SkillStatus(31,54,31,55,24,56,4000),16,8,1033,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,5) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.F1_LEGENDS, 100 }})},
			{27,new Player("player.name.27",27,new SkillStatus(3,14,35,155,13,36,7200),10,5,444,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,22) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.THE_CARD_MASTER, 50 }})},
			{28,new Player("player.name.28",28,new SkillStatus(30,74,12,41,33,88,3000),20,10,1059,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,12) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.WW3,49 }})},
			{29,new Player("player.name.29",29,new SkillStatus(24,62,15,34,41,133,3900),15,7,1100,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,11) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.WW3,49 }})},
			{30,new Player("player.name.30",30,new SkillStatus(20,104,22,105,13,56,5001),17,8,1001,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,17) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.ARENA_OF_GLORY, 35 }})},

			//around 300 300 300
			{31,new Player("player.name.31",31,new SkillStatus(45,100,45,100,45,100,8000),18,9,1700,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,16) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.ARENA_OF_GLORY, 50 }})},
			{32,new Player("player.name.32",32,new SkillStatus(33,68,56,99,44,99,10000),18,9,1759,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,16) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.ARENA_OF_GLORY, 60 }})},
			{33,new Player("player.name.33",33,new SkillStatus(30,157,32,159,28,158,1323),18,9,1990,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,11) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.WW3,60 }})},
			{34,new Player("player.name.34",34,new SkillStatus(40,146,28,84,27,81,7000),18,9,1704,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,10) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.WW3,80 }})},
			{35,new Player("player.name.35",35,new SkillStatus(6,27,49,198,6,28,8000),15,7,1700,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,21) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.THE_CARD_MASTER, 80 }})},
			{36,new Player("player.name.36",36,new SkillStatus(42,76,22,41,45,159,9090),20,10,1756,new List<Qualification>{new TeamCompetitionQualification<TeamController>(3,2) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.CITY_FIGHTER, 95 }})},

			//around 500 500 500
			{37,new Player("player.name.37",37,new SkillStatus(22,44,99,300,23,111,12932),40,20,2600,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,20) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.THE_CARD_MASTER, 100 }})},
			{38,new Player("player.name.38",38,new SkillStatus(61,200,52,200,80,200,11111),40,20,2222,new List<Qualification>{new TeamCompetitionQualification<TeamController>(1,15) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.ARENA_OF_GLORY, 100 }})},
			{39,new Player("player.name.39",39,new SkillStatus(64,256,55,128,96,64,12322),40,20,2543,new List<Qualification>{new TeamCompetitionQualification<TeamController>(2,1) },new Dictionary<EsportsGame, float>{ {EsportGameInitConst.TETRIS, 100 }})},
//			{40,new Player("player.name.40",40,new SkillStatus(10,4,2,5,3,6,100),10,4,100,new List<Qualification>{new TeamFameQualification<TeamController,EsportsGame>(100,EsportGameInitConst.ARENA_OF_GLORY) },null)},
//			{41,new Player("player.name.41",41,new SkillStatus(10,4,2,5,3,6,100),10,4,100,new List<Qualification>{new TeamFameQualification<TeamController,EsportsGame>(100,EsportGameInitConst.ARENA_OF_GLORY) },null)},
//			{42,new Player("player.name.42",42,new SkillStatus(10,4,2,5,3,6,100),10,4,100,new List<Qualification>{new TeamFameQualification<TeamController,EsportsGame>(100,EsportGameInitConst.ARENA_OF_GLORY) },null)},

		};
	}
}