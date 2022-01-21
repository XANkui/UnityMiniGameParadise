using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{

	/// <summary>
	/// 水果类型
	/// </summary>
	public enum FruitType { 
		Apple = 0,
		Lemon,
		Watermelon,

		SUM_COUNT, // 水果类型总数
	}

	/// <summary>
	/// 破碎水果类型
	/// </summary>
	public enum FruitBrokenType { 
		Apple_Broken = 0,
		Lemon_Broken,
		Watermelon_Broken,


		SUM_COUNT,  // 破碎水果类型总数
	}
}
