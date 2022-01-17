using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class EffectManager : IManager
	{
        private GameObject m_EffectPrefab;
        private Queue<Effect> m_IdleEffectQueue; // 闲置特效队列，可重复使用

        public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
            m_EffectPrefab = Resources.Load<GameObject>(ResPathDefine.EFFECT_PREFAB_PATH);
            if (m_EffectPrefab==null)
            {
                Debug.LogError(GetType()+ "/Init()/m_EffectPrefab Loaded is null, path = " + ResPathDefine.EFFECT_PREFAB_PATH);
            }

            m_IdleEffectQueue = new Queue<Effect>();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            m_EffectPrefab = null;

            while (m_IdleEffectQueue.Count>0)
            {
                GameObject.Destroy(m_IdleEffectQueue.Dequeue());
            }

            m_IdleEffectQueue.Clear();
            m_IdleEffectQueue=null;
        }


        public void ShowEffect(Color32 color, Vector3 pos)
        {
            Effect effect = GetEffect();
            effect.transform.position = pos;
            effect.transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(-180, 180));
            effect.Show(color, OnEffectShowEndAction);
        }

        /// <summary>
        /// 简单对象池获取特效
        /// </summary>
        /// <returns></returns>
        private Effect GetEffect() {
            if (m_IdleEffectQueue.Count > 0)
            {
                Effect effect = m_IdleEffectQueue.Dequeue();
                effect.gameObject.SetActive(true);
                return effect;
            }
            else {
                GameObject go = GameObject.Instantiate(m_EffectPrefab);
                return go.AddComponent<Effect>();
                 
            }
        }

        /// <summary>
        /// 特效动画结束，回收特效到对象池
        /// </summary>
        /// <param name="effect"></param>
        private void OnEffectShowEndAction(Effect effect) {
            effect.gameObject.SetActive(false);
            m_IdleEffectQueue.Enqueue(effect);
        }
    }
}
