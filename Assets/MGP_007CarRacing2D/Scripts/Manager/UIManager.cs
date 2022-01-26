using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MGP_007CarRacing2D { 

	public class UIManager : IManager, IGamePause, IGameResume, IGameOver
    {
        private GameObject m_PlayPanelGo;
        private GameObject m_GamePanelGo;
        private GameObject m_PausePanelGo;
        private GameObject m_GameOverPanelGo;

        private Button m_PlayImageButton;
        private Button m_PauseImageButton;
        private Text m_ScoreText;
        private Button m_HomeImageButton;
        private Button m_ResumeImageButton;
        private Button m_RestartImageButton;

        private Action m_OnGameResume;
        private Action m_OnGamePause;

        private AudioServer m_AudioServer;
        private DataModelManager m_DataModelManager;

        public void Init(Transform rootTrans, params object[] objs)
        {
            m_PlayPanelGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_PLAY_PANEL_PATH).gameObject;
            m_GamePanelGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_GAME_PANEL_PATH).gameObject;
            m_PausePanelGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_PAUSE_PANEL_PATH).gameObject;
            m_GameOverPanelGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_GAME_OVER_PANEL_PATH).gameObject;

            m_PlayImageButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_PLAY_IMAGE_BUTTON_PATH).GetComponent<Button>();
            m_PauseImageButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_PAUSE_IMAGE_BUTTON_PATH).GetComponent<Button>();
            m_ScoreText = rootTrans.Find(GameObjectPathInSceneDefine.UI_SCORE_TEXT_PATH).GetComponent<Text>();
            m_HomeImageButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_HOME_IMAGE_BUTTON_PATH).GetComponent<Button>();
            m_ResumeImageButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESUME_IMAGE_BUTTON_PATH).GetComponent<Button>();
            m_RestartImageButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESTART_IMAGE_BUTTON_PATH).GetComponent<Button>();

            m_AudioServer = objs[0] as AudioServer;
            m_DataModelManager = objs[1] as DataModelManager;

            m_PlayImageButton.onClick.AddListener(OnPlayButton);
            m_PauseImageButton.onClick.AddListener(OnPauseButton);
            m_HomeImageButton.onClick.AddListener(OnRestartButton);
            m_ResumeImageButton.onClick.AddListener(OnPlayButton);
            m_RestartImageButton.onClick.AddListener(OnRestartButton);

            m_DataModelManager.Score.OnValueChanged += UpdateScoreText;

            ShowPanel(m_PlayPanelGo);
        }

        public void Update()
        {
            
        }

        public void Destroy()
        {
            m_PlayPanelGo = null;
            m_GamePanelGo = null;
            m_PausePanelGo = null;
            m_GameOverPanelGo = null;

            m_PlayImageButton .onClick.RemoveAllListeners();
            m_PauseImageButton.onClick.RemoveAllListeners();
            m_HomeImageButton.onClick.RemoveAllListeners();
            m_ResumeImageButton.onClick.RemoveAllListeners();
            m_RestartImageButton.onClick.RemoveAllListeners();

            m_PlayImageButton = null;
            m_PauseImageButton = null;
            m_ScoreText = null;
            m_HomeImageButton = null;
            m_ResumeImageButton = null;
            m_RestartImageButton = null;
        }

        public void SetOnGameResume(Action onGameResume) {
            m_OnGameResume = onGameResume;
        }
        public void SetOnGamePause(Action onGamePause) {
            m_OnGamePause = onGamePause;
        }

        public void GamePause()
        {
            ShowPanel(m_PausePanelGo);
        }

        public void GameResume()
        {
            ShowPanel(m_GamePanelGo);
        }

        public void GameOver()
        {
            ShowPanel(m_GameOverPanelGo);
        }

        void ShowPanel(GameObject panel) {
            HideAllPanel();
            panel.SetActive(true);
        }

        void HideAllPanel() {
            m_PlayPanelGo.SetActive(false);
            m_GamePanelGo.SetActive(false);
            m_PausePanelGo.SetActive(false);
            m_GameOverPanelGo.SetActive(false);
        }

        private void OnRestartButton()
        {
            m_AudioServer.PlayAudio(AudioClipSet.ButtonClick);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnPlayButton() {
            m_AudioServer.PlayAudio(AudioClipSet.ButtonClick);
            if (m_OnGameResume != null)
            {
                m_OnGameResume.Invoke();
            }
        }

        private void OnPauseButton()
        {
            m_AudioServer.PlayAudio(AudioClipSet.ButtonClick);
            if (m_OnGamePause != null)
            {
                m_OnGamePause.Invoke();
            }
        }
        private void UpdateScoreText(int score) {
            m_ScoreText.text = score.ToString();
        }
    }
}
