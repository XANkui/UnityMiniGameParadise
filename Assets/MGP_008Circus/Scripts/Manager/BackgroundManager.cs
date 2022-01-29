using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class BackgroundManager : IManager,IGameOver
	{
        private ResLoadServer m_ResLoadServer;
        private Transform m_SpawnBackgroundPosTrans;

        private Material m_BgMat;
        private Vector2 m_BgMainTexVect;
        private float m_MainTexRunSpeed;
        private bool m_IsRun;
        private const string MAIN_TEX = "_MainTex";

        public void Init(Transform rootTrans)
        {
            m_SpawnBackgroundPosTrans = rootTrans.Find(GameObjectPathInSceneDefine.SPAWN_BACKGROUND_POS_PATH);

            m_ResLoadServer = GameManager.Instance.GetServer<ResLoadServer>();
            m_MainTexRunSpeed = GameConfig.BACKGROUND_MOVE_SPEED;

            LoadPrefab();

            Run();
        }

        public void Update()
        {
            if (m_IsRun==false)
            {
                return;
            }

            UpdatePosOperation();
        }

        public void GameOver()
        {
            Stop();
        }

        public void Destroy()
        {
            m_ResLoadServer = null;
            m_SpawnBackgroundPosTrans = null;
            m_BgMat = null;
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        private void LoadPrefab()
        {
            GameObject prefab = m_ResLoadServer.LoadPrefab(ResPathDefine.PREFAB_BACKGROUND_PATH);
            GameObject bg = GameObject.Instantiate(prefab, m_SpawnBackgroundPosTrans);
            m_BgMat = bg.GetComponent<SpriteRenderer>().material;
            m_BgMainTexVect = m_BgMat.GetTextureOffset(MAIN_TEX);
        }

        /// <summary>
		/// 更新 Sprite Shader 图片的 Offset 位置
		/// 从而实现无限循环
		/// </summary>
		private void UpdatePosOperation()
        {
            m_BgMat.SetTextureOffset(MAIN_TEX, new Vector2(m_BgMainTexVect.x += m_MainTexRunSpeed * Time.deltaTime, m_BgMainTexVect.y));

        }

        void Run()
        {
            m_IsRun = true;
        }

        void Stop()
        {
            m_IsRun = false;
        }

        
    }
}
