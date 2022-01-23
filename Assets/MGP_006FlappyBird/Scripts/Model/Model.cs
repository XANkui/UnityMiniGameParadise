using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird
{

	/// <summary>
	/// 数据模型
	/// </summary>
	public class Model
	{
		private int m_Value;
		public int Value
		{
			get { return m_Value; }
			set
			{
				if (m_Value != value)
				{
					m_Value = value;

					if (OnValueChanged != null)
					{
						OnValueChanged.Invoke(value);

					}
				}
			}
		}

		/// <summary>
		/// 数值变化事件
		/// </summary>
		public Action<int> OnValueChanged;
	}
}
