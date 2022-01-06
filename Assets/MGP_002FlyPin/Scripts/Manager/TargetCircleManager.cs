using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_002FlyPin
{ 
	/// <summary>
	/// Pin 飞向目标圆管理类
	/// </summary>
	public class TargetCircleManager 
	{
		// 实体Transform
		Transform m_TargetCircleTrans;
		// 转动速度
		float m_Speed;
		// 是否开始转动
		bool m_IsRotated = false;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="target">目标实体</param>
		/// <param name="rotSpeed">旋转速度</param>
		public TargetCircleManager(Transform target, float rotSpeed) {
			m_TargetCircleTrans = target;
			m_Speed = rotSpeed;
			m_IsRotated = false;
		}

		/// <summary>
		/// 更新自身旋转
		/// </summary>
		public void UpdateRotateSelf() {
            if (m_TargetCircleTrans != null && m_IsRotated==true)
            {
				m_TargetCircleTrans.Rotate(Vector3.forward,Time.deltaTime * m_Speed * -1);  // -1 是让其反向旋转
            }
		}

		/// <summary>
		/// 开始旋转
		/// </summary>
		public void StartRotateSelf()
		{
			m_IsRotated = true;
		}

		/// <summary>
		/// 停止旋转
		/// </summary>
		public void StopRotateSelf() {
			m_IsRotated = false;
		}
	}
}
