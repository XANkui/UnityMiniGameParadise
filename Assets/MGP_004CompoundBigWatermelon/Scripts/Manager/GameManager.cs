using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{

    public class GameManager : IManager
    {

        private FruitManager m_FruitManager;
        private LineManager m_LineManager;
        private EffectManager m_EffectManager;
        private AudioManager m_AudioManager;
        private ScoreManager m_ScoreManager;
        private UIManager m_UIManager;

        private Transform m_WorldTrans;
        private Transform m_UITrans;
        private Transform m_Warningline;
        private Transform m_SpawnFruitPosTrans;

        private bool m_IsGameOverWarning;
        public bool GameOverWarning => m_IsGameOverWarning;
        private bool m_IsGameOVer ;
        public bool GameOver=> m_IsGameOVer;
 
        private float m_OverTimer = 0;
        private float m_WarningTimer = 0;
        private MonoBehaviour m_Mono;

        private static GameManager m_Instance;
        public static GameManager Instance
        {
            get {
                if (m_Instance==null)
                {
                    m_Instance = new GameManager();
                }

                return m_Instance;
            }
        }
        

        public void Awake(MonoBehaviour mono) {
            m_Mono = mono;
            m_FruitManager = new FruitManager();
            m_LineManager = new LineManager();
            m_EffectManager = new EffectManager();
            m_AudioManager = new AudioManager();
            m_ScoreManager = new ScoreManager();
            m_UIManager = new UIManager();
        }

		public void Start()
		{
            FindGameObjectInScene();
            Init(m_WorldTrans, m_UITrans,this);

        }

        public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
            m_IsGameOverWarning = false;
            m_IsGameOVer = false;
            m_ScoreManager.Init(worldTrans, uiTrans);
            m_EffectManager.Init(worldTrans, uiTrans);
            m_AudioManager.Init(worldTrans, uiTrans);
            m_FruitManager.Init(worldTrans, uiTrans, m_Mono, m_EffectManager,m_AudioManager, m_ScoreManager);
            m_LineManager.Init(worldTrans, uiTrans, m_FruitManager);            
            m_UIManager.Init(worldTrans, uiTrans, m_ScoreManager);
        }

        public void Update()
        {
            if (m_IsGameOVer==true)
            {
                return;
            }
            m_FruitManager.Update();
            m_LineManager.Update();
            m_EffectManager.Update();
            m_AudioManager.Update();
            m_ScoreManager.Update();
            m_UIManager.Update();

            UpdateJudgeGaveOverAndWarning();
        }

        public void Destroy()
        {
            m_FruitManager.Destroy();
            m_LineManager.Destroy();
            m_EffectManager.Destroy();
            m_AudioManager.Destroy();
            m_ScoreManager.Destroy();
            m_UIManager.Destroy();

            m_WorldTrans = null;
            m_UITrans = null;

            m_Warningline = null;
            m_SpawnFruitPosTrans = null;

            m_IsGameOverWarning = false;
            m_IsGameOVer = false;
        }

        void FindGameObjectInScene() {
            m_WorldTrans = GameObject.Find(GameObjectPathInSceneDefine.WORLD_PATH).transform;
            m_UITrans = GameObject.Find(GameObjectPathInSceneDefine.UI_PATH).transform;

            m_Warningline = m_WorldTrans.Find(GameObjectPathInSceneDefine.WARNING_LINE_PATH);
            m_SpawnFruitPosTrans = m_WorldTrans.Find(GameObjectPathInSceneDefine.SPAWN_FRUIT_POS_TRANS_PATH);
        }

        void UpdateJudgeGaveOverAndWarning() {
            if (IsGameOverWarning() == true)
            {
                m_WarningTimer += Time.deltaTime;
                if (m_WarningTimer >= GameConfig.JUDGE_GAME_OVER_WARNING_TIME_LENGHT)
                {
                    m_Warningline.gameObject.SetActive(true);

                }

                if (IsJudgeGameOver() == true)
                {
                    m_OverTimer += Time.deltaTime;
                    if (m_OverTimer >= GameConfig.JUDGE_GAME_OVER_TIME_LENGHT)
                    {
                        m_IsGameOVer = true;

                        if (m_FruitManager.CurFruit != null)
                        {
                            m_FruitManager.CurFruit.DisableFruit();
                        }

                        OnGameOver();
                    }
                }
                else
                {
                    m_OverTimer = 0;
                }
            }
            else
            {
                m_Warningline.gameObject.SetActive(false);
                m_OverTimer = 0;
                m_WarningTimer = 0;
            }
        }

        bool IsGameOverWarning()
        {
            Fruit fruit;
            foreach (Transform item in m_SpawnFruitPosTrans)
            {
                if (item.gameObject.activeSelf == true)
                {
                    fruit = item.GetComponent<Fruit>();
                    if (fruit != null)
                    {
                        if (fruit.CircleCollider2D.enabled == true && fruit != m_FruitManager.CurFruit)
                        {
                            if (m_Warningline.transform.position.y - (fruit.transform.position.y + fruit.CircleCollider2D.radius) < GameConfig.GAME_OVER_WARNING_LINE_DISTANCE)
                            {
                                return true;
                            }
                        }
                    }
                }

            }

            return false;
        }

        bool IsJudgeGameOver()
        {
            Fruit fruit;
            foreach (Transform item in m_SpawnFruitPosTrans)
            {
                if (item.gameObject.activeSelf == true)
                {
                    fruit = item.GetComponent<Fruit>();
                    if (fruit != null)
                    {
                        if (fruit.CircleCollider2D.enabled == true)
                        {
                            if (m_Warningline.transform.position.y - (fruit.transform.position.y + fruit.CircleCollider2D.radius) <= 0)
                            {
                                return true;
                            }
                        }
                    }
                }

            }

            return false;
        }

        void OnGameOver() {
            m_FruitManager.OnGameOver();
            m_UIManager.OnGameOver();
        }
    }
}
