using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
	public class CompetitionOpponent
	{
		public string nameField;
		private readonly float attitude;
		private readonly float cogitation;
		private readonly float reaction;
		private readonly float gameExperience;

		public float minAttitudeRate;
		public float maxAttitudeRate;
		public float minCogitationRate;
		public float maxCogitationRate;
		public float minReactionRate;
		public float maxReactionRate;
		public float minGameExperienceRate;
		public float maxGameExperienceRate;


		public CompetitionOpponent(string nameField, float attitude, float cogitation, float reaction, float gameExperience)
		{
			this.nameField = nameField;
			this.attitude = attitude;
			this.cogitation = cogitation;
			this.reaction = reaction;
			this.gameExperience = gameExperience;
			minAttitudeRate = 1f;
			maxAttitudeRate = 1f;
			minCogitationRate = 1f;
			maxCogitationRate = 1f;
			minReactionRate = 1f;
			maxReactionRate = 1f;
			minGameExperienceRate = 1f;
			maxGameExperienceRate = 1f;
		}

		public CompetitionOpponent(string nameField, float attitude, float cogitation, float reaction, float gameExperience,
		float minAttitudeRate, float maxAttitudeRate, float minCogitationRate, float maxCogitationRate, float minReactionRate, float maxReactionRate,
		float minGameExperienceRate, float maxGameExperienceRate)
		{
			this.nameField = nameField;
			this.attitude = attitude;
			this.cogitation = cogitation;
			this.reaction = reaction;
			this.gameExperience = gameExperience;
			this.minAttitudeRate = minAttitudeRate;
			this.maxAttitudeRate = maxAttitudeRate;
			this.minCogitationRate = minCogitationRate;
			this.maxCogitationRate = maxCogitationRate;
			this.minReactionRate = minReactionRate;
			this.maxReactionRate = maxReactionRate;
			this.minGameExperienceRate = minGameExperienceRate;
			this.maxGameExperienceRate = maxGameExperienceRate;
		}

		public float GetAttitude()
		{
			return GetRandomValue(attitude, minAttitudeRate, maxAttitudeRate);
		}

		public float GetCogitation()
		{
			return GetRandomValue(cogitation, minCogitationRate, maxCogitationRate);
		}

		public float GetReaction()
		{
			return GetRandomValue(reaction, minReactionRate, maxReactionRate);
		}

		public float GetGameExperience()
		{
			return GetRandomValue(gameExperience, minGameExperienceRate, maxGameExperienceRate);
		}

		private float GetRandomValue(float original, float min, float max)
		{
			return Random.Range(min, max) * original;
		}

	}
}