using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_006FlappyBird
{ 

	public class GameConfig 
	{
		public const float BACKGROUND_SPRITE_INTERVAL_X = 20.25f;
		public const float BACKGROUND_MOVE_LEFT_X = 2;
		public const int BACKGROUND_TILE_COUNT = 2;

		public const float BIRD_MOVE_UP_Y = 3;

		public const float PIPE_SPAWN_TIME_INTERVAL = 3;
		public const float PIPE_SPAWN_POS_Y_LIMIT_MIN = -1;
		public const float PIPE_SPAWN_POS_Y_LIMIT_MAX = 3.5f;

		public const float PIPE_SPAWN_POS_RIGHT_LIMIT_X = 3.5f;
		public const float PIPE_MOVE_POS_LEFT_LIMIT_X = 3.5f;
	}
}
