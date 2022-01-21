using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class ScoreManager : IManager
	{

		//分数参数
		private int m_Score;
		public int Score
		{
			get { return m_Score; }
			set
			{
				// 判断分数是否更新，更新则触发更新事件
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

		// 分数变化委托
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
