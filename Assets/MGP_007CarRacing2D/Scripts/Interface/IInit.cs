using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_007CarRacing2D
{ 

	public interface IInit
	{
		void Init(Transform rootTrans,params object[] objs);
	}
}
