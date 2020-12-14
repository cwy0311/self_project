using System;
using UnityEngine;

namespace esports
{
	public class SkillStatus
	{
		private int levelUpAttitude;
		public int LevelUpAttitude
		{
			get { return levelUpAttitude; }
			private set
			{
				levelUpAttitude = Math.Max(1, value);
			}
		}

		private int levelUpCogitation;
		public int LevelUpCogitation
		{
			get { return levelUpCogitation; }
			private set
			{
				levelUpCogitation = Math.Max(1, value);
			}
		}

		private int levelUpReaction;
		public int LevelUpReaction
		{
			get { return levelUpReaction; }
			private set
			{
				levelUpReaction = Math.Max(1, value);
			}
		}

		private float currentAttitude;
		public float CurrentAttitude
		{
			get { return currentAttitude; }
			private set
			{
				currentAttitude = Math.Max(1f, value);
			}
		}

		private float currentCogitation;
		public float CurrentCogitation
		{
			get { return currentCogitation; }
			private set
			{
				currentCogitation = Math.Max(1f, value);
			}
		}

		private float currentReaction;
		public float CurrentReaction
		{
			get { return currentReaction; }
			private set
			{
				currentReaction = Math.Max(1f, value);
			}
		}

		public int CurrentLevel { get; private set; }
		public int LevelUpCost { get; private set; }

		public SkillStatus(int levelUpAttitude, float currentAttitude, int levelUpCogitation, float currentCogitation, int levelUpReaction, float currentReaction, int lvUpCost, int currentLevel=0)
		{
			LevelUpAttitude = levelUpAttitude;
			CurrentAttitude = currentAttitude;
			LevelUpCogitation = levelUpCogitation;
			CurrentCogitation = currentCogitation;
			LevelUpReaction = levelUpReaction;
			CurrentReaction = currentReaction;
			CurrentLevel = currentLevel;
			LevelUpCost = lvUpCost;
		}

		public int GetAbility()
		{
			return Mathf.FloorToInt(GetTotalAttitude() + GetTotalCogitation() + GetTotalReaction());
		}

		public int GetTotalAttitude()
        {
			return levelUpAttitude * CurrentLevel + Mathf.RoundToInt(currentAttitude);
        }

		public int GetTotalCogitation()
        {
			return levelUpCogitation * CurrentLevel + Mathf.RoundToInt(currentCogitation);
        }

		public int GetTotalReaction()
        {
			return levelUpReaction * CurrentLevel + Mathf.RoundToInt(currentReaction);
        }
		
		public void SetLevel(int level)
        {
			CurrentLevel = Math.Min(GameSettingConst.MAX_PLAYER_LEVEL, level);
        }

		public bool IsMaxLevel()
        {
			return CurrentLevel == GameSettingConst.MAX_PLAYER_LEVEL;
        }

		public SkillStatus DeepClone()
        {
			return new SkillStatus(LevelUpAttitude, CurrentAttitude, LevelUpCogitation, CurrentCogitation, LevelUpReaction, CurrentReaction, LevelUpCost, CurrentLevel);
        }
	}

}