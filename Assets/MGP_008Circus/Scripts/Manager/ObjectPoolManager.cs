using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class ObjectPoolManager : IManager
	{

        // 是否打印对象池对象使用情况（默认不打印）
        private bool m_IsLogUsedPoolObject = false;

        // 预制体（对象池对象）生成对象池的字典
        private Dictionary<GameObject, ObjectPool<GameObject>> m_PrefabPoolDictinary;

        // 正在使用的对象的字典
        private Dictionary<GameObject, ObjectPool<GameObject>> m_UsedPoolObjectbDictinary;

        // 对象池是否更新使用的标志
        private bool m_Dirty = false;

        public void Init(Transform rootTrans)
        {
            // 初始化字典
            m_PrefabPoolDictinary = new Dictionary<GameObject, ObjectPool<GameObject>>();
            m_UsedPoolObjectbDictinary = new Dictionary<GameObject, ObjectPool<GameObject>>();
        }

        public void Update()
        {
            if (m_IsLogUsedPoolObject == true && m_Dirty == true)
            {
                PrintUsedPoolObjectStatue();
                m_Dirty = false;
            }
        }


        public void Destroy()
        {
            ClearAllPool();
        }


        /// <summary>
        /// 是否打印对象池对象使用情况（默认不打印）
        /// </summary>
        /// <param name="isLogUsedPoolObject"></param>
        public void SetIsLogUsedPoolObject(bool isLogUsedPoolObject)
        {
            this.m_IsLogUsedPoolObject = isLogUsedPoolObject;
        }

        /// <summary>
        /// 孵化器孵化指定个数对象池对象
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <param name="count">要预生成对象池对象</param>
        public void WarmPool(GameObject prefab,Transform parent, int count)
        {
            if (m_PrefabPoolDictinary.ContainsKey(prefab))
            {
                Debug.Log("Pool for prefab " + prefab.name + " has already been created");
            }

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(() => {
                return InstantiatePrefab(prefab, parent);

            }, DestroyClone, count);

            // 添加到字典中
            m_PrefabPoolDictinary[prefab] = pool;

            // 更新使用数据标志
            m_Dirty = true;

        }

        /// <summary>
        /// 从对象池拿出指定对象使用
        /// </summary>
        /// <param name="prefab">要使用的对象</param>
        /// <returns>对象池返回的可用对象</returns>
        public GameObject SpawnObject(GameObject prefab, Transform parent)
        {
            return SpawnObject(prefab, parent, Vector3.zero, Quaternion.identity);
        }


        public GameObject SpawnObject(GameObject prefab,Transform parent, Vector3 position, Quaternion rotation)
        {
            // 如果该预制体没有孵化，则先进行孵化 1 个
            if (m_PrefabPoolDictinary.ContainsKey(prefab) == false)
            {
                WarmPool(prefab, parent,1);
            }

            // 从对象池中获取对象
            ObjectPool<GameObject> pool = m_PrefabPoolDictinary[prefab];
            GameObject clone = pool.GetObjectPoolContainerItem();

            // 设置对象的位置旋转，显示物体
            clone.transform.position = position;
            clone.transform.rotation = rotation;
            clone.SetActive(true);

            // 把拿出来的对象添加到已使用的字典中
            m_UsedPoolObjectbDictinary.Add(clone, pool);

            // 更新使用数据标志
            m_Dirty = true;

            return clone;
        }

        /// <summary>
        /// 释放使用的对象池对象
        /// </summary>
        /// <param name="clone">对象</param>
        public void ReleaseObject(GameObject clone)
        {
            clone.SetActive(false);

            // 已使用的字典中
            if (m_UsedPoolObjectbDictinary.ContainsKey(clone))
            {
                m_UsedPoolObjectbDictinary[clone].ReleaseItem(clone);
                m_UsedPoolObjectbDictinary.Remove(clone);

                // 更新使用数据标志
                m_Dirty = true;
            }
            else
            {

                Debug.Log("No pool contains the object: " + clone.name);

            }
        }

        /// <summary>
        /// 清空所有池子
        /// </summary>
        public void ClearAllPool() {
            m_UsedPoolObjectbDictinary.Clear();
            foreach (ObjectPool<GameObject> op in m_PrefabPoolDictinary.Values)
            {
                op.ClearPool();
            }

            m_PrefabPoolDictinary.Clear();

            m_UsedPoolObjectbDictinary = null;
            m_PrefabPoolDictinary = null;
        }

        /// <summary>
        /// 打印吃对象使用情况
        /// </summary>
        private void PrintUsedPoolObjectStatue()
        {

            foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in m_PrefabPoolDictinary)
            {
                Debug.Log(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
            }
        }

        /// <summary>
        /// 生成函数,父物体为被物体
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <returns></returns>
        private GameObject InstantiatePrefab(GameObject prefab, Transform parent)
        {
            var go = GameObject.Instantiate(prefab) as GameObject;
            go.transform.parent = parent;
            go.SetActive(false);
            return go;
        }

        /// <summary>
        /// 最后销毁
        /// </summary>
        /// <param name="clone"></param>
        private void DestroyClone(GameObject clone) {
            GameObject.Destroy(clone);
        }
     
    }
}

