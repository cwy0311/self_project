using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace esports
{
	public class CustomDateTimeScheduleController : MonoBehaviour
	{
		[SerializeField]
		private CustomDateTime currentDateTime;

		public float secondsPerDayTimer;

		public float gameSpeed;

		public List<Competition> YearlyCompetitionEventList { get; private set; }

		public int competitionEventFlag;

		private int todayCompetitionIndex = 1;
		public Competition todayCompetition;	//current setting: only one competition is allowed each day


		[Header("UI")]
		public Text monthText;
		public Text weekText;
		public Text monthNumberText;
		public Text weekNumberText;
		public Text weekdayNumberText;

		private readonly string playerPrefsMonth = "Month";
		private readonly string playerPrefsWeek = "Week";
		private readonly string playerPrefsWeekday = "Weekday";
		private readonly string playerPrefsDateTimer = "DateTimer";


		void Start()
		{
			if (PlayerPrefs.HasKey(playerPrefsMonth) && PlayerPrefs.HasKey(playerPrefsWeek) && PlayerPrefs.HasKey(playerPrefsWeekday))
            {
				currentDateTime = new CustomDateTime(2, PlayerPrefs.GetInt(playerPrefsMonth), PlayerPrefs.GetInt(playerPrefsWeek), PlayerPrefs.GetInt(playerPrefsWeekday));
			}
			else
            {
				currentDateTime = new CustomDateTime(2, 0, 0, 1);
			}



			//TODO get competition event
			//TODO set all event year to 0, because all event repeated every year
			YearlyCompetitionEventList = new List<Competition>();
			Competition competition2Add;
			for (int i=1; ; i++)
            {
				competition2Add = CompetitionInitConst.GetCompetition(i);
				if (competition2Add == null) break;
				competition2Add.time.Year = 0;
				YearlyCompetitionEventList.Add(competition2Add);
            }

			if (PlayerPrefs.HasKey(playerPrefsDateTimer))
			{
				secondsPerDayTimer = PlayerPrefs.GetFloat(playerPrefsDateTimer);
			}
			else
			{
				InitDayTimer();
			}

			InitDayTimer();
			SortYearlyCompetitionEventList();

			SetCompetitionEventFlag();
			SetTodayCompetitionScedule();

			//set UI
			monthText.text = GlobalGameController.Instance.I18Translate("customdatetime.month");
			weekText.text = GlobalGameController.Instance.I18Translate("customdatetime.week");
			SetCurrentDateTimeText();
		}

		void Update()
		{
			if (GlobalGameController.Instance.CheckIsPause())
            {
				return;
            }

			secondsPerDayTimer -= Time.deltaTime * GlobalGameController.Instance.gameSpeed;
			
			
			/*
			 * timer will save only after competition/the beginning of the day, to ensure player will not miss any competition
			 * one concern is the player can earn the extra gold using this setting, as the gold will be saved everytime it changed
			 * however, currently player can only earn the gold by the first day of the month/ competition/ advertisement, so it is okay now.
			 */
			//PlayerPrefs.SetFloat(playerPrefsDateTimer,secondsPerDayTimer);

			//if (todayCompetition != null && secondsPerDayTimer <= GameSettingConst.SECONDS_PER_DAY * 0.5f)
			if (todayCompetition != null && secondsPerDayTimer <= 0.5f)
			{ //TODO
				ShowTodayCompetition();
				todayCompetitionIndex++;
			}
			if (secondsPerDayTimer <= 0)
			{
				NextDay();
				InitDayTimer();

			}
		}


		private void SetTodayCompetitionScedule()
		{
			todayCompetition = GetTodayAvailableCompetition();
			/*foreach (Competition c in availableCompetition)
			{
				todayCompetition.Add(c.Clone());
			}
			foreach (Competition c in GlobalGameController.Instance.teamController.joiningCompetition)
			{
				if (c.time == currentDateTime)
				{
					todayCompetition.Add(c.Clone());
				}
			}*/
		}

		private void ShowTodayCompetition()
		{
			if (todayCompetition != null)
			{
				GlobalGameController.Instance.panelController.OpenJoinCompetition(todayCompetition);

				todayCompetition = null;
			}

			//GlobalGameController.Instance.panelController.SetCompetitionView(todayCompetitionIndex - 1);
			//GlobalGameController.Instance.panelController.OpenPanel(0); //assume 0 is ask you whether joining competition
		}

		public List<Competition> GetRecentCompetition(int amount)
		{
			List<Competition> recentCompetition = new List<Competition>();
			for (int i = 0;i< YearlyCompetitionEventList.Count && recentCompetition.Count < amount; i++)
			{
				recentCompetition.Add(YearlyCompetitionEventList[(i + competitionEventFlag) % YearlyCompetitionEventList.Count]);
			}
			return recentCompetition;
		}

		/*
		private List<Competition> CheckTodayCompetition()
		{
			List<Competition> todayCompetition = new List<Competition>();
			for (int i = 0;i< YearlyCompetitionEventList.Count; i++)
			{
				if (YearlyCompetitionEventList[(i + competitionEventFlag) % YearlyCompetitionEventList.Count].time == currentDateTime)
				{
					todayCompetition.Add(YearlyCompetitionEventList[(i + competitionEventFlag) % YearlyCompetitionEventList.Count]);
				}
				else
				{
					return todayCompetition;
				}
			}
			return todayCompetition;

		}**/
		public List<Competition> GetRecentAvailableCompetition(int amount)
		{
			List<Competition> recentCompetition = new List<Competition>();
			for (int i = 0;i< YearlyCompetitionEventList.Count && recentCompetition.Count < amount; i++)
			{
				if (YearlyCompetitionEventList[(i + competitionEventFlag) % YearlyCompetitionEventList.Count].FulfillQualifications(GlobalGameController.Instance.teamController))
				{
					recentCompetition.Add(YearlyCompetitionEventList[(i + competitionEventFlag) % YearlyCompetitionEventList.Count]);
				}
			}
			return recentCompetition;
		}


		private Competition GetTodayAvailableCompetition()
		{
			if (YearlyCompetitionEventList[competitionEventFlag % YearlyCompetitionEventList.Count].time.TotalDaysWithoutYear() == currentDateTime.TotalDaysWithoutYear())
			{
				if (YearlyCompetitionEventList[competitionEventFlag % YearlyCompetitionEventList.Count].FulfillQualifications(GlobalGameController.Instance.teamController))
				{
					return (YearlyCompetitionEventList[competitionEventFlag % YearlyCompetitionEventList.Count]);
				}
			}


			return null;
		}	

		private void InitDayTimer()
		{
			secondsPerDayTimer = GameSettingConst.SECONDS_PER_DAY;
		}

		public void NextDay()
		{
			currentDateTime.AddWeekday(1);
			InitDayTimer();
			SetCompetitionEventFlag();

			SetCurrentDateTimeText();

			SetTodayCompetitionScedule();
			todayCompetitionIndex = 1;


			//save date
			PlayerPrefs.SetInt(playerPrefsMonth, currentDateTime.Month);
			PlayerPrefs.SetInt(playerPrefsWeek, currentDateTime.Week);
			PlayerPrefs.SetInt(playerPrefsWeekday, currentDateTime.Weekday);
			PlayerPrefs.SetFloat(playerPrefsDateTimer, secondsPerDayTimer);

			//every 1st of the month can get sponsor income, also refresh adv bonus count && pay salary
			if (currentDateTime.Week==0 && currentDateTime.Weekday == 0)
            {
				GlobalGameController.Instance.teamController.Gold += GlobalGameController.Instance.teamController.GetSponsorshipIncome();
				GlobalGameController.Instance.teamController.GoldAdvBonusCount = GameSettingConst.MONTHLY_BONUS_GOLD_LIMIT;
				GlobalGameController.Instance.teamController.PaySalary();

			}
			//bi-weekly update hiring list
			if ((currentDateTime.Week == 0 && currentDateTime.Weekday == 0) || (currentDateTime.Week == 2 && currentDateTime.Weekday == 0))
            {
				GlobalGameController.Instance.humanResourcesController.RefreshRecruitmentList();

			}
			//update players everyday
			GlobalGameController.Instance.teamController.UpdatePlayersStatus();

			//reset achievement every year
			if (currentDateTime.Week == 0 && currentDateTime.Weekday == 0 && currentDateTime.Month == 0)
            {
				GlobalGameController.Instance.teamController.ResetAllCompetitionAchievement();
            }
		}

		private void SortYearlyCompetitionEventList()
		{
			if (YearlyCompetitionEventList != null)
			{
				YearlyCompetitionEventList.Sort(delegate (Competition c1, Competition c2) { return c1.time.CompareTo(c2.time); });
			}
		}

		private void SetCompetitionEventFlag()
		{
			if (currentDateTime.TotalDaysWithoutYear() == 0) competitionEventFlag = 0;
			for (int i = competitionEventFlag; i < YearlyCompetitionEventList.Count; i++)
			{
				if (currentDateTime.TotalDaysWithoutYear() <= YearlyCompetitionEventList[i].time.TotalDaysWithoutYear())
				{
					competitionEventFlag = i ;
					return;
				}
			}
		}

		private void SetCurrentDateTimeText()
		{
			monthNumberText.text = (currentDateTime.Month + 1).ToString();
			weekNumberText.text = (currentDateTime.Week + 1).ToString();
			weekdayNumberText.text = "(" + GlobalGameController.Instance.I18Translate("customdatetime.weekday" + currentDateTime.Weekday.ToString()) + ")";
		}

		public int GetTodayCompetitionIndex()
        {
			return todayCompetitionIndex;

		}
	}
}
