using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus
{ 

	public abstract class BaseGameManager<T> : IManager where T : BaseGameManager<T>, new()
    {
        protected MonoBehaviour m_Mono;

        private Dictionary<Type, object> m_ManagerDict;
        private Dictionary<Type, object> m_ServerDict;

        // 单例
        private static T m_Instance;
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new T();
                }

                return m_Instance;
            }
        }

        public virtual void Awake(MonoBehaviour mono)
        {
            m_Mono = mono;
            m_ManagerDict = new Dictionary<Type, object>();
            m_ServerDict = new Dictionary<Type, object>();
        }

        public virtual void Start()
        {

            Init(null);

        }

        /// <summary>
        ///  Manager 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="objs"></param>
        protected abstract void InitManager(Transform rootTrans);

        /// <summary>
        ///  Server 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="objs"></param>
        protected abstract void InitServer(Transform rootTrans);

        public virtual void Init(Transform rootTrans)
        {

            // 确定顺序
            InitServer(null);
            InitManager(null);
        }

        public virtual void Update()
        {
            foreach (var item in m_ManagerDict.Values)
            {
                (item as IManager).Update();
            }
        }

        public virtual void Destroy()
        {
            foreach (var item in m_ServerDict.Values)
            {
                (item as IServer).Destroy();
            }

            foreach (var item in m_ManagerDict.Values)
            {
                (item as IManager).Destroy();
            }

            m_ManagerDict.Clear();
            m_ServerDict.Clear();
            m_ManagerDict = null;
            m_ServerDict = null;
            m_Mono = null;
        }

        /// <summary>
        /// 注册 Manager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="manager"></param>
        protected void RegisterManager<T>(T manager) where T : IManager
        {
            Type t = typeof(T);
            if (m_ManagerDict.ContainsKey(t) == true)
            {
                m_ManagerDict[t] = manager;
            }
            else
            {
                m_ManagerDict.Add(t, manager);
            }
        }

        /// <summary>
        /// 注册 Server
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="server"></param>
        protected void RegisterServer<T>(T server) where T : IServer
        {
            Type t = typeof(T);
            if (m_ServerDict.ContainsKey(t) == true)
            {
                m_ServerDict[t] = server;
            }
            else
            {
                m_ServerDict.Add(t, server);
            }
        }

        /// <summary>
        /// 获取指定 Manger 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetManager<T>() where T : class
        {
            Type t = typeof(T);
            if (m_ManagerDict.ContainsKey(t) == true)
            {
                return m_ManagerDict[t] as T;
            }
            else
            {
                Debug.LogError("GetManager()/ not exist ,t = " + t.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取指定 Server 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetServer<T>() where T : class
        {
            Type t = typeof(T);
            if (m_ServerDict.ContainsKey(t) == true)
            {
                return m_ServerDict[t] as T;
            }
            else
            {
                Debug.LogError("GetServer()/ not exist ,t = " + t.ToString());
                return null;
            }
        }
    }
}
