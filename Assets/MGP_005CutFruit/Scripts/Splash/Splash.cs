using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class Splash : MonoBehaviour
	{
		private SpriteRenderer m_SpriteRenderer;
		private float m_ColorValue = 0;
		private const float SPEED = 5;
		public SpriteRenderer SpriteRenderer
		{
			get
			{
				if (m_SpriteRenderer == null)
				{
					m_SpriteRenderer = GetComponent<SpriteRenderer>();
				}

				return m_SpriteRenderer;
			}
		}

		public void Show(Color32 color, Action<Splash> showAnimationEndAction)
		{
			StartCoroutine(EffectAnimation(color, showAnimationEndAction));
		}

		IEnumerator EffectAnimation(Color color, Action<Splash> showAnimationEndAction)
		{
			m_ColorValue = 0;
			color.a = 0;
			SpriteRenderer.color = color;
			while (true)
			{
				// lerp 匀速插值处理
				m_ColorValue += 1.0f / SPEED * Time.deltaTime;
				color.a = Mathf.Lerp(color.a, 1, m_ColorValue);
				SpriteRenderer.color = color;
				if ((1 - color.a <= 0.05f))
				{
					color.a = 1;
					SpriteRenderer.color = color;

					break;
				}


				yield return new WaitForEndOfFrame();
			}
			m_ColorValue = 0;
			while (true)
			{
				// lerp 匀速插值处理
				m_ColorValue += 1.0f / SPEED * Time.deltaTime;
				color.a = Mathf.Lerp(color.a, 0, m_ColorValue);
				SpriteRenderer.color = color;
				if ((color.a - 0) <= 0.05f)
				{
					color.a = 0;
					SpriteRenderer.color = color;
					break;
				}


				yield return new WaitForEndOfFrame();
			}


			if (showAnimationEndAction != null)
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
