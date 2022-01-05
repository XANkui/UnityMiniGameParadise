using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MGP_001BreakTheBricks
{ 

	public class GameStart : MonoBehaviour
	{
		[Header("砖块")]
		[SerializeField] // 显示在面板上
		GameObject Brick;

		[Header("砖块父物体")]
		[SerializeField] // 显示在面板上
		Transform BrickParent;

		[Header("砖块起始位置")]
		[SerializeField] // 显示在面板上
		Transform BrickStartPosition;

		[Header("砖块列数")]
		[SerializeField] // 显示在面板上
		int BrickCol=10;

		[Header("砖块行数")]
		[SerializeField] // 显示在面板上
		int BrickRow = 10;

		[Header("砖块间距")]
		[SerializeField] // 显示在面板上
		float BrickDistance = 1.0f;

		[Header("射击球")]
		[SerializeField] // 显示在面板上
		GameObject Ball;

		[Header("球父物体")]
		[SerializeField] // 显示在面板上
		Transform BallParent;

		[Header("球发射速度")]
		[SerializeField] // 显示在面板上
		float BallShootSpeed = 50.0f;

		/// <summary>
		/// GameStart 单例
		/// </summary>
		private GameStart m_Instance;
		public GameStart Instance => m_Instance;

		/// <summary>
		/// 砖块管理类实例
		/// </summary>
		private BrickManager m_BrickManager;

		/// <summary>
		/// 球管理类实例
		/// </summary>
		private BallManager m_BallManager;

		/// <summary>
		/// Main Camera 
		/// </summary>
		private Camera m_MainCamera;

		private void Awake()
        {
			// 实例化参数
			m_Instance = this;
			m_BrickManager = new BrickManager();
			m_BallManager = new BallManager();
			m_MainCamera = Camera.main;
		}

		// Start is called before the first frame update
		void Start()
		{
			// 生成砖块
			m_BrickManager.SpawnBricks(Brick,BrickParent,BrickStartPosition.position,
				BrickCol,BrickRow,BrickDistance);

		}

		// Update is called once per frame
		void Update()
		{
			// 鼠标按下，发射球
            if (Input.GetMouseButtonDown(0))
            {
				// 计算当前鼠标在屏幕的位置，得到球的发射目标方向
				Vector3 forwardDir = (Tools.MousePosScreenToWorldPos(BrickParent,m_MainCamera)-m_MainCamera.transform.position).normalized;
				
				// 生成并发射球体
				m_BallManager.ShootBall(Ball, BallParent,m_MainCamera.transform.position, forwardDir, BallShootSpeed);

			}

			// 按下空格键，重新加载当前游戏
            if (Input.GetKeyDown(KeyCode.Space))
            {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            }
		}

		// Unity 周期函数 销毁的时候调用
        private void OnDestroy()
        {
			m_BrickManager.DestoryBricks();
			m_BallManager.DestoryBalls();

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
				"操作说明：\n1、鼠标位置是球发射的目标方向；\n2、点击鼠标左键发射球体；\n3、点击空格键重新开始游戏；", 
				fontStyle);

		}
    }
}
