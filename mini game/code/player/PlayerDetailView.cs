using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace esports {
    public class PlayerDetailView : MonoBehaviour
    {
        public Image expButton;
        public Image infoButton;
        public GameObject expGO;
        public GameObject infoGO;
        private int playerIdToView;
        private bool confirmDismiss;
        public void RenderPlyerDetail(bool infoPage = true)
        {
            confirmDismiss = false;
            bool isRecruit = !GlobalGameController.Instance.teamController.HasPlayer(PlayerInitConst.PLAYER_LIST[playerIdToView]);
            Player playerToShow = !isRecruit ? GlobalGameController.Instance.teamController.GetPlayer(playerIdToView) : PlayerInitConst.PLAYER_LIST[playerIdToView];

            if (infoPage)
            {
                infoButton.color = new Color(240f / 255, 240f / 255, 240f / 255);
                expButton.color = new Color(180f / 255, 180f / 255, 180f / 255);
                expGO.SetActive(false);
                //render info
                infoGO.transform.Find("Name Image");
                infoGO.transform.Find("Name Text").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate(playerToShow.nameField);
                infoGO.transform.Find("Cogitation").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text1");
                infoGO.transform.Find("Cogitation Count").GetComponent<Text>().text = Mathf.RoundToInt(playerToShow.skillStatus.CurrentCogitation).ToString() + (playerToShow.skillStatus.LevelUpCogitation * playerToShow.skillStatus.CurrentLevel > 0 ? " (+" + playerToShow.skillStatus.LevelUpCogitation * playerToShow.skillStatus.CurrentLevel + ")" : "");
                infoGO.transform.Find("Reaction").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text2");
                infoGO.transform.Find("Reaction Count").GetComponent<Text>().text = Mathf.RoundToInt(playerToShow.skillStatus.CurrentReaction).ToString() + (playerToShow.skillStatus.LevelUpReaction * playerToShow.skillStatus.CurrentLevel > 0 ? " (+" + playerToShow.skillStatus.LevelUpReaction * playerToShow.skillStatus.CurrentLevel + ")" : "");
                infoGO.transform.Find("Attitude").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text3");
                infoGO.transform.Find("Attitude Count").GetComponent<Text>().text = Mathf.RoundToInt(playerToShow.skillStatus.CurrentAttitude).ToString() + (playerToShow.skillStatus.LevelUpAttitude * playerToShow.skillStatus.CurrentLevel > 0 ? " (+" + playerToShow.skillStatus.LevelUpAttitude * playerToShow.skillStatus.CurrentLevel + ")" : "");
                infoGO.transform.Find("Stamina").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text4");
                infoGO.transform.Find("Stamina Count").GetComponent<Text>().text = playerToShow.CurrentStamina + " / " + playerToShow.MaxStamina;
                infoGO.transform.Find("Salary").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text5");
                infoGO.transform.Find("Salary Count").GetComponent<Text>().text = playerToShow.MonthSalary.ToString();
                infoGO.transform.Find("Level").GetComponent<Text>().text = "LV." + (playerToShow.skillStatus.IsMaxLevel() ? "MAX" : playerToShow.skillStatus.CurrentLevel.ToString());
                GameObject buttons = infoGO.transform.Find("Buttons").gameObject;
                if (isRecruit)
                {
                    buttons.transform.Find("Level Up Button").gameObject.SetActive(false);
                    buttons.transform.Find("Dismiss Button").gameObject.SetActive(false);
                    GameObject hireButton = buttons.transform.Find("Hire Button").gameObject;
                    hireButton.transform.Find("Hire").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text8");
                    hireButton.transform.Find("Hire Count").GetComponent<Text>().text = playerToShow.MonthSalary.ToString();
                    hireButton.transform.Find("Hire Count").GetComponent<Text>().color = GlobalGameController.Instance.teamController.CanPay(playerToShow.MonthSalary) ?
                       new Color(230f / 255, 230f / 255, 230f / 255, 1f) : new Color(1f, 0f, 0f, 1f);
                    hireButton.SetActive(true);

                }
                else
                {
                    buttons.transform.Find("Hire Button").gameObject.SetActive(false);
                    buttons.transform.Find("Level Up Button").Find("Level Up Count").GetComponent<Text>().text = playerToShow.skillStatus.IsMaxLevel() ? " -------- " : playerToShow.skillStatus.LevelUpCost.ToString();
                    buttons.transform.Find("Level Up Button").Find("Level Up Count").GetComponent<Text>().color = GlobalGameController.Instance.teamController.CanPay(playerToShow.skillStatus.LevelUpCost)?
                        new Color(230f / 255, 230f / 255, 230f / 255, 1f): new Color(230f / 255, 230f / 255, 230f / 255, 0.5f);
                    buttons.transform.Find("Dismiss Button").Find("Text").GetComponent<Text>().text= GlobalGameController.Instance.I18Translate("playerdetail.view.text6");
                    buttons.transform.Find("Level Up Button").gameObject.SetActive(true);
                    buttons.transform.Find("Dismiss Button").gameObject.SetActive(true);
                }
                infoGO.SetActive(true);
            }
            else
            {
                expButton.color = new Color(240f / 255, 240f / 255, 240f / 255);
                infoButton.color = new Color(180f / 255, 180f / 255, 180f / 255);
                infoGO.SetActive(false);
                //render exp
                expGO.transform.Find("Status").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text9");
                expGO.transform.Find("Status Text").GetComponent<Text>().text = isRecruit || playerToShow.esportsGamePlaying == null ? " - " : GlobalGameController.Instance.I18Translate("playerdetail.view.text10") + GlobalGameController.Instance.I18Translate(playerToShow.esportsGamePlaying.NameField);
                expGO.transform.Find("Experience").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text11");
                GameObject esportsGO = expGO.transform.Find("Esports").gameObject;
                GameObject subEsports;
                List<EsportsGame> esportsGamesList = new List<EsportsGame>
                {
                    EsportGameInitConst.TETRIS,
                    EsportGameInitConst.CITY_FIGHTER,
                    EsportGameInitConst.F1_LEGENDS ,
                    EsportGameInitConst.GREAT_PIANIST ,
                    EsportGameInitConst.WW3 ,
                    EsportGameInitConst.ARENA_OF_GLORY,
                    EsportGameInitConst.THE_CARD_MASTER,
                    EsportGameInitConst.NBA,
                };
                for (int i = 1; i <= esportsGamesList.Count; i++)
                {
                    subEsports = esportsGO.transform.Find("Esports" + i).gameObject;
                    SetPlayGameEvent(subEsports, esportsGamesList[i - 1], playerToShow, isRecruit);
                    subEsports.transform.Find("Icon").GetComponent<Masamune.TextIconApplicator>().icon = GlobalGameController.Instance.panelController.esportsGameView.GetEsportsGameText(esportsGamesList[i - 1]);
                    subEsports.transform.Find("Exp").GetComponent<Text>().text = playerToShow.GetGameExperience(esportsGamesList[i - 1]).ToString();
                    subEsports.transform.Find("Name").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate(esportsGamesList[i - 1].NameField);
                }
                expGO.SetActive(true);
            }
        }

        public void SetPlayerViewId(int id)
        {
            playerIdToView = id;
        }

        public void Hire()
        {
            if (GlobalGameController.Instance.humanResourcesController.Hire(playerIdToView))
            {
                GlobalGameController.Instance.panelController.GoBack();
                GlobalGameController.Instance.panelController.GoBack();
            }
        }

        public void LevelUp()
        {
            Player player = GlobalGameController.Instance.teamController.GetPlayer(playerIdToView);
            if (player!=null && !player.skillStatus.IsMaxLevel() && GlobalGameController.Instance.teamController.CanPay(player.skillStatus.LevelUpCost))
            {
                GlobalGameController.Instance.teamController.PlayerLevelUp(player);
            }
            RenderPlyerDetail();
        }

        public void Dismiss()
        {
            if (confirmDismiss)
            {
                GlobalGameController.Instance.humanResourcesController.Dismiss(playerIdToView);
                GlobalGameController.Instance.panelController.GoBack();
            }
            else
            {
                infoGO.transform.Find("Buttons").Find("Dismiss Button").Find("Text").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text7");
                confirmDismiss = true;
            }
        }

        private void SetPlayGameEvent(GameObject subEsports,EsportsGame game,Player playerToShow,bool isRecruit)
        {
            subEsports.GetComponent<Image>().color = playerToShow.esportsGamePlaying == game ?
                new Color(1f, 47f / 255, 47f / 255, 1) : new Color(1f, 1f, 1f, 0f);
            subEsports.GetComponent<Button>().onClick.RemoveAllListeners();
            if (!isRecruit)
            {
                subEsports.GetComponent<Button>().onClick.AddListener(() =>
                {
                    playerToShow.esportsGamePlaying = game;
                    GlobalGameController.Instance.teamController.SavePlayerGamePlaying();
                    RenderPlyerDetail(false);
                });
            }
        }
    }
}
