using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
	public class Player : IEquatable<Player>
	{
		public int Id { get; private set; }

		public SkillStatus skillStatus;

		private int maxStamina;
		public int MaxStamina
		{
			get { return maxStamina; }
			private set
			{
				maxStamina = Math.Min(GameSettingConst.MAX_PLAYER_STAMINA, Math.Max(1, value));
			}
		}

		private int currentStamina;
		public int CurrentStamina
		{
			get { return currentStamina; }
			set
			{
				currentStamina = Math.Min(MaxStamina, Math.Max(0, value));
			}
		}

		private int monthSalary;
		public int MonthSalary
		{
			get { return monthSalary; }
			private set
			{
				monthSalary = Math.Min(GameSettingConst.MAX_PLAYER_SALARY, Math.Max(1, value));
			}

		}

		public string nameField;
		private Dictionary<EsportsGame, float> gamesExperience;
		public EsportsGame esportsGamePlaying;
		public List<Qualification> joinTeamRequirement;


		public Player(string nameField, int id, SkillStatus skillStatus, int maxStamina, int currentStamina, int salary, List<Qualification> joinTeamRequirement, Dictionary<EsportsGame,float> gamesExperience)
		{
			Id = id;
			this.skillStatus = skillStatus;
			MaxStamina = maxStamina;
			CurrentStamina = currentStamina;
			MonthSalary = salary;
			this.nameField = nameField;
			this.joinTeamRequirement = joinTeamRequirement;
			this.gamesExperience = gamesExperience;
			if (this.gamesExperience == null)
            {
				this.gamesExperience = new Dictionary<EsportsGame, float>();
			}
		}

		public int GetGameExperience(EsportsGame game)
		{
			if (gamesExperience.ContainsKey(game))
			{
				return Mathf.RoundToInt(gamesExperience[game]);
			}
			return 0;
		}

		public void SetGameExperience(EsportsGame game, float newValue)
		{
			gamesExperience[game] = Math.Max(0, Math.Min(newValue, GameSettingConst.MAX_PLAYER_GAME_EXPERIENCE));
		}

		public void GameExperienceDecay(EsportsGame exceptionGame, float decayValue)
        {
			float decayRandom;
			Dictionary<EsportsGame, float> newExpDict = new Dictionary<EsportsGame, float>();
			foreach(KeyValuePair<EsportsGame,float> pairs in gamesExperience)
            {
				if (pairs.Key != exceptionGame)
				{
					decayRandom = UnityEngine.Random.Range(decayValue * 0.75f, decayValue * 1.25f);
					newExpDict[pairs.Key] = Math.Max(0, gamesExperience[pairs.Key] - decayRandom);
				}
                else
                {
					newExpDict[pairs.Key] = GetGameExperience(pairs.Key);
				}
            }
			gamesExperience= newExpDict;
			

		}

		public bool ContainQualifiction2Recruit(TeamController teamController)
		{
			if (joinTeamRequirement == null)
			{
				return true;
			}
			foreach (Qualification quali in joinTeamRequirement)
			{
				if (!QualificationFactory.FulfillQualification(quali,teamController))
                {
					return false;
                }
			}
			return true;
		}


		public static bool operator ==(Player obj1, Player obj2)
		{
			if (ReferenceEquals(obj1, obj2))
			{
				return true;
			}
			if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
			{
				return false;
			}
			return obj1.Equals(obj2);
		}

		public static bool operator !=(Player obj1, Player obj2)
		{
			return !(obj1 == obj2);
		}

		public bool Equals(Player other)
		{
			if (other == null)
            {
				return false;
            }
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Player);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return Id.GetHashCode();
			}
		}



		public Player DeepClone()
        {
			List<Qualification> copyQualificationList;
			if (joinTeamRequirement == null) {
				copyQualificationList = null;
			}
			else {
				copyQualificationList = new List<Qualification>();
				foreach (Qualification q in joinTeamRequirement)
				{
					copyQualificationList.Add(q.DeepClone());
				}
			}
			Dictionary<EsportsGame, float> newExpDict = new Dictionary<EsportsGame, float>();
			foreach(KeyValuePair<EsportsGame,float> pair in gamesExperience)
            {
				newExpDict[pair.Key] = pair.Value;
			}
			return new Player(nameField, Id, skillStatus.DeepClone(), maxStamina, currentStamina, MonthSalary, copyQualificationList, newExpDict);

		}
	}
}
