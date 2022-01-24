using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird
{ 

	public class GrassTileManager : IManager
	{
        private ResLoadManager m_ResLoadManager;
        private Transform m_SpawnGrassTilePosTrans;
        private List<GrassTile> m_GrassTileList;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_SpawnGrassTilePosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_GRASS_TILE_POS_PATH);
            m_ResLoadManager = managers[0] as ResLoadManager;
            m_GrassTileList = new List<GrassTile>();

            LoadPrefab();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            if (m_GrassTileList != null && m_GrassTileList.Count > 0)
            {
                for (int i = m_GrassTileList.Count - 1; i >= 0; i--)
                {
                    GameObject.Destroy(m_GrassTileList[i]);
                }
                m_GrassTileList.Clear();
                m_GrassTileList = null;
            }

            m_ResLoadManager = null;
            m_SpawnGrassTilePosTrans = null;
        }

        public void GamePause()
        {
            foreach (GrassTile item in m_GrassTileList)
            {
                item.Pause();
            }
        }

        public void GameResume()
        {
            foreach (GrassTile item in m_GrassTileList)
            {
                item.Resume();
            }
        }

        public void GameOver()
        {
            foreach (GrassTile item in m_GrassTileList)
            {
                item.GaomeOver();
            }
        }

        /// <summary>
        /// 加载实例化预制体
        /// </summary>
        private void LoadPrefab()
        {
            GameObject prefab = m_ResLoadManager.LoadPrefab(ResPathDefine.PREFAB_GRASS_TILE_PATH);
            for (int i = 0; i < GameConfig.BACKGROUND_TILE_COUNT; i++)
            {
                GameObject tile = GameObject.Instantiate(prefab, m_SpawnGrassTilePosTrans);
                GrassTile grassTile = tile.AddComponent<GrassTile>();
                grassTile.Init(i);
                m_GrassTileList.Add(grassTile);
            }
        }

       
    }
}
