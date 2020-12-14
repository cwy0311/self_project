using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace esports
{
    public class PlayerView : MonoBehaviour
    {
        public List<GameObject> playerList;



        public void PlayerViewRender()
        {
            GameObject content;
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].GetComponent<Button>().onClick.RemoveAllListeners();


                if (GlobalGameController.Instance.teamController.team != null && i < GlobalGameController.Instance.teamController.team.Count)
                {
                    playerList[i].transform.Find("Empty Icon").gameObject.SetActive(false);
                    playerList[i].transform.Find("Unlock").gameObject.SetActive(false);
                    content = playerList[i].transform.Find("Content").gameObject;
                    Transform gameIcon = content.transform.Find("Player Image");
                    if (GlobalGameController.Instance.teamController.team[i].esportsGamePlaying == null)
                    {
                        gameIcon.gameObject.SetActive(false);
                    }
                    else
                    {
                        gameIcon.gameObject.SetActive(true);
                        gameIcon.GetComponent<Masamune.TextIconApplicator>().icon = GlobalGameController.Instance.panelController.esportsGameView.GetEsportsGameText(GlobalGameController.Instance.teamController.team[i].esportsGamePlaying);
                    }
                    content.transform.Find("Player Name").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate(GlobalGameController.Instance.teamController.team[i].nameField);
                    content.transform.Find("Ability").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text1");
                    content.transform.Find("Ability Count").GetComponent<Text>().text = GlobalGameController.Instance.teamController.team[i].skillStatus.GetAbility().ToString();
                    content.transform.Find("Stamina").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text2");
                    content.transform.Find("Stamina Count").GetComponent<Text>().text = GlobalGameController.Instance.teamController.team[i].CurrentStamina + " / " + GlobalGameController.Instance.teamController.team[i].MaxStamina;

                    AddPlayerDetailEvent(playerList[i].GetComponent<Button>(), GlobalGameController.Instance.teamController.team[i]);

                    content.SetActive(true);

                }
                else
                {
                    playerList[i].transform.Find("Empty Icon").gameObject.SetActive(false);
                    playerList[i].transform.Find("Content").gameObject.SetActive(false);
                    playerList[i].transform.Find("Unlock").gameObject.SetActive(false);


                    if (i == GlobalGameController.Instance.teamController.team.Count)
                    {
                        if (i < GlobalGameController.Instance.teamController.TeamSlot)
                        {
                            playerList[i].transform.Find("Empty Icon").GetComponent<Masamune.TextIconApplicator>().icon = "fa-plus";
                            playerList[i].GetComponent<Button>().onClick.AddListener(() => {
                                GlobalGameController.Instance.panelController.OpenInnerContent(6);
                            });
                            playerList[i].transform.Find("Empty Icon").gameObject.SetActive(true);
                        }
                        else
                        {
                            content = playerList[i].transform.Find("Unlock").gameObject;
                            content.transform.Find("Unlock Text").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text3");
                            content.transform.Find("Gold Count").GetComponent<Text>().text = GameSettingConst.UNLOCK_TEAMMATE_SLOT_COST[GlobalGameController.Instance.teamController.TeamSlot].ToString();
                            content.transform.Find("Buy Button").GetComponentInChildren<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text4");
                            content.transform.Find("Buy Button").GetComponentInChildren<Text>().color = GlobalGameController.Instance.teamController.Gold >= GameSettingConst.UNLOCK_TEAMMATE_SLOT_COST[GlobalGameController.Instance.teamController.TeamSlot] ?
                                new Color(220f / 255, 220f / 255, 220f / 255, 1f) : new Color(220f / 255, 220f / 255, 220f / 255, 0.4f);
                            content.transform.Find("Video Button").transform.Find("Text").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text5");
                            content.SetActive(true);
                        }
                    }
                    else
                    {
                        playerList[i].transform.Find("Empty Icon").GetComponent<Masamune.TextIconApplicator>().icon = "fa-lock";
                        playerList[i].transform.Find("Empty Icon").gameObject.SetActive(true);
                    }
                }


            }
        }

        public void WatchVideoUnlockSlot()
        {
            GlobalGameController.Instance.advertisementController.bonusType = 2;
            GlobalGameController.Instance.advertisementController.PlayRewardedVideo();
        }

        public void BuySlot()
        {
            if (GlobalGameController.Instance.teamController.CanPay(GameSettingConst.UNLOCK_TEAMMATE_SLOT_COST[GlobalGameController.Instance.teamController.TeamSlot]))
            {
                GlobalGameController.Instance.teamController.Gold -= GameSettingConst.UNLOCK_TEAMMATE_SLOT_COST[GlobalGameController.Instance.teamController.TeamSlot];
                GlobalGameController.Instance.teamController.TeamSlot++;
                PlayerViewRender();
            }
        }

        private void AddPlayerDetailEvent(Button button,Player player)
        {
            button.onClick.AddListener(() =>
            {
                GlobalGameController.Instance.panelController.playerDetailView.SetPlayerViewId(player.Id);
                GlobalGameController.Instance.panelController.OpenInnerContent(7);
            });
        }
    }
}