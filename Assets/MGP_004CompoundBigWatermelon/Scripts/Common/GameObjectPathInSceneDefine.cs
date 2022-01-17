using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{

	/// <summary>
	/// 场景中游戏物体路径定义类，统一管理景中游戏物体路径
	/// </summary>
	public class GameObjectPathInSceneDefine 
	{
		public const string WORLD_PATH = "World";
		public const string UI_PATH = "UI";

		public const string UI_SCORE_TEXT_PATH = "Canvas/ScoreText";
		public const string UI_GAME_OVER_IMAGE_PATH = "Canvas/GameOverImage";
		public const string UI_RESTART_GAME_BUTTON_PATH = "Canvas/GameOverImage/RestartGameButton";

		public const string BORDERLINE_DOWN_PATH = "BorderLine_Down";
		public const string BORDERLINE_LEFT_PATH = "BorderLine_Left";
		public const string BORDERLINE_RIGHT_PATH = "BorderLine_Right";
		public const string AIM_LINE_PATH = "AimLine";
		public const string WARNING_LINE_PATH = "Warningline";

		public const string SPAWN_FRUIT_POS_TRANS_PATH = "SpawnFruitPos";
		public const string Main_Camera_TRANS_PATH = "Main Camera";

		public const string AUDIO_SOURCE_PATH = "AudioSource";
	}
}
