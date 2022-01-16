using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MGP_004CompoundBigWatermelon
{ 

	public class UIManager : IManager
	{
        private Text m_ScoreText;
        private GameObject m_GameOverImageGo;
        private Button m_RestartGameButton;

        private ScoreManager m_ScoreManager;
        public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
            m_ScoreText = uiTrans.Find(GameObjectPathInSceneDefine.UI_SCORE_TEXT_PATH).GetComponent<Text>();
            m_GameOverImageGo = uiTrans.Find(GameObjectPathInSceneDefine.UI_GAME_OVER_IMAGE_PATH).gameObject;
            m_RestartGameButton = uiTrans.Find(GameObjectPathInSceneDefine.UI_RESTART_GAME_BUTTON_PATH).GetComponent<Button>();

            m_ScoreManager = manager[0] as ScoreManager;

            m_GameOverImageGo.SetActive(false);
            m_ScoreText.text = m_ScoreManager.Score.ToString();
            m_ScoreManager.OnValueChanged += OnScroeValueChanged;
            m_RestartGameButton.onClick.AddListener(OnRestartButton);
        }

        public void Update()
        {
            
        }

        public void Destroy()
        {
            m_ScoreManager.OnValueChanged -= OnScroeValueChanged;
            m_RestartGameButton.onClick.RemoveAllListeners();

            m_ScoreText = null;
            m_GameOverImageGo = null;
            m_RestartGameButton = null;
            m_ScoreManager = null;
        }

        public void OnGameOver() {
            m_GameOverImageGo.SetActive(true);
        }

        private void OnScroeValueChanged(int score) {
            m_ScoreText.text = score.ToString();
        }

        private void OnRestartButton() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
