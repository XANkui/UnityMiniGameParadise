using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit {

    /// <summary>
    /// 游戏管理类
    /// </summary>
    public class GameManager : IManager
    {

        FruitManager m_FruitManager;
        UIManager m_UIManager;
        SplashManager m_SplashManager;
        DataModelManager m_DataModelManager;
        KnifeManager m_KnifeManager;
        BombManager m_BmobManager;
        BombEffectManager m_BombEffectManager;

        private Transform m_WorldTrans;
        private Transform m_UITrans;

        private MonoBehaviour m_Mono;

        private float m_Bottom_Spawn_X_Min;
        private float m_Bottom_Spawn_X_Max;
        private float m_Bottom_Spawn_Y;
        private bool m_IsGameOver;

        private static GameManager m_Instance;
        public static GameManager Instance  {
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
            m_UIManager = new UIManager();
            m_SplashManager = new SplashManager();
            m_DataModelManager = new DataModelManager();
            m_KnifeManager = new KnifeManager();
            m_BmobManager = new BombManager();
            m_BombEffectManager = new BombEffectManager();

        }

        public void Start() {
            m_WorldTrans = GameObject.Find(GameObjectPathInSceneDefine.WORLD_PATH).transform;
            m_UITrans = GameObject.Find(GameObjectPathInSceneDefine.UI_PATH).transform;
            Init(null);

            InitBottomSpawnLimit();
            m_DataModelManager.Life.OnValueChanged += JudageGameOverByLife;
            m_Mono.StartCoroutine(ToBottomSpawn(GameConfig.BOTTOM_SPAWN_INTERVAL_TIME));
        }


        public void Init(Transform rootTrans, params object[] manager)
        {
            m_FruitManager.Init(m_WorldTrans);
            m_DataModelManager.Init(null);
            m_UIManager.Init(m_UITrans, m_DataModelManager);
            m_SplashManager.Init(m_WorldTrans);
            m_KnifeManager.Init(m_WorldTrans);
            m_BmobManager.Init(m_WorldTrans);
            m_BombEffectManager.Init(m_WorldTrans);
        }

        public void Update()
        {
            m_FruitManager.Update();
            m_UIManager.Update();
            m_SplashManager.Update();
            m_DataModelManager.Update();
            m_KnifeManager.Update();
            m_BmobManager.Update();
            m_BombEffectManager.Update();
        }

        public void Destroy()
        {
            m_DataModelManager.Destroy();
            m_FruitManager.Destroy();
            m_UIManager.Destroy();
            m_SplashManager.Destroy();
            m_KnifeManager.Destroy();
            m_BmobManager.Destroy();
            m_BombEffectManager.Destroy();

            m_IsGameOver = false;
            m_Mono.StopAllCoroutines();
        }


        /// <summary>
        /// 计算底部生成水果的位置范围
        /// x 方向是：宽度的20% - 80%
        /// y 方向是：屏幕向下高度的 20 %
        /// </summary>
        private void InitBottomSpawnLimit() {
            GameObject go = new GameObject();
            go.transform.position = Vector3.forward * GameConfig.GAME_OBJECT_Z_VALUE;
            m_Bottom_Spawn_X_Min = Tools.ScreenPosToWorldPos(go.transform,Camera.main,Vector2.right * Screen.width * 0.2f).x;
            m_Bottom_Spawn_X_Max = Tools.ScreenPosToWorldPos(go.transform,Camera.main,Vector2.right * Screen.width * 0.8f).x;
            m_Bottom_Spawn_Y = Tools.ScreenPosToWorldPos(go.transform,Camera.main,Vector2.down * Screen.height * 0.2f).y;

            GameObject.Destroy(go);
        }

        /// <summary>
        /// 底部生成水果的协程
        /// 游戏结束，停止生成水果
        /// </summary>
        /// <param name="waitSeconds"></param>
        /// <returns></returns>
        IEnumerator ToBottomSpawn(float waitSeconds) {
            while (true)
            {
                if (m_IsGameOver==true)
                {
                    break;
                }
                BottomSpawnFruitAndBomb();
                yield return new WaitForSeconds(waitSeconds);
            }
        }

        /// <summary>
        /// 底部生成
        /// </summary>
        private void BottomSpawnFruitAndBomb()
        {
            GameObject go = null;
            // 随机数，判断生成水果还是炸弹
            int ran = Random.Range(GameConfig.FRUIT_RANDOM_MIN_VALUE, GameConfig.FRUIT_RANDOM_MAX_VALUE);
            if (ran != GameConfig.BOMB_RANDOM_VALUE)
            {
                BaseFruit fruit = m_FruitManager.GetRandomFruit();
                fruit.Init(m_FruitManager, m_SplashManager, m_DataModelManager);
                go = fruit.gameObject;
            }
            else
            {
                Bomb bomb = m_BmobManager.GetBmob();
                bomb.Init(m_BmobManager,m_BombEffectManager,m_DataModelManager);
                go = bomb.gameObject;
            }

            
            // 生成物体的位置和，向上速度
            go.transform.position = new Vector3(Random.Range(m_Bottom_Spawn_X_Min, m_Bottom_Spawn_X_Max), m_Bottom_Spawn_Y, GameConfig.GAME_OBJECT_Z_VALUE);
            go.transform.rotation = Quaternion.identity;
            go.GetComponent<Rigidbody>().velocity = new Vector2(0, Random.Range(GameConfig.FRUIT_UP_Y_VELOSITY_MIN_VALUE,
                GameConfig.FRUIT_UP_Y_VELOSITY_MAX_VALUE));

        }

        /// <summary>
        /// 判断游戏是否结束
        /// </summary>
        /// <param name="life"></param>
        void JudageGameOverByLife(int life) {
            if (life == GameConfig.REMAIN_LIFE_IS_GAME_OVER)
            {
                OnGameOver();
            }
        }

        /// <summary>
        /// 结束的操作
        /// </summary>
        void OnGameOver() {
            m_IsGameOver = true;
            m_UIManager.OnGameOver();
            m_KnifeManager.OnGameOver();
        }


    }
}
