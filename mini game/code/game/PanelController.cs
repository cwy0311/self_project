using Masamune;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace esports
{
    public class PanelController:MonoBehaviour
    {
        public List<GameObject> panelContentList;
        public GameObject popUpWindowGameObject;
        public Text popUpWindowTitle;
        public GameObject backButton;
        private int panelIndex;
        public Stack<int> backOrderStack;

        [Header("View")]
        public CompetitionView competitionView;
        public AchievementView achievementView;
        public EsportsGameView esportsGameView;
        public TeamView teamView;
        public AddGoldView addGoldView;
        public PlayerView playerView;
        public RecruitmentView recruitmentView;
        public PlayerDetailView playerDetailView;
        public JoinView joinView;

        [Header("Setting")]
        public TextIconApplicator soundIconText;
        private void Start()
        {
            backOrderStack = new Stack<int>();
            popUpWindowGameObject.SetActive(false);
            backButton.SetActive(false);
            CleanAllPanelContent();


            //find gameobject if View gameobject is null
            if (competitionView == null)
            {
                competitionView = FindObjectOfType<CompetitionView>();
            }
            if (achievementView == null)
            {
                achievementView = FindObjectOfType<AchievementView>();
            }
            if (esportsGameView == null)
            {
                esportsGameView = FindObjectOfType<EsportsGameView>();
            }
            if (teamView == null)
            {
                teamView = FindObjectOfType<TeamView>();
            }
            if (addGoldView == null)
            {
                addGoldView = FindObjectOfType<AddGoldView>();
            }
            if (playerView == null)
            {
                playerView = FindObjectOfType<PlayerView>();
            }
            if (recruitmentView == null)
            {
                recruitmentView = FindObjectOfType<RecruitmentView>();
            }
            if (playerDetailView == null)
            {
                playerDetailView = FindObjectOfType<PlayerDetailView>();
            }
            if (joinView == null)
            {
                joinView= FindObjectOfType<JoinView>();
            }
        }

        public void OpenJoinCompetition(Competition todayCompetition)
        {
            CleanBackStack();
            CleanAllPanelContent();
            panelIndex = 1;
            competitionView.detailCompetition = todayCompetition;
            competitionView.RenderCompetitionDetail(true);
            popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title3");
            panelContentList[panelIndex].SetActive(true);
            popUpWindowGameObject.SetActive(true);
        }

        public void OpenJoinCompetitionPage()
        {
            CleanBackStack();
            CleanAllPanelContent();
            panelIndex = 8;
            Render();
            panelContentList[panelIndex].SetActive(true);
            popUpWindowGameObject.SetActive(true);
        }

        public void OpenPanel(int index)
        {
            if (GlobalGameController.Instance.CheckIsPause() || panelContentList == null || index < 0 || index >= panelContentList.Count)
            {               
                return;
            }
            CleanBackStack();
            CleanAllPanelContent();
            panelIndex = index;

            Render();

            panelContentList[panelIndex].SetActive(true);
            popUpWindowGameObject.SetActive(true);

        }


        public void OpenInnerContent(int index)
        {
            if (panelContentList == null || index < 0 || index >= panelContentList.Count && panelContentList[panelIndex] != null)
            {
                return;
            }


            if (popUpWindowGameObject.activeSelf)
            {
                AddBackStack(panelIndex);
            }
            CleanAllPanelContent();
            panelIndex = index;
            Render();

            panelContentList[panelIndex].SetActive(true);

        }


        public void ClosePanel()
        {
            popUpWindowGameObject.SetActive(false);
            backOrderStack.Clear();
            // if (panelContentList != null && panelContentList[panelIndex] != null)
            // {
            //     panelContentList[panelIndex].SetActive(false);
            // }
            // isOpenPanel = false;

            //reset freeSeatIndex
            GlobalGameController.Instance.playerPrefabManager.SetFreeSeatIndex(-1);
        }

        public void CleanAllPanelContent()
        {
            if (panelContentList != null)
            {
                foreach (GameObject go in panelContentList) {
                    if (go != null)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }



        private void AddBackStack(int index)
        {
            backOrderStack.Push(index);
            backButton.SetActive(true);
        }


        public void GoBack()
        {
            if (backOrderStack.Count > 0) {
                int lastIndex = backOrderStack.Pop();
                CleanAllPanelContent();
                panelIndex = lastIndex;
                Render();
                panelContentList[panelIndex].SetActive(true);
                if (backOrderStack.Count <= 0)
                {
                    backButton.SetActive(false);
                }
                return;
            }
            ClosePanel();
        }

        private void CleanBackStack()
        {
            backButton.SetActive(false);
            backOrderStack.Clear();
        }

        private void Render()
        {
            switch (panelIndex)
            {
                case (0):
                    competitionView.RenderCompetition();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title3");
                    break;
                case (1):
                    competitionView.RenderCompetitionDetail();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title7");
                    break;
                case (2):
                    achievementView.RenderAchievementInfo();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title4");
                    break;
                case (3):
                    teamView.TeamViewRender();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title5");
                    break;
                case (4):
                    addGoldView.RenderAddGoldView();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title6");
                    break;
                case (5):
                    playerView.PlayerViewRender();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title1");
                    break;
                case (6):
                    recruitmentView.RenderHireList();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title2");
                    break;
                case (7):
                    playerDetailView.RenderPlyerDetail();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title1");
                    break;
                case (8):
                    joinView.RenderJoinView();
                    popUpWindowTitle.text = GlobalGameController.Instance.I18Translate("popupwindow.title3");
                    break;
            }
        }


    }
}