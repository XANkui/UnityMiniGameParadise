using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D { 

	public abstract class BaseGameManager : IManager
	{
        protected MonoBehaviour m_Mono;

        private Dictionary<Type, object> m_ManagerDict ;
        private Dictionary<Type, object> m_ServerDict ;



        public virtual void Awake(MonoBehaviour mono) {
            m_Mono = mono;
            m_ManagerDict = new Dictionary<Type, object>();
            m_ServerDict = new Dictionary<Type, object>();
        }

        public virtual void Start() {

            Init(null);
           
            
        }

        /// <summary>
        ///  Manager 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="objs"></param>
        protected abstract void InitManager(Transform rootTrans, params object[] objs);

        /// <summary>
        ///  Server 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="objs"></param>
        protected abstract void InitServer(Transform rootTrans, params object[] objs);

        public virtual void Init(Transform rootTrans, params object[] objs) {

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

        protected T GetManager<T>() where T : class
        {
            Type t = typeof(T);
            if (m_ManagerDict.ContainsKey(t) == true)
            {
                return m_ManagerDict[t] as T;
            }
            else
            {
                Debug.LogError("GetManager()/ not exist ,t = "+t.ToString());
                return null;
            }
        }
        protected T GetServer<T>() where T : class
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
