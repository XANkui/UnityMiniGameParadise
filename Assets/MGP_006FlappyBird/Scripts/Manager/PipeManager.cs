using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird { 

	public class PipeManager : ObjectPool<Pipe>, IManager
    {
        private ResLoadManager m_ResLoadManager;
        private Transform m_SpawnPipePosTrans;
        private GameObject m_PipePrefab;
        private List<Pipe> m_PipeList;

        private bool m_IsPause;
        private bool m_IsGameOver;

        private float m_SpawnTimer = 0;
        private float m_SpawnPosX = 0;
        private float m_TargetMovePosX = 0;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_SpawnPipePosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_PIPE_POS_PATH);
            m_ResLoadManager = managers[0] as ResLoadManager;

            m_PipeList = new List<Pipe>();
            m_IsPause = false;
            m_IsGameOver = false;

            m_SpawnTimer = GameConfig.PIPE_SPAWN_TIME_INTERVAL;

            m_SpawnPosX = Tools.ScreenPosToWorldPos(m_SpawnPipePosTrans,Camera.main,Vector2.right*(Screen.width *(1+0.1f))).x;
            m_TargetMovePosX = Tools.ScreenPosToWorldPos(m_SpawnPipePosTrans,Camera.main,Vector2.left*(Screen.width *(0.1f))).x;
            m_PipePrefab = m_ResLoadManager.LoadPrefab(ResPathDefine.PREFAB_Pipe_PATH);
            LoadPrefab(m_PipePrefab, m_SpawnPipePosTrans);
        }

        public void Update()
        {
            if (m_IsPause == true || m_IsGameOver == true)
            {
                return;
            }

            UpdateSpawnPipe();
        }

        public void Destroy()
        {
            ClearPool();
            m_PipeList = null;
        }

        public void GamePause()
        {
            m_IsPause = true;
            if (m_PipeList != null && m_PipeList.Count > 0)
            {
                foreach (var item in m_PipeList)
                {
                    item.Pause();
                }
            }
        }

        public void GameResume()
        {
            m_IsPause = false;
            if (m_PipeList != null && m_PipeList.Count > 0)
            {
                foreach (var item in m_PipeList)
                {
                    item.Resume();
                }
            }
        }

        public void GameOver()
        {
            m_IsGameOver = true;
            if (m_PipeList!=null && m_PipeList.Count>0)
            {
                foreach (var item in m_PipeList)
                {
                    item.GaomeOver();
                }
            }
        }

        /// <summary>
        /// 预载实例化对象
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        private void LoadPrefab(GameObject prefab,Transform parent)
        {
            PreloadT(prefab, parent);
        }

        /// <summary>
        /// 计时生成管子，以及初始化管子和设置回收管子事件
        /// </summary>
        void UpdateSpawnPipe() {

            m_SpawnTimer += Time.deltaTime;
            if (m_SpawnTimer>=GameConfig.PIPE_SPAWN_TIME_INTERVAL)
            {
                m_SpawnTimer -= GameConfig.PIPE_SPAWN_TIME_INTERVAL;
                Pipe pipe = Get(m_PipePrefab, m_SpawnPipePosTrans);
                float spawnPosY = Random.Range(GameConfig.PIPE_SPAWN_POS_Y_LIMIT_MIN,GameConfig.PIPE_SPAWN_POS_Y_LIMIT_MAX);
                pipe.Init(m_SpawnPosX, spawnPosY,m_TargetMovePosX, 
                    (p)=> {
                        m_PipeList.Remove(p);
                        Recycle(p);
                    });

                m_PipeList.Add(pipe);
            }
        }
    }
}
