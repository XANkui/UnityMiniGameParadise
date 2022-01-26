using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D { 

	public class NPCManager : IManager, IGamePause, IGameResume, IGameOver
    {
        private ResLoadServer m_ResLoadServer;
        private ObjectPoolManager m_ObjectPoolManager;
        private Transform m_SpawnNPCPosTrans;
        private Dictionary<NPCType,GameObject> m_NPCPrefabDict;
        private List<Rigidbody2D> m_ShowNPCRigidbodyList;
        private List<Transform> m_ShowNPCTransformList;

        private float m_SpawnTimer = 0;
        private Vector3[] m_SpawnPosArray;
        private float m_TargetMovePosY = 0;
        private Vector2 m_NPCMoveVelocity;

        private bool m_IsStopSpawnTimer = false;
        public void Init(Transform rootTrans, params object[] objs)
        {
            m_SpawnNPCPosTrans = rootTrans.Find("SpawnNPCPos");

            m_ResLoadServer = objs[0] as ResLoadServer;
            m_ObjectPoolManager = objs[1] as ObjectPoolManager;

            m_NPCPrefabDict = new Dictionary<NPCType, GameObject>();
            m_ShowNPCRigidbodyList = new List<Rigidbody2D>();
            m_ShowNPCTransformList = new List<Transform>();

            m_SpawnTimer = 0;

            m_SpawnPosArray = new Vector3[4];
            m_SpawnPosArray[0] = new Vector3(GameConfig.CAR_LEFT_OUTSIDE_LIMIT,Tools.ScreenPosToWorldPos(m_SpawnNPCPosTrans, Camera.main, Vector2.up * (Screen.height * (1 + 0.2f))).y,0);
            m_SpawnPosArray[1] = new Vector3(GameConfig.CAR_LEFT_MIDDLE_LIMIT, Tools.ScreenPosToWorldPos(m_SpawnNPCPosTrans, Camera.main, Vector2.up * (Screen.height * (1 + 0.2f))).y,0);
            m_SpawnPosArray[2] = new Vector3(GameConfig.CAR_RIGHT_MIDDLE_LIMIT, Tools.ScreenPosToWorldPos(m_SpawnNPCPosTrans, Camera.main, Vector2.up * (Screen.height * (1 + 0.2f))).y,0);
            m_SpawnPosArray[3] = new Vector3(GameConfig.CAR_RIGHT_OUTSIDE_LIMIT, Tools.ScreenPosToWorldPos(m_SpawnNPCPosTrans, Camera.main, Vector2.up * (Screen.height * (1 + 0.2f))).y,0);
            m_TargetMovePosY = Tools.ScreenPosToWorldPos(m_SpawnNPCPosTrans, Camera.main, Vector2.down * (Screen.height * (0.2f))).y;
            
            m_NPCMoveVelocity = Vector2.down * GameConfig.ROAD_MOVEVELOCITY_Y;
            m_IsStopSpawnTimer = true;

            LoadPrefab();
        }

        public void Update()
        {
            if (m_IsStopSpawnTimer==true)
            {
                return;
            }

            UpdateSpawnNPC();
            UpdateNPCPosRecycle();

        }

        public void GamePause()
        {
            m_IsStopSpawnTimer = true;
            foreach (Rigidbody2D rb in m_ShowNPCRigidbodyList)
            {
                rb.velocity = Vector2.zero;
            }
        }

        public void GameResume()
        {
            m_IsStopSpawnTimer = false;
            foreach (Rigidbody2D rb in m_ShowNPCRigidbodyList)
            {
                rb.velocity = m_NPCMoveVelocity;
            }
        }

        public void GameOver()
        {
            m_IsStopSpawnTimer = true;
            foreach (Rigidbody2D rb in m_ShowNPCRigidbodyList)
            {
                rb.velocity = Vector2.zero;
            }   
        }

        public void Destroy()
        {
            m_NPCPrefabDict.Clear();
            m_ShowNPCRigidbodyList.Clear();
            m_ShowNPCTransformList.Clear();
            m_NPCPrefabDict = null;
            m_ShowNPCRigidbodyList = null;
            m_ShowNPCTransformList = null;        
           
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        private void LoadPrefab() {

            for (NPCType npc = NPCType.Coin; npc < NPCType.SUM_COUNT; npc++)
            {
                m_NPCPrefabDict.Add(npc,m_ResLoadServer.LoadPrefab(ResPathDefine.PREFAB_NPC_BASE_PATH + npc.ToString()));
            }
        }

        /// <summary>
        /// 计时生成管子，以及初始化管子和设置回收管子事件
        /// </summary>
        void UpdateSpawnNPC()
        {
            m_SpawnTimer += Time.deltaTime;
            if (m_SpawnTimer >= GameConfig.NPC_SPAWN_TIME_INTERVAL)
            {
                m_SpawnTimer -= GameConfig.NPC_SPAWN_TIME_INTERVAL;

                int rand = Random.Range((int)NPCType.Coin, (int)NPCType.SUM_COUNT);
                GameObject npc = m_ObjectPoolManager.SpawnObject(m_NPCPrefabDict[(NPCType)rand],m_SpawnNPCPosTrans);
                npc.transform.position = m_SpawnPosArray[Random.Range(0, m_SpawnPosArray.Length)];
                Rigidbody2D rb = npc.GetComponent<Rigidbody2D>();
                rb.velocity = m_NPCMoveVelocity;
                m_ShowNPCRigidbodyList.Add(rb);
                m_ShowNPCTransformList.Add(npc.transform);
            }
        }

        /// <summary>
        /// 判断位置，回收对象
        /// </summary>
        void UpdateNPCPosRecycle() {
            if (m_ShowNPCTransformList!=null)
            {
                foreach (Transform npc in m_ShowNPCTransformList)
                {
                    if (npc.position.y<=m_TargetMovePosY)
                    {
                        npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        m_ObjectPoolManager.ReleaseObject(npc.gameObject);
                        npc.position = m_SpawnPosArray[0];
                    }
                }
            }
        }
    }
}
