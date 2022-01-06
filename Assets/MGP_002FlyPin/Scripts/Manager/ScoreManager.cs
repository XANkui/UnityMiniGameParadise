using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_002FlyPin { 

	/// <summary>
	/// 分数管理类
	/// </summary>
	public class ScoreManager 
	{
		//分数参数
		private int m_Scroe;
		public int Score { get { return m_Scroe; }
			set {
				// 判断分数是否更新，更新则触发更新事件
                if (m_Scroe!=value) 
                {
					m_Scroe = value;
                    if (OnChangeValue!=null) 
                    {
						OnChangeValue.Invoke(value);

					}
                }
			}
		}

		// 分数变化委托
		public Action<int> OnChangeValue;
	}
}
