using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird { 

    /// <summary>
    /// 预制体加载管理类
    /// </summary>
	public class ResLoadManager : IManager
	{
        public Dictionary<string, GameObject> m_PrefabsDict;
        public Dictionary<string, AudioClip> m_AudioClipsDict;

        public void Init(Transform rootTrans, params object[] managers)
        {
            m_PrefabsDict = new Dictionary<string, GameObject>();
            m_AudioClipsDict = new Dictionary<string, AudioClip>();
        }

        public void Update()
        {
        }
        public void GameOver()
        {
        }

        public void Destroy()
        {
            m_PrefabsDict.Clear();
            m_AudioClipsDict.Clear();
            m_PrefabsDict=null;
            m_AudioClipsDict = null;
        }

        /// <summary>
        /// 加载预制体 GameObject 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public GameObject LoadPrefab(string path) {
            if (m_PrefabsDict.ContainsKey(path) == true)
            {
                return m_PrefabsDict[path];
            }
            else {
                GameObject prefab = Load<GameObject>(path);
                if (prefab!=null)
                {
                    m_PrefabsDict.Add(path, prefab);
                }

                return prefab;
            }
        }

        /// <summary>
        /// 加载预制体 AudioClip 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AudioClip LoadAudioClip(string path)
        {
            if (m_AudioClipsDict.ContainsKey(path) == true)
            {
                return m_AudioClipsDict[path];
            }
            else
            {
                AudioClip prefab = Load<AudioClip>(path);
                if (prefab != null)
                {
                    m_AudioClipsDict.Add(path, prefab);
                }

                return prefab;
            }
        }

        /// <summary>
        /// 泛型加载预制体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        private T Load<T>(string path) where T:Object{
            T prefab = Resources.Load<T>(path);
            if (prefab == null)
            {
                Debug.LogError(GetType() + "/Load()/prefab is null,path = " + path);
            }

            return prefab;
        }

        
    }
}
