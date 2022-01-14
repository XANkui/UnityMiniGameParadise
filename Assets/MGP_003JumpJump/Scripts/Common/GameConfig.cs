using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{

	/// <summary>
	/// 一些控制游戏效果的游戏配置常量定义
	/// </summary>
	public class GameConfig 
	{
		// Camera 跟随 Player 的移动速度
		public const int CAMERA_FOLLOW_PLAYER_SPEED = 5;

		// 每个新生成的 Platform 间的间距
		public const float PLATFRM_CUBE_DISTANCE = 3;

		// 新生成 Platform 有上升动画的 Y 运动距离
		public const float PLATFRM_SPAWN_ANIMATION_DISTANCE_Y = 4;

		// 新生成 Platform 有上升动画的 Y 运动速度
		public const float PLATFRM_SPAWN_ANIMATION_MOVE_SPEED = 4;

		// 场景中 Platform 存在的个数
		public const int PLATFORM_EXIST_COUNT = 5;

		// Player 跳到 Platform 增加的分数
		public const int PLATFORM_ADD_SCORE = 100;

		// 鼠标蓄力按压的最大时间长度限制
		public const float MOUSE_PRESSING_TIME_LENGTH = 3;

		// 鼠标蓄力按压时，Player 缩小的最小限制比例 Y
		public const float PLAYER_MODEL_MIN_SCALE_Y = 0.2f;

		// 鼠标蓄力按压时，Player 缩小的比例的变化速度
		public const float PLAYER_MODEL_SCALE_Y_SAMLL_SPEED = 1f;

		// Player 从 Platform 坠落的距离，算 Player 是否坠落
		public const float PLAYER_MODEL_FALL_DISTANCE_FROM_PLATFORM = 2.0f;

		// Player 抛物线运动的最大高度距离
		public const float PLAYER_MODEL_JUMP_TOP_DISTANCE = 3.0f;

		// Player 抛物线运动的时间
		public const float PLAYER_MODEL_JUMP_TIME_LENGTH = 0.5f;

		// Player 抛物线运动的跨度距离平滑系数
		public const float PLAYER_MODEL_JUMP_LENGTH_SMOOTH_VALUE = 3.0f;
	}
}
