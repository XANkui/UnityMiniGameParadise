using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 
	public class BombManager : IManager
	{
        private Queue<Bomb> m_BombQueue;
        private GameObject m_BombPrefab;
        private Transform m_SpawnBombPosTrans;
        public void Init(Transform rootTrans, params object[] manager)
        {
            m_SpawnBombPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_BMOB_POS_PATH);
            InitBmob();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            while (m_BombQueue.Count > 0)
            {
                GameObject.Destroy(m_BombQueue.Dequeue());
            }

            m_BombQueue = null;
            m_BombPrefab = null;
            m_SpawnBombPosTrans = null;
        }

        public Bomb GetBmob()
        {
            if (m_BombQueue.Count > 0)
            {
                Bomb bomb = m_BombQueue.Dequeue();
                bomb.gameObject.SetActive(true);
                return bomb;
            }
            else
            {
                return InstantiateBomb();
            }
        }


        public void RecycleBomb(Bomb bomb)
        {
            bomb.gameObject.SetActive(false);
            m_BombQueue.Enqueue(bomb);
        }

        private void InitBmob()
        {

            m_BombPrefab = Resources.Load<GameObject>(ResPathDefine.BOMB_PREFAB_PATH);

            // 预载 Bomb
            m_BombQueue = new Queue<Bomb>();
            RecycleBomb(InstantiateBomb());
        }

        private Bomb InstantiateBomb()
        {
            GameObject go = GameObject.Instantiate(m_BombPrefab, m_SpawnBombPosTrans);

            return go.AddComponent<Bomb>();
        }
    }
}
