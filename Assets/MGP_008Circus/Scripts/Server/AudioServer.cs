using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class AudioServer : IServer
	{
        private ResLoadServer m_ResLoadServer;
        private Transform m_AudioSourceTrans;
        private AudioSource m_AudioSource;
        private Dictionary<AudioClipSet, AudioClip> m_AudioClipDict;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="managers"></param>
        public void Init(Transform rootTrans)
        {
            m_AudioSourceTrans = rootTrans.Find(GameObjectPathInSceneDefine.AUDIO_SOURCE_TRANS_PATH);
            m_ResLoadServer = GameManager.Instance.GetServer<ResLoadServer>();
            m_AudioClipDict = new Dictionary<AudioClipSet, AudioClip>();

            m_AudioSource = m_AudioSourceTrans.gameObject.AddComponent<AudioSource>();

            Load();
        }

        public void Destroy()
        {
            if (m_AudioSource!=null)
            {
                m_AudioSource.Stop();
                GameObject.Destroy(m_AudioSource);
                m_AudioSource = null;

            }
            m_AudioSourceTrans = null;
            m_ResLoadServer = null;
            m_AudioClipDict.Clear();
            m_AudioClipDict = null;

        }

        /// <summary>
        /// 播放指定背景音乐
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayBG(AudioClipSet audioName,bool isLoop=true)
        {
            if (m_AudioClipDict.ContainsKey(audioName) == true)
            { 
                m_AudioSource.Stop();
                m_AudioSource.clip = m_AudioClipDict[audioName];
                m_AudioSource.loop = isLoop;
                m_AudioSource.Play();
            }
            else
            {
                Debug.LogError(GetType() + "/PlayAudio()/ audio clip is null,audioName = " + audioName);
            }
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBG() {
            m_AudioSource.Stop();
        }

        /// <summary>
        /// 播放指定音频
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayAudio(AudioClipSet audioName)
        {
            if (m_AudioClipDict.ContainsKey(audioName) == true)
            {
                m_AudioSource.PlayOneShot(m_AudioClipDict[audioName]);
            }
            else
            {
                Debug.LogError(GetType() + "/PlayAudio()/ audio clip is null,audioName = " + audioName);
            }
        }

        /// <summary>
        /// 加载音频
        /// </summary>
        private void Load()
        {
            for (AudioClipSet clipPath = AudioClipSet.Circus_BG; clipPath < AudioClipSet.SUM_COUNT; clipPath++)
            {
                AudioClip audioClip = m_ResLoadServer.LoadAudioClip(ResPathDefine.AUDIO_CLIP_BASE_PATH + clipPath.ToString());
                m_AudioClipDict.Add(clipPath, audioClip);
            }
        }
    }
}
