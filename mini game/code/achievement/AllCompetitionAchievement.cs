using System;

namespace esports
{
    public class AllCompetitionAchievement : Achievement
    {
        public AllCompetitionAchievement()
        {
            ResetAchievement();
        }

        public override bool IsHighestAchievenment()
        {
            return value >= CompetitionInitConst.AllCompetitionCount();
        }

        public override void UpdateAchievement(int newValue)
        {
            if (!IsHighestAchievenment())
            {
                value = newValue;
            }
        }

        public override void ResetAchievement()
        {
            value = 0;
        }

        public override string AchievementToString()
        {
            return GlobalGameController.Instance.I18Translate("achievement.allcompetition.text") + "\n" + value + " / " + CompetitionInitConst.AllCompetitionCount();
        }

        public override string PlayersPrefStoringString()
        {
            return "AllCompetitionAchievement";
        }
    }
}
