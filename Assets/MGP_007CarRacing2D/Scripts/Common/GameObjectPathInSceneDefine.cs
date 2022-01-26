using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D { 

	/// <summary>
	/// 场景中的游戏物体定义
	/// </summary>
	public class GameObjectPathInSceneDefine 
	{
		public const string WORLD_PATH = "World";
		public const string UI_PATH = "UI";

		public const string MAIN_CAMERA_PATH = "Main Camera";

		public const string AUDIO_SOURCE_TRANS_PATH = "AudioSourceTrans";
		public const string SPAWN_ROAD_POS_PATH = "SpawnRoadPos";

		public const string UI_PLAY_PANEL_PATH = "Canvas/PlayPanel";
		public const string UI_GAME_PANEL_PATH = "Canvas/GamePanel";
		public const string UI_PAUSE_PANEL_PATH = "Canvas/PausePanel";
		public const string UI_GAME_OVER_PANEL_PATH = "Canvas/GameOverPanel";
		public const string UI_PLAY_IMAGE_BUTTON_PATH = "Canvas/PlayPanel/PlayImageButton";
		public const string UI_PAUSE_IMAGE_BUTTON_PATH = "Canvas/GamePanel/PauseImageButton";
		public const string UI_SCORE_TEXT_PATH = "Canvas/GamePanel/ScoreText";
		public const string UI_RESUME_IMAGE_BUTTON_PATH = "Canvas/PausePanel/ResumeImageButton";
		public const string UI_HOME_IMAGE_BUTTON_PATH = "Canvas/PausePanel/HomeImageButton";
		public const string UI_RESTART_IMAGE_BUTTON_PATH = "Canvas/GameOverPanel/RestartImageButton";
	}
}
