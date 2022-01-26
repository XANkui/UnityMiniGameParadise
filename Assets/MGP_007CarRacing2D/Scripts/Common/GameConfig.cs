

namespace MGP_007CarRacing2D { 

	/// <summary>
	/// 游戏配置
	/// </summary>
	public class GameConfig 
	{
		public const int GAME_DEVELOP_BASE_SCREEN_WIDTH = 1080;
		public const int GAME_DEVELOP_BASE_SCREEN_HEIGHT = 1920;

		public const int ROAD_TILE_COUNT = 2;
		public const int ROAD_MOVEVELOCITY_Y = 6;
		public const float ROAD_SPRITE_INTERVAL_Y = 11.25f;

		public const float NPC_SPAWN_TIME_INTERVAL = 1f;

		public const int DRIVING_DISTANCE_SCORE = 1;
		public const int ENTER_COIN_SCORE = 10;

		public const float CAR_LEFT_OUTSIDE_LIMIT = -1.6f;
		public const float CAR_RIGHT_OUTSIDE_LIMIT = 1.6f;

		public const float CAR_LEFT_MIDDLE_LIMIT = -0.55f;
		public const float CAR_RIGHT_MIDDLE_LIMIT = 0.55f;

		public const float CAR_ROTATE_POS_VALUE_LIMIT = 20f;
		public const float CAR_ROTATE_VELOCITY_VALUE_LIMIT = 5f;
	}
}
