using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_001BreakTheBricks { 

	/// <summary>
	/// 球管理类
	/// </summary>
	public class BallManager 
	{
		// 球容器
		private List<GameObject> m_BallsList = new List<GameObject>();

		/// <summary>
		/// 生成并发射球体
		/// </summary>
		/// <param name="ball">球的预制体</param>
		/// <param name="parentTra">球生成的父物体</param>
		/// <param name="startPost">球生成的初始位置</param>
		/// <param name="forwardDir">球的运动方向</param>
		/// <param name="speed">球的运动速度</param>
		public void ShootBall(GameObject ball, Transform parentTra, Vector3 startPost,Vector3 forwardDir,float speed) {
            if (ball!=null)
            {
				// 生成球体，设置初始位置，并且添加运动速度
				GameObject go = GameObject.Instantiate(ball, parentTra);
				go.transform.position = startPost;
				go.GetComponent<Rigidbody>().velocity = forwardDir * speed;
				m_BallsList.Add(go);
			}
		}

		/// <summary>
		/// 销毁球体
		/// </summary>
		public void DestoryBalls() {
			if (m_BallsList != null)
			{
				// 依次销毁球体
				for (int i = m_BallsList.Count - 1; i >= 0; i--)
				{
					GameObject.Destroy(m_BallsList[i]);
				}

				m_BallsList.Clear();
			}
		}
	}
}
