using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace esports
{
    public class EsportsBattleController : MonoBehaviour
    {
        public GameObject competitionWindow;

        public ComputerSeatController playerSeat;
        public ComputerSeatController thirdSeedSeat;
        public ComputerSeatController secondSeedSeat;
        public ComputerSeatController firstSeedSeat;

        public Text descriptionText;

        public Slider playerHealthSlider;
        public Slider playerStrengthSlider;
        public Slider playerDefenseSlider;

        public Slider opponentsHealthSlider;
        public Slider opponentsStrengthSlider;
        public Slider opponentsDefenseSlider;

        public Button attackButton;
        public Button defenseButton;

        public GameObject playerSpotLight;
        public GameObject thirdSeedSpotLight;
        public GameObject secondSeedSpotLight;
        public GameObject firstSeedSpotLight;

        public GameObject competitionCamera;

        private Opponents opponents;
        private bool isAttack; //true = attack, false = defense
        private SimpleBattleStatus playerStatus;
        private SimpleBattleStatus thirdSeedStatus;
        private SimpleBattleStatus secondSeedStatus;
        private SimpleBattleStatus firstSeedStatus;
        private int currentCompetitionId;

        private void Start()
        {
            competitionWindow.SetActive(false);
            competitionCamera.SetActive(false);
        }

        public void StartCompetition(List<Player> joinPlayerList, int competitionId)
        {
            GlobalGameController.Instance.musicController.SwitchMusic(true);
            GlobalGameController.Instance.panelController.ClosePanel();

            //close all spot light
            playerSpotLight.SetActive(false);
            thirdSeedSpotLight.SetActive(false);
            secondSeedSpotLight.SetActive(false);
            firstSeedSpotLight.SetActive(false);

            //get opponents
            opponents = OpponentsInitConst.GetOpponents(competitionId);

            //init computer seats
            CleanAllChildren(playerSeat.playerPrefabPos);
            CleanAllChildren(thirdSeedSeat.playerPrefabPos);
            CleanAllChildren(secondSeedSeat.playerPrefabPos);
            CleanAllChildren(firstSeedSeat.playerPrefabPos);
            AddPlayerPrefab(playerSeat.playerPrefabPos,joinPlayerList[0].Id);
            AddPlayerPrefab(thirdSeedSeat.playerPrefabPos, opponents.thirdSeedPlayerPrefabId);
            AddPlayerPrefab(secondSeedSeat.playerPrefabPos, opponents.secondSeedPlayerPrefabId);
            AddPlayerPrefab(firstSeedSeat.playerPrefabPos, opponents.firstSeedPlayerPrefabId);


            //init description text
            descriptionText.text = "";

            //init button
            isAttack = true;
            SetActionButtonView();

            //init battle status
            thirdSeedStatus = opponents.thirdSeed;
            secondSeedStatus = opponents.secondSeed;
            firstSeedStatus = opponents.firstSeed;
            thirdSeedStatus.MaximizeStatus();
            secondSeedStatus.MaximizeStatus();
            firstSeedStatus.MaximizeStatus();


            playerStatus = PlayersAbility2BattleStatus(joinPlayerList, competitionId);

            //init Slider
            SetSlider(playerStatus, playerHealthSlider, playerStrengthSlider, playerDefenseSlider);
            SetSlider(thirdSeedStatus, opponentsHealthSlider, opponentsStrengthSlider, opponentsDefenseSlider);

            currentCompetitionId = competitionId;
            competitionWindow.SetActive(true);
            competitionCamera.SetActive(true);
            StartCoroutine(StartCompetitionDetail(joinPlayerList));
        }


        private void CleanAllChildren(GameObject parentGO)
        {
            Transform[] oldPrefabs= parentGO.GetComponentsInChildren<Transform>();
            for (int i = 0; i < oldPrefabs.Length; i++)
            {

                if (oldPrefabs[i].gameObject.transform.parent == parentGO || oldPrefabs[i].gameObject.transform.parent.name.Equals(parentGO.name))
                {
                    Debug.Log("delete");
                    Destroy(oldPrefabs[i].gameObject);
                }
            }
        }

        private void AddPlayerPrefab(GameObject parentGO, int prefabId)
        {
            GameObject prefab = (GameObject)Instantiate(GlobalGameController.Instance.playerPrefabManager.GetPrefab(prefabId));
            prefab.transform.parent = parentGO.transform;
            prefab.transform.localPosition = Vector3.zero;
            prefab.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            prefab.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }

        private void SetActionButtonView()
        {
            if (isAttack)
            {
                attackButton.GetComponentInChildren<Text>().color = new Color(96f / 255, 96f / 255, 96f / 255, 1f);
                defenseButton.GetComponentInChildren<Text>().color = new Color(96f / 255, 96f / 255, 96f / 255, 0.4f);

            }
            else
            {
                defenseButton.GetComponentInChildren<Text>().color = new Color(96f / 255, 96f / 255, 96f / 255, 1f);
                attackButton.GetComponentInChildren<Text>().color = new Color(96f / 255, 96f / 255, 96f / 255, 0.4f);
            }
        }

        public void SetIsAttack(bool isAttack)
        {
            this.isAttack = isAttack;
            SetActionButtonView();
        }


        private void SetSlider(SimpleBattleStatus status, Slider healthSlider, Slider strengthSlider, Slider defenseSlider)
        {
            healthSlider.maxValue = status.MaxHealth;
            healthSlider.value = status.CurrentHealth;
            strengthSlider.maxValue = status.MaxStrength;
            strengthSlider.value = status.CurrentStrength;
            defenseSlider.maxValue = status.MaxDefense;
            defenseSlider.value = status.CurrentDefense;

        }


        private SimpleBattleStatus PlayersAbility2BattleStatus(List<Player> players, int competitionId)
        {
            EsportsGame game = CompetitionInitConst.GetCompetition(competitionId).Game;
            int ttlHealth = 10;
            int ttlStrength = 1;
            int ttlDefense = 1;

            float staminaRate = 0;
            int cogitation = 0;
            int reaction = 0;
            int attitude = 0;
            int exp = 0;

            foreach (Player p in players)
            {
                staminaRate += Mathf.Min(1f, Mathf.Max(0.5f, p.CurrentStamina / Mathf.Max((float)p.CurrentStamina, 5f)));
                cogitation += p.skillStatus.GetTotalCogitation();
                reaction += p.skillStatus.GetTotalReaction();
                attitude += p.skillStatus.GetTotalAttitude();
                exp += p.GetGameExperience(game);
            }
            staminaRate /= players.Count;


            Debug.Log("stamina rate " + staminaRate);
            Debug.Log("cogitation " + cogitation);
            Debug.Log("reaction " + reaction);
            Debug.Log("attitude " + attitude);
            Debug.Log("exp " + exp);


            ttlHealth += exp*3 + attitude *3 + reaction*3;
            ttlStrength += Mathf.RoundToInt((exp + cogitation + attitude) * staminaRate);
            ttlDefense += Mathf.RoundToInt((exp + reaction + cogitation) *staminaRate);

            Debug.Log("ttlHealth " + ttlHealth);
            Debug.Log("ttlStrength " + ttlStrength);
            Debug.Log("ttlDefense " + ttlDefense);


            SimpleBattleStatus sbs = new SimpleBattleStatus(ttlHealth, ttlStrength, ttlDefense);

            return sbs;
        }

        IEnumerator StartCompetitionDetail(List<Player> joinPlayerList)
        {
            int round = 1;//1=vs third seed, 2=vs second seed, 3=vs first seed
            SimpleBattleStatus currentOpponent;
            bool opponentIsAttack;
            for (; round <= 3; round++)
            {
                SetInitAnimByRound();
                SetInitDesciptionByRound(round);
                yield return new WaitForSeconds(GameSettingConst.COMPETITION_INTERVAL_TIME);
                SetLightByRound(round);
                yield return new WaitForSeconds(GameSettingConst.COMPETITION_INTERVAL_TIME);
                //SetMatchBeginText();
                currentOpponent = round == 1 ? thirdSeedStatus : round == 2 ? secondSeedStatus : firstSeedStatus;
                MaximizeStatus(round);

                //no money will affect the competition performance
                //if (GlobalGameController.Instance.teamController.Gold < 0)
                //{
                //    playerStatus.CurrentStrength = playerStatus.CurrentHealth * 2 / 3;
                //    playerStatus.CurrentDefense = playerStatus.CurrentHealth * 2 / 3;
               // }

                SetRunAnim(playerSeat);
                SetIdleAnim(thirdSeedSeat);
                SetIdleAnim(secondSeedSeat);
                SetIdleAnim(firstSeedSeat);
                SetRunAnim(round == 1 ? thirdSeedSeat : round == 2 ? secondSeedSeat : firstSeedSeat);

                while (!currentOpponent.IsDead() && !playerStatus.IsDead())
                {
                     yield return new WaitForSeconds(GameSettingConst.COMPETITION_INTERVAL_TIME/1.5f);
                    //check attack or defense, and then update description (optional)
                    currentOpponent.CurrentStrength = Mathf.RoundToInt(currentOpponent.CurrentStrength * Random.Range(0.75f, 0.85f));
                    currentOpponent.CurrentDefense = Mathf.RoundToInt(currentOpponent.CurrentDefense * Random.Range(0.75f, 0.85f));
                    opponentIsAttack = Random.Range(0f, 1f) > 0.5;
                    if (opponentIsAttack)
                    {
                        currentOpponent.CurrentStrength += Mathf.RoundToInt(currentOpponent.MaxStrength * Random.Range(0.15f, 0.2f));
                    }
                    else
                    {
                        currentOpponent.CurrentDefense += Mathf.RoundToInt(currentOpponent.MaxDefense * Random.Range(0.3f, 0.4f));
 
                    }

                    playerStatus.CurrentStrength = Mathf.RoundToInt(playerStatus.CurrentStrength * Random.Range(0.75f, 0.825f));
                    playerStatus.CurrentDefense = Mathf.RoundToInt(playerStatus.CurrentDefense * Random.Range(0.75f, 0.825f));

                    if (isAttack)
                    {
                        playerStatus.CurrentStrength += Mathf.RoundToInt(playerStatus.MaxStrength * Random.Range(0.15f, 0.175f));
                    }
                    else
                    {
                        playerStatus.CurrentDefense += Mathf.RoundToInt(playerStatus.MaxDefense * Random.Range(0.3f, 0.35f));
                    }

                    playerStatus.Attack(currentOpponent, isAttack);
                    if (!currentOpponent.IsDead())
                    {
                        currentOpponent.Attack(playerStatus, opponentIsAttack);
                    }
                    SetSlider(playerStatus, playerHealthSlider, playerStrengthSlider, playerDefenseSlider);
                    SetSlider(currentOpponent, opponentsHealthSlider, opponentsStrengthSlider, opponentsDefenseSlider);

                }
                //end
                if (playerStatus.IsDead())
                {
                    //lose 
                    SetLoseDescription(round);
                    playerSpotLight.SetActive(false);
                    SetJumpAnim(round == 1 ? thirdSeedSeat : round == 2 ? secondSeedSeat : firstSeedSeat);
                    SetIdleAnim(playerSeat);
                    break;
                }
                else
                {
                    //win, next round
                   // SetWinRoundDescription(round);
                }
                yield return new WaitForSeconds(GameSettingConst.COMPETITION_INTERVAL_TIME);
            }

            //player win 
            if (!playerStatus.IsDead())
            {
                SetJumpAnim(playerSeat);
                SetIdleAnim(thirdSeedSeat);
                SetIdleAnim(secondSeedSeat);
                SetIdleAnim(firstSeedSeat);

                SetVictoryDescription();
                SetLightByRound(round);
            }
            yield return new WaitForSeconds(GameSettingConst.COMPETITION_INTERVAL_TIME);
            //increase gold, fame, according to rank, update rank
            int rank = 5 - round;
            for (int i = 0; i < joinPlayerList.Count; i++)
            {
                joinPlayerList[i].CurrentStamina -= GameSettingConst.COMPETITION_STAMINA_COST;
            }
            Competition competition = CompetitionInitConst.GetCompetition(currentCompetitionId);
            GlobalGameController.Instance.teamController.SetEsportsGameFame(competition.Game, competition.GetTeamFame(rank) + GlobalGameController.Instance.teamController.GetEsportsGameFame(competition.Game));
            GlobalGameController.Instance.teamController.Gold += competition.GetPrize(rank);
            GlobalGameController.Instance.teamController.SetAchievementResult(currentCompetitionId, rank);
            //competition spend whole day
            GlobalGameController.Instance.customDateTimeScheduleController.NextDay();






            yield return new WaitForSeconds(GameSettingConst.COMPETITION_INTERVAL_TIME*2);
            competitionCamera.SetActive(false);
            competitionWindow.SetActive(false);
            GlobalGameController.Instance.musicController.SwitchMusic(false);
        }


        private void SetLightByRound(int round)
        {
            if (round == 1) //first round
            {
                playerSpotLight.SetActive(true);
                thirdSeedSpotLight.SetActive(true);
                secondSeedSpotLight.SetActive(false);
                firstSeedSpotLight.SetActive(false);
            }
            else if (round == 2) //second round
            {
                playerSpotLight.SetActive(true);
                thirdSeedSpotLight.SetActive(false);
                secondSeedSpotLight.SetActive(true);
                firstSeedSpotLight.SetActive(false);
            }
            else if (round==3)
            {
                playerSpotLight.SetActive(true);
                thirdSeedSpotLight.SetActive(false);
                secondSeedSpotLight.SetActive(false);
                firstSeedSpotLight.SetActive(true);
            }
            else //4, win
            {
                playerSpotLight.SetActive(true);
                thirdSeedSpotLight.SetActive(false);
                secondSeedSpotLight.SetActive(false);
                firstSeedSpotLight.SetActive(false);
            }
        }


        private void MaximizeStatus(int round)
        {
            playerStatus.MaximizeStatus();
            SetSlider(playerStatus, playerHealthSlider, playerStrengthSlider, playerDefenseSlider);

            if (round == 1) //first round
            {
                thirdSeedStatus.MaximizeStatus();
                SetSlider(thirdSeedStatus, opponentsHealthSlider, opponentsStrengthSlider, opponentsDefenseSlider);
            }
            else if (round == 2) //second round
            {
                secondSeedStatus.MaximizeStatus();
                SetSlider(secondSeedStatus, opponentsHealthSlider, opponentsStrengthSlider, opponentsDefenseSlider);
            }
            else //last round
            {
                firstSeedStatus.MaximizeStatus();
                SetSlider(firstSeedStatus, opponentsHealthSlider, opponentsStrengthSlider, opponentsDefenseSlider);
            }
        }

        private void SetInitAnimByRound()
        {
            SetIdleAnim(playerSeat);
            SetIdleAnim(thirdSeedSeat);
            SetIdleAnim(secondSeedSeat);
            SetIdleAnim(firstSeedSeat);
        }

        private void SetRunAnim(ComputerSeatController seat)
        {
            seat.playerPrefabPos.GetComponentInChildren<Animator>().runtimeAnimatorController = GlobalGameController.Instance.playerPrefabManager.runAnim;
        }
        private void SetJumpAnim(ComputerSeatController seat)
        {
            seat.playerPrefabPos.GetComponentInChildren<Animator>().runtimeAnimatorController = GlobalGameController.Instance.playerPrefabManager.jumpAnim;
        }
        private void SetIdleAnim(ComputerSeatController seat)
        {
            seat.playerPrefabPos.GetComponentInChildren<Animator>().runtimeAnimatorController = GlobalGameController.Instance.playerPrefabManager.idleAnim;
        }

        //TODO
        private void SetInitDesciptionByRound(int round)
        {
            if (round == 1) //first round
            {
                descriptionText.text = GlobalGameController.Instance.I18Translate("competition.live.text1");
            }
            else if (round == 2) //second round
            {
                descriptionText.text = GlobalGameController.Instance.I18Translate("competition.live.text2.1") + GlobalGameController.Instance.teamController.TeamName + " " +
                    GlobalGameController.Instance.I18Translate("competition.live.text2.2");
            }
            else //last round
            {
                descriptionText.text = GlobalGameController.Instance.I18Translate("competition.live.text3.1") + GlobalGameController.Instance.teamController.TeamName + " " +
                    GlobalGameController.Instance.I18Translate("competition.live.text3.2");
            }
        }

        private void SetVictoryDescription()
        {
            descriptionText.text = GlobalGameController.Instance.I18Translate("competition.live.text5.1") + GlobalGameController.Instance.teamController.TeamName + " "+
                GlobalGameController.Instance.I18Translate("competition.live.text5.2") + "$" + CompetitionInitConst.GetCompetition(currentCompetitionId).GetPrize(1);

        }


        private void SetLoseDescription(int round)
        {
            descriptionText.text = GlobalGameController.Instance.I18Translate("competition.live.text4.1") + GlobalGameController.Instance.teamController.TeamName + " " +
                GlobalGameController.Instance.I18Translate("competition.live.text4.2") + (5 - round) + GlobalGameController.Instance.I18Translate("competition.live.text4.3") +
                 "$" + CompetitionInitConst.GetCompetition(currentCompetitionId).GetPrize(5 - round) + GlobalGameController.Instance.I18Translate("competition.live.text4.4");
        }

       // private void SetWinRoundDescription(int round)
      //  {
      //  }
    }
}
