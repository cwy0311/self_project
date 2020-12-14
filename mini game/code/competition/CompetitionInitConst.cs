using System.Collections.Generic;

namespace esports
{
    public static class CompetitionInitConst
    {
        private readonly static Dictionary<int, Competition> ALL_COMPEIITION_DICTIONARY = new Dictionary<int, Competition>() {
            {1,new Competition(1,"competition.competition1",1,4,EsportGameInitConst.TETRIS,
                new List<int>(){10000,5000,100,100},null,new CustomDateTime(1,0,1,2))},
            {2,new Competition(2,"competition.competition2",3,4,EsportGameInitConst.CITY_FIGHTER,
                new List<int>(){8000,4000,2000,500},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,3)},new CustomDateTime(1,7,3,6)) },
            {3,new Competition(3,"competition.competition3",2,2,EsportGameInitConst.CITY_FIGHTER,
                new List<int>(){4000,2500,1000,100 },null,new CustomDateTime(1,5,1,6)) },
            {4,new Competition(4,"competition.competition4",2,1,EsportGameInitConst.CITY_FIGHTER,
                new List<int>(){1000, 500, 50,50 },null,new CustomDateTime(1,3,3,0)) },
            {5,new Competition(5,"competition.competition5",4,3,EsportGameInitConst.F1_LEGENDS,
                new List<int>{5000,3000,1000,500},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,6)},new CustomDateTime(1,8,2,4)) },
            {6,new Competition(6,"competition.competition6",3,2,EsportGameInitConst.F1_LEGENDS,
                new List<int>{4000,2000,1000,200},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,7)},new CustomDateTime(1,1,1,1)) },
            {7,new Competition(7,"competition.competition7",1,1,EsportGameInitConst.F1_LEGENDS,
                new List<int>{2500,2000,1500,100},null,new CustomDateTime(1,6,2,4))},
            {8,new Competition(8,"competition.competition8",1,1,EsportGameInitConst.GREAT_PIANIST,
                new List<int>{3000,2000,1000,100},null,new CustomDateTime(1,1,0,0)) },
            {9,new Competition(9,"competition.competition9",5,5,EsportGameInitConst.WW3,
                new List<int>(){35000,12000,8000,6000 },new List<Qualification>(){new TeamCompetitionQualification<TeamController>(3,10)},new CustomDateTime(1,4,3,0)) },
            {10,new Competition(10,"competition.competition10",4,4,EsportGameInitConst.WW3,
                new List<int>(){6000,4000,3000,2000},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(3,11)},new CustomDateTime(1,2,2,0)) },
            {11,new Competition(11,"competition.competition11",3,3,EsportGameInitConst.WW3,
                new List<int>{4500,3500,3000,1000},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(3,12)},new CustomDateTime(1,10,1,0)) },
            {12,new Competition(12,"competition.competition12",1,2,EsportGameInitConst.WW3,
                new List<int>{3500,2500,1500,500},null,new CustomDateTime(1,9,0,0)) },
            {13,new Competition(13,"competition.competition13",5,5,EsportGameInitConst.ARENA_OF_GLORY,
                new List<int>{50000,25000,12500,6250},
                new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,14)},new CustomDateTime(1,11,3,6)) },
            {14,new Competition(14,"competition.competition14",5,4,EsportGameInitConst.ARENA_OF_GLORY,
                new List<int>(){12000,6000,3000,1500},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,15)},new CustomDateTime(1,6,2,6)) },
            {15,new Competition(15,"competition.competition15",3,4,EsportGameInitConst.ARENA_OF_GLORY,
                new List<int>(){10000,5000,2500,1250},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,16)},new CustomDateTime(1,3,1,6)) },
            {16,new Competition(16,"competition.competition16",2,3,EsportGameInitConst.ARENA_OF_GLORY,
                new List<int>(){7000,5000,3000,1000},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,17)},new CustomDateTime(1,2,0,6)) },
            {17,new Competition(17,"competition.competition17",1,3,EsportGameInitConst.ARENA_OF_GLORY,
                new List<int>(){6000,4000,2000,1000},null,new CustomDateTime(1,10,3,6)) },
            {18,new Competition(18,"competition.competition18",4,2,EsportGameInitConst.ARENA_OF_GLORY,
                new List<int>{8888,7777,6666,5555},
                new List<Qualification>(){new TeamFameQualification<TeamController,EsportsGame>(50,EsportGameInitConst.ARENA_OF_GLORY)},
                new CustomDateTime(1,7,3,1)) },
            {19,new Competition(19,"competition.competition19",5,5,EsportGameInitConst.THE_CARD_MASTER,
                new List<int>{35000, 25000, 15000, 5000},
                new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,20)},new CustomDateTime(1,11,1,5)) },
            {20,new Competition(20,"competition.competition20",3,4,EsportGameInitConst.THE_CARD_MASTER,
                new List<int>(){12000,9000,6000,3000},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,21)},new CustomDateTime(1,0,0,5)) },
            {21,new Competition(21,"competition.competition21",2,4,EsportGameInitConst.THE_CARD_MASTER,
                 new List<int>(){9000,7000,5000,3000},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(2,22)},new CustomDateTime(1,8,0,5)) },
            {22,new Competition(22,"competition.competition22",1,2,EsportGameInitConst.THE_CARD_MASTER,
                new List<int>(){5000,4000,2000,1000},null,new CustomDateTime(1,4,0,5)) },
            {23,new Competition(23,"competition.competition23",3,2,EsportGameInitConst.NBA,
                new List<int>(){7500, 5500, 3500, 1500},new List<Qualification>(){new TeamCompetitionQualification<TeamController>(4,24)},new CustomDateTime(1,11,3,3))},
            {24,new Competition(24,"competition.competition24",2,1,EsportGameInitConst.NBA,
                new List<int>(){5500, 4000, 2500,1000 },null,new CustomDateTime(1,9,2,3)) },
        }; 

        public static Competition GetCompetition(int id)
        {
            if (ALL_COMPEIITION_DICTIONARY.ContainsKey(id))
            {
                return ALL_COMPEIITION_DICTIONARY[id];
            }
            return null;
        }

        public static int AllCompetitionCount()
        {
            return ALL_COMPEIITION_DICTIONARY.Count;
        }

        public static List<int> GetAllCompetitionsId()
        {
            List<int> idList = new List<int>();
            foreach(KeyValuePair<int,Competition> pairs in ALL_COMPEIITION_DICTIONARY)
            {
                idList.Add(pairs.Key);
            }
            return idList;
        }
    }
}
