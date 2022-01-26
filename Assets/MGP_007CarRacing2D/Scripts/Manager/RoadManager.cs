using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D { 

	public class RoadManager : IManager,IGamePause,IGameResume,IGameOver
	{
        private ResLoadServer m_ResLoadServer;
        private Transform m_SpawnSkyTilePosTrans;
        private List<Rigidbody2D> m_RoadRigidbodyList;
        private List<Transform> m_RoadTransformList;
        private Vector2 m_RoadMoveVelocity ;
        private float m_TargetPosY;
        private float m_ResetMoveDistanceY;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_SpawnSkyTilePosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_ROAD_POS_PATH);
            m_ResLoadServer = managers[0] as ResLoadServer;
            m_RoadRigidbodyList = new List<Rigidbody2D>();
            m_RoadTransformList = new List<Transform>();
            m_RoadMoveVelocity = Vector2.down * GameConfig.ROAD_MOVEVELOCITY_Y;
            m_TargetPosY = -1 * GameConfig.ROAD_SPRITE_INTERVAL_Y;
            m_ResetMoveDistanceY = 2 * GameConfig.ROAD_SPRITE_INTERVAL_Y;

            LoadPrefab();

            RoadMove();
        }

        public void Update()
        {
            UpdatePosOperation();
        }

        public void Destroy()
        {
            if (m_RoadRigidbodyList != null && m_RoadRigidbodyList.Count > 0)
            {
                for (int i = m_RoadRigidbodyList.Count - 1; i >= 0; i--)
                {
                    GameObject.Destroy(m_RoadRigidbodyList[i]);
                }
                m_RoadRigidbodyList.Clear();
                m_RoadRigidbodyList = null;
            }

            if (m_RoadTransformList != null && m_RoadTransformList.Count > 0)
            {
                for (int i = m_RoadTransformList.Count - 1; i >= 0; i--)
                {
                    GameObject.Destroy(m_RoadTransformList[i]);
                }
                m_RoadTransformList.Clear();
                m_RoadTransformList = null;
            }

            m_ResLoadServer = null;
            m_SpawnSkyTilePosTrans = null;
        }

        public void GamePause()
        {
            foreach (Rigidbody2D rigidbody2D in m_RoadRigidbodyList)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }

        public void GameResume()
        {
            RoadMove();
        }

        public void GameOver()
        {
            foreach (Rigidbody2D rigidbody2D in m_RoadRigidbodyList)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        private void LoadPrefab()
        {
            GameObject prefab = m_ResLoadServer.LoadPrefab(ResPathDefine.PREFAB_ROAD_PATH);
            for (int i = 0; i < GameConfig.ROAD_TILE_COUNT; i++)
            {
                GameObject road = GameObject.Instantiate(prefab, m_SpawnSkyTilePosTrans);
                Vector3 curPos = road.transform.position;
                road.transform.position = new Vector3(curPos.x, curPos.y+(i*GameConfig.ROAD_SPRITE_INTERVAL_Y),curPos.z);
                m_RoadTransformList.Add(road.transform);
                m_RoadRigidbodyList.Add(road.GetComponent<Rigidbody2D>());
            }
        }

        /// <summary>
		/// 更新Road位置
		/// 当位置到达指定位置，进行位置左移，从而实现无限循环
		/// </summary>
		private void UpdatePosOperation()
        {

            foreach (var item in m_RoadTransformList)
            {
                Vector3 curPos = item.position;

                if (curPos.y <= m_TargetPosY)
                {
                    // 移动到右边（以为走了，右边的右边，所以增加 2 * BACKGROUND_SPRITE_INTERVAL_X ）
                    curPos = new Vector3( curPos.x, (curPos.y+ m_ResetMoveDistanceY), curPos.z);
                    item.position = curPos;
                }
            }
            
        }

        void RoadMove() {
            foreach (Rigidbody2D rigidbody2D in m_RoadRigidbodyList)
            {
                rigidbody2D.velocity = m_RoadMoveVelocity;
            }
        }
    }
}
