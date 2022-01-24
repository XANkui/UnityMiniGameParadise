using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird
{
	/// <summary>
	/// 天空背景
	/// 移动是 Rigidbody2D.velocity 
	/// </summary>
	public class SkyTile : MonoBehaviour
	{

		private float m_TargetPosX;
		private Vector2 m_Velocity;
		private bool m_IsPause;

		private Rigidbody2D m_Rigidbody2D;
		public Rigidbody2D Rigidbody2D
        {
			get {
                if (m_Rigidbody2D ==null)
                {
					m_Rigidbody2D = GetComponent<Rigidbody2D>();

				}

				return m_Rigidbody2D;
			}
		}

		// Update is called once per frame
		void Update()
		{
            if (m_IsPause==true)
            {
				return;
            }

			UpdatePosOperation();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="index">第几块天空，从 0 开始</param>
		public void Init(int index) {
			m_TargetPosX = -1 * GameConfig.BACKGROUND_SPRITE_INTERVAL_X;
			m_Velocity = Vector2.left * GameConfig.BACKGROUND_MOVE_LEFT_X;

			Vector3 curPos = transform.position;
			transform.position = new Vector3(curPos.x+(index)* GameConfig.BACKGROUND_SPRITE_INTERVAL_X,
				curPos.y, curPos.z);

			m_IsPause = false;
			Move();
		}

		public void Resume() {
			m_IsPause = false;
			Move();
		}

		public void Pause()
		{
			m_IsPause = true;
			Rigidbody2D.velocity = Vector2.zero;
		}
		public void GaomeOver()
		{
			Rigidbody2D.velocity = Vector2.zero;
		}

		private void Move()
		{
			Rigidbody2D.velocity = m_Velocity;
		}

		/// <summary>
		/// 更新天空背景位置
		/// 当位置到达指定位置，进行位置左移，从而实现无限循环
		/// </summary>
		private void UpdatePosOperation() {


			Vector3 curPos = transform.position;

			if (curPos.x <= m_TargetPosX)
            {
				// 移动到右边（以为走了，右边的右边，所以增加 2 * BACKGROUND_SPRITE_INTERVAL_X ）
				curPos = new Vector3((curPos.x + 2* GameConfig.BACKGROUND_SPRITE_INTERVAL_X), curPos.y, curPos.z);
				transform.position = curPos;
			}
		}
	}
}
