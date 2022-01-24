using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird { 

    /// <summary>
    /// 泛型对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ObjectPool<T>  where T : MonoBehaviour
	{
        private Queue<T> m_TQueue;

        /// <summary>
        /// 获取 T
        /// </summary>
        /// <returns></returns>
        public T Get(GameObject prefab,Transform parent)
        {
            if (m_TQueue!=null && m_TQueue.Count > 0)
            {
                T t = m_TQueue.Dequeue();
                t.gameObject.SetActive(true);
                return t;
            }
            else
            {
                return InstantiateT(prefab, parent);
            }
        }

        /// <summary>
        /// 回收 T
        /// </summary>
        /// <param name="t"></param>
        public void Recycle(T t)
        {
            t.gameObject.SetActive(false);
            if (m_TQueue==null)
            {
                m_TQueue = new Queue<T>();
            }
            m_TQueue.Enqueue(t);
        }

        /// <summary>
        /// 预载
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="preloadCount"></param>
        public void PreloadT(GameObject prefab, Transform parent,int preloadCount=1)
        {
            // 预载Splash
            if (m_TQueue==null)
            {
                m_TQueue = new Queue<T>();
            }
            for (int i = 0; i < preloadCount; i++)
            {
                Recycle(InstantiateT(prefab, parent));
            }
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        public void ClearPool() {
            if (m_TQueue!=null)
            {
                while (m_TQueue.Count > 0)
                {
                    GameObject.Destroy(m_TQueue.Dequeue().gameObject);
                }
            }

            m_TQueue = null;
        }

        /// <summary>
        /// 生成实例
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private T InstantiateT(GameObject prefab,Transform parent)
        {
            GameObject go = GameObject.Instantiate(prefab, parent);

            return go.AddComponent<T>();
        }
    }
}
