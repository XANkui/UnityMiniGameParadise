using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class FruitManager : IManager
	{
        private Dictionary<FruitType, Queue<BaseFruit>> m_FruitObjectPoolDict;
        private Dictionary<FruitBrokenType, Queue<FruitBroken>> m_FruitBrokenObjectPoolDict;
        private Dictionary<string, GameObject> m_FruitPrefabsDict;
        private Transform m_SpawnFruitPosTrans;
        public void Init(Transform rootTrans, params object[] manager)
        {
            m_SpawnFruitPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_FRUIT_POS_PATH);
            InitFruit();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            if (m_FruitObjectPoolDict!=null)
            {
                foreach (Queue<BaseFruit> queue in m_FruitObjectPoolDict.Values)
                {
                    while (queue.Count > 0)
                    {
                        GameObject.Destroy(queue.Dequeue());
                    }

                }
            }

            if (m_FruitBrokenObjectPoolDict!=null)
            {
                foreach (Queue<FruitBroken> queue in m_FruitBrokenObjectPoolDict.Values)
                {
                    while (queue.Count > 0)
                    {
                        GameObject.Destroy(queue.Dequeue());

                    }
                }
            }
            

            m_FruitObjectPoolDict.Clear();
            m_FruitBrokenObjectPoolDict.Clear();
            m_FruitPrefabsDict.Clear();

            m_FruitObjectPoolDict = null;
            m_FruitBrokenObjectPoolDict = null;
            m_FruitPrefabsDict = null;
            m_SpawnFruitPosTrans = null;

        }

        /// <summary>
        /// 生成随机水果
        /// </summary>
        /// <returns></returns>
        public BaseFruit GetRandomFruit() {
            int random = UnityEngine.Random.Range(0,(int)FruitType.SUM_COUNT);

            return GetFruit((FruitType)random);
        }

        /// <summary>
        /// 获取指定水果
        /// </summary>
        /// <param name="fruitType"></param>
        /// <returns></returns>
        public BaseFruit GetFruit(FruitType fruitType) {
            Queue<BaseFruit> fruitQue = m_FruitObjectPoolDict[fruitType];
            if (fruitQue.Count > 0)
            {
                BaseFruit baseFruit = fruitQue.Dequeue();
                baseFruit.gameObject.SetActive(true);
                return baseFruit;
            }
            else {
                return InstantiateFruit(fruitType);
            }
        }

        /// <summary>
        /// 获取水果碎片
        /// </summary>
        /// <param name="fruitBrokenType"></param>
        /// <returns></returns>
        public FruitBroken GetFruitBroken(FruitBrokenType fruitBrokenType)
        {
            Queue<FruitBroken> fruitBrokenQue = m_FruitBrokenObjectPoolDict[fruitBrokenType];
            if (fruitBrokenQue.Count > 0)
            {
                return fruitBrokenQue.Dequeue();
            }
            else
            {
                return InstantiateFruitBroken(fruitBrokenType);
            }
        }

        /// <summary>
        /// 回收水果
        /// </summary>
        /// <param name="fruitType"></param>
        /// <param name="baseFruit"></param>
        public void RecycleFruit(FruitType fruitType,BaseFruit baseFruit) {
            baseFruit.gameObject.SetActive(false);
            if (m_FruitObjectPoolDict!=null && m_FruitObjectPoolDict[fruitType]!=null)
            {
                m_FruitObjectPoolDict[fruitType].Enqueue(baseFruit);

            }
        }

        /// <summary>
        /// 回收水果碎片
        /// </summary>
        /// <param name="fruitBrokenType"></param>
        /// <param name="fruitBroken"></param>
        public void RecycleFruitBroken(FruitBrokenType fruitBrokenType, FruitBroken fruitBroken)
        {
            fruitBroken.gameObject.SetActive(false);
            m_FruitBrokenObjectPoolDict[fruitBrokenType].Enqueue(fruitBroken);
        }

        /// <summary>
        /// 加载预制体和预载水果到对象池中
        /// </summary>
        private void InitFruit() {

            // 加载预制体
            m_FruitPrefabsDict = new Dictionary<string, GameObject>();
            for (FruitType fruitType = FruitType.Apple; fruitType < FruitType.SUM_COUNT; fruitType++)
            {
                GameObject prefab = Resources.Load<GameObject>(ResPathDefine.FRUIT_PREFAB_BASE_PATH+fruitType.ToString());
                if (prefab == null)
                {
                    Debug.LogError(GetType() + "/InitFruit()/prefab is null, path = " + ResPathDefine.FRUIT_PREFAB_BASE_PATH + fruitType.ToString());
                }
                else {
                    m_FruitPrefabsDict.Add(fruitType.ToString(),prefab);
                }
            }

            // 加载预制体
            for (FruitBrokenType fruitBrokenType = FruitBrokenType.Apple_Broken; fruitBrokenType < FruitBrokenType.SUM_COUNT; fruitBrokenType++)
            {
                GameObject prefab = Resources.Load<GameObject>(ResPathDefine.FRUIT_PREFAB_BASE_PATH + fruitBrokenType.ToString());
                if (prefab == null)
                {
                    Debug.LogError(GetType() + "/InitFruit()/prefab is null, path = " + ResPathDefine.FRUIT_PREFAB_BASE_PATH + fruitBrokenType.ToString());
                }
                else
                {
                    m_FruitPrefabsDict.Add(fruitBrokenType.ToString(), prefab);
                }
            }

            // 预载水果
            m_FruitObjectPoolDict = new Dictionary<FruitType, Queue<BaseFruit>>();
            for (FruitType fruitType = FruitType.Apple; fruitType < FruitType.SUM_COUNT; fruitType++) {
                Queue<BaseFruit> fruits = new Queue<BaseFruit>();
                m_FruitObjectPoolDict.Add(fruitType, fruits);

                RecycleFruit(fruitType, InstantiateFruit(fruitType));
            }

            // 预载水果 Broken
            m_FruitBrokenObjectPoolDict = new Dictionary<FruitBrokenType, Queue<FruitBroken>>();
            for (FruitBrokenType fruitBrokenType = FruitBrokenType.Apple_Broken; fruitBrokenType < FruitBrokenType.SUM_COUNT; fruitBrokenType++)
            {
                Queue<FruitBroken> fruitBrokens = new Queue<FruitBroken>();
                m_FruitBrokenObjectPoolDict.Add(fruitBrokenType, fruitBrokens);

                RecycleFruitBroken(fruitBrokenType, InstantiateFruitBroken(fruitBrokenType));
            }
        }

        /// <summary>
        /// 生成指定水果对象
        /// </summary>
        /// <param name="fruitType"></param>
        /// <returns></returns>
        private BaseFruit InstantiateFruit(FruitType fruitType) {
            GameObject fruit = GameObject.Instantiate(m_FruitPrefabsDict[fruitType.ToString()], m_SpawnFruitPosTrans);
            // 反射获取脚本类型，添加脚本 (注意添加命名空间)
            Type type = Type.GetType( GameConfig.NAME_SPACE_NAME+ fruitType.ToString());
            return (fruit.AddComponent(type) as BaseFruit);
        }

        /// <summary>
        /// 生成破碎水果
        /// </summary>
        /// <param name="fruitBrokenType"></param>
        /// <returns></returns>
        private FruitBroken InstantiateFruitBroken(FruitBrokenType fruitBrokenType)
        {
            GameObject fruit = GameObject.Instantiate(m_FruitPrefabsDict[fruitBrokenType.ToString()], m_SpawnFruitPosTrans);
          
            return fruit.AddComponent<FruitBroken>();
        }
    }
}
