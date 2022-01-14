using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{ 
	/// <summary>
	/// 游戏管理类，管理各个Manager
	/// </summary>
	public class CameraManager 
	{
		private PlatformManager m_PlatformManager;
		private Transform m_CameraTrans;
		Vector3 m_OffsetPos = Vector3.zero;

		public void Init(Transform cameraTrans, PlatformManager platformManager)
		{
			m_CameraTrans = cameraTrans;
			m_PlatformManager = platformManager;
			SetOffSet();
		}

		public void Update()
		{
			SetCameraTransPos();
		}

		public void Destroy() {
			m_OffsetPos = Vector3.zero;
			m_CameraTrans = null;
			m_PlatformManager = null;
		}

		/// <summary>
		/// Camera 动画移动到指定 Platform 
		/// </summary>
		void SetCameraTransPos()
		{
			if (m_PlatformManager.CurPlatformCube != null)
			{
				m_CameraTrans.position = Vector3.Lerp(m_CameraTrans.position, 
					m_PlatformManager.CurPlatformCube.transform.position - m_OffsetPos, 
					Time.deltaTime * GameConfig.CAMERA_FOLLOW_PLAYER_SPEED);

			}
		}

		/// <summary>
		/// Camera 和 Platform 的位置差值
		/// </summary>
		void SetOffSet()
		{
			m_OffsetPos = m_PlatformManager.CurPlatformCube.transform.position - m_CameraTrans.position;
		}
	}
}
