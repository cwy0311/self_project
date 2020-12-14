using System;

namespace esports
{
    public class SingleCompetitionAchievement : Achievement
    {
        public readonly int competitionId;

        public SingleCompetitionAchievement(int value, int competitionId) : base(value)
        {
            this.competitionId = competitionId;
        }

        public SingleCompetitionAchievement(int competitionId)
        {
            this.competitionId = competitionId;
            ResetAchievement();
        }


        public override bool IsHighestAchievenment()
        {
            return value == 1;
        }

        public override void UpdateAchievement(int newValue)
        {
            if (IsHighestAchievenment()) return;
            if (value <= 0) value = newValue;
            value = Math.Min(newValue, value);
        }

        public override void ResetAchievement()
        {
            value = -1;
        }

        public override string AchievementToString()
        {
            return GlobalGameController.Instance.I18Translate(CompetitionInitConst.GetCompetition(competitionId).NameField) + "\n" + GlobalGameController.Instance.I18Translate("achievement.singlecompetition.text")
                + ": " + (value <= 0 ? "-" : value.ToString());
        }

        public override string PlayersPrefStoringString()
        {
            return "SingleCompetitionAchievement" + competitionId;
        }

        public int GetCurrentRank()
        {
            return value;
        }
    }
}
