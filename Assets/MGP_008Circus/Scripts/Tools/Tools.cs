using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus
{ 
	public class Tools 
	{
		/// <summary>
		/// 把屏幕坐标转为世界坐标
		/// </summary>
		/// <param name="refTran">对应参照对象</param>
		/// <param name="refCamera">对应参照相机</param>
		/// <param name="screenPos">屏幕位置</param>
		/// <returns>屏幕位置的世界位置</returns>
		public static Vector3 ScreenPosToWorldPos(Transform refTran, Camera refCamera, Vector2 screenPos)
		{
			//将对象坐标换成屏幕坐标
			Vector3 pos = refCamera.WorldToScreenPoint(refTran.position);
			//让鼠标的屏幕坐标与对象坐标一致
			Vector3 mousePos = new Vector3(screenPos.x, screenPos.y, pos.z);
			//将正确的鼠标屏幕坐标换成世界坐标交给物体
			return refCamera.ScreenToWorldPoint(mousePos);

		}


		/// <summary>
		/// 2D游戏 屏幕适配 
		/// vaildWidth =（Screen.width/Screen.height*2*orthographicSize）
		/// </summary>
		/// <param name="BaseScreenWitdh">开发时的参考屏幕宽</param>
		/// <param name="BaseScreenHeight">开发时的参考屏幕高</param>
		/// <param name="orthographicCamera2D">正交的相加</param>
		public static void AdaptationFor2DGame(int BaseScreenWitdh,int BaseScreenHeight,Camera orthographicCamera2D)
        {
            if (orthographicCamera2D.orthographic ==false)
            {
				Debug.LogError("AdaptationFor2DGame()/ Camera is not orthographic , Please Check !! ");
				return;
            }
			// 获取开发设置的 正交相机的 size  
			float BaseOrSize = orthographicCamera2D.orthographicSize;
			 // 获得有效的可视宽度
            float vaildWidth = (BaseScreenWitdh * 1.0f / BaseScreenHeight) * 2 * BaseOrSize;
			// 实际屏幕的 宽高比
            float aspectRatio = Screen.width * 1f / Screen.height;
			// 根据实际屏幕和有效的可视宽度得到，适配的 size ,并赋值到相机
            float adapterOrthoSize = vaildWidth / aspectRatio / 2;
			orthographicCamera2D.orthographicSize = adapterOrthoSize;
        }
    }
}
