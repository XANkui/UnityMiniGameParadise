using System;
using UnityEngine;

namespace MGP_007CarRacing2D { 

	public class PCCarManager : IManager, IGamePause, IGameResume, IGameOver
    {
        private ResLoadServer m_ResLoadServer;
        private AudioServer m_AudioServer;
        private DataModelManager m_DataModelManager;
        private ObjectPoolManager m_ObjectPoolManager;
   
        private Transform m_SpawnPCCarPos;
        private PCCar m_PCCar;
        private bool m_IsCanMove;

        private Action m_PCCarColliderNPCCarAction;

        public void Init(Transform rootTrans, params object[] objs)
        {
            m_SpawnPCCarPos = rootTrans.Find("SpawnPCCarPos");
            m_ResLoadServer = objs[0] as ResLoadServer;
            m_ObjectPoolManager = objs[1] as ObjectPoolManager;
            m_AudioServer = objs[2] as AudioServer;
            m_DataModelManager = objs[3] as DataModelManager;

            LoadPrefab();

        }

        public void Update()
        {
            if (m_IsCanMove==false)
            {
                return;
            }

            m_PCCar?.UpdateChangeLaneRotation();
        }

        public void GamePause()
        {
            m_IsCanMove = false;
            m_PCCar.Pause();
        }

        public void GameResume()
        {
            m_IsCanMove = true;
            m_PCCar.Resume();
        }

        public void GameOver()
        {
            m_IsCanMove = false;
            m_PCCar.GameOver();
        }

        public void Destroy()
        {
            m_SpawnPCCarPos = null;
            m_ResLoadServer = null;
            m_PCCar = null;
            m_PCCarColliderNPCCarAction = null;
        }

        public void SetPCCarColliderNPCCarAction(Action pcCarColliderNPCCarAction) {
            m_PCCarColliderNPCCarAction = pcCarColliderNPCCarAction;
        }

        /// <summary>
        /// 加载实例化PCCar
        /// </summary>
        private void LoadPrefab()
        {
            GameObject prefab = m_ResLoadServer.LoadPrefab(ResPathDefine.PREFAB_PC_CAR_PATH);
            GameObject car = GameObject.Instantiate(prefab, m_SpawnPCCarPos);
            car.transform.position = Vector3.right * UnityEngine.Random.Range(GameConfig.CAR_LEFT_OUTSIDE_LIMIT,GameConfig.CAR_RIGHT_OUTSIDE_LIMIT);
            m_PCCar = car.AddComponent<PCCar>();

            m_PCCar.Init(OnNPCCarCollisionEnter, OnCoinCollisionEnter);
        }

        private void OnNPCCarCollisionEnter()
        {
            m_AudioServer.PlayAudio(AudioClipSet.CarCrash);

            if (m_PCCarColliderNPCCarAction!=null)
            {
                m_PCCarColliderNPCCarAction.Invoke();
            }

        }

        /// <summary>
        /// 游戏加分事件
        /// </summary>
        /// <param name="coinClone">coin 克隆体</param>
        private void OnCoinCollisionEnter(GameObject coinClone)
        {
            m_AudioServer.PlayAudio(AudioClipSet.Bonus);

            m_DataModelManager.Score.Value += GameConfig.ENTER_COIN_SCORE;

            m_ObjectPoolManager.ReleaseObject(coinClone);

        }
    }
}
