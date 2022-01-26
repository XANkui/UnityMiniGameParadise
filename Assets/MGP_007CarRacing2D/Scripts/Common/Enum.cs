using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D {

	/// <summary>
	/// 声音枚举
	/// </summary>
	public enum AudioClipSet
	{
		Bonus = 0,
		ButtonClick,
		CarCrash,

		BG_FutureWorld_Dark,  // BG  背景音乐

		SUM_COUNT, // 仅作为计数
	}

	/// <summary>
	/// NPCType 枚举
	/// </summary>
	public enum NPCType
	{
		Coin = 0,
		NPCCar2,
		NPCCar3,
		NPCCar4,
		NPCCar6,
		NPCCar8,
		NPCCar9,

		SUM_COUNT, // 仅作为计数
	}

	/// <summary>
	/// 汽车转向
	/// </summary>
	public enum CarRoateDir { 
		Normal=0,
		Left,
		Right,

		SUM_COUNT, // 仅作为计数
	}
}
