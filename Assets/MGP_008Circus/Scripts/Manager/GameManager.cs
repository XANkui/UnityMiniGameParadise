using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class GameManager : BaseGameManager<GameManager>,IGameOver
	{
        private ResLoadServer m_ResLoadServer;
        private AudioServer m_AudioServer;
        private DataModelManager m_DataModelManager;
        private ObjectPoolManager m_ObjectPoolManager;
        private BackgroundManager m_BackgroundManager;
        private FireCircleManager m_FireCircleManager;
        private JokerManager m_JokerManager;
        private UIManager m_UIManager;

        private Transform m_WorldTrans;
        private Transform m_UITrans;
        private Camera m_MainCamera;

        public override void Awake(MonoBehaviour mono)
        {
            base.Awake(mono);

            m_WorldTrans = GameObject.Find(GameObjectPathInSceneDefine.WORLD_PATH).transform;
            m_UITrans = GameObject.Find(GameObjectPathInSceneDefine.UI_PATH).transform;
            m_MainCamera = GameObject.Find(GameObjectPathInSceneDefine.MAIN_CAMERRA_PATH).GetComponent<Camera>();

            // 2D 游戏屏幕适配
            Tools.AdaptationFor2DGame(GameConfig.GAME_DEVELOP_BASE_SCREEN_WIDTH, GameConfig.GAME_DEVELOP_BASE_SCREEN_HEIGHT, m_MainCamera);

            m_ResLoadServer = new ResLoadServer();
            m_AudioServer = new AudioServer();
            RegisterServer(m_ResLoadServer);
            RegisterServer(m_AudioServer);

            m_DataModelManager = new DataModelManager();            
            m_ObjectPoolManager = new ObjectPoolManager();            
            m_BackgroundManager = new BackgroundManager();
            m_FireCircleManager = new FireCircleManager();
            m_JokerManager = new JokerManager();
            m_UIManager = new UIManager();

            RegisterManager(m_DataModelManager);
            RegisterManager(m_ObjectPoolManager);
            RegisterManager(m_BackgroundManager);
            RegisterManager(m_FireCircleManager);           
            RegisterManager(m_JokerManager);           
            RegisterManager(m_UIManager);           

        }

        public override void Start()
        {
            base.Start();

            m_AudioServer.PlayBG(AudioClipSet.Circus_BG);
        }

        protected override void InitManager(Transform rootTrans)
        {
            m_DataModelManager.Init(null);
            m_ObjectPoolManager.Init(null);
            m_BackgroundManager.Init(m_WorldTrans);
            m_FireCircleManager.Init(m_WorldTrans);
            m_JokerManager.Init(m_WorldTrans);
            m_UIManager.Init(m_UITrans);
        }

        protected override void InitServer(Transform rootTrans)
        {
            m_ResLoadServer.Init(null);
            m_AudioServer.Init(m_WorldTrans);
        }

        public void GameOver()
        {
            m_AudioServer.StopBG();

            m_BackgroundManager.GameOver();
            m_FireCircleManager.GameOver();
            m_JokerManager.GameOver();
            m_UIManager.GameOver();

        }
    }
}
