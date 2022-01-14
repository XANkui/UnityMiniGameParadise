using System;


namespace MGP_003JumpJump
{ 

	public class ScoreManager
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
	}
}
