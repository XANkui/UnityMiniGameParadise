using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 
	
	public interface IManager 
	{
		// 初始化
		void Init(Transform rootTrans, params object[] managers);
		// 帧更新
		void Update();
		// 销毁时调用（主要是释放数据等使用）
		void Destroy();
	}
}
