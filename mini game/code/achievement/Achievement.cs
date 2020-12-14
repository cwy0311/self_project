
namespace esports
{
    public abstract class Achievement
    {
        public int value;

        public Achievement()
        {
            ResetAchievement();
        }

        public Achievement(int value)
        {
            this.value = value;
        }

        public abstract bool IsHighestAchievenment();

        public abstract void UpdateAchievement(int newValue);

        public abstract void ResetAchievement();

        public abstract string AchievementToString();

        public abstract string PlayersPrefStoringString();
    }

}