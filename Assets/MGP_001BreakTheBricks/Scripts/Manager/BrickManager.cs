using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_001BreakTheBricks { 

	/// <summary>
	/// 砖块管理类
	/// </summary>
	public class BrickManager 
	{
		/// <summary>
		/// 砖块容器
		/// </summary>
		private List<GameObject> m_BricksList = new List<GameObject>();

		/// <summary>
		/// 生成砖块矩阵
		/// </summary>
		/// <param name="startPost">砖块的起始位置</param>
		/// <param name="col">砖块的列数</param>
		/// <param name="row">砖块的行数</param>
		/// <param name="distance">砖块的间隔距离</param>
		public void SpawnBricks(GameObject brick,Transform parentTra,Vector3 startPost, int col,int row,float distance) {
            if (brick != null )
            {
				
				GameObject go = null;
				// 根据行列生成 砖块
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
						// 生成砖块，并且设置位置
						go = GameObject.Instantiate(brick, parentTra);
						go.transform.position = new Vector3(startPost.x+ j* distance, startPost.y+ i* distance, startPost.z);
						m_BricksList.Add(go);
					}
                }
            }
		}

		/// <summary>
		/// 销毁砖块
		/// </summary>
		public void DestoryBricks() {
            if (m_BricksList!=null)
            {
				// 依次销毁砖块
                for (int i = m_BricksList.Count-1; i >= 0; i--)
                {
					GameObject.Destroy(m_BricksList[i]);
                }

				m_BricksList.Clear();
            }
		}
	}
}
