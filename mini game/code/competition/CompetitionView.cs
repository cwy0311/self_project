using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace esports
{
    public class CompetitionView:MonoBehaviour
    {
        public Image recentAvailableImage;
        public Image recentAllImage;
        public List<Button> competitionGameObjectList;
        public Competition detailCompetition;

        [Header("Detail")]
        public Masamune.TextIconApplicator detailGameTypeIcon;
        public List<Text> detailIconText;
        public Text detailNameText;
        public Text detailTimeText;
        public Text detailPrizeText;
        public Text detailInfoText;
        public Text playerRequiredText;
        public GameObject buttons;

        public void RenderCompetition(bool isRecentAvailable = true)
        {
            List<Competition> recentCompetitions;
            if (isRecentAvailable)
            {

                recentAvailableImage.color = new Color(240f/255, 240f / 255, 240f / 255);
                recentAllImage.color = new Color(180f / 255, 180f / 255, 180f / 255);
                recentCompetitions = GlobalGameController.Instance.customDateTimeScheduleController.GetRecentAvailableCompetition(GameSettingConst.COMPETITION_SHOW_COUNT);
            }
            else
            {
                recentAllImage.color = new Color(240f / 255, 240f / 255, 240f / 255);
                recentAvailableImage.color = new Color(180f / 255, 180f / 255, 180f / 255);
                recentCompetitions = GlobalGameController.Instance.customDateTimeScheduleController.GetRecentCompetition(GameSettingConst.COMPETITION_SHOW_COUNT);
            }

            string name;
            string dateTime;
            bool fulfill;
            Text text;
            string month = GlobalGameController.Instance.I18Translate("customdatetime.month");
            string week = GlobalGameController.Instance.I18Translate("customdatetime.week");
            for (int i = 0; i < GameSettingConst.COMPETITION_SHOW_COUNT; i++)
            {
                if (competitionGameObjectList.Count > i && competitionGameObjectList[i]!=null)
                {
                    if (recentCompetitions.Count > i)
                    {
                        fulfill = recentCompetitions[i].FulfillQualifications(GlobalGameController.Instance.teamController);
                        name = GlobalGameController.Instance.I18Translate(recentCompetitions[i].NameField);
                        dateTime = (recentCompetitions[i].time.Month + 1) + month + "\n" + (recentCompetitions[i].time.Week + 1) + week+ "\n" + "(" 
                            + GlobalGameController.Instance.I18Translate("customdatetime.weekday" + recentCompetitions[i].time.Weekday.ToString()) 
                            + ")";
                        if (competitionGameObjectList[i].transform.Find("Name") != null)
                        {
                            text = competitionGameObjectList[i].transform.Find("Name").GetComponent<Text>();
                            text.text = name;
                            text.color = !fulfill ? new Color(text.color.r, text.color.g, text.color.b, 0.5f) : new Color(text.color.r, text.color.g, text.color.b, 1f);
                        }

                        if (competitionGameObjectList[i].transform.Find("DateTime") != null)
                        {
                            text = competitionGameObjectList[i].transform.Find("DateTime").GetComponent<Text>();
                            text.text = dateTime;
                            text.color = !fulfill ? new Color(text.color.r, text.color.g, text.color.b, 0.5f) : new Color(text.color.r, text.color.g, text.color.b, 1f);
                        }
                        competitionGameObjectList[i].onClick.RemoveAllListeners();

                        if (i == 0)
                        {
                            competitionGameObjectList[i].onClick.AddListener(() =>
                            {
                                detailCompetition = recentCompetitions[0];
                                GlobalGameController.Instance.panelController.OpenInnerContent(1);
                            });
                        }
                        else if (i == 1)
                        {
                            competitionGameObjectList[i].onClick.AddListener(() =>
                            {
                                detailCompetition = recentCompetitions[1];
                                GlobalGameController.Instance.panelController.OpenInnerContent(1);
                            });
                        }
                        else if (i == 2)
                        {
                            competitionGameObjectList[i].onClick.AddListener(() =>
                            {
                                detailCompetition = recentCompetitions[2];
                                GlobalGameController.Instance.panelController.OpenInnerContent(1);
                            });
                        }
                        else
                        {
                            competitionGameObjectList[i].onClick.AddListener(() =>
                            {
                                detailCompetition = recentCompetitions[3];
                                GlobalGameController.Instance.panelController.OpenInnerContent(1);
                            });
                        }

                        competitionGameObjectList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        competitionGameObjectList[i].gameObject.SetActive(false);
                    }
                }

            }


        }

        public void RenderCompetitionDetail(bool isJoinCometition = false)
        {
            //Debug.Log(detailCompetition.Id);
            string month = GlobalGameController.Instance.I18Translate("customdatetime.month");
            string week = GlobalGameController.Instance.I18Translate("customdatetime.week");
            detailGameTypeIcon.icon = GlobalGameController.Instance.panelController.esportsGameView.GetEsportsGameText(detailCompetition.Game);
            detailNameText.text = GlobalGameController.Instance.I18Translate(detailCompetition.NameField);
            detailPrizeText.text = detailCompetition.prize.Count > 0 ? "$" + detailCompetition.prize[0] : "$0";
            detailTimeText.text = (detailCompetition.time.Month + 1) + month + " " + (detailCompetition.time.Week + 1) + week + " ("
                            + GlobalGameController.Instance.I18Translate("customdatetime.weekday" + detailCompetition.time.Weekday.ToString()) + ")";
            for (int i = 0; i < detailIconText.Count; i++)
            {
                detailIconText[i].color = i < detailCompetition.Difficulty ? new Color(1f, 1f, 0f) : new Color(76f / 255, 152f / 255, 152f / 255);
            }
            playerRequiredText.text = detailCompetition.Game.playerCount.ToString();
            if (isJoinCometition)
            {
                detailInfoText.text = GlobalGameController.Instance.I18Translate("competition.view.text5");
                buttons.transform.Find("Join Button").GetComponentInChildren<Text>().text = GlobalGameController.Instance.I18Translate("competition.view.text6");
                buttons.transform.Find("Cancel Button").GetComponentInChildren<Text>().text = GlobalGameController.Instance.I18Translate("competition.view.text7");
                buttons.SetActive(true);
            }
            else
            {
                buttons.SetActive(false);
                //info text
                SetCompetitionQualificationDescription(detailCompetition);
            }
        }

        private void SetCompetitionQualificationDescription(Competition competition)
        {
            string requirement = GlobalGameController.Instance.I18Translate("competition.view.text4") + ":";
            if (competition.qualifications == null || competition.qualifications.Count == 0)
            {
                detailInfoText.text = requirement + GlobalGameController.Instance.I18Translate("competition.view.text1");
            }
            else
            {
                Qualification q = competition.qualifications[0];
                for (int i=1;i< competition.qualifications.Count; i++)
                {

                    if (!QualificationFactory.FulfillQualification(competition.qualifications[i], GlobalGameController.Instance.teamController)) {
                        q = competition.qualifications[i];
                        break;
                    }
                }
                if (q is TeamFameQualification<TeamController, EsportsGame>)
                {
                    detailInfoText.text = requirement + GlobalGameController.Instance.I18Translate(competition.Game.NameField)+
                        GlobalGameController.Instance.I18Translate("competition.view.text2")+ "≥" + Mathf.FloorToInt(q.theshold);
                }
                else if (q is TeamCompetitionQualification<TeamController>)
                {
                    TeamCompetitionQualification<TeamController> newQ = (TeamCompetitionQualification<TeamController>)q;
                    detailInfoText.text = requirement + GlobalGameController.Instance.I18Translate(CompetitionInitConst.GetCompetition(newQ.auditTarget).NameField) +
                        GlobalGameController.Instance.I18Translate("competition.view.text3") + "≥" + Mathf.FloorToInt(q.theshold);

                }
                else
                {
                    detailInfoText.text = requirement + GlobalGameController.Instance.I18Translate("competition.view.text1");
                }

                if (competition.FulfillQualifications(GlobalGameController.Instance.teamController))
                {
                    detailInfoText.color = new Color(detailInfoText.color.r, detailInfoText.color.g, detailInfoText.color.b, 1f);
                }
                else
                {
                    detailInfoText.color = new Color(detailInfoText.color.r, detailInfoText.color.g, detailInfoText.color.b, 0.5f);
                }
            }
        }


        public void GoJoinCompetitionPage()
        {
            GlobalGameController.Instance.panelController.joinView.SetTargetCompetition(detailCompetition);
            GlobalGameController.Instance.panelController.OpenJoinCompetitionPage();
        }
    }
}