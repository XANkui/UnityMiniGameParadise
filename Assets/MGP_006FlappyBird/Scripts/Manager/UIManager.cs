using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MGP_006FlappyBird { 

	public class UIManager : IManager
	{
        private Text m_ScoreText;
        private GameObject m_ResumeGameImageGo;
        private GameObject m_GameOverImageGo;
        private Button m_ResumeGameButton;
        private Button m_PauseGameButton;
        private Button m_RestartGameButton;

        private bool m_IsPause;
        public bool IsPause=>m_IsPause;

        private DataModelManager m_DataModelManager;
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_ScoreText = rootTrans.Find(GameObjectPathInSceneDefine.UI_SCORE_TEXT_PATH).GetComponent<Text>();
            m_ResumeGameImageGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESUME_GAME_IMAGE_PATH).gameObject;
            m_GameOverImageGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_GAME_OVER_IMAGE_PATH).gameObject;
            m_ResumeGameButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESUME_GAME_BUTTON_PATH).GetComponent<Button>();
            m_PauseGameButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_PAUSE_GAME_BUTTON_PATH).GetComponent<Button>();
            m_RestartGameButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESTART_GAME_BUTTON_PATH).GetComponent<Button>();

            m_DataModelManager = managers[0] as DataModelManager;

            m_GameOverImageGo.SetActive(false);
            m_ScoreText.text = m_DataModelManager.Score.Value.ToString();
            m_DataModelManager.Score.OnValueChanged += OnScroeValueChanged;
            m_ResumeGameButton.onClick.AddListener(OnResumeGameButton);
            m_PauseGameButton.onClick.AddListener(OnPauseGameButton);
            m_RestartGameButton.onClick.AddListener(OnRestartButton);

            m_ResumeGameImageGo.SetActive(true);
            m_IsPause = true;
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

        public void GameOver()
        {
            m_GameOverImageGo.SetActive(true);

        }

        private void OnScroeValueChanged(int score)
        {
            m_ScoreText.text = score.ToString();
        }

        private void OnPauseGameButton()
        {
            m_IsPause = true;
            m_ResumeGameImageGo.SetActive(true);
        }

        private void OnResumeGameButton()
        {
            m_IsPause = false;
            m_ResumeGameImageGo.SetActive(false);
        }

        private void OnRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }
}
