using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D { 

	public class GameManager : BaseGameManager, IGamePause, IGameResume, IGameOver
    {
        private ResLoadServer m_ResLoadServer;
        private AudioServer m_AudioServer;

        private DataModelManager m_DataModelManager;
        private DrivingDistanceScoreManager m_DrivingDistanceScoreManager;
        private UIManager m_UIManager;
        private RoadManager m_RoadManager;
        private ObjectPoolManager m_ObjectPoolManager;
        private NPCManager m_NPCManager;
        private PCCarManager m_PCCarManager;

        private Transform m_WorldTrans;
        private Transform m_UITrans;
        private Camera m_Camera;

        private static GameManager m_Instance;
        public static GameManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GameManager();
                }

                return m_Instance;
            }
        }

        public override void Awake(MonoBehaviour mono)
        {
            base.Awake(mono);

            m_WorldTrans = GameObject.Find(GameObjectPathInSceneDefine.WORLD_PATH).transform;
            m_UITrans = GameObject.Find(GameObjectPathInSceneDefine.UI_PATH).transform;
            m_Camera = m_WorldTrans.Find(GameObjectPathInSceneDefine.MAIN_CAMERA_PATH).GetComponent<Camera>();

            // 2D 游戏屏幕适配
            Tools.AdaptationFor2DGame(1080,1920, m_Camera);

            m_ResLoadServer = new ResLoadServer();
            m_AudioServer = new AudioServer();

            m_DataModelManager = new DataModelManager();
            m_DrivingDistanceScoreManager = new DrivingDistanceScoreManager();
            m_UIManager = new UIManager();
            m_RoadManager = new RoadManager();
            m_ObjectPoolManager = new ObjectPoolManager();
            m_NPCManager = new NPCManager();
            m_PCCarManager = new PCCarManager();

            RegisterServer(m_ResLoadServer);
            RegisterServer(m_AudioServer);

            RegisterManager(m_DataModelManager);
            RegisterManager(m_DrivingDistanceScoreManager);
            RegisterManager(m_UIManager);
            RegisterManager(m_RoadManager);
            RegisterManager(m_ObjectPoolManager);
            RegisterManager(m_NPCManager);
            RegisterManager(m_PCCarManager);
        }

        public override void Start()
        {

            base.Start();

            m_AudioServer.PlayBG(AudioClipSet.BG_FutureWorld_Dark);
        }

        protected override void InitServer(Transform rootTrans, params object[] objs) {

            m_ResLoadServer.Init(null);
            m_AudioServer.Init(m_WorldTrans, m_ResLoadServer);
        }

        protected override void InitManager(Transform rootTrans, params object[] objs)
        {
            m_DataModelManager.Init(null);
            m_DrivingDistanceScoreManager.Init(null, m_DataModelManager);
            m_UIManager.Init(m_UITrans, m_AudioServer, m_DataModelManager);
            m_ObjectPoolManager.Init(null);
            m_RoadManager.Init(m_WorldTrans, m_ResLoadServer);
            m_NPCManager.Init(m_WorldTrans, m_ResLoadServer, m_ObjectPoolManager);
            m_PCCarManager.Init(m_WorldTrans, m_ResLoadServer, m_ObjectPoolManager,m_AudioServer,m_DataModelManager);

            m_UIManager.SetOnGamePause(GamePause);
            m_UIManager.SetOnGameResume(GameResume);
            m_PCCarManager.SetPCCarColliderNPCCarAction(GameOver);

        }

        public void GamePause()
        {
            m_RoadManager.GamePause();
            m_NPCManager.GamePause();
            m_PCCarManager.GamePause();
            m_UIManager.GamePause();
            m_DrivingDistanceScoreManager.GamePause();
        }

        public void GameResume()
        {
            m_RoadManager.GameResume();
            m_NPCManager.GameResume();
            m_PCCarManager.GameResume();
            m_UIManager.GameResume();
            m_DrivingDistanceScoreManager.GameResume();
        }

        public void GameOver()
        {
            m_RoadManager.GameOver();
            m_NPCManager.GameOver();
            m_PCCarManager.GameOver();
            m_UIManager.GameOver();
            m_DrivingDistanceScoreManager.GameOver();
        }

    }
}
