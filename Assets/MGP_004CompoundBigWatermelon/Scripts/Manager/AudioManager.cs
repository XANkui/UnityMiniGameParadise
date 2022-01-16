using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class AudioManager : IManager
	{

        private AudioClip m_SpawnSound;
        private AudioClip m_BombSound;

        private AudioSource m_AudioSource;
       

        public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
            m_SpawnSound = Resources.Load<AudioClip>(ResPathDefine.AUDIO_SPAWN_PATH);
            m_BombSound = Resources.Load<AudioClip>(ResPathDefine.AUDIO_BOMB_PATH);

            GameObject audiosSourceGO = worldTrans.Find(GameObjectPathInSceneDefine.AUDIO_SOURCE_PATH).gameObject;
            if (audiosSourceGO == null)
            {
                Debug.LogError(GetType() + "/AudioSource()/ audiosSourceGO is null , path = " + GameObjectPathInSceneDefine.AUDIO_SOURCE_PATH);
            }
            else
            {
                m_AudioSource = audiosSourceGO.AddComponent<AudioSource>();

            }
        }

        public void Update()
        {
        }

        public void Destroy()
        {
            m_SpawnSound = null;
            m_BombSound = null;
            m_AudioSource = null;
        }


        public void PlaySpawnSound()
        {
            m_AudioSource.PlayOneShot(m_SpawnSound);
        }

        public void PlayBombSound()
        {
            m_AudioSource.PlayOneShot(m_BombSound);
        }
    }
}
