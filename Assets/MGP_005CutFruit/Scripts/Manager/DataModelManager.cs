using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	public class DataModelManager : IManager
	{
        private Model m_Scroe;
        private Model m_Life;


        public Model Score => m_Scroe;
        public Model Life => m_Life;

        public void Init(Transform rootTrans, params object[] manager)
        {
            m_Scroe = new Model();
            m_Scroe.Value = 0;
            m_Life = new Model();
            m_Life.Value = GameConfig.GAME_LIFE_LENGTH;
        }

        public void Update()
        {
            
        }

        public void Destroy()
        {
            m_Scroe.OnValueChanged = null;
            m_Scroe.Value = 0;
            m_Scroe = null;

            m_Life.OnValueChanged = null;
            m_Life.Value = 0;
            m_Life = null;
        }
    }
}
