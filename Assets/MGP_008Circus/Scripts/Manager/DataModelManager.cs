using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class DataModelManager : IManager
	{
        private Model m_Scroe;

        public Model Score => m_Scroe;

        public void Init(Transform rootTrans)
        {
            m_Scroe = new Model();
            m_Scroe.Value = 0;
       
        }

        public void Update()
        {

        }

        public void Destroy()
        {
            m_Scroe.OnValueChanged = null;
            m_Scroe.Value = 0;
            m_Scroe = null;
        }
    }
}
