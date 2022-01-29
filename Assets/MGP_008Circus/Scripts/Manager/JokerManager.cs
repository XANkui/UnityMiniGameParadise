using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MGP_008Circus { 

	public class JokerManager : IManager,IGameOver
	{
        private ResLoadServer m_ResLoadServer;
        private DataModelManager m_DataModelManager;
        private AudioServer m_AudioServer;
        private Transform m_SpawnJokerPosTrans;
        private Vector3 m_SpawnJokerPos;
        private Joker m_Joker;
        private bool m_IsGameOver;
        private bool m_IsJump;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans)
        {
            m_SpawnJokerPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_JOKER_POS_PATH);
            m_ResLoadServer = GameManager.Instance.GetServer<ResLoadServer>();
            m_AudioServer = GameManager.Instance.GetServer<AudioServer>();
            m_DataModelManager = GameManager.Instance.GetManager<DataModelManager>();

            m_IsGameOver = false;
            m_SpawnJokerPos = new Vector3(-1.2f,0,0);
            LoadPrefab();

           
        }

        public void Update()
        {
            if ( m_IsGameOver == true)
            {
                return;
            }

            UpdatePosOperation();
        }

        public void Destroy()
        {
            m_SpawnJokerPosTrans = null;
            m_ResLoadServer = null;
            m_DataModelManager = null;
            m_AudioServer = null;
            m_Joker = null;
        }




        public void GameOver()
        {
            m_IsGameOver = true;
            m_Joker.GameOver();

        }

        /// <summary>
        /// 加载实例化 Joker 
        /// </summary>
        private void LoadPrefab()
        {
            GameObject prefab = m_ResLoadServer.LoadPrefab(ResPathDefine.PREFAB_JOKER_PATH);
            GameObject joker = GameObject.Instantiate(prefab, m_SpawnJokerPosTrans);
            joker.transform.position = m_SpawnJokerPos;
            m_Joker = joker.AddComponent<Joker>();
            m_Joker.Init(OnJokerColliderCollisionEnter, OnBirdScoreCollisionEnter);
            m_Joker.Run();
        }

        /// <summary>
        /// 监听是否鼠标按下，向上飞
        /// </summary>
        private void UpdatePosOperation()
        {

            if (Input.GetMouseButtonDown(0) == true
                && EventSystem.current.IsPointerOverGameObject() == false) // 鼠标点击在 UI 上不触发（注意场景中要有 EventSystem组件）
            {
                if (m_IsJump==false)
                {
                    m_AudioServer.PlayAudio(AudioClipSet.Circus_Jump);
                    m_Joker.Jump();
                    m_IsJump = true;
                }
                
            }
        }

        /// <summary>
        /// 游戏碰撞火圈和地面事件
        /// </summary>
        private void OnJokerColliderCollisionEnter(Collision2D collision)
        {

            if (collision.collider.CompareTag(Tag.FIRE_CIRCLE))
            {
                GameManager.Instance.GameOver();

                m_AudioServer.PlayAudio(AudioClipSet.Circus_GameOver);
            }

            if (collision.collider.CompareTag(Tag.GROUND))
            {
                m_IsJump = false;
                m_Joker.Run();
            }
            

        }

        /// <summary>
        /// 游戏加分事件
        /// </summary>
        private void OnBirdScoreCollisionEnter()
        {
            m_DataModelManager.Score.Value += GameConfig.JOKER_PASS_FIRECIRCLE_GET_SCORE;

        }
    }
}
