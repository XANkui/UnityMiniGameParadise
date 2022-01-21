using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	public class GameConfig 
	{
		public const string NAME_SPACE_NAME = "MGP_005CutFruit.";
		public const string KNIFE_NAME = "Knife";

		public const float GAME_OBJECT_Z_VALUE = 5;	

		public const float BOTTOM_SPAWN_INTERVAL_TIME = 2;	

		public const int REMAIN_LIFE_IS_GAME_OVER = 2;

		public const float FRUIT_UP_Y_VELOSITY_MIN_VALUE = 8;
		public const float FRUIT_UP_Y_VELOSITY_MAX_VALUE = 10;

		public const int FRUIT_RANDOM_MIN_VALUE = 0;
		public const int FRUIT_RANDOM_MAX_VALUE = 15;
		public const int BOMB_RANDOM_VALUE = 10;


		public const int FRUIT_APPLE_SCORE = 10;
		public const int FRUIT_LEMON_SCORE = 20;
		public const int FRUIT_WATERMELON_SCORE = 30;

		public const int BOMB_REDUCE_LIFE = 1;
	}
}
