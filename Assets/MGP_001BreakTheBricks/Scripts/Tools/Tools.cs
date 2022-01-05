using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_001BreakTheBricks { 

	/// <summary>
	/// 工具类
	/// </summary>
	public class Tools 
	{
		/// <summary>
		/// 把鼠标屏幕坐标转为世界坐标
		/// </summary>
		/// <param name="refTran">对应参照对象</param>
		/// <param name="refCamera">对应参照相机</param>
		/// <returns>鼠标的世界位置</returns>
		public static Vector3 MousePosScreenToWorldPos(Transform refTran,Camera refCamera) {
			//将对象坐标换成屏幕坐标
			Vector3 pos = refCamera.WorldToScreenPoint(refTran.position);
			//让鼠标的屏幕坐标与对象坐标一致
			Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
			//将正确的鼠标屏幕坐标换成世界坐标交给物体
			return refCamera.ScreenToWorldPoint(mousePos);

		}
	}
}
