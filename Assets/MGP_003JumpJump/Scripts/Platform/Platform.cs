using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{ 

	public class Platform : MonoBehaviour
	{
		private bool m_IsSpawnAnimation = false;
		private float m_MoveDeltra = 0;
		private Vector3 m_TargetPos;
		private Action<Vector3> m_OnCollisionEnterAction;
		private bool m_IsOnCollisionEntered = false;


        #region Unity Functions

        // Update is called once per frame
        void Update()
		{
			PlatformSpawnAniamtion();
		}

		private void OnCollisionEnter(Collision collision)
		{
			FirstOnCollisionEnter();
		}

        #endregion

        #region 游戏逻辑

        public void Init(Action<Vector3> onCollisionEnterAction, Vector3 targetPos, bool isMoveAnimation = false)
        {
			ResetInfo();
			m_OnCollisionEnterAction = onCollisionEnterAction;

			SpawnAnimation(targetPos, isMoveAnimation);
		}

		private void SpawnAnimation(Vector3 targetPos,  bool isMoveAnimation = false)
		{
			
			m_IsSpawnAnimation = isMoveAnimation;
			m_TargetPos = targetPos;

			if (m_IsSpawnAnimation == true)
			{
				Vector3 curPos = targetPos;
				curPos.y -= GameConfig.PLATFRM_SPAWN_ANIMATION_DISTANCE_Y;
				transform.position = curPos;
			}
			else
			{
				transform.position = targetPos;
			}


		}

		/// <summary>
		/// 平台的上升动画
		/// </summary>
		void PlatformSpawnAniamtion() {
			if (m_IsSpawnAnimation == true)
			{
				m_MoveDeltra += 1f / GameConfig.PLATFRM_SPAWN_ANIMATION_MOVE_SPEED * Time.deltaTime;
				transform.position = Vector3.Lerp(transform.position, m_TargetPos, m_MoveDeltra);
				if (Vector3.Distance(transform.position, m_TargetPos) <= 0.01f)
				{
					transform.position = m_TargetPos;

					m_IsSpawnAnimation = false;
				}
			}
		}

		/// <summary>
		/// 第一次碰撞进入平台，触发对应事件
		/// 这里事件主要是 生成下一个 Platform 和 增加分数
		/// </summary>
		private void FirstOnCollisionEnter()
		{
			if (m_IsOnCollisionEntered == false)
			{
				m_IsOnCollisionEntered = true;
				m_OnCollisionEnterAction?.Invoke(this.transform.position);

			}
		}

		private void ResetInfo()
		{
			m_IsSpawnAnimation = false;
			m_IsOnCollisionEntered = false;
			m_OnCollisionEnterAction = null;
		}

		#endregion


	}
}
