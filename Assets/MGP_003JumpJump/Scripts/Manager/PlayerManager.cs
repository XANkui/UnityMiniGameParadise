using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_003JumpJump
{ 

	public class PlayerManager 
	{
		private GameObject m_PlayerPrefab;
		private Player m_Player;
		private Transform m_PlayerSpawnPosTrans;
		PlatformManager m_PlatformManager;
		public void Init(Transform playerSpawnPosTrans,PlatformManager platformManager) {
			m_PlayerSpawnPosTrans = playerSpawnPosTrans;
			m_PlatformManager = platformManager;
			m_Player = SpawnPlayer(m_PlayerSpawnPosTrans.position, m_PlayerSpawnPosTrans);
			m_Player.Init(m_PlatformManager);

		}
		public void Update() {

			m_Player.UpdateJumpOperation();
		}

		public void Destroy()
		{
			m_Player.Destroy();
			m_Player = null;
			m_PlayerPrefab = null;
			m_PlatformManager = null;
			m_PlayerSpawnPosTrans = null;
		}

		/// <summary>
		/// Player 是否坠落
		/// </summary>
		/// <returns></returns>
		public bool IsFallen() {
			return m_Player.IsFallen;
		}
		

		/// <summary>
		/// 指定位置生成 Player
		/// </summary>
		/// <param name="pos">位置</param>
		/// <param name="parent">父物体</param>
		/// <returns></returns>
		Player SpawnPlayer(Vector3 pos,Transform parent) {
			Player player = null;

			if (m_PlayerPrefab==null)
            {
				GameObject prefab = Resources.Load<GameObject>(ResPathDefine.PLAYER_RES_PATH);
				if (prefab != null)
				{
					m_PlayerPrefab = (prefab);
				}
				else
				{
					Debug.LogError(GetType() + "/LoadPlatformPrefabs()/ prefab is null, resPath = " + ResPathDefine.PLAYER_RES_PATH);
				}
			}

			player = GameObject.Instantiate<GameObject>(m_PlayerPrefab, pos, Quaternion.identity).AddComponent<Player>();
			player.transform.SetParent(parent);

			return player;
		}


	}
}
