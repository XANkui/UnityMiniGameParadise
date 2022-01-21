using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MGP_005CutFruit {

    public class UIManager : IManager
    {
        private Text m_LifeText;
        private Text m_ScoreText;
        private GameObject m_GameOverImageGo;
        private Button m_RestartGameButton;

        private DataModelManager m_DataModelManager;
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_LifeText = rootTrans.Find(GameObjectPathInSceneDefine.UI_LIFE_TEXT_PATH).GetComponent<Text>();
            m_ScoreText = rootTrans.Find(GameObjectPathInSceneDefine.UI_SCORE_TEXT_PATH).GetComponent<Text>();
            m_GameOverImageGo = rootTrans.Find(GameObjectPathInSceneDefine.UI_GAME_OVER_IMAGE_PATH).gameObject;
            m_RestartGameButton = rootTrans.Find(GameObjectPathInSceneDefine.UI_RESTART_GAME_BUTTON_PATH).GetComponent<Button>();

            m_DataModelManager = managers[0] as DataModelManager;

            m_GameOverImageGo.SetActive(false);
            m_LifeText.text = m_DataModelManager.Life.Value.ToString();
            m_ScoreText.text = m_DataModelManager.Score.Value.ToString();
            m_DataModelManager.Life.OnValueChanged += OnLifeValueChanged;
            m_DataModelManager.Score.OnValueChanged += OnScroeValueChanged;
            m_RestartGameButton.onClick.AddListener(OnRestartButton);
        }

        public void Update()
        {

        }

        public void Destroy()
        {
            m_RestartGameButton.onClick.RemoveAllListeners();

            m_LifeText = null;
            m_ScoreText = null;
            m_GameOverImageGo = null;
            m_RestartGameButton = null;
            m_DataModelManager = null;
        }

        public void OnGameOver()
        {
            m_GameOverImageGo.SetActive(true);
        }

        private void OnLifeValueChanged(int life)
        {
            m_LifeText.text = life.ToString();
        }

        private void OnScroeValueChanged(int score)
        {
            m_ScoreText.text = score.ToString();
        }

        private void OnRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
