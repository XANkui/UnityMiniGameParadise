using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class GameConfig 
	{
		// 生成水果的比例（用来控制游戏中生成水果的整体比例）
		public const float FRUIT_SCALE = 0.80f;

		// 每次生成水果的间隔时间
		public const float FRUIT_SPAWN_INTERVAL_TIME = 0.5f;

		// 水果触及警告线持续多少秒，算游戏结束
		public const float JUDGE_GAME_OVER_TIME_LENGHT = 3;

		// 水果满足条件差不多到警告线前，持续多少秒，发出警告信息
		public const float JUDGE_GAME_OVER_WARNING_TIME_LENGHT = 0.5f;

		// 某个水果距离警告线多远，触发警告条件
		public const float GAME_OVER_WARNING_LINE_DISTANCE = 1.5f;

		// 特效的动画速度
		public const float EFFECT_ANIMATION_SPEED = 5f;

		// 水果合成的基础分 （实际计算规则是 : 水果的类型 * 该 base score）
		public const int COMPOUND_FRUIT_BASE_SCORE = 10;

	}
}
