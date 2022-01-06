using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MGP_002FlyPin
{ 
    /// <summary>
    /// 游戏管理类
    /// </summary>
	public class GameManager : MonoBehaviour
	{
        [Header("TargetCircle 旋转速度")]
        [SerializeField]
        private float RotateSpeed = 5;
        [Header("Pin 生成的位置")]
        [SerializeField]
        private Transform m_PinSpawnPos;
        [Header("Pin 准备好插针的位置")]
        [SerializeField]
        private Transform m_PinReadyPos;
        [Header("Pin 飞向目标位置距离多远停止飞行")]
        [SerializeField]
        private float m_PinFlyTargetPosDistance;
        [Header("Pin 飞行移动速度")]
        [SerializeField]
        private float m_PinMoveSpeed;
        [Header("Camera 视口大小")]
        [SerializeField]
        private float m_OrthographicSize=4;
        [Header("Scroe 分数 Text")]
        [SerializeField]
        private TextMesh ScoreText;
        // Pin 飞行目标圆管理类
        TargetCircleManager m_TargetCircleManager;
        // Pin 管理类
        PinsManager m_PinsManager;
        // 分数管理类
        ScoreManager m_ScoreManager;
        // 主 Camera
        Camera m_MainCamera;
        // 游戏是否结束
        bool m_IsGameOver = false;

        private void Awake()
        {
            // 初始化参数值
            m_MainCamera = Camera.main;
            m_IsGameOver = false;
            Transform targetCircleTrans = GameObject.Find(ConstStr.WORLD_TARGETCIRCLE_NAME_PATH).transform;
            m_TargetCircleManager = new TargetCircleManager(targetCircleTrans, RotateSpeed);
            m_PinsManager = new PinsManager(m_PinSpawnPos.position,m_PinReadyPos.position, targetCircleTrans.position,
                m_PinFlyTargetPosDistance,m_PinMoveSpeed,targetCircleTrans);
            m_ScoreManager = new ScoreManager();
        }

        private void Start()
        {
            // 开始TargetCircle旋转
            m_TargetCircleManager.StartRotateSelf();
            // 生成第一个 Pin 
            m_PinsManager.SpawnPin();
            // 注册监听游戏结束消息
            SimpleMessageCenter.Instance.RegisterMsg(MsgType.GameOver,ToGameOver);
            // 初始化分数 0
            m_ScoreManager.Score = 0;
            // 分数更新事件，更新 UI
            m_ScoreManager.OnChangeValue += (score)=> { ScoreText.text = score.ToString(); };
        }

        private void Update()
        {
            // 游戏结束
            if (m_IsGameOver == true)
            {
                return;
            }
            // TargetCircle 更新旋转
            m_TargetCircleManager.UpdateRotateSelf();

            // 监听鼠标左键按下
            if (Input.GetMouseButtonDown(0))
            {
                // Pin 飞向目标，则增加分数，并且生成下一个 Pin
                bool isFly = m_PinsManager.FlyPin();
                if (isFly==true)
                {
                    m_ScoreManager.Score++;
                    m_PinsManager.SpawnPin(); // 生成下一个
                }
               
            }
        }

        private void OnDestroy()
        {
            // 销毁所有 Pin
            m_PinsManager.DestroyAllPins();
            // 清空消息中心
            SimpleMessageCenter.Instance.ClearAllMsg();
            // 置空分数更新事件
            m_ScoreManager.OnChangeValue = null;
            // 置空相关参数
            m_PinsManager = null;
            m_TargetCircleManager = null;
            m_ScoreManager = null;
            // 停止所有协程
            StopAllCoroutines();
        }

        // Unity 周期函数 每帧调用
        private void OnGUI()
        {
            // 游戏操作说明
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.background = null;    //设置背景填充
            fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色
            fontStyle.fontSize = 40;       //字体大小
            GUI.Label(new Rect(10, 10, 200, 200),
                "操作说明：\n1、点击鼠标左键发射球体；\n2、两针 Pin 碰撞会自动触发重新开始游戏；",
                fontStyle);

        }

        /// <summary>
        /// 游戏结束
        /// </summary>
        void ToGameOver() {
            // 游戏结束
            if (m_IsGameOver==true)
            {
                return;
            }
            // 游戏结束
            m_IsGameOver = true;
            // 停止目标旋转
            m_TargetCircleManager.StopRotateSelf();
            // 开始结束协程
            StartCoroutine(GameOver());
        }

        /// <summary>
        /// 游戏结束协程
        /// </summary>
        /// <returns></returns>
        IEnumerator GameOver() {
            while (true)
            {
                // 等待帧最后
                yield return new WaitForEndOfFrame();

                // 更新主Camera 视口
                m_MainCamera.orthographicSize = Mathf.Lerp(m_MainCamera.orthographicSize, m_OrthographicSize,Time.deltaTime * 10);
                // 更新主Camera 背景色
                m_MainCamera.backgroundColor = Color.Lerp(m_MainCamera.backgroundColor, Color.red,Time.deltaTime * 5);
                // 更新主Camera 视口 到位，跳出循环
                if ((m_MainCamera.orthographicSize - m_OrthographicSize)<0.01f)
                {
                    break;
                }
            }

            // 加载当前场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
