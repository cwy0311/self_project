using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace esports
{

	public class HumanResourcesController:MonoBehaviour
	{
		//manage hiring and dismissing people
		private List<Player> RecruitmentList { get; set; }
		private string playersprefHireList;


        private void Start()
        {
			playersprefHireList = "recruitmentlist.hire";
		}



        public void RefreshRecruitmentList()
		{
			List<Player> availableList = GetAvailableRecruitmentList();
			int quota = GameSettingConst.RECRUITMENT_QUOTA;
			if (availableList.Count <= quota)
			{
				RecruitmentList = availableList;
			}
			else
			{
				List<Player> newRecruitmentList = new List<Player>();
				int rand;
				for (int i = 0; i < quota; i++)
				{
					rand = UnityEngine.Random.Range(0, availableList.Count);
					newRecruitmentList.Add(availableList[rand]);
					availableList.RemoveAt(rand);
				}
				RecruitmentList = newRecruitmentList;
			}
			SaveHireList();
		}


		private List<Player> GetAvailableRecruitmentList()
		{
			List<Player> playerList = new List<Player>();
			Player p;
			foreach (KeyValuePair<int,Player> pair in PlayerInitConst.PLAYER_LIST)
			{
				p = pair.Value;
				//check fulfill requirement and check if repeated
				if (p.ContainQualifiction2Recruit(GlobalGameController.Instance.teamController) && GlobalGameController.Instance.teamController.team!=null && !GlobalGameController.Instance.teamController.HasPlayer(p))
				{
					playerList.Add(p.DeepClone());
				}


			}
			return playerList;
		}

		public bool CanHire(int id)
		{
			Player target = null;
			foreach (Player p in RecruitmentList)
            {
				if (p.Id == id)
                {
					target = p;
					break;
                }
            }

			return (GlobalGameController.Instance.teamController.team.Count <= GameSettingConst.MAX_TEAM_COUNT && target != null && !GlobalGameController.Instance.teamController.HasPlayer(target) && GlobalGameController.Instance.teamController.CanPay(target.MonthSalary));

		}


		public bool Hire(int id)
		{
			Player target = null;
			foreach (Player p in RecruitmentList)
			{
				if (p.Id == id)
				{
					target = p;
					break;
				}
			}

			if (CanHire(id))
			{
				GlobalGameController.Instance.teamController.AddPlayer(target);
				RecruitmentList.Remove(target);
				SaveHireList();
				GlobalGameController.Instance.playerPrefabManager.SetComputerSeatView(GlobalGameController.Instance.teamController.team.Count-1);
				GlobalGameController.Instance.playerPrefabManager.SetFreeSeatIndex(-1);
				return true;
			}
			Debug.Log("fail");
			return false;
		}

		public void Dismiss(int id)
		{
			GlobalGameController.Instance.teamController.RemovePlayer(id);
			GlobalGameController.Instance.playerPrefabManager.RemoveComputerSeatView(id); 
		}

		public void SaveHireList()
        {
			string saveString = "";
			for(int i=0;i<RecruitmentList.Count-1;i++)
            {
				saveString += RecruitmentList[i].Id + " ";
            }
			if (RecruitmentList.Count > 0)
            {
				saveString += RecruitmentList[RecruitmentList.Count - 1].Id.ToString();
			}
			PlayerPrefs.SetString(playersprefHireList, saveString);

        }

		public int GetRecruitmentListSize()
        {
			return RecruitmentList.Count;

		}

		public Player GetRecruitmentPlayer(int index)
        {
			if (index<0 || index>= RecruitmentList.Count)
            {
				return null;
            }
			return RecruitmentList[index];
		}

		public void Init()
        {
			if (RecruitmentList == null)
			{

				if (PlayerPrefs.HasKey(playersprefHireList))
				{
					RecruitmentList = new List<Player>();
					string[] ids = PlayerPrefs.GetString(playersprefHireList).Split(' ');
					for (int i = 0; i < ids.Length; i++)
					{
						RecruitmentList.Add(PlayerInitConst.PLAYER_LIST[Int32.Parse(ids[i])]);
					}
				}
				else
				{
					RefreshRecruitmentList();
				}
			}
		}

	}

}