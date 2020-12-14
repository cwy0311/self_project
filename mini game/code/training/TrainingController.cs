using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
    public class TrainingController : MonoBehaviour
    {
		public List<TrainingActivity> currentTrainingList;

		public void SetAvailableTrainingActivity(Player player)
		{
			currentTrainingList = new List<TrainingActivity>();
			foreach (TrainingActivity ta in TrainingInitConst.TRAINING_LIST)
			{
				if (ta.UnlockActivity(player))
				{
					currentTrainingList.Add(ta);
				}
			}
		}


		public void SetPlayerTrainingActivity(int trainingActivityIndex, Player player)
		{
			if (currentTrainingList == null || trainingActivityIndex < 0 || trainingActivityIndex >= currentTrainingList.Count)
			{
				return;
			}
			TrainingActivity trainingActivity = currentTrainingList[trainingActivityIndex];
			//TODO set player activity
		}
    }
}