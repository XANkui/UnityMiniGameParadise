using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	public class BombEffectManager : IManager
	{
        private Queue<BombEffect> m_BombEffectQueue;
        private GameObject m_BombEffectPrefab;
        private Transform m_SpawnBombEffectPosTrans;
        public void Init(Transform rootTrans, params object[] manager)
        {

            m_SpawnBombEffectPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_BMOB_EFFECT_POS_PATH);
            InitBombEffect();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            while (m_BombEffectQueue.Count > 0)
            {
                GameObject.Destroy(m_BombEffectQueue.Dequeue());
            }

            m_BombEffectQueue = null;
            m_BombEffectPrefab = null;
            m_SpawnBombEffectPosTrans = null;
        }

        /// <summary>
        /// 获取爆炸特效
        /// </summary>
        /// <returns></returns>
        public BombEffect GetBombEffect()
        {
            if (m_BombEffectQueue.Count > 0)
            {
                BombEffect bombEffect = m_BombEffectQueue.Dequeue();
                bombEffect.gameObject.SetActive(true);
                return bombEffect;
            }
            else
            {
                return InstantiateBombEffect();
            }
        }

        /// <summary>
        /// 回收爆炸特效
        /// </summary>
        /// <param name="bombEffect"></param>
        public void RecycleBombEffect(BombEffect bombEffect)
        {
            bombEffect.gameObject.SetActive(false);
            m_BombEffectQueue.Enqueue(bombEffect);
        }

        /// <summary>
        /// 初始化特效
        /// </summary>
        private void InitBombEffect()
        {
            // 加载预制体
            m_BombEffectPrefab = Resources.Load<GameObject>(ResPathDefine.BOMB_EFFECT_PREFAB_PATH);

            // 预载 BombEffect
            m_BombEffectQueue = new Queue<BombEffect>();
            RecycleBombEffect(InstantiateBombEffect());
        }

        private BombEffect InstantiateBombEffect()
        {
            GameObject go = GameObject.Instantiate(m_BombEffectPrefab, m_SpawnBombEffectPosTrans);

            return go.AddComponent<BombEffect>();
        }
    }
}
