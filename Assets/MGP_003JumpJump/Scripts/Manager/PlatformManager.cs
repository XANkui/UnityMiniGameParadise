using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{
	
	/// <summary>
	/// Platform 管理类
	/// </summary>
	public class PlatformManager 
	{

		private List<GameObject> m_PlatformPrefabList;
		private GameObject m_CurPlatformCube;
		public GameObject CurPlatformCube=> m_CurPlatformCube;
		private GameObject m_NextPlatformCube;
		private Dir m_CurDir;
		public Dir CurDir => m_CurDir;
		Action<int> m_OnPlatformEnterAction;
		private Queue<GameObject> m_PlatformsQueue;
		

		public void Init(Action<int> onPlatformEnterAction, Vector3 curPos, int score ,Transform parent = null)
		{
			m_PlatformPrefabList = new List<GameObject>();
			m_PlatformsQueue = new Queue<GameObject>();

			LoadPlatformPrefabs();
			SetOnPlatformCubeEnterAction(onPlatformEnterAction);
			m_NextPlatformCube = Spawn(curPos, score, parent);
			m_CurPlatformCube = m_NextPlatformCube;
			
		}

		public void Update() {
			JudgePlatformQueueCount();
		}

		public void Destroy() {

			m_CurPlatformCube = null;
			m_NextPlatformCube = null;

			while (m_PlatformsQueue.Count>0)
            {
				GameObject.Destroy(m_PlatformsQueue.Dequeue());
			}

			m_PlatformsQueue.Clear();
			m_PlatformsQueue = null;

			m_PlatformPrefabList.Clear();
			m_PlatformPrefabList = null;

			m_OnPlatformEnterAction = null;
		}

		/// <summary>
		/// 加载 Platform 预制体
		/// </summary>
		void LoadPlatformPrefabs() {
            foreach (string resPath in ResPathDefine.PLATFORM_RES_PATH_LIST)
            {
				GameObject prefab = Resources.Load<GameObject>(resPath);
				if (prefab != null)
				{
					m_PlatformPrefabList.Add(prefab);
				}
				else {
					Debug.LogError(GetType()+ "/LoadPlatformPrefabs()/ prefab is null, resPath = " + resPath );
				}
            }
		}

		/// <summary>
		/// 设置平台回调事件（这个主要是分数事件）
		/// </summary>
		/// <param name="onPlatformEnterAction"></param>
		void SetOnPlatformCubeEnterAction(Action<int> onPlatformEnterAction)
		{
			m_OnPlatformEnterAction = onPlatformEnterAction;
		}

		/// <summary>
		/// 随机生成下一个 Platform 方向，并 生成下一个 Platform
		/// </summary>
		/// <param name="curPos">位置</param>
		/// <param name="score">增加的分数</param>
		/// <param name="parent">附载的父物体</param>
		/// <returns></returns>
		GameObject SpawnNext(Vector3 curPos, int score, Transform parent = null)
		{
			int rand = UnityEngine.Random.Range(0, (int)Dir.ENUM_COUNT);
			m_CurDir = (Dir)rand;
			switch (m_CurDir)
			{
				case Dir.Right:
					curPos.x += GameConfig.PLATFRM_CUBE_DISTANCE;

					break;
				case Dir.Forword:
					curPos.z += GameConfig.PLATFRM_CUBE_DISTANCE;
					break;
				case Dir.ENUM_COUNT:
					break;
				default:
					break;
			}

			return Spawn(curPos, score,parent, true);

		}

		/// <summary>
		///  随机生成一个 Platform
		/// </summary>
		/// <param name="curPos">位置</param>
		/// <param name="score">增加的分数</param>
		/// <param name="parent">附载的父物体</param>
		/// <param name="isMoveAnimation">是否进行上升动画</param>
		/// <returns></returns>
		GameObject Spawn(Vector3 pos, int score, Transform parent = null, bool isMoveAnimation = false)
		{
			int randValue = UnityEngine.Random.Range(0, m_PlatformPrefabList.Count);
			GameObject go = GameObject.Instantiate(m_PlatformPrefabList[randValue], parent);
			m_PlatformsQueue.Enqueue(go);
			Platform platform = go.GetComponent<Platform>();
            if (platform==null)
            {
				platform = go.AddComponent<Platform>();
			}

			platform.Init(
				(curPos) => {
					m_CurPlatformCube = m_NextPlatformCube;
					m_NextPlatformCube = SpawnNext(curPos, score, parent);

                    if (m_OnPlatformEnterAction!=null)
                    {
						m_OnPlatformEnterAction.Invoke(score);
                    }
				},
				pos,
				isMoveAnimation);

			return go;
		}

		/// <summary>
		/// 控制场景中 Platform 的数量
		/// 过多则删除最初的 Platform
		/// </summary>
		void JudgePlatformQueueCount() {
            if (m_PlatformsQueue.Count> GameConfig.PLATFORM_EXIST_COUNT)
            {
				GameObject.Destroy(m_PlatformsQueue.Dequeue());
            }
		}
	}
}
