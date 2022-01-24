

namespace MGP_006FlappyBird
{ 
	/// <summary>
	/// 一些游戏配置
	/// </summary>
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

		public const int PASS_PIPE_GET_SCORE = 10;

		public const string GROUND_EDGE_COLLIDER2D_NAME = "GroundEdgeCollider2D";
		public const string SCORE_EDGE_COLLIDER2D_NAME = "ScoreEdgeCollider2D";
	}
}
