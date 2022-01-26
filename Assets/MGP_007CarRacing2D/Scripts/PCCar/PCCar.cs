using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MGP_007CarRacing2D { 

	public class PCCar : MonoBehaviour
	{
		private Vector2 m_MoveLeftVelocity;
		private Vector2 m_MoveRightVelocity;
		private Vector2 m_CurVelocity;

		private Rigidbody2D m_Rigidbody2D;

		private Action m_OnNPCCollisionEnter2D;
		private Action<GameObject> m_OnCoinCollisionEnter2D;

		private float m_RotateAnimationSpeed = 5;
		private float m_HalfWidth;
		private CarRoateDir m_CurCarRoateDir;

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

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init(Action onNPCCollisionEnter2D, Action<GameObject> onCoinCollisionEnter2D)
		{
			m_OnNPCCollisionEnter2D = onNPCCollisionEnter2D;
			m_OnCoinCollisionEnter2D = onCoinCollisionEnter2D;

			m_HalfWidth = Screen.width / 2;

			m_MoveLeftVelocity = Vector2.left * GameConfig.CAR_ROTATE_VELOCITY_VALUE_LIMIT;
			m_MoveRightVelocity = Vector2.right * GameConfig.CAR_ROTATE_VELOCITY_VALUE_LIMIT;

			m_CurCarRoateDir = CarRoateDir.Normal;
		}

		public void Resume()
		{

			Rigidbody2D.velocity = m_CurVelocity;
		}

		public void Pause()
		{
			m_CurVelocity = Rigidbody2D.velocity;
			Rigidbody2D.velocity = Vector2.zero;

		}


		public void GameOver()
		{
			Rigidbody2D.velocity = Vector2.zero;
		}

		/// <summary>
		/// 车子切道转向
		/// </summary>
		public void UpdateChangeLaneRotation() {

			if (Input.GetMouseButtonDown(0)
				   && EventSystem.current.IsPointerOverGameObject() == false)// 不是点击 UI
			{
				if (Input.mousePosition.x <= m_HalfWidth)  // 左转
				{
					m_CurCarRoateDir = CarRoateDir.Left;
					Rigidbody2D.velocity = m_MoveLeftVelocity;
				}
				else {									// 右转
					m_CurCarRoateDir = CarRoateDir.Right;
					Rigidbody2D.velocity = m_MoveRightVelocity;
				}
			}
			else if(Input.GetMouseButtonUp(0)) {
				m_CurCarRoateDir = CarRoateDir.Normal;
				Rigidbody2D.velocity = Vector2.zero ;
			}

			RotateSelf(m_CurCarRoateDir);
			UpdateLimitPos();
		}

		/// <summary>
		/// 旋转
		/// </summary>
		/// <param name="dir"></param>
		private void RotateSelf(CarRoateDir dir) {
            switch (dir)
            {
                case CarRoateDir.Normal:
					RotateLerp(0);
					break;
                case CarRoateDir.Left:
					RotateLerp(GameConfig.CAR_ROTATE_POS_VALUE_LIMIT);
					break;
                case CarRoateDir.Right:
					RotateLerp(-1 * GameConfig.CAR_ROTATE_POS_VALUE_LIMIT);
					break;

                default:
					Debug.LogError(GetType()+ "/RotateSelf ()/ dir is not Setted !! ");
                    break;
            }
        }

		/// <summary>
		/// 旋转插值动画
		/// </summary>
		/// <param name="target"></param>
		private void RotateLerp(float target) {
			Vector3 curRot = transform.eulerAngles;
			if (curRot.z <= 90)
			{
				curRot.z = Mathf.Lerp(curRot.z, target, Time.deltaTime * m_RotateAnimationSpeed);
			}
			else { // 修正右边旋转赋值，会赋值转为对应正值旋转值（例如 -5，自动转为 360 +（-5））
				curRot.z = Mathf.Lerp(curRot.z, 360+target, Time.deltaTime * m_RotateAnimationSpeed);
			}
			// Debug.Log(curRot.z);
			transform.eulerAngles = curRot;
		}

		/// <summary>
		/// 车子位置限制
		/// </summary>
        private void UpdateLimitPos()
        {
			Vector3 curPos = transform.position;
			if (curPos.x <= GameConfig.CAR_LEFT_OUTSIDE_LIMIT)
			{
				curPos.x = GameConfig.CAR_LEFT_OUTSIDE_LIMIT;

			}
			else if (curPos.x >= GameConfig.CAR_RIGHT_OUTSIDE_LIMIT)
			{
				curPos.x = GameConfig.CAR_RIGHT_OUTSIDE_LIMIT;
			}

			transform.position = curPos;

		}

        /// <summary>
        /// 触发碰撞
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
		{
			if ( collision.CompareTag(Tag.COIN))   // 吃金币
			{
				if (m_OnCoinCollisionEnter2D != null)
				{
					m_OnCoinCollisionEnter2D.Invoke(collision.gameObject);

				}
			}

			if (collision.CompareTag(Tag.NPC_CAR))  // 撞车
			{
				if (m_OnNPCCollisionEnter2D != null)
				{
					m_OnNPCCollisionEnter2D.Invoke();

				}
			}
		}
	}
}
