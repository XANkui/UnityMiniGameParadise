using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird { 

	public class AudioManager : IManager
	{
        private ResLoadManager m_ResLoadManager;
        private Transform m_AudioSourceTrans;
        private AudioSource m_AudioSource;
        private Dictionary<AudioClipSet, AudioClip> m_AudioClipDict;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans, params object[] managers)
        {
            m_AudioSourceTrans = rootTrans.Find(GameObjectPathInSceneDefine.AUDIO_SOURCE_TRANS_PATH);
            m_ResLoadManager = managers[0] as ResLoadManager;
            m_AudioClipDict = new Dictionary<AudioClipSet, AudioClip>();

            m_AudioSource = m_AudioSourceTrans.gameObject.AddComponent<AudioSource>();

            Load();
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            m_AudioClipDict.Clear();
            m_AudioClipDict=null;
        }

        public void GameOver()
        {
        }

        /// <summary>
        /// 播放指定音频
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayAudio(AudioClipSet audioName) {
            if (m_AudioClipDict.ContainsKey(audioName) == true)
            {
                m_AudioSource.PlayOneShot(m_AudioClipDict[audioName]);
            }
            else {
                Debug.LogError(GetType()+ "/PlayAudio()/ audio clip is null,audioName = "+ audioName);
            }
        }
        
        /// <summary>
        /// 加载音频
        /// </summary>
        private void Load() {
            for (AudioClipSet clipPath = AudioClipSet.Collider; clipPath < AudioClipSet.SUM_COUNT; clipPath++)
            {
                AudioClip audioClip = m_ResLoadManager.LoadAudioClip(ResPathDefine.AUDIO_CLIP_BASE_PATH+ clipPath.ToString());
                m_AudioClipDict.Add(clipPath, audioClip);
            }
        }
    }
}
