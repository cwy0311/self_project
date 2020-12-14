using UnityEngine;
using System.Collections.Generic;

namespace esports
{
	public class PlayGameTraining : TrainingActivity
	{
		public EsportsGame game;
		public PlayGameTraining(EsportsGame game, string nameField, string descriptionField, List<Qualification> unlockRequirements) :base(nameField,descriptionField, unlockRequirements)
		{
			this.game = game;
		}
		public override void Training(Player player)
		{
			player.SetGameExperience(game, player.GetGameExperience(game) + Random.Range(0, 3));
		}
	}
}