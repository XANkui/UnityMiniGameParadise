using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class SplashManager : IManager
	{
        private Queue<Splash> m_SplashQueue;
        private GameObject m_SplashPrefab;
        private Transform m_SpawnSplashPosTrans;
        public void Init(Transform rootTrans, params object[] manager)
        {
            
            m_SpawnSplashPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_SPLASH_POS_PATH);
            InitSplash();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            while (m_SplashQueue.Count>0)
            {
                GameObject.Destroy(m_SplashQueue.Dequeue());
            }

            m_SplashQueue = null;
            m_SplashPrefab = null;
            m_SpawnSplashPosTrans = null;
        }

        /// <summary>
        /// 获取 Splash
        /// </summary>
        /// <returns></returns>
        public Splash GetSplash()
        {
            if (m_SplashQueue.Count > 0)
            {
                Splash splash = m_SplashQueue.Dequeue();
                splash.gameObject.SetActive(true);
                return splash;
            }
            else
            {
                return InstantiateSplash();
            }
        }

        /// <summary>
        /// 回收 Splash
        /// </summary>
        /// <param name="splash"></param>
        public void RecycleSplah(Splash splash)
        {
            splash.gameObject.SetActive(false);
            m_SplashQueue.Enqueue(splash);
        }

        /// <summary>
        /// 初始化 SPlash
        /// </summary>
        private void InitSplash()
        {
            // 加载预制体
            m_SplashPrefab = Resources.Load<GameObject>(ResPathDefine.SPLASH_PREFAB_PATH);

            // 预载Splash
            m_SplashQueue = new Queue<Splash>();
            RecycleSplah(InstantiateSplash());
        }

        private Splash InstantiateSplash()
        {
            GameObject go = GameObject.Instantiate(m_SplashPrefab, m_SpawnSplashPosTrans);

            return go.AddComponent<Splash>();
        }
    }
}
