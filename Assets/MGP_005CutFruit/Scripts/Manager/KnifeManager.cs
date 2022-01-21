using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class KnifeManager : IManager
	{
        private GameObject m_KnifePrefab;
        private Transform m_KnifeTrans;
        private Transform m_SpawnKnifePosTrans;
        private bool m_IsGameOver;
        public void Init(Transform rootTrans, params object[] manager)
        {
            m_SpawnKnifePosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_KNIFE_POS_PATH);
            LoadPrefab();
            m_IsGameOver = false;
        }

        public void Update()
        {
            UpdateShowKnife();
        }

        public void Destroy()
        {
            m_KnifePrefab = null;
            m_KnifeTrans = null;
            m_SpawnKnifePosTrans = null;
            m_IsGameOver = false;
        }

        public void OnGameOver() {
            m_IsGameOver = true;
            m_KnifeTrans.gameObject.SetActive(false);
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        void LoadPrefab() {
            m_KnifePrefab = Resources.Load<GameObject>(ResPathDefine.KNIFE_PREFAB_PATH);
            if (m_KnifePrefab == null)
            {
                Debug.LogError(GetType() + "/LoadPrefab()/ prefab  is  null, path = " + ResPathDefine.KNIFE_PREFAB_PATH);
            }
            else {
                m_KnifeTrans = GameObject.Instantiate(m_KnifePrefab,m_SpawnKnifePosTrans).transform;
                m_KnifeTrans.position = Vector3.forward * GameConfig.GAME_OBJECT_Z_VALUE;
                m_KnifeTrans.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 游戏未结束时，鼠标按下更新刀锋
        /// </summary>
        void UpdateShowKnife() {
            if (m_KnifeTrans!=null && m_IsGameOver==false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_KnifeTrans.position = Tools.ScreenPosToWorldPos(m_KnifeTrans, Camera.main, Input.mousePosition);
                    m_KnifeTrans.gameObject.SetActive(true);
                }
                else if (Input.GetMouseButton(0))
                {
                    m_KnifeTrans.position = Tools.ScreenPosToWorldPos(m_KnifeTrans, Camera.main, Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    m_KnifeTrans.gameObject.SetActive(false);
                }
            }
            
        }
    }
}
