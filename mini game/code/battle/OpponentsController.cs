using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports {
    public class OpponentsController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

            //check valid opponents const
            Opponents opponentsCheck;
            List<int> competitionIdList = CompetitionInitConst.GetAllCompetitionsId();
            foreach (int competitionId in competitionIdList)
            {
                opponentsCheck = OpponentsInitConst.GetOpponents(competitionId);
                if (opponentsCheck.firstSeed==null || opponentsCheck.secondSeed==null || opponentsCheck.thirdSeed == null)
                {
                    Debug.LogError("OpponentsInitConst competition id:" + competitionId + " has no opponent!");
                }

                if (GlobalGameController.Instance.playerPrefabManager.prefabList[opponentsCheck.thirdSeedPlayerPrefabId]==null ||
                    GlobalGameController.Instance.playerPrefabManager.prefabList[opponentsCheck.secondSeedPlayerPrefabId] == null ||
                    GlobalGameController.Instance.playerPrefabManager.prefabList[opponentsCheck.firstSeedPlayerPrefabId] == null)
                {
                    Debug.LogError("OpponentsInitConst competition id:" + competitionId + " invalid player prefab id!");
                }
            }
        }
    }
}