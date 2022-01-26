
using UnityEngine;

namespace MGP_007CarRacing2D { 

	public class DrivingDistanceScoreManager : IManager, IGamePause, IGameResume, IGameOver
    {
        private DataModelManager m_DataModelManager;

        private bool m_IsAddScroe = false;
        private float m_Timer = 0;
        public void Init(Transform rootTrans, params object[] objs)
        {
            m_DataModelManager = objs[0] as DataModelManager;

            m_IsAddScroe = false;
            m_Timer = 0;
        }

        public void Update()
        {
            if (m_IsAddScroe == false)
            {
                return;
            }

            UpdateDistanceScore();
        }

        public void Destroy()
        {
            m_DataModelManager = null;
        }

        public void GamePause()
        {
            m_IsAddScroe = false;
        }

        public void GameResume()
        {
            m_IsAddScroe = true;
        }

        public void GameOver()
        {
            m_IsAddScroe = false;
        }

        /// <summary>
        /// 行程加分
        /// </summary>
        void UpdateDistanceScore()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer>= (1.0f/GameConfig.ROAD_MOVEVELOCITY_Y))
            {
                m_Timer -= (1.0f / GameConfig.ROAD_MOVEVELOCITY_Y);
                m_DataModelManager.Score.Value += GameConfig.DRIVING_DISTANCE_SCORE;

            }
        }

    }
}
