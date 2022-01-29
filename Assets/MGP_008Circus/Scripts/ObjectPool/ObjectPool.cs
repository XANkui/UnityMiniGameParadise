using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus {

    /// <summary>
    /// 对象池
    /// </summary>
    public class ObjectPool<T>
    {
        // 对象池中的对象列表
        private List<ObjectPoolContainer<T>> m_ListObjects;

        // 对象池中使用了的对象字典
        private Dictionary<T, ObjectPoolContainer<T>> m_UsedObjectDictionary;

        // 回调事件(具体生成函数)
        private Func<T> m_FactorySpawnFuc;
        private Action<T> m_FactoryClearAction;

        // 对象列表索引
        private int m_LastObjectIndex = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factorySpawnFunc">工厂回调函数</param>
        /// <param name="initialSize">初始化个数</param>
        public ObjectPool(Func<T> factorySpawnFunc, Action<T> factoryClearAction, int initialSize)
        {

            this.m_FactorySpawnFuc = factorySpawnFunc;
            this.m_FactoryClearAction = factoryClearAction;

            m_ListObjects = new List<ObjectPoolContainer<T>>();
            m_UsedObjectDictionary = new Dictionary<T, ObjectPoolContainer<T>>();

            Warm(initialSize);
        }

        /// <summary>
        /// 孵化器，生成对象池实例
        /// </summary>
        /// <param name="capacity">实例个数</param>
        private void Warm(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                CreateContainer();
            }
        }

        /// <summary>
        /// 生成对象池实例
        /// </summary>
        /// <returns></returns>
        private ObjectPoolContainer<T> CreateContainer()
        {
            ObjectPoolContainer<T> container = new ObjectPoolContainer<T>();

            // 生成实例
            container.Item = m_FactorySpawnFuc.Invoke();

            // 实例添加到对象池列表中
            m_ListObjects.Add(container);

            return container;
        }

        /// <summary>
        /// 从对象列中获取可用的对象
        /// </summary>
        /// <returns>返回可用的对象</returns>
        public T GetObjectPoolContainerItem()
        {
            ObjectPoolContainer<T> container = null;

            for (int i = 0; i < m_ListObjects.Count; i++)
            {
                // 对象列表索引递增，并且防止越界
                m_LastObjectIndex++;
                m_LastObjectIndex %= m_ListObjects.Count;

                // 如果列表中的对象正在使用，则进行下一循环,否则返回该对象,并退出循环
                if (m_ListObjects[m_LastObjectIndex].Used)
                {
                    continue;
                }
                else
                {

                    container = m_ListObjects[m_LastObjectIndex];
                    break;
                }
            }

            // 如果没有可用的对象，重新生成一个对象
            if (container == null)
            {
                container = CreateContainer();
            }

            // 标记当前对象已经被使用，并添加到使用列表中 
            container.Consume();
            m_UsedObjectDictionary.Add(container.Item, container);
            return container.Item;
        }

        /// <summary>
        /// 释放正在使用的对象
        /// </summary>
        /// <param name="item">要释放的对象</param>
        /// <returns>true：释放成功/false: 释放失败</returns>
        public bool ReleaseItem(object item)
        {
            return ReleaseItem((T)item);
        }

        /// <summary>
        /// 从已使用字典中，释放正在使用的对象
        /// </summary>
        /// <param name="item">释放对象</param>
        /// <returns>true：释放成功/false: 释放失败</returns>
        public bool ReleaseItem(T item)
        {

            // 判断是否存在已使用对象字典中
            if (m_UsedObjectDictionary.ContainsKey(item))
            {

                // 存在，即释放该对象，并从已使用字典中移除
                ObjectPoolContainer<T> container = m_UsedObjectDictionary[item];
                container.Release();
                m_UsedObjectDictionary.Remove(item);

                return true;
            }
            else
            {
                Debug.Log("This object pool does not contain the item provided: " + item);

                return false;
            }
        }

        /// <summary>
        /// 清空池子
        /// </summary>
        public void ClearPool() {
            m_UsedObjectDictionary.Clear();
            if (m_FactoryClearAction != null)
            {
                for (int i = m_ListObjects.Count - 1; i >= 0; i--)
                {
                    m_FactoryClearAction.Invoke(m_ListObjects[i].Item);
                }
            }
            m_ListObjects.Clear();

            m_UsedObjectDictionary = null;
            m_ListObjects = null;

            this.m_FactorySpawnFuc = null;
            this.m_FactoryClearAction = null;

        }

        /// <summary>
        /// 获取对象池对象列表中的个数
        /// </summary>
        public int Count
        {
            get
            {
                return m_ListObjects.Count;
            }
        }

        /// <summary>
        /// 获取对象池已经使用的对象个数
        /// </summary>
        public int CountUsedItems
        {
            get
            {
                return m_UsedObjectDictionary.Count;
            }
        }
    }
}
