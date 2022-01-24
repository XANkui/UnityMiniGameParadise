
using System;
using UnityEngine;

namespace MGP_006FlappyBird { 

	public class Bird : MonoBehaviour
	{
		private Vector2 m_UpVelocity;
		private Vector2 m_CurVelocity;


		private Rigidbody2D m_Rigidbody2D;
		private Animator m_Animator;

		private Action m_OnGroundCollisionEnter2D;
		private Action m_OnScoreCollisionEnter2D;

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
		public void Init(Action onGroundCollisionEnter2D, Action onScoreCollisionEnter2D)
		{
			m_OnGroundCollisionEnter2D = onGroundCollisionEnter2D;
			m_OnScoreCollisionEnter2D = onScoreCollisionEnter2D;
			m_UpVelocity = Vector2.up * GameConfig.BIRD_MOVE_UP_Y;
			PlayFlyAnimation();
		}

		public void Fly() {
			Rigidbody2D.velocity = m_UpVelocity;
		}

		public void Resume()
		{
			
			Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
			Move(m_CurVelocity);

			PlayFlyAnimation();
		}

		public void Pause()
		{
			m_CurVelocity = Rigidbody2D.velocity;
			Rigidbody2D.bodyType = RigidbodyType2D.Static;

			PlayIdleAnimation();
		}


		public void GameOver() {
			//Rigidbody2D.bodyType = RigidbodyType2D.Static;
			PlayDieAnimation();
		}

		private void Move(Vector2 velocity)
		{
			Rigidbody2D.velocity = velocity;
		}

		private void PlayIdleAnimation() {
			Animator.SetBool(AnimatorParametersDefine.IS_FLY, false);
			Animator.SetBool(AnimatorParametersDefine.IS_DIE, false);
		}

		private void PlayFlyAnimation()
		{
			Animator.SetBool(AnimatorParametersDefine.IS_FLY, true);
			Animator.SetBool(AnimatorParametersDefine.IS_DIE, false);
		}

		private void PlayDieAnimation()
		{
			Animator.SetBool(AnimatorParametersDefine.IS_FLY, false);
			Animator.SetBool(AnimatorParametersDefine.IS_DIE, true);
		}

		/// <summary>
		/// 触发死亡碰撞
		/// </summary>
		/// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name.StartsWith(GameConfig.GROUND_EDGE_COLLIDER2D_NAME) 
				|| collision.collider.CompareTag(TagDefine.PIPE))
            {
                if (m_OnGroundCollisionEnter2D!=null)
                {
					m_OnGroundCollisionEnter2D.Invoke();

				}
            }

			
		}

		/// <summary>
		/// 触发加分碰撞
		/// </summary>
		/// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
			if (collision.name.StartsWith(GameConfig.SCORE_EDGE_COLLIDER2D_NAME))
			{
				if (m_OnScoreCollisionEnter2D != null)
				{
					m_OnScoreCollisionEnter2D.Invoke();

				}
			}
		}
    }
}
