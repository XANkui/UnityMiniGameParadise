using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{

	/// <summary>
	/// Resources 预制体的路径常量定义
	/// </summary>
	public class ResPathDefine 
	{

		public static List<string> PLATFORM_RES_PATH_LIST = new List<string>() {
			"Prefabs/Platform/PlatForm_Cube_Green",
			"Prefabs/Platform/PlatForm_Cube_Green_Plus",
			"Prefabs/Platform/PlatForm_Cube_Red",
			"Prefabs/Platform/PlatForm_Cube_Red_Plus",
			"Prefabs/Platform/PlatForm_Cube_Yellow",
			"Prefabs/Platform/PlatForm_Cube_Yellow_Plus",
		};

		public const string PLAYER_RES_PATH = "Prefabs/Player";
	}
}
