using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	public class Bomb : MonoBehaviour
	{
        protected BombManager m_BmobManager;
        protected BombEffectManager m_BombEffectManager;
        protected DataModelManager m_DataModelManager;
        private bool m_IsRecycle = false;
        public void Init( params object[] objs)
        {
            m_BmobManager = objs[0] as BombManager;
            m_BombEffectManager = objs[1] as BombEffectManager;
            m_DataModelManager = objs[2] as DataModelManager;
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.name.StartsWith( GameConfig.KNIFE_NAME))
            {
                //减少生命值
                m_DataModelManager.Life.Value -= GameConfig.BOMB_REDUCE_LIFE;

                //产生爆炸特效
                BombEffect bombEffect = m_BombEffectManager.GetBombEffect();
                bombEffect.Show(transform.position, m_BombEffectManager.RecycleBombEffect);

                //隐藏物体
                m_BmobManager.RecycleBomb(this);
            }
        }


        private void OnBecameVisible()
        {
            m_IsRecycle = true;
        }


        /// <summary>
        /// 相机是野外事件
        /// </summary>
        private void OnBecameInvisible()
        {
            if (m_IsRecycle == true)
            {
                m_BmobManager.RecycleBomb( this);
                m_IsRecycle = false;

            }
        }
    }
}
