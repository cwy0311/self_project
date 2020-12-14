using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace esports
{
	public class TeamController : MonoBehaviour //TODO save everything
	{
		public List<Player> team;
		private List<Achievement> competitionAchievement;
		private Dictionary<EsportsGame, int> esportsGameFame;
		
		private string teamName;
		public string TeamName
        {
			get { return teamName; }
			set
            {
				teamName = value;
				if (teamName.Length > GameSettingConst.MAX_TEAM_NAME_LENGTH)
                {
					teamName = teamName.Substring(0, GameSettingConst.MAX_TEAM_NAME_LENGTH);
                }
				PlayerPrefs.SetString(playerPrefsTeamName, teamName);
            }
        }

		[SerializeField]
		private int gold;
		public int Gold
		{
			get { return gold; }
			set
			{
				gold = Math.Max(-999999999, Math.Min(value, 999999999));
				if (goldText != null)
                {
					goldText.text = GameUtil.Money2String(Gold);
					goldText.color = gold > 0 ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 0f, 0f, 1f);
                }
				PlayerPrefs.SetInt(playerPrefsGold, Gold);

			}
		}

		private int goldAdvBonusCount;
		public int GoldAdvBonusCount
        {
			get { return goldAdvBonusCount; }
			set
            {
				goldAdvBonusCount = value;
				PlayerPrefs.SetInt(playerPrefsGoldBonusCount, goldAdvBonusCount);
			}
        }


		private int teamSlot;
		public int TeamSlot { 
			get { return teamSlot; }
			set
            {
				teamSlot = value;
				PlayerPrefs.SetInt(playerPrefsTeamSlot, TeamSlot);
			} 
		}

		private string playerPrefsGoldBonusCount;
		private string playerPrefsGold;
		private string playerPrefsTeamName;
		private string playerPrefsTeamSlot;
		private string playerPrefsEsportsGameFame;
		private string playerPrefsTeam;
		private List<EsportsGame> esportsGameList;
		[Header("Gold Text")]
		public TextMeshProUGUI goldText;


        //public List<Competition> joiningCompetition;
        void Start()
		{
			playerPrefsGold = "Gold";
			playerPrefsTeamName = "TeamName";
			playerPrefsGoldBonusCount = "GoldBonusCount";
			playerPrefsTeamSlot = "Team Slot";

			playerPrefsEsportsGameFame = "teamcontroller.esportsgame.";
			playerPrefsTeam = "teamcontroller.playerteam."; //get id

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

			//TODO get info from saved data
			//TODO init everything

			//init team
			team = new List<Player>();
			for (int i = 0; i < GameSettingConst.MAX_TEAM_COUNT; i++)
            {
				if (PlayerPrefs.HasKey(playerPrefsTeam + i) && PlayerPrefs.GetInt(playerPrefsTeam + i)>0)
                {
					team.Add(PlayerInitConst.PLAYER_LIST[PlayerPrefs.GetInt(playerPrefsTeam + i)].DeepClone());
					team[i].skillStatus.SetLevel(PlayerPrefs.GetInt(GetPlayerPrefsLevelString(i)));
					foreach(EsportsGame game in esportsGameList)
                    {
						team[i].SetGameExperience(game, PlayerPrefs.GetFloat(GetPlayerPrefsExpString(i, game)));
					}

					if (PlayerPrefs.HasKey(GetPlayerPrefsPlayerGamePlayingString(i))){
						team[i].esportsGamePlaying = EsportGameInitConst.GetEsportsGameById(PlayerPrefs.GetInt(GetPlayerPrefsPlayerGamePlayingString(i)));

					}
					else
                    {
						team[i].esportsGamePlaying = null;
                    }

					if (PlayerPrefs.HasKey(GetPlayerPrefsStaminaString(i)))
                    {
						team[i].CurrentStamina = PlayerPrefs.GetInt(GetPlayerPrefsStaminaString(i));
					}


				}
				else
                {
					break;
                }
            }

			//set view after init team
			GlobalGameController.Instance.playerPrefabManager.SetAllComputerSeatView();

			//init team slot
			if (PlayerPrefs.HasKey(playerPrefsTeamSlot))
			{
				TeamSlot = PlayerPrefs.GetInt(playerPrefsTeamSlot);
			}
			else
			{
				for (int i = 0; i < GameSettingConst.UNLOCK_TEAMMATE_SLOT_COST.Count; i++)
                {
					if (GameSettingConst.UNLOCK_TEAMMATE_SLOT_COST[i] > 0)
                    {
						TeamSlot = i;
						break;
                    }
                }
			}

			//init game fame
			esportsGameFame = new Dictionary<EsportsGame, int>();
			InitEsportsGameFame();

			//init team name
			if (PlayerPrefs.HasKey(playerPrefsTeamName))
			{
				TeamName = PlayerPrefs.GetString(playerPrefsTeamName);
			}
			else
			{
				TeamName = "Twitchy Esports";
			}


			//init gold
			if (PlayerPrefs.HasKey(playerPrefsGold))
            {
				Gold = PlayerPrefs.GetInt(playerPrefsGold);
			}
			else
            {
				Gold = GameSettingConst.START_GOLD;
            }

			//init gold adv bonus count
			if (PlayerPrefs.HasKey(playerPrefsGoldBonusCount))
            {
				goldAdvBonusCount= PlayerPrefs.GetInt(playerPrefsGoldBonusCount);

			}
			else
            {
				goldAdvBonusCount = GameSettingConst.MONTHLY_BONUS_GOLD_LIMIT;
			}

			//init achievement
			competitionAchievement = new List<Achievement>();
			Achievement storedAchievement;
			for (int i = 0; i < AchievementInitConst.ALL_ACHIEVEMENTS_LIST.Count; i++)
            {
				storedAchievement = AchievementInitConst.ALL_ACHIEVEMENTS_LIST[i];
				if (PlayerPrefs.HasKey(storedAchievement.PlayersPrefStoringString()))
                {
					storedAchievement.UpdateAchievement(PlayerPrefs.GetInt(storedAchievement.PlayersPrefStoringString()));
                }
				competitionAchievement.Add(storedAchievement);
			}

			//joiningCompetition = new List<Competition>();
		}



		public Player GetPlayer(int id)
		{
			foreach(Player p in team)
            {
				if (p.Id == id)
                {
					return p;
                }
            }
			return null;
		}

		public bool HasPlayer(Player player)
		{
			foreach (Player p in team)
			{
				if (p == player)
				{
					return true;
				}
			}
			return false;
		}


		//assume everything is checked
		public void AddPlayer(Player player)
		{
			Player playerToAdd = player.DeepClone();
			Gold -= playerToAdd.MonthSalary;
			for (int i = 0; i < team.Count; i++)
			{
				if (team[i] == null)
				{
					team[i] = playerToAdd;
					return;
				}
			}
			team.Add(playerToAdd);

			PlayerPrefs.SetInt(playerPrefsTeam + (team.Count - 1), playerToAdd.Id);
			PlayerPrefs.SetInt(GetPlayerPrefsLevelString(team.Count - 1), playerToAdd.skillStatus.CurrentLevel);

			PlayerPrefs.SetInt(GetPlayerPrefsPlayerGamePlayingString(team.Count - 1), -1);
			PlayerPrefs.SetInt(GetPlayerPrefsPlayerGamePlayingString(team.Count - 1), playerToAdd.CurrentStamina);

			foreach (EsportsGame game in esportsGameList)
			{
				PlayerPrefs.SetFloat(GetPlayerPrefsExpString(team.Count - 1, game), playerToAdd.GetGameExperience(game));
			}
		}


		public void RemovePlayer(int id)
		{
			team.Remove(GetPlayer(id));

			for (int i = 0; i < team.Count; i++)
            {
				PlayerPrefs.SetInt(playerPrefsTeam + i, team[i].Id);
				PlayerPrefs.SetInt(GetPlayerPrefsLevelString(i), team[i].skillStatus.CurrentLevel);
				PlayerPrefs.SetInt(GetPlayerPrefsStaminaString(i), team[i].CurrentStamina);
				if (team[i].esportsGamePlaying == null)
				{
					PlayerPrefs.SetInt(GetPlayerPrefsPlayerGamePlayingString(i), -1);
				}
				else
				{
					PlayerPrefs.SetInt(GetPlayerPrefsPlayerGamePlayingString(i), team[i].esportsGamePlaying.Id);
				}

				foreach (EsportsGame game in esportsGameList)
				{
					PlayerPrefs.SetFloat(GetPlayerPrefsExpString(i, game), team[i].GetGameExperience(game));
				}
			}
			PlayerPrefs.SetInt(playerPrefsTeam + team.Count, -1);
		}

		public List<Achievement> GetAllAchievement()
        {
			return competitionAchievement;
        }
		private void InitEsportsGameFame()
        {

			foreach (EsportsGame eg in esportsGameList)
            {
				if (PlayerPrefs.HasKey(GetEsportsGameFamePlayerPref(eg)))
				{
					esportsGameFame[eg] = PlayerPrefs.GetInt(GetEsportsGameFamePlayerPref(eg));
					//SetEsportsGameFame(eg, PlayerPrefs.GetInt(GetEsportsGameFamePlayerPref(eg)));
				}
            }
        }

		public Achievement GetAchievement(int index)
        {
			return competitionAchievement[index];
        }


		public void SetAchievementResult(int competitionId,int ranking)
        {
			for(int i=0;i<competitionAchievement.Count;i++)
            {
				Achievement ach = competitionAchievement[i];
				if (ach is AllCompetitionAchievement)
				{
					if (ranking == 1)
					{
						ach.UpdateAchievement(ach.value + 1);
						PlayerPrefs.SetInt(competitionAchievement[i].PlayersPrefStoringString(), ach.value);
					}
				}
				else
				{
					SingleCompetitionAchievement sca = (SingleCompetitionAchievement)ach;
					if (sca.competitionId == competitionId)
					{
						sca.UpdateAchievement(ranking);
						PlayerPrefs.SetInt(competitionAchievement[i].PlayersPrefStoringString(), ranking);
					}
				}
            }
		}

		private string GetEsportsGameFamePlayerPref(EsportsGame game)
        {
			return playerPrefsEsportsGameFame + game.Id;
        }


		public int GetEsportsGameFame(EsportsGame game)
        {
			return esportsGameFame.ContainsKey(game) ? esportsGameFame[game] : 0;
		}

		public void SetEsportsGameFame(EsportsGame game, int newValue)
        {
			esportsGameFame[game] = Math.Min(Math.Max(0, newValue), GameSettingConst.MAX_GAME_FAME);
			PlayerPrefs.SetInt(GetEsportsGameFamePlayerPref(game), esportsGameFame[game]);
		}

		public int GetTotalAchievementCount()
        {
			return competitionAchievement.Count;
        }


		public int GetSponsorshipIncome()
        {
			int income = 0;
			foreach (KeyValuePair<EsportsGame, int> entry in esportsGameFame)
            {
				income += entry.Value * entry.Key.HitRate * 5;
            }
			return income;
			//TODO calculate the income
        }

		public bool CanPay(int amount)
        {
			return Gold >= amount;
        }

		public void PaySalary()
        {
			foreach(Player p in team)
            {
				Gold -= p.MonthSalary;
            }
        }

		public string GetPlayerPrefsLevelString(int index)
        {
			return playerPrefsTeam + index + ".level";

		}


		public string GetPlayerPrefsExpString(int index,EsportsGame game)
		{
			return playerPrefsTeam + index + ".game." + game.Id;

		}

		public string GetPlayerPrefsStaminaString(int index)
		{
			return playerPrefsTeam + index + ".stamina";

		}


		public void PlayerLevelUp(Player player)
        {
			Gold -= player.skillStatus.LevelUpCost;
			player.skillStatus.SetLevel(player.skillStatus.CurrentLevel + 1);

			for (int i = 0; i < team.Count; i++)
            {
				if (player == team[i])
                {
					PlayerPrefs.SetInt(GetPlayerPrefsLevelString(i), player.skillStatus.CurrentLevel);
					break;
                }
            }
		}

		private string GetPlayerPrefsPlayerGamePlayingString(int index)
        {
			return playerPrefsTeam + index + ".gameplaying";
		}

		public void SavePlayerGamePlaying()
        {
			for (int i = 0; i < team.Count; i++)
			{
				if (team[i].esportsGamePlaying == null)
				{
					PlayerPrefs.SetInt(GetPlayerPrefsPlayerGamePlayingString(i), -1);
				}
				else
				{
					PlayerPrefs.SetInt(GetPlayerPrefsPlayerGamePlayingString(i), team[i].esportsGamePlaying.Id);
				}
			}
        }

		public void SavePlayerStamina()
        {
			for (int i = 0; i < team.Count; i++)
			{
				PlayerPrefs.SetInt(GetPlayerPrefsStaminaString(i), team[i].CurrentStamina);
				
			}
		}


		//call everyday passed
		public void UpdatePlayersStatus()
        {
			for(int i=0;i<team.Count;i++)
            {
				Player p = team[i];
				if (UnityEngine.Random.Range(0f, 1f) > (1-GameSettingConst.RESTORE_STAMINA_RATE))
                {
					p.CurrentStamina++;
                }
				if (p.esportsGamePlaying != null)
                {
					p.SetGameExperience(p.esportsGamePlaying, p.GetGameExperience(p.esportsGamePlaying) + UnityEngine.Random.Range(GameSettingConst.PLAYING_GAME_INCREASE_RATE * 0.75f, GameSettingConst.PLAYING_GAME_INCREASE_RATE * 1.25f));
                }
				p.GameExperienceDecay(p.esportsGamePlaying, UnityEngine.Random.Range(GameSettingConst.NOT_PLAYING_GAME_DECREASE_RATE * 0.75f, GameSettingConst.NOT_PLAYING_GAME_DECREASE_RATE * 1.25f));

				foreach (EsportsGame game in esportsGameList)
				{
					PlayerPrefs.SetFloat(GetPlayerPrefsExpString(i, game), team[i].GetGameExperience(game));
				}

			}
			SavePlayerStamina();
        }

		public void ResetAllCompetitionAchievement()
        {
			Achievement ach;
			for(int i=0;i<competitionAchievement.Count;i++)
            {
				ach = competitionAchievement[i];
				if(ach is AllCompetitionAchievement)
                {
					ach.UpdateAchievement(0);
					PlayerPrefs.SetInt(competitionAchievement[i].PlayersPrefStoringString(), 0);
				}
            }
        }
	}


}