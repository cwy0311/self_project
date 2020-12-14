using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace esports {
    public class TeamView : MonoBehaviour
    {
        private string teamNameEditIconString;
        private string teamNameConfirmIconString;
        private List<EsportsGame> esportsGameList;
        private bool isEdit;

        public Text teamNameText;
        public InputField teamNameInputField;
        public Masamune.TextIconApplicator teamNameButtonIcon;
        public List<GameObject> gameFameContents;
        public Text sponsorText;
        public Text sponsorCountText;
        public Text sponsorDescriptionText;

        private void Start()
        {
            teamNameEditIconString = "fa-edit";
            teamNameConfirmIconString = "fa-check";

            //init esports game list
            esportsGameList = new List<EsportsGame>
            {
                EsportGameInitConst.TETRIS,
                EsportGameInitConst.GREAT_PIANIST,
                EsportGameInitConst.NBA,
                EsportGameInitConst.F1_LEGENDS,
                EsportGameInitConst.CITY_FIGHTER,
                EsportGameInitConst.WW3,
                EsportGameInitConst.THE_CARD_MASTER,
                EsportGameInitConst.ARENA_OF_GLORY
            };
        }

        public void TeamViewRender()
        {
            isEdit = false;
            teamNameText.text = GlobalGameController.Instance.teamController.TeamName;
            teamNameText.gameObject.SetActive(true);
            teamNameInputField.gameObject.SetActive(false);
            teamNameButtonIcon.icon = teamNameEditIconString;

            sponsorText.text = GlobalGameController.Instance.I18Translate("team.view.text3");
            sponsorText.color = new Color(230f / 255, 230f / 255, 0, 1);
            sponsorCountText.text = GlobalGameController.Instance.teamController.GetSponsorshipIncome().ToString();
            sponsorCountText.color = new Color(230f / 255, 230f / 255, 0, 1);
            sponsorDescriptionText.text = GlobalGameController.Instance.I18Translate("team.view.text4");
            sponsorDescriptionText.color = new Color(230f / 255, 230f / 255, 0, 1);


            for (int i=0;i< gameFameContents.Count; i++)
            {
                if (i< esportsGameList.Count)
                {
                    gameFameContents[i].transform.Find("Icon").GetComponent<Masamune.TextIconApplicator>().icon = GlobalGameController.Instance.panelController.esportsGameView.GetEsportsGameText(esportsGameList[i]);
                    gameFameContents[i].transform.Find("Name").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate(esportsGameList[i].NameField);
                    gameFameContents[i].transform.Find("Icon").GetComponent<Masamune.TextIconApplicator>().text.color = new Color(100f/255, 1f, 100f / 255, 1);
                    gameFameContents[i].transform.Find("Name").GetComponent<Text>().color = new Color(100f / 255, 1f, 100f / 255, 1);
                    gameFameContents[i].transform.Find("Hot Text").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("team.view.text1");
                    gameFameContents[i].transform.Find("Hot Text").GetComponent<Text>().color = new Color(1f, 165f / 255, 42f / 255, 1);
                    gameFameContents[i].transform.Find("Fame Text").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("team.view.text2");
                    gameFameContents[i].transform.Find("Fame Text").GetComponent<Text>().color = new Color(180f/255, 180f / 255, 1f, 1);
                    gameFameContents[i].transform.Find("Fame Count").GetComponent<Text>().text = GlobalGameController.Instance.teamController.GetEsportsGameFame(esportsGameList[i]).ToString();
                    gameFameContents[i].transform.Find("Fame Count").GetComponent<Text>().color = new Color(180f / 255, 180f / 255, 1f, 1);

                    Transform[] hotIcons = gameFameContents[i].transform.Find("Hot").GetComponentsInChildren<Transform>();
                    for(int j = 0; j < hotIcons.Length; j++) 
                    {
                        if (j <= esportsGameList[i].HitRate)
                        {
                            hotIcons[j].gameObject.SetActive(true);
                        }
                        else
                        {
                            hotIcons[j].gameObject.SetActive(false);

                        }
                    }

                    gameFameContents[i].SetActive(true);
                }
                else
                {
                    gameFameContents[i].SetActive(false);
                }
            }


        }

        public void EditToggleEvent()
        {
            isEdit = !isEdit;
            SetTeamNameInputField();
            if (isEdit)
            {
                teamNameText.gameObject.SetActive(false);
                teamNameButtonIcon.icon = teamNameConfirmIconString; 
            }
            else
            {

                teamNameButtonIcon.icon = teamNameEditIconString;
                teamNameText.gameObject.SetActive(true);

                if (teamNameInputField.text.Length > 0)
                {
                    GlobalGameController.Instance.teamController.TeamName = teamNameInputField.text;
                    teamNameText.text = teamNameInputField.text;
                }
            }

        }

        private void SetTeamNameInputField()
        {
            if (isEdit)
            {
                teamNameInputField.text = GlobalGameController.Instance.teamController.TeamName;
                teamNameInputField.gameObject.SetActive(true);
                teamNameInputField.ActivateInputField();
            }
            else
            {
                teamNameInputField.DeactivateInputField();
                teamNameInputField.gameObject.SetActive(false);
            }
        }
    }

}