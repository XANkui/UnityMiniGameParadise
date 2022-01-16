using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public interface IManager 
	{
		// 初始化
		void Init(Transform worldTrans, Transform uiTrans, params object[] manager);
		// 帧更新
		void Update();
		// 销毁时调用（主要是释放数据等使用）
		void Destroy();
	}
}
