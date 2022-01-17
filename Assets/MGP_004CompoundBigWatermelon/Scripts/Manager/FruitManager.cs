using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class FruitManager : IManager
	{
		private List<GameObject> m_FuritPrefabList;

		private Transform m_SpawnFruitPosTrans;
		private Transform m_MainCameraTrans;
		private Camera m_MainCamera;

		private EffectManager m_EffectManager;
		private AudioManager m_AudioManager;
		private ScoreManager m_ScoreManager;

		private Fruit m_CurFruit;
		public Fruit CurFruit=> m_CurFruit;


		private float m_FruitXLeftLimitValue;
		private float m_FruitXRightLimitValue;

		private bool m_IsFalled = false;
		private bool m_IsCanSpawn = false;
		private MonoBehaviour m_Mono;


		public void Init(Transform worldTrans, Transform uiTrans, params object[] manager)
        {
			m_FuritPrefabList = new List<GameObject>();
			LoadFruitsPrefab();
			m_SpawnFruitPosTrans = worldTrans.Find(GameObjectPathInSceneDefine.SPAWN_FRUIT_POS_TRANS_PATH);
			m_MainCameraTrans = worldTrans.Find(GameObjectPathInSceneDefine.Main_Camera_TRANS_PATH);
			m_MainCamera = m_MainCameraTrans.GetComponent<Camera>();
			m_FruitXLeftLimitValue = Tools.ScreenPosToWorldPos(m_SpawnFruitPosTrans, m_MainCamera, Vector2.zero).x;
			m_FruitXRightLimitValue = Tools.ScreenPosToWorldPos(m_SpawnFruitPosTrans, m_MainCamera, Vector2.right * Screen.width).x;
			m_Mono = manager[0] as MonoBehaviour;
			m_EffectManager = manager[1] as EffectManager;
			m_AudioManager = manager[2] as AudioManager;
			m_ScoreManager = manager[3] as ScoreManager;
			m_Mono.StartCoroutine(SpawnRandomFruit(m_SpawnFruitPosTrans.position, m_SpawnFruitPosTrans));

		}

        public void Update()
        {
			UpdateFruitOperation();

		}

        public void Destroy()
        {
            // 清空生成的水果
            if (m_SpawnFruitPosTrans!=null)
            {
				int childCount = m_SpawnFruitPosTrans.childCount;
				for (int i = (childCount - 1); i >= 0; i--)
				{
					GameObject.Destroy(m_SpawnFruitPosTrans.GetChild(i));
				}
			}			

			m_FuritPrefabList.Clear();
			m_SpawnFruitPosTrans = null;
			m_Mono = null;
			m_EffectManager = null;
			m_AudioManager = null;
		}

		/// <summary>
		/// 游戏结束的时候禁用水果的重力和碰撞
		/// </summary>
		public void OnGameOver() {
            foreach (Transform item in m_SpawnFruitPosTrans)
            {
				Fruit fruit = item.GetComponent<Fruit>();
                if (fruit!=null)
                {
					fruit.DisableFruit();

				}

			}
		}

		/// <summary>
		/// 
		/// </summary>
		void LoadFruitsPrefab() {
			m_FuritPrefabList.Clear();
			// FruitSeriesType 枚举的名字和预制体名字一致
			for (FruitSeriesType i = 0; i < FruitSeriesType.SUM_COUNT; i++)
			{
				Debug.Log(GetType() + "/LoadFruitsPrefab()/ " + i);
				string path = ResPathDefine.FRUITS_PREFAB_BASE_PATH + i;
				GameObject fruit = Resources.Load<GameObject>(path);
				if (fruit == null)
				{
					Debug.LogError(GetType()+ "/LoadFruitsPrefab()/fruit is null, path =  "+ path);
				}
				else {
					m_FuritPrefabList.Add(fruit);
				}
			}
		}

		/// <summary>
		/// 监控鼠标情况，进行水果移动和释放
		/// </summary>
		void UpdateFruitOperation()
		{


			if (Input.GetMouseButtonDown(0))
			{

			}
			else if (Input.GetMouseButton(0))
			{
				UpdateCurFuritPos();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				m_IsFalled = true;
				if (m_CurFruit != null)
				{
					m_CurFruit.Fall();

				}

				if (m_IsCanSpawn == true)
				{
					m_Mono.StartCoroutine(SpawnRandomFruit(m_SpawnFruitPosTrans.position, m_SpawnFruitPosTrans));

				}
			}

		}


		/// <summary>
		/// 写成生成水果
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		IEnumerator SpawnRandomFruit(Vector2 pos, Transform parent)
		{
			m_IsCanSpawn = false;
			yield return new WaitForSeconds(GameConfig.FRUIT_SPAWN_INTERVAL_TIME);
			if (GameManager.Instance.GameOver == false)
			{
				int random = Random.Range(0, (int)((int)FruitSeriesType.SUM_COUNT / 2));
				m_CurFruit = SpawnFruit((FruitSeriesType)random, pos, parent);
				m_IsFalled = false;
				m_IsCanSpawn = true;
			}

		}

		/// <summary>
		/// 生成水果(非合成水果)
		/// </summary>
		/// <param name="fruitSeriesType"></param>
		/// <param name="pos"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		Fruit SpawnFruit(FruitSeriesType fruitSeriesType, Vector2 pos, Transform parent)
		{
			m_AudioManager.PlaySpawnSound();
			return SpawnFruit(fruitSeriesType, pos, parent, false);
		}

		/// <summary>
		/// 生成水果(可合成水果)
		/// </summary>
		/// <param name="fruitSeriesType"></param>
		/// <param name="pos"></param>
		/// <param name="parent"></param>
		/// <param name="isCompound"></param>
		/// <returns></returns>
		Fruit SpawnFruit(FruitSeriesType fruitSeriesType, Vector2 pos, Transform parent, bool isCompound)
		{

			GameObject go = GameObject.Instantiate(m_FuritPrefabList[(int)fruitSeriesType], parent);
			Fruit fruit = go.AddComponent<Fruit>();
			fruit.Init(fruitSeriesType, OnCompoundAction);
			if (isCompound == true)
			{
				fruit.EnableFruit();
			}

			fruit.transform.position = pos;
			fruit.transform.localScale *= GameConfig.FRUIT_SCALE;

			return fruit;
		}

		/// <summary>
		/// 鼠标操作时，实时更新对应的当前的水果位置
		/// </summary>
		private void UpdateCurFuritPos()
		{
			if (m_CurFruit != null && m_IsFalled == false)
			{
				Vector3 pos = Tools.ScreenPosToWorldPos(m_CurFruit.transform, m_MainCamera, Input.mousePosition);
				m_CurFruit.transform.position = ClampFruitPos(pos);

				
			}
		}

		/// <summary>
		/// 移动水果的时候，水平位置的限制
		/// </summary>
		/// <param name="pos"></param>
		/// <returns>限制后的水果位置</returns>
		private Vector3 ClampFruitPos(Vector3 pos)
		{
			if (m_CurFruit != null)
			{
				if (pos.x - m_CurFruit.GetRadius() < m_FruitXLeftLimitValue)
				{
					pos.x = m_FruitXLeftLimitValue + m_CurFruit.GetRadius();

				}

				if (pos.x + m_CurFruit.GetRadius() > m_FruitXRightLimitValue)
				{
					pos.x = m_FruitXRightLimitValue - m_CurFruit.GetRadius();

				}

				pos.y = m_CurFruit.transform.position.y;
			}

			return pos;
		}

		/// <summary>
		/// 可合成水果的委托
		/// </summary>
		/// <param name="col"></param>
		/// <param name="toColFruit"></param>
		void OnCompoundAction(Collision2D col, Fruit toColFruit)
		{

			if (col.transform.position.y < toColFruit.transform.position.y)  // 同一水平位置Y比较难判断,分开判断
			{
				CompoundHandle(col, toColFruit);
			}
			else if (col.transform.position.y == toColFruit.transform.position.y)
			{
				if (col.transform.position.x < toColFruit.transform.position.x)
				{
					CompoundHandle(col, toColFruit);
				}

			}

		}

		/// <summary>
		/// 满足合成条件的处理
		/// </summary>
		/// <param name="col"></param>
		/// <param name="toColFruit"></param>
		void CompoundHandle(Collision2D col, Fruit toColFruit)
		{
			Debug.Log($"融合 {toColFruit.FruitType.ToString()} ============");
			int colFruitId = (int)toColFruit.FruitType;
			FruitSeriesType compoundFruitType = (FruitSeriesType)(colFruitId + 1);

			SpawnFruit(compoundFruitType,
				col.contacts[0].point, // 碰撞点
				m_SpawnFruitPosTrans,
				true).Rigidbody2D.angularVelocity = toColFruit.Rigidbody2D.angularVelocity; // 附上角速度

			// 当前是直接销毁，好一点的方式的建立对象池，循环利用
			GameObject.Destroy(col.gameObject);
			GameObject.Destroy(toColFruit.gameObject);


			m_EffectManager.ShowEffect(new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), 1),
				col.contacts[0].point);// 碰撞点

			m_AudioManager.PlayBombSound();

			m_ScoreManager.Score += colFruitId * GameConfig.COMPOUND_FRUIT_BASE_SCORE;

		}

	}
}
