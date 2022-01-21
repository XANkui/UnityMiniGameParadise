using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	/// <summary>
	/// 游戏配置数据
	/// </summary>
	public class GameConfig 
	{
		public const string NAME_SPACE_NAME = "MGP_005CutFruit.";

		// Knife 预制体名称（用于碰撞触发判断使用）
		public const string KNIFE_NAME = "Knife";

		// 水果、炸弹、Knife 等游戏生成的统一深度距离
		public const float GAME_OBJECT_Z_VALUE = 5;	

		// 底部间隔几秒生成物体
		public const float BOTTOM_SPAWN_INTERVAL_TIME = 1;	

		// 游戏生命（可切破几次炸弹）
		public const int GAME_LIFE_LENGTH = 3;
		// 游戏剩余几条命结束
		public const int REMAIN_LIFE_IS_GAME_OVER = 0;

		// 水果 炸弹生成向上的最小最大速度
		public const float FRUIT_UP_Y_VELOSITY_MIN_VALUE = 8;
		public const float FRUIT_UP_Y_VELOSITY_MAX_VALUE = 10;

		/// <summary>
		/// 随机生成水果炸弹的数值范围
		/// 随机数多少是炸弹
		/// </summary>
		public const int FRUIT_RANDOM_MIN_VALUE = 0;
		public const int FRUIT_RANDOM_MAX_VALUE = 15;
		public const int BOMB_RANDOM_VALUE = 10;

		/// <summary>
		/// 切刀不同水果的得分
		/// </summary>
		public const int FRUIT_APPLE_SCORE = 10;
		public const int FRUIT_LEMON_SCORE = 20;
		public const int FRUIT_WATERMELON_SCORE = 30;

		//切到炸弹失去的生命值
		public const int BOMB_REDUCE_LIFE = 1;
	}
}
