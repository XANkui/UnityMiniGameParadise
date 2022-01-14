using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{ 

	public class Player : MonoBehaviour
	{
		PlatformManager m_PlatformManager;
		float m_PressTime = 0;
		Vector3 m_ModelOriPos;
		Vector3 m_ModelOriScale;
		Vector3 m_ModelMinScale;
		float m_ModelMinScaleY;
		bool m_IsCanJump = false;
		bool m_IsFallen = false;
		public bool IsFallen => m_IsFallen;
		bool m_IsInitCollisionEnter = false;

		float m_JumpTime_Length = GameConfig.PLAYER_MODEL_JUMP_TIME_LENGTH;
		float m_JumpTime = 0;

		Vector3 oriPos;
		Vector3 movePos;

		public void Init(PlatformManager platformManager)
		{
			m_PlatformManager = platformManager;

			m_ModelOriScale = transform.localScale;
			m_ModelMinScaleY = m_ModelOriScale.y * GameConfig.PLAYER_MODEL_MIN_SCALE_Y;
			m_ModelMinScale = new Vector3(m_ModelOriScale.x, m_ModelMinScaleY, m_ModelOriScale.z);

		}

		public void Destroy() {
			m_PlatformManager = null;
		}

		/// <summary>
		/// Jump 跳一跳相关鼠标监听事件
		/// </summary>
		public void UpdateJumpOperation()
		{
			if (m_IsFallen == true)
			{
				return;
			}

			JudgeFallen();

			if (m_IsCanJump == false)
			{
				return;
			}

			if (Input.GetMouseButtonDown(0))
			{

				UpdatePlayerRotate();
				m_PressTime = 0;

			}
			if (Input.GetMouseButton(0))
			{

				m_PressTime += Time.deltaTime;
				if (m_PressTime >= GameConfig.MOUSE_PRESSING_TIME_LENGTH)
				{
					m_PressTime = GameConfig.MOUSE_PRESSING_TIME_LENGTH;

				}

				SmallScaleYAnimation();
			}
			if (Input.GetMouseButtonUp(0))
			{

				StartCoroutine(Jump());
				BackOriScale();
				m_IsCanJump = false;
			}

		}


        #region Unity Functions

		/// <summary>
		/// 碰撞事件，在平台才能 Jump
		/// </summary>
		/// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
		{
			m_IsCanJump = true;

			// 第一碰撞平台的基础数据设置
			if (m_IsInitCollisionEnter == false)
			{
				m_IsInitCollisionEnter = true;
				m_ModelOriPos = this.transform.position;

			}

		}

		#endregion

		/// <summary>
		/// 协程，进行跳跃实现
		/// </summary>
		/// <returns></returns>
		IEnumerator Jump()
		{
			float jumpLength = m_PressTime * GameConfig.PLAYER_MODEL_JUMP_LENGTH_SMOOTH_VALUE;
			oriPos = this.transform.localPosition;
			Vector3 nextPos = oriPos + transform.forward * jumpLength;
			movePos = nextPos - oriPos;
			m_JumpTime = m_JumpTime_Length;

			while (true)
			{
				if (m_JumpTime < 0)
				{
					break;
				}
				JumpMoving(jumpLength);
				yield return new WaitForEndOfFrame();
			}
		}
		
		/// <summary>
		/// 根据蓄力结果，生成跳跃轨迹
		/// </summary>
		/// <param name="jumpLength"></param>
		void JumpMoving(float jumpLength)
		{
			m_JumpTime -= Time.deltaTime;

			float deltra = (m_JumpTime_Length - m_JumpTime) / m_JumpTime_Length;
			var x = oriPos.x + movePos.x * deltra;
			var y = oriPos.y + YParabola(jumpLength * deltra, jumpLength / 2, GameConfig.PLAYER_MODEL_JUMP_TOP_DISTANCE);
			var z = oriPos.z + movePos.z * deltra;
			this.transform.localPosition = new Vector3(x, y, z);

		}

		/// <summary>
		/// 计算抛物线的 Y 值
		/// </summary>
		/// <param name="x"></param>
		/// <param name="k"></param>
		/// <param name="top">抛物线最高的点高度</param>
		/// <returns></returns>
		float YParabola(float x, float k, float top)
		{
			if (k == 0)
			{
				k = 1;
			}

			return top - (top * (x - k) * (x - k) / (k * k));
		}

		/// <summary>
		/// 根据 Platform 位置，Player 进行转向
		/// </summary>
		void UpdatePlayerRotate()
		{
			float angle = 0;

			switch (m_PlatformManager.CurDir)
			{
				case Dir.Right:
					angle = Vector3.Angle(transform.forward, Vector3.right);
					this.transform.Rotate(Vector3.up, angle);
					break;
				case Dir.Forword:
					angle = Vector3.Angle(transform.forward, Vector3.forward);
					this.transform.Rotate(Vector3.up, -angle);
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// 蓄力 Player 的动画
		/// </summary>
		void SmallScaleYAnimation()
		{
			if (this.transform.localScale.y <= m_ModelMinScaleY)
			{
				return;
			}

			this.transform.localScale = Vector3.Lerp(this.transform.localScale, 
				m_ModelMinScale, 
				Time.deltaTime * GameConfig.PLAYER_MODEL_SCALE_Y_SAMLL_SPEED);
		}

		/// <summary>
		/// 回到蓄力动画前
		/// </summary>
		void BackOriScale()
		{
			this.transform.localScale = m_ModelOriScale;
		}

		/// <summary>
		/// 判断 Player 是否坠落 Platform 
		/// </summary>
		void JudgeFallen()
		{
			if (this.transform.position.y < m_ModelOriPos.y - GameConfig.PLAYER_MODEL_FALL_DISTANCE_FROM_PLATFORM)
			{
				m_IsFallen = true;
			}
		}

	}
}
