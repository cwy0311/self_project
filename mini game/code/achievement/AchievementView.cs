using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace esports {
    public class AchievementView : MonoBehaviour
    {
        public GameObject content;
        public void RenderAchievementInfo()
        {
            Transform[] contentChildren = content.GetComponentsInChildren<Transform>();
            List<Transform> contentList = new List<Transform>();
            for (int i = 0; i < contentChildren.Length; i++)
            {
                if (contentChildren[i].parent.gameObject == content.gameObject)
                {
                    contentList.Add(contentChildren[i]);
                }
            }


            Text achieve;
            Text description;
            Button button;
            for (int i = 0; i < contentList.Count; i++)
            {
                if (i < GlobalGameController.Instance.teamController.GetTotalAchievementCount())
                {
                    button = contentList[i].GetComponent<Button>();
                    AddCompetitionInfoListeners(button, GlobalGameController.Instance.teamController.GetAchievement(i));
                    contentList[i].gameObject.SetActive(true);
                    achieve = contentList[i].Find("Achieve").GetComponent<Text>();
                    description = contentList[i].Find("Content").GetComponent<Text>();
                    description.text = GlobalGameController.Instance.teamController.GetAchievement(i).AchievementToString();

                    if (GlobalGameController.Instance.teamController.GetAchievement(i) is SingleCompetitionAchievement)
                    {
                        SingleCompetitionAchievement sca = (SingleCompetitionAchievement)GlobalGameController.Instance.teamController.GetAchievement(i);
                        switch (sca.GetCurrentRank())
                        {
                            case (1):
                                achieve.color = new Color(1f, 1f, 0f, 1f);
                                break;
                            case (2):
                                achieve.color = new Color(192f / 255f, 192f / 255f, 192f / 255f, 1f);
                                break;
                            case (3):
                                achieve.color = new Color(205f / 255f, 127f / 255f, 50f / 255f, 1f);
                                break;
                            default:
                                achieve.color = new Color(1f, 1f, 1f, 0.1f);
                                break;
                        }
                    }
                    else
                    {
                        achieve.color = GlobalGameController.Instance.teamController.GetAchievement(i).IsHighestAchievenment() ? new Color(1f, 1f, 0f,1f) : new Color(1f,1f,1f,0.1f);
                    }
                }
                else
                {
                    contentList[i].gameObject.SetActive(false);
                }
            }
        }

        private void AddCompetitionInfoListeners(Button button, Achievement achievement)
        {
            if (achievement is SingleCompetitionAchievement)
            {
                SingleCompetitionAchievement sca = (SingleCompetitionAchievement)achievement;
                
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => {
                    GlobalGameController.Instance.panelController.competitionView.detailCompetition = CompetitionInitConst.GetCompetition(sca.competitionId);
                    GlobalGameController.Instance.panelController.OpenInnerContent(1);
                });
            }
        }
    }
}