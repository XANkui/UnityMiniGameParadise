using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
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
	}
}
