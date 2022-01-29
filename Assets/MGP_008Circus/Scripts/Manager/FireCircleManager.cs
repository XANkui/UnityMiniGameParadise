using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class FireCircleManager : IManager,IGameOver
	{
        private ResLoadServer m_ResLoadServer;
        private ObjectPoolManager m_ObjectPoolManager;
        private Transform m_SpawnFireCirclePosTrans;

        private GameObject m_FireCirclePrefab;
        private List<Transform> m_ShowFireCircleTransformList;
        private Queue<Transform> m_RemoveFireCircleTrQue;

        private float m_SpawnTimer;
        private Vector3 m_SpawnPos;
        private float m_TargetMovePosX;
        private float m_MoveSpeed;

        private bool m_IsStopSpawnTimer = false;

        public void Init(Transform rootTrans)
        {
            m_SpawnFireCirclePosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_FIRECIRCLE_POS_PATH);

            m_ResLoadServer = GameManager.Instance.GetServer<ResLoadServer>();
            m_ObjectPoolManager = GameManager.Instance.GetManager<ObjectPoolManager>();

            m_ShowFireCircleTransformList = new List<Transform>();
            m_RemoveFireCircleTrQue = new Queue<Transform>();

            m_SpawnTimer = GameConfig.FIRECIRLE_SPAWN_TIME_INTERVAL;

            float outScreenWidthScale = 0.1f;
            m_SpawnPos = new Vector3(Tools.ScreenPosToWorldPos(m_SpawnFireCirclePosTrans, Camera.main, Vector2.right * (Screen.width * (1 + outScreenWidthScale))).x, 0.09f,  0);
            m_TargetMovePosX = Tools.ScreenPosToWorldPos(m_SpawnFireCirclePosTrans, Camera.main, Vector2.left * (Screen.width * (outScreenWidthScale))).x;

            m_MoveSpeed = GameConfig.FIRECIRLE_MOVE_SPEED;
            m_IsStopSpawnTimer = false;

            LoadPrefab();
        }

        public void Update()
        {
            if (m_IsStopSpawnTimer == true)
            {
                return;
            }

            UpdateSpawnFireCircle();
            UpdateFireClePosRecycle();

        }

        public void GameOver()
        {
            m_IsStopSpawnTimer = true;
            
        }

        public void Destroy()
        {

            while (m_RemoveFireCircleTrQue.Count>0)
            {
                m_RemoveFireCircleTrQue.Dequeue();
            }
            m_RemoveFireCircleTrQue.Clear();
            m_ShowFireCircleTransformList.Clear();
            m_FireCirclePrefab = null;
            m_ResLoadServer = null;
            m_ObjectPoolManager = null;
            m_ShowFireCircleTransformList = null;
            m_RemoveFireCircleTrQue = null;

        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        private void LoadPrefab()
        {
            m_FireCirclePrefab = m_ResLoadServer.LoadPrefab(ResPathDefine.PREFAB_FIRECIRCLE_PATH);
        }

        /// <summary>
        /// 计时生成火圈，以及初始化管子和设置回收管子事件
        /// </summary>
        void UpdateSpawnFireCircle()
        {
            m_SpawnTimer += Time.deltaTime;
            if (m_SpawnTimer >= GameConfig.FIRECIRLE_SPAWN_TIME_INTERVAL)
            {
                m_SpawnTimer -= GameConfig.FIRECIRLE_SPAWN_TIME_INTERVAL;

                GameObject npc = m_ObjectPoolManager.SpawnObject(m_FireCirclePrefab, m_SpawnFireCirclePosTrans);
                npc.transform.position = m_SpawnPos;
                m_ShowFireCircleTransformList.Add(npc.transform);
            }

            foreach (Transform trans in m_ShowFireCircleTransformList)
            {
                trans.Translate(Vector3.left * m_MoveSpeed * Time.deltaTime,Space.World);
            }
        }

        /// <summary>
        /// 判断位置，回收对象
        /// </summary>
        void UpdateFireClePosRecycle()
        {
            if (m_ShowFireCircleTransformList != null)
            {
                foreach (Transform fireTrans in m_ShowFireCircleTransformList)
                {
                    if (fireTrans.position.x <= m_TargetMovePosX)
                    {
                        m_RemoveFireCircleTrQue.Enqueue(fireTrans);
                        m_ObjectPoolManager.ReleaseObject(fireTrans.gameObject);
                        fireTrans.position = m_SpawnPos;
                    }
                }
                
                // 移除
                while (m_RemoveFireCircleTrQue.Count > 0)
                {
                    m_ShowFireCircleTransformList.Remove(m_RemoveFireCircleTrQue.Dequeue());
                }
            }
        }
    }
}
