using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MGP_008Circus { 

	public class UIManager : IManager,IGameOver
	{
        private Text m_ScoreText;
        private GameObject m_GameOverImageGo;
        private Button m_RestartGameButton;

        private DataModelManager m_DataModelManager;
        public void Init(Transform rootTrans)
        {
            m_ScoreText = rootTrans.Find(GameObjectPathInSceneDefine.UI_SCORE_TEXT_PATH).GetComponent<Text>();
            m_GameOverImageGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_GAME_OVER_IMAGE_PATH).gameObject;
            m_RestartGameButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESTART_GAME_BUTTON_PATH).GetComponent<Button>();

            m_DataModelManager = GameManager.Instance.GetManager<DataModelManager>();
            m_ScoreText.text = m_DataModelManager.Score.Value.ToString();
            m_DataModelManager.Score.OnValueChanged += OnScroeValueChanged;
            m_RestartGameButton.onClick.AddListener(OnRestartButton);

            m_GameOverImageGo.SetActive(false);

        }

        public void Update()
        {

        }

        public void Destroy()
        {
            m_RestartGameButton.onClick.RemoveAllListeners();

            m_ScoreText = null;
            m_GameOverImageGo = null;
            m_RestartGameButton = null;
            m_DataModelManager = null;
        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        public void GameOver()
        {
            m_GameOverImageGo.SetActive(true);

        }

        /// <summary>
        /// 更新分数显示
        /// </summary>
        /// <param name="score"></param>
        private void OnScroeValueChanged(int score)
        {
            m_ScoreText.text = score.ToString();
        }

        /// <summary>
        /// 重新开始按钮事件
        /// </summary>
        private void OnRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
