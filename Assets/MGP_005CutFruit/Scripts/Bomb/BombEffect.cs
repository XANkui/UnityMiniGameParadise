using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit { 

	public class BombEffect : MonoBehaviour
	{
		private float m_AnimationLength = 1;

		public void Show(Vector3 pos, Action<BombEffect> showAnimationEndAction)
		{
			// 赋值位置和随机旋转，并且定时回收
			transform.position = pos;
			transform.rotation = Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(-180, 180));
			StartCoroutine(Recycle(showAnimationEndAction));

		}

		IEnumerator Recycle(Action<BombEffect> showAnimationEndAction) {
			yield return new WaitForSeconds(m_AnimationLength);
            if (showAnimationEndAction!=null)
            {
				showAnimationEndAction.Invoke(this);

			}
		}

        private void OnDisable()
        {
			StopAllCoroutines();

		}
	}
}
