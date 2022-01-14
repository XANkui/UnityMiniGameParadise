using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MGP_003JumpJump
{ 

	public class GameManager 
	{
		PlatformManager PlatformManager;
		PlayerManager PlayerManager;
		CameraManager CameraManager;
		ScoreManager ScoreManager;

		Transform m_WorldTrans;
		Transform m_UITrans;
		Transform m_FirstPlatFormCubeSpawnPosTrans;
		Transform m_PlayerSpawnPosTrans;
		Transform m_CameraGroupTrans;
		Text m_ScoreText;

		bool m_IsGameOver = false;

		private static GameManager m_Instance;
		public static GameManager Instance {
			get {
                if (m_Instance==null)
                {
					m_Instance = new GameManager();

				}

				return m_Instance;
			}
		}

		// protected ，避免外界 new 
		protected GameManager() { }

		public void Awake()
		{
			PlatformManager = new PlatformManager();
			PlayerManager = new PlayerManager();
			CameraManager = new CameraManager();
			ScoreManager = new ScoreManager();
		}

		public void Start()
		{
			FindGameObjectInScene();

			PlatformManager.Init(OnPlatformEnterAction, m_FirstPlatFormCubeSpawnPosTrans.position, GameConfig.PLATFORM_ADD_SCORE, m_FirstPlatFormCubeSpawnPosTrans);
			PlayerManager.Init(m_PlayerSpawnPosTrans, PlatformManager);
			CameraManager.Init(m_CameraGroupTrans,PlatformManager);

			ScoreManager.Score = 0;
			ScoreManager.OnValueChanged += (socre) => { m_ScoreText.text = ScoreManager.Score.ToString(); };
		}

		public void Update()
		{
			JudgeGameOver();

			PlatformManager.Update();
			PlayerManager.Update();
			CameraManager.Update();
		}

		public void OnDestroy()
		{
			PlatformManager.Destroy();
			PlayerManager.Destroy();
			CameraManager.Destroy();

			m_WorldTrans = null;
			m_UITrans = null;
			m_FirstPlatFormCubeSpawnPosTrans = null;
			m_PlayerSpawnPosTrans = null;
			m_CameraGroupTrans = null;
			m_ScoreText = null;
			m_IsGameOver = false;

			PlatformManager = null;
			PlayerManager = null;
			CameraManager = null;
			ScoreManager = null;
		}

		public void OnGUI()
		{
			// 游戏操作说明
			GUIStyle fontStyle = new GUIStyle();
			fontStyle.normal.background = null;    //设置背景填充
			fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色
			fontStyle.fontSize = 32;       //字体大小
			GUI.Label(new Rect(10, 10, 200, 200),
				"操作说明：\n1、按下鼠标左键蓄力；\n2、松开鼠标左键起跳；\n3、坠落，重新开始；",
				fontStyle);

		}

		/// <summary>
		/// 获取场景中的游戏物体
		/// </summary>
		private void FindGameObjectInScene()
		{
			m_WorldTrans = GameObject.Find(GameObjectPathInSceneDefine.WORLD_PATH).transform;
			m_UITrans = GameObject.Find(GameObjectPathInSceneDefine.UI_PATH).transform;
			m_FirstPlatFormCubeSpawnPosTrans = m_WorldTrans.Find(GameObjectPathInSceneDefine.FIRST_PLATFORM_CUBE_SPAWN_POS_PATH);
			m_PlayerSpawnPosTrans = m_WorldTrans.Find(GameObjectPathInSceneDefine.PLAYER_SPAWN_POS_PATH);
			m_CameraGroupTrans = m_WorldTrans.Find(GameObjectPathInSceneDefine.CAMERA_GROUP_PATH);
			m_ScoreText = m_UITrans.Find(GameObjectPathInSceneDefine.CANVAS_SCORE_TEXT_PATH).GetComponent<Text>() ;
		}

		/// <summary>
		/// 判断游戏是否结束
		/// </summary>
		void JudgeGameOver()
		{
			if (PlayerManager.IsFallen() == true)
			{
				if (m_IsGameOver == false)
				{
					m_IsGameOver = true;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
		}

		/// <summary>
		/// 分数增加委托
		/// </summary>
		/// <param name="score"></param>
		void OnPlatformEnterAction(int score) {
			ScoreManager.Score += score;
		}
	}
}
