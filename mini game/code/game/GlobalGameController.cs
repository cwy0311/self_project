using UnityEngine;

namespace esports
{
	public class GlobalGameController : MonoBehaviour
	{
		public static GlobalGameController Instance { get; private set; }

		public float gameSpeed;

		
		[Header("Controller")]
		public TeamController teamController;
		public PanelController panelController;
		public CustomDateTimeScheduleController customDateTimeScheduleController;
		public AdvertisementController advertisementController;
		public HumanResourcesController humanResourcesController;
		public PlayerPrefabManager playerPrefabManager;
		public EsportsBattleController esportsBattleController;
		public OpponentsController opponentsController;
		public MusicController musicController;
		//setting
		private int isVolumeOn;
		public int IsVolumeOn
        {
            get { return isVolumeOn; }
			set
            {
				isVolumeOn = value;
				panelController.soundIconText.icon = IsVolumeOn!=0 ? GameSettingConst.VOLUME_ON_ICON : GameSettingConst.VOLUME_OFF_ICON;
				PlayerPrefs.SetInt(GameSettingConst.PLAYERPREFS_VOLUME, IsVolumeOn);
            }
        }

		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}


			//try to get team controller if doesn't set
			if (teamController == null)
			{
				teamController = FindObjectOfType<TeamController>();
			}
			//try to get panel controller if doesn't set
			if (panelController == null)
			{
				panelController = FindObjectOfType<PanelController>();
			}
			if (humanResourcesController == null)
			{
				humanResourcesController = FindObjectOfType<HumanResourcesController>();
			}
			if (customDateTimeScheduleController == null)
			{
				customDateTimeScheduleController = FindObjectOfType<CustomDateTimeScheduleController>();
			}
			if (advertisementController == null)
			{
				advertisementController = FindObjectOfType<AdvertisementController>();
			}
			if (playerPrefabManager == null)
			{
				playerPrefabManager = FindObjectOfType<PlayerPrefabManager>();
			}
			if (esportsBattleController == null)
			{
				esportsBattleController = FindObjectOfType<EsportsBattleController>();
			}
			if (opponentsController == null)
			{
				opponentsController = FindObjectOfType<OpponentsController>();
			}
			if (musicController == null)
			{
				musicController = FindObjectOfType<MusicController>();
			}
		}

		void Start()
		{
			gameSpeed = GameSettingConst.NORMAL_GAME_SPEED;

			//set volume
			IsVolumeOn = PlayerPrefs.HasKey(GameSettingConst.PLAYERPREFS_VOLUME) ? PlayerPrefs.GetInt(GameSettingConst.PLAYERPREFS_VOLUME) : 1;
		}


        public string I18Translate(string fieldId)
		{
			return I18n.Fields[fieldId];
		}

		public bool CheckIsPause()
        {
			return panelController.popUpWindowGameObject.activeSelf || esportsBattleController.competitionWindow.activeSelf;
        }

		public void VolumeToggle()
        {
			IsVolumeOn = 1-IsVolumeOn;
        }

	}
}