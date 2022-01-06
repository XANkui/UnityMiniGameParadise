using System;
using System.Collections.Generic;

namespace MGP_002FlyPin {

	/// <summary>
	/// 简单消息中心
	/// </summary>
	public class SimpleMessageCenter 
	{
		// 消息字典集合
		private Dictionary<MsgType, Action> m_MsgDict = new Dictionary<MsgType, Action>();

		// 消息单例
		private static SimpleMessageCenter m_Instance;

		public static SimpleMessageCenter Instance {
			get {
                if (m_Instance==null)
                {
					m_Instance = new SimpleMessageCenter();
                }

				return m_Instance;
			}
		}

		/// <summary>
		/// 注册消息
		/// </summary>
		/// <param name="msgType"></param>
		/// <param name="action"></param>
		public void RegisterMsg(MsgType msgType, Action action) {
			if (m_MsgDict.ContainsKey(msgType) == true)
			{
				m_MsgDict[msgType] += action;
			}
			else {
				m_MsgDict.Add(msgType,action);
			}
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="msgType"></param>
		public void SendMsg(MsgType msgType)
		{
			if (m_MsgDict.ContainsKey(msgType) == true)
			{
				m_MsgDict[msgType].Invoke();
			}
			
		}

		/// <summary>
		/// 清空消息
		/// </summary>
		public void ClearAllMsg() {

			m_MsgDict.Clear();
		}
	}
}
