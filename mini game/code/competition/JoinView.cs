using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace esports
{
    public class JoinView : MonoBehaviour
    {
        public Text joinText;
        public Button confirmButton;
        public Text poepleCountText;
        public GameObject playersGameObject;
        private Competition targetCompetition;

        private int peopleCount;
        private int PeopleCount
        {
            get { return peopleCount; }
            set
            {
                peopleCount = value;
                poepleCountText.text = peopleCount.ToString();
                confirmButton.GetComponentInChildren<Text>().color = PeopleCount > 0 ? new Color(220f / 255, 220f / 255, 220f / 255, 0.4f) : new Color(220f / 255, 220f / 255, 220f / 255, 1f);
            }
        }
        private List<Player> joinPlayerList;
        public void RenderJoinView()
        {
            joinPlayerList = new List<Player>();
            PeopleCount = targetCompetition.Game.playerCount;
            
            joinText.text = GlobalGameController.Instance.I18Translate("join.view.text1");
            confirmButton.GetComponentInChildren<Text>().text = GlobalGameController.Instance.I18Translate("join.view.text2");
            confirmButton.GetComponentInChildren<Text>().color = PeopleCount > 0 ? new Color(220f / 255, 220f / 255, 220f / 255, 0.4f) : new Color(220f / 255, 220f / 255, 220f / 255, 1f);
            Transform subPlayer;
            for (int i = 0; i < GameSettingConst.MAX_TEAM_COUNT; i++)
            {
                subPlayer = playersGameObject.transform.Find("Player" + (i + 1));
                if (i < GlobalGameController.Instance.teamController.team.Count)
                {
                    subPlayer.Find("Player Image");
                    subPlayer.Find("Player Name").GetComponent<Text>().text=GlobalGameController.Instance.I18Translate(GlobalGameController.Instance.teamController.team[i].nameField);
                    subPlayer.Find("Ability").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text1");
                    subPlayer.Find("Ability Count").GetComponent<Text>().text = GlobalGameController.Instance.teamController.team[i].skillStatus.GetAbility().ToString();
                    subPlayer.Find("Exp").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("playerdetail.view.text11");
                    subPlayer.Find("Exp Count").GetComponent<Text>().text = GlobalGameController.Instance.teamController.team[i].GetGameExperience(targetCompetition.Game).ToString();
                    subPlayer.Find("Stamina").GetComponent<Text>().text = GlobalGameController.Instance.I18Translate("player.view.text2");
                    subPlayer.Find("Stamina Count").GetComponent<Text>().text = GlobalGameController.Instance.teamController.team[i].CurrentStamina + " / " + GlobalGameController.Instance.teamController.team[i].MaxStamina;
                    SetSelectPlayerEvent(subPlayer.GetComponent<Button>(), GlobalGameController.Instance.teamController.team[i]);
                    subPlayer.gameObject.SetActive(true);

                }
                else
                {
                    subPlayer.gameObject.SetActive(false);

                }
            }
        }

        public void SetTargetCompetition(Competition targetCompetition)
        {
            this.targetCompetition = targetCompetition;
        }

        public void ConfirmToStartCompetiton()
        {
            //foreach(Player p in joinPlayerList)
            //{
                //Debug.Log(p.Id + ":" + p.nameField);
            //}

            if (PeopleCount == 0)
            {
                GlobalGameController.Instance.esportsBattleController.StartCompetition(joinPlayerList, targetCompetition.Id);
            }
        }

        private void SetSelectPlayerEvent(Button playerButton,Player player)
        {
            playerButton.gameObject.GetComponent<Image>().color = new Color(120f / 255, 120f / 255, 60f / 255, 0f);
            playerButton.onClick.RemoveAllListeners();
            playerButton.onClick.AddListener(() =>
            {
                if (hasPlayer(player))
                {
                    joinPlayerList.Remove(player);
                    playerButton.gameObject.GetComponent<Image>().color = new Color(120f / 255, 120f / 255, 60f / 255, 0f);
                    PeopleCount++;
                }
                else
                {
                    if (PeopleCount > 0)
                    {
                        joinPlayerList.Add(player);
                        playerButton.gameObject.GetComponent<Image>().color = new Color(120f / 255, 120f / 255, 60f / 255, 0.5f);
                        PeopleCount--;
                    }
                }
            });

        }


        private bool hasPlayer(Player player)
        {
            foreach (Player p in joinPlayerList)
            {
                if (p == player)
                {
                    return true;
                }
            }
            return false;
        }
    }
}