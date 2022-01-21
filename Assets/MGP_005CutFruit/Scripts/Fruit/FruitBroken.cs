using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	public class FruitBroken : MonoBehaviour
	{
        protected FruitManager m_FruitManager;
        protected FruitBrokenType m_FruitBrokenType;
        private bool m_IsRecycle = false;
        public void Init(FruitBrokenType fruitBrokenType,params object[] objs)
        {
            m_FruitBrokenType = fruitBrokenType;
            m_FruitManager = objs[0] as FruitManager;
        }

        private void OnBecameVisible()
        {
            m_IsRecycle = true;
        }

        private void OnBecameInvisible()
        {
            if (m_IsRecycle == true)
            {
                m_FruitManager.RecycleFruitBroken(m_FruitBrokenType, this);
                m_IsRecycle = false;

            }
        }
    }
}
