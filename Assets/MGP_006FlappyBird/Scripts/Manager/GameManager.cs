using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird {

    public class GameManager : IManager
    {
        private ResLoadManager m_ResLoadManager;
        private SkyTileManager m_SkyTileManager;
        private GrassTileManager m_GrassTileManager;
        private BirdManager m_BirdManager;
        private PipeManager m_PipeManager;
        private DataModelManager m_DataModelManager;
        private UIManager m_UIManager;
        private AudioManager m_AduioManager;

        private Transform m_WorldTrans;
        private Transform m_UITrans;

        private bool m_IsGameOver;
        private bool m_IsGamePause;
        private bool IsGamePause {
            get { return m_IsGameOver; }
            set {
                if (m_IsGamePause != value)
                {
                    m_IsGamePause = value;
                    if (m_IsGamePause == true)
                    {
                        GamePause();
                    }
                    else {
                        GameResume();
                    }
                }
            }
        }

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

        public void Awake() {
            m_ResLoadManager = new ResLoadManager();
            m_AduioManager = new AudioManager();
            m_SkyTileManager = new SkyTileManager();
            m_GrassTileManager = new GrassTileManager();
            m_BirdManager = new BirdManager();
            m_PipeManager = new PipeManager();
            m_DataModelManager = new DataModelManager();
            m_UIManager = new UIManager();
        }

        public void Start() {
            m_WorldTrans = GameObject.Find(GameObjectPathInSceneDefine.WORLD_PATH).transform;
            m_UITrans = GameObject.Find(GameObjectPathInSceneDefine.UI_PATH).transform;

            Init(null);

            m_IsGameOver = false;
        }

        public void Init(Transform rootTrans, params object[] managers)
        {
            m_DataModelManager.Init(null);
            m_ResLoadManager.Init(null);
            m_AduioManager.Init(m_WorldTrans, m_ResLoadManager);
            m_SkyTileManager.Init(m_WorldTrans, m_ResLoadManager);
            m_GrassTileManager.Init(m_WorldTrans, m_ResLoadManager);
            m_BirdManager.Init(m_WorldTrans, m_ResLoadManager, m_DataModelManager, m_AduioManager);
            m_PipeManager.Init(m_WorldTrans, m_ResLoadManager);
            m_UIManager.Init(m_UITrans, m_DataModelManager);
        }

        public void Update()
        {
            JudgeGamePauseOrResume();
            JudgeGameOver();

            m_DataModelManager.Update();
            m_ResLoadManager.Update();
            m_SkyTileManager.Update();
            m_GrassTileManager.Update();
            m_BirdManager.Update();
            m_PipeManager.Update();
            m_UIManager.Update();
            m_AduioManager.Update();

        }

        public void Destroy()
        {
            m_DataModelManager.Destroy();
            m_ResLoadManager.Destroy();
            m_SkyTileManager.Destroy();
            m_GrassTileManager.Destroy();
            m_BirdManager.Destroy();
            m_PipeManager.Destroy();
            m_UIManager.Destroy();
            m_AduioManager.Destroy();
        }

        public void GamePause() {
            m_BirdManager.GamePause();
            m_SkyTileManager.GamePause();
            m_GrassTileManager.GamePause();
            m_PipeManager.GamePause();
        }
        public void GameResume() {
            m_BirdManager.GameResume();
            m_SkyTileManager.GameResume();
            m_GrassTileManager.GameResume();
            m_PipeManager.GameResume();
        }

        public void GameOver()
        {
            m_DataModelManager.GameOver();
            m_BirdManager.GameOver();
            m_SkyTileManager.GameOver();
            m_GrassTileManager.GameOver();
            m_PipeManager.GameOver();
            m_UIManager.GameOver();
            m_AduioManager.GameOver();
        }

        private void JudgeGameOver() {
            if (m_IsGameOver == true)
            {
                return;
            }
            else
            {
                m_IsGameOver = m_BirdManager.IsGameOver;
                if (m_IsGameOver == true)
                {
                    GameOver();

                }
            }
        }

        private void JudgeGamePauseOrResume()
        {
            if (m_IsGameOver == true) {
                return;
            }
            if (m_UIManager.IsPause == true)
            {
                IsGamePause = true;
            }
            else
            {
                IsGamePause = false;
            }
        }
    }
}
