using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class LineManager : IManager
	{
		private Transform m_BorderLine_Left;
		private Transform m_BorderLine_Right;
		private Transform m_Aimline;
		private Transform m_Warningline;
        private Transform m_MainCameraTrans;
        private Camera m_MainCamera;
        private FruitManager m_FruitManager;


        public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
            m_BorderLine_Left = worldTrans.Find(GameObjectPathInSceneDefine.BORDERLINE_LEFT_PATH);
            m_BorderLine_Right = worldTrans.Find(GameObjectPathInSceneDefine.BORDERLINE_RIGHT_PATH);
            m_Aimline = worldTrans.Find(GameObjectPathInSceneDefine.AIM_LINE_PATH);
            m_Warningline = worldTrans.Find(GameObjectPathInSceneDefine.WARNING_LINE_PATH);
            m_MainCameraTrans = worldTrans.Find(GameObjectPathInSceneDefine.Main_Camera_TRANS_PATH);
            m_MainCamera = m_MainCameraTrans.GetComponent<Camera>();

            BorderLineSimpleAdaptScreen();

            m_FruitManager = manager[0] as FruitManager;

            m_Aimline.gameObject.SetActive(false);
        }

        public void Update()
        {
            UpdateWarninglineHandle();
            UpdateAimlineHandle();
        }

        public void Destroy()
        {
            m_BorderLine_Left = null;
            m_BorderLine_Right = null;
            m_Aimline = null;
            m_Warningline = null; 

            m_FruitManager = null;
        }

        /// <summary>
        /// 简单边界适配屏幕
        /// </summary>
        void BorderLineSimpleAdaptScreen()
        {
            // 左边缘屏幕适配
            float left_X = Tools.ScreenPosToWorldPos(m_BorderLine_Left, m_MainCamera, Vector2.zero).x; 
            m_BorderLine_Left.position = new Vector3(left_X, m_BorderLine_Left.position.y, m_BorderLine_Left.position.z);

            // 右边缘屏幕适配
            float Right_X = Tools.ScreenPosToWorldPos(m_BorderLine_Right, m_MainCamera, Vector2.right * Screen.width).x;
            m_BorderLine_Right.position = new Vector3(Right_X, m_BorderLine_Right.position.y, m_BorderLine_Right.position.z);

        }

        /// <summary>
        /// 警告线的显示隐藏处理
        /// </summary>
        void UpdateWarninglineHandle() {
            m_Warningline.gameObject.SetActive(GameManager.Instance.GameOverWarning);
        }

        /// <summary>
        /// 瞄准线的显示隐藏，移动位置处理
        /// </summary>
        void UpdateAimlineHandle() {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_FruitManager != null && m_FruitManager.CurFruit != null)
                {
                    m_Aimline.gameObject.SetActive(true);

                }

            }
            else if (Input.GetMouseButton(0))
            {
                UpdateAimlinePos();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_Aimline.gameObject.SetActive(false);

            }
        }

        /// <summary>
        /// 瞄准线的移动处理
        /// </summary>
        void UpdateAimlinePos() {
            if (m_FruitManager!=null && m_FruitManager.CurFruit!=null)
            {
                m_Aimline.transform.position = new Vector3(m_FruitManager.CurFruit.transform.position.x,
               m_Aimline.transform.position.y,
               m_Aimline.transform.position.z);
            }
           
        }
    }
}
