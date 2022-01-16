using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{
	/// <summary>
	/// 水果类型
	/// 注意：其中顺序是合成的顺序
	/// </summary>
	public enum FruitSeriesType
	{

		Mangosteen = 0,		// 山竹
		Apple = 1,			// 苹果
		Orange = 2,			// 桔子
		Lemon = 3,			// 柠檬
		Kiwi = 4,           // 猕猴桃
		Tomato = 5,			// 西红柿
		Peach = 6,			// 桃子
		Pineapple = 7,		// 菠萝
		Coco = 8,           // 椰子
		Watermelon = 9,		// 西瓜
		BigWatermelon = 10,	// 大西瓜

		SUM_COUNT = 11,		//总数（计数使用）
	}
}
