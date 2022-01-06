using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_002FlyPin { 

	/// <summary>
	/// Pins 管理类
	/// </summary>
	public class PinsManager 
	{
		// Pin 预制体
		private GameObject m_PinPrefab;
		// Pin 生成位置
		private Vector3 m_PinSpawnPos;
		// Pin 准备位置
		private Vector3 m_PinReadyPos;
		// Pin 飞向目标位置
		private Vector3 m_PinFlyTargetPos;
		// Pin 飞向目标插入间距
		private float m_PinFlyTargetPosDistance;
		// Pin 移动速度
		private float m_PinMoveSpeed;
		// Pin 飞向目标实体
		private Transform m_PinTargetParentTrans;
		// 准备的 Pin 
		private Pin m_ReadyPin;
		// 当前可飞行的 Pin 
		private Pin m_CurPin;
		public Pin CurPin { get => m_CurPin; set => m_CurPin = value; }

		// Pin 集合
		private List<GameObject> m_PinsList = new List<GameObject>();

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pinSpawnPos"></param>
		/// <param name="pinReadyPos"></param>
		/// <param name="flyTargetPos"></param>
		/// <param name="flyTargetPosDistance"></param>
		/// <param name="pinMoveSpeed"></param>
		/// <param name="pinTargetParentTrans"></param>
		public PinsManager(Vector3 pinSpawnPos, Vector3 pinReadyPos,Vector3 flyTargetPos,
			float flyTargetPosDistance, float pinMoveSpeed,Transform pinTargetParentTrans) {

			// 获取预制体
			m_PinPrefab = Resources.Load<GameObject>(ConstStr.RESOURCES_PREFABS_PIN_PATH);

			// 参数赋值
			m_PinSpawnPos = pinSpawnPos;
			m_PinReadyPos = pinReadyPos;
			m_PinFlyTargetPos = flyTargetPos;
			m_PinFlyTargetPosDistance = flyTargetPosDistance;
			m_PinMoveSpeed = pinMoveSpeed;
			m_PinTargetParentTrans = pinTargetParentTrans;
		}

		/// <summary>
		/// 生成准备的 Pin
		/// </summary>
		public void SpawnPin() {

			// 生成准备的 Pin
			if (m_PinPrefab!=null && m_ReadyPin == null)
            {
				GameObject pinGo = GameObject.Instantiate(m_PinPrefab);
				// 设置生成位置
				pinGo.transform.position = m_PinSpawnPos;
				// 添加 Pin 脚本
				m_ReadyPin = pinGo.AddComponent<Pin>();
				// Pin 脚本 初始化
				m_ReadyPin.Init(this,m_PinReadyPos,m_PinFlyTargetPos,
					m_PinFlyTargetPosDistance,m_PinMoveSpeed,m_PinTargetParentTrans);

				// 添加到集合中
				m_PinsList.Add(pinGo);

			}
		}

		/// <summary>
		/// 让当前 Pin 飞向目标位置
		/// 返回是否有可飞行的 Pin 
		/// </summary>
		/// <returns>true : 有可飞行 Pin </returns>
		public bool FlyPin() {
            if (CurPin!=null)
            {
				m_ReadyPin = null;

				if (CurPin.CurPinState==PinState.ReadyOK)
                {
					CurPin.CurPinState = PinState.Fly;

				}
				return true;
			}

			return false;
		}

		/// <summary>
		/// 销毁清空 Pins 集合
		/// </summary>
		public void DestroyAllPins() {
            for (int i=m_PinsList.Count-1; i >= 0 ; i--)
            {
				GameObject.Destroy(m_PinsList[i]);
            }

			m_PinsList.Clear();
		}

	}
}
