using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird { 

	public class Pipe : MonoBehaviour
	{
		private float m_TargetPosX;
		private Vector2 m_Velocity;
		private bool m_IsPause;

		private Action<Pipe> m_OnRecycleSelfAction;

		private Rigidbody2D m_Rigidbody2D;
		public Rigidbody2D Rigidbody2D
		{
			get
			{
				if (m_Rigidbody2D == null)
				{
					m_Rigidbody2D = GetComponent<Rigidbody2D>();

				}

				return m_Rigidbody2D;
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (m_IsPause == true)
			{
				return;
			}

			UpdatePosOperation();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="spawnPosX"></param>
		/// <param name="spawnPosY"></param>
		/// <param name="movePosTargetPosX"></param>
		/// <param name="onRecycleSelfAction"></param>
		public void Init(float spawnPosX, float spawnPosY, float movePosTargetPosX, Action<Pipe> onRecycleSelfAction)
		{
			m_TargetPosX = movePosTargetPosX;
			m_Velocity = Vector2.left * GameConfig.BACKGROUND_MOVE_LEFT_X;

			Vector3 curPos = transform.position;
			transform.position = new Vector3(spawnPosX, spawnPosY, curPos.z);

			m_OnRecycleSelfAction = onRecycleSelfAction;

			m_IsPause = false;
			Move();
		}

		public void Resume()
		{
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
		/// 位置更新
		/// 判断位置是否到达指定位置，进行对象回收
		/// </summary>
		private void UpdatePosOperation()
		{


			Vector3 curPos = transform.position;

			if (curPos.x <= m_TargetPosX)
			{
				if (m_OnRecycleSelfAction != null)
                {
					m_OnRecycleSelfAction.Invoke(this);

				}
			}
		}
	}
}
