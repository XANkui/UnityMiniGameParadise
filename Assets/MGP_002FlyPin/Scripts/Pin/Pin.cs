using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_002FlyPin {

	
	/// <summary>
	/// Pin 类
	/// </summary>
	public class Pin :MonoBehaviour
	{
		// Pin 状态
		private PinState m_CurPinState=PinState.Idle;
		public PinState CurPinState {
			get => m_CurPinState;
			set => m_CurPinState = value;
		}
		// Pins 管理类
		private PinsManager m_PinsManager;

		// Pin 准备好的位置
		private Vector3 m_PinReadyPos;
		// Pin 飞行的目标位置
		private Vector3 m_PinFlyTargetPos;
		// Pin 插入目标位置的距离间隔
		private float m_PinFlyTargetPosDistance;
		// Pin 的移动速度
		private float m_PinMoveSpeed;
		// Pin 飞到目标位置的 Transfrorm
		private Transform m_PinTargetParentTrans;
		// PinHead
		private PinHead m_PinHead;

		/// <summary>
		/// 初始化函数，得到相关位置和 PinHead
		/// </summary>
		/// <param name="pinsManager"></param>
		/// <param name="pinReadyPos"></param>
		/// <param name="flyTargetPos"></param>
		/// <param name="flyTargetPosDistance"></param>
		/// <param name="pinMoveSpeed"></param>
		/// <param name="pinTargetParentTrans"></param>
		public void Init(PinsManager pinsManager,  Vector3 pinReadyPos, Vector3 flyTargetPos, 
			float flyTargetPosDistance, float pinMoveSpeed, Transform pinTargetParentTrans) {
			m_PinsManager = pinsManager;
			m_PinReadyPos = pinReadyPos;
			m_PinFlyTargetPos = flyTargetPos;
			m_PinFlyTargetPosDistance = flyTargetPosDistance;
			m_PinMoveSpeed = pinMoveSpeed;
			m_PinTargetParentTrans = pinTargetParentTrans;
			// PinHead 子物体添加PinHead脚本
			m_PinHead = transform.Find(ConstStr.PIN_HEAD_NAME).gameObject.AddComponent<PinHead>() ;
			// 设置状态为准备状态
			m_CurPinState = PinState.Readying;
		}

        private void Update()
        {
			UpdatePinState();

		}

		/// <summary>
		/// 状态监听
		/// </summary>
        void UpdatePinState() {
            switch (CurPinState)
            {
                case PinState.Idle:
                    break;
                case PinState.Readying:
					Readying();

					break;
				case PinState.ReadyOK:
					m_PinsManager.CurPin = this;
					break;
				case PinState.Fly:
					Fly();
					m_PinsManager.CurPin = null;

					break;
                default:
                    break;
            }
        }
		
		/// <summary>
		/// 正在准备的状态函数
		/// </summary>
		void Readying() {

			// 移动到准备位置
			transform.position = Vector3.Lerp(transform.position, m_PinReadyPos, Time.deltaTime * m_PinMoveSpeed);
			// 判断是否到达准备位置
            if (Vector3.Distance(transform.position, m_PinReadyPos)<=0.1f)
            {
				transform.position = m_PinReadyPos;
				// 到达后切换状态
				CurPinState = PinState.ReadyOK;
			}
		}


		/// <summary>
		/// 飞行目标位置状态
		/// </summary>
		void Fly()
		{
			// 移动到目标位置
			transform.position = Vector3.Lerp(transform.position, m_PinFlyTargetPos, Time.deltaTime * m_PinMoveSpeed);
			if (Vector3.Distance(transform.position, m_PinFlyTargetPos) <= m_PinFlyTargetPosDistance)
			{
				// 到达后切换状态，并且置于 飞到的目标下，使之随目标一起转动
				CurPinState = PinState.Idle;
				this.transform.SetParent(m_PinTargetParentTrans);
			}
		}
	}
}
