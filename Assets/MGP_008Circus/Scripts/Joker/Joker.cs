using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class Joker : MonoBehaviour,IGameOver
	{
		private Vector2 m_UpVelocity;

		private Action<Collision2D> m_OnColliderCollisionEnter2D;
		private Action m_OnScoreCollisionEnter2D;

		private Rigidbody2D m_Rigidbody2D;
		private Animator m_Animator;

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

		public Animator Animator
		{
			get
			{
				if (m_Animator == null)
				{
					m_Animator = GetComponent<Animator>();

				}

				return m_Animator;
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init(Action<Collision2D> onColliderCollisionEnter2D, Action onScoreCollisionEnter2D)
		{
			Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
			m_OnColliderCollisionEnter2D = onColliderCollisionEnter2D;
			m_OnScoreCollisionEnter2D = onScoreCollisionEnter2D;

			m_UpVelocity = Vector2.up * GameConfig.JOKER_MOVE_UP_VELOCITY;

		}

		public void Jump()
		{
			Rigidbody2D.velocity = m_UpVelocity;
			PlayJumpAnimation();
		}

		public void Run()
		{
			PlayRunAnimation();
		}

		public void GameOver()
		{
			PlayCollderAnimation();
			Rigidbody2D.bodyType = RigidbodyType2D.Static;
		}

		private void PlayCollderAnimation()
		{
			Animator.SetTrigger(AnimatorParametersDefine.JOKER_TRIGGER_COLLIDER);
		}

		private void PlayJumpAnimation()
		{
			Animator.SetBool(AnimatorParametersDefine.JOKER_IS_JUMP,true);
		}

		private void PlayRunAnimation()
		{
			Animator.SetBool(AnimatorParametersDefine.JOKER_IS_JUMP,false);
		}

		/// <summary>
		/// 触发碰撞
		/// </summary>
		/// <param name="collision"></param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (m_OnColliderCollisionEnter2D != null)
			{
				m_OnColliderCollisionEnter2D.Invoke(collision);
			}
		}

		/// <summary>
		/// 触发加分碰撞
		/// </summary>
		/// <param name="collision"></param>
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (m_OnScoreCollisionEnter2D != null)
			{
				m_OnScoreCollisionEnter2D.Invoke();
			}
		}
	}
}
