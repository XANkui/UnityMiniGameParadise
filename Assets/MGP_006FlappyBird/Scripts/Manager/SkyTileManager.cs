using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird
{ 

	public class SkyTileManager : IManager
	{
        private ResLoadManager m_ResLoadManager;
        private Transform m_SpawnSkyTilePosTrans;
        private List<GameObject> m_SkyTileList;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_SpawnSkyTilePosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_SKY_TILE_POS_PATH);
            m_ResLoadManager = managers[0] as ResLoadManager;
            m_SkyTileList = new List<GameObject>();

            LoadPrefab();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            if (m_SkyTileList!=null && m_SkyTileList.Count>0)
            {
                for (int i = m_SkyTileList.Count-1; i >=0; i--)
                {
                    GameObject.Destroy(m_SkyTileList[i]);
                }
                m_SkyTileList.Clear();
                m_SkyTileList = null;
            }

            m_ResLoadManager = null;
            m_SpawnSkyTilePosTrans = null;
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        private void LoadPrefab() {
            GameObject prefab = m_ResLoadManager.LoadPrefab(ResPathDefine.PREFAB_SKY_TILE_PATH);
            for (int i = 0; i < GameConfig.BACKGROUND_TILE_COUNT; i++)
            {
                GameObject skyTile = GameObject.Instantiate(prefab,m_SpawnSkyTilePosTrans);
                skyTile.AddComponent<SkyTile>().Init(i);
                m_SkyTileList.Add(skyTile);
            }
        }

        public void GamePause()
        {
            foreach (GameObject item in m_SkyTileList)
            {
                item.GetComponent<SkyTile>().Pause();
            }
        }

        public void GameResume()
        {
            foreach (GameObject item in m_SkyTileList)
            {
                item.GetComponent<SkyTile>().Resume();
            }
        }

        public void GameOver()
        {
            foreach (GameObject item in m_SkyTileList)
            {
                item.GetComponent<SkyTile>().GaomeOver();
            }
        }
    }
}
