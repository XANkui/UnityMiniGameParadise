using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MGP_006FlappyBird {

    public class BirdManager : IManager
    {
        private ResLoadManager m_ResLoadManager;
        private DataModelManager m_DataModelManager;
        private AudioManager m_AudioManager;
        private Transform m_SpawnBirdPosTrans;
        private Bird m_Bird;
        private bool m_IsPause;
        private bool m_IsGameOver;
        public bool IsGameOver => m_IsGameOver;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_SpawnBirdPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_BIRD_POS_PATH);
            m_ResLoadManager = managers[0] as ResLoadManager;
            m_DataModelManager = managers[1] as DataModelManager;
            m_AudioManager = managers[2] as AudioManager;

            m_IsPause = false;
            m_IsGameOver = false;

            LoadPrefab();
        }

        public void Update()
        {
            if (m_IsPause == true || m_IsGameOver == true)
            {
                return;
            }

            UpdatePosOperation();
        }

        public void Destroy()
        {
            m_SpawnBirdPosTrans = null;
            m_ResLoadManager = null;
            m_Bird = null;
        }



        public void GameResume() {
            m_IsPause = false;
            m_Bird.Resume();
        }
        public void GamePause() {
			m_IsPause = true;
            m_Bird.Pause();
        }

        public void GameOver()
        {
            m_IsGameOver = true;
            m_Bird.GameOver();
        }

        /// <summary>
        /// 加载实例化鸟
        /// </summary>
        private void LoadPrefab()
        {
            GameObject prefab = m_ResLoadManager.LoadPrefab(ResPathDefine.PREFAB_BIRD_PATH);
            GameObject bird = GameObject.Instantiate(prefab, m_SpawnBirdPosTrans);
            m_Bird =  bird.AddComponent<Bird>();

            m_Bird.Init(OnBirdGroundCollisionEnter, OnBirdScoreCollisionEnter);
        }

        /// <summary>
        /// 监听是否鼠标按下，向上飞
        /// </summary>
        private void UpdatePosOperation()
        {

            if (Input.GetMouseButtonDown(0) == true 
                && EventSystem.current.IsPointerOverGameObject() ==false) // 鼠标点击在 UI 上不触发
            {
                m_AudioManager.PlayAudio(AudioClipSet.Fly);
                m_Bird.Fly();
            }
        }

        /// <summary>
        /// 游戏结束事件
        /// </summary>
        private void OnBirdGroundCollisionEnter() {
            m_AudioManager.PlayAudio(AudioClipSet.Collider);
            m_IsGameOver = true;

        }

        /// <summary>
        /// 游戏加分事件
        /// </summary>
        private void OnBirdScoreCollisionEnter()
        {
            m_AudioManager.PlayAudio(AudioClipSet.Tip);
            m_DataModelManager.Score.Value += GameConfig.PASS_PIPE_GET_SCORE;

        }
    }
}
