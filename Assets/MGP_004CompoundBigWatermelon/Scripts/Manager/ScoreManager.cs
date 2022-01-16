using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class ScoreManager : IManager
	{

		// 分数
		private int m_Score;
		public int Score
		{
			get { return m_Score; }
			set
			{
				if (m_Score != value)
				{
					m_Score = value;

					if (OnValueChanged != null)
					{
						OnValueChanged.Invoke(value);

					}
				}
			}
		}

		/// <summary>
		/// 分数变化事件
		/// </summary>
		public Action<int> OnValueChanged;

		public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
			m_Score = 0;

		}

        public void Update()
        {
        }

        public void Destroy()
        {
			OnValueChanged = null;
			m_Score = 0;
		}
    }
}
