using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_002FlyPin {

	/// <summary>
	/// Pin 状态
	/// </summary>
	public enum PinState
	{
		Idle = 0,	// 闲置状态
		Readying,	// 正在准备状态
		ReadyOK,	// 装备好状态
		Fly,		// 飞向目标状态
	}


	/// <summary>
	/// 消息类型
	/// </summary>
	public enum MsgType
	{
		GameOver = 0, // 游戏结束
	}
}
