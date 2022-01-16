using System;
using UnityEngine;

namespace MGP_004CompoundBigWatermelon
{ 

	public class Fruit : MonoBehaviour
	{
        private Rigidbody2D m_Rigidbody2D;
        private CircleCollider2D m_CircleCollider2D;
        private FruitSeriesType m_FruitSeriesType;
        private Action<Collision2D, Fruit> m_OnCompoundAction;

        public FruitSeriesType FruitType => m_FruitSeriesType;


        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (m_Rigidbody2D == null)
                {
                    m_Rigidbody2D = GetComponent<Rigidbody2D>();
                }

                return m_Rigidbody2D;
            }
        }

        public CircleCollider2D CircleCollider2D
        {
            get
            {
                if (m_CircleCollider2D == null)
                {
                    m_CircleCollider2D = GetComponent<CircleCollider2D>();
                }

                return m_CircleCollider2D;
            }
        }


        public void Init(FruitSeriesType fruitSeriesType, Action<Collision2D, Fruit> onCompoundAction)
        {
            DisableFruit();
            m_FruitSeriesType = fruitSeriesType;
            m_OnCompoundAction = onCompoundAction;
        }

        public void EnableFruit()
        {
            Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            CircleCollider2D.enabled = true;
        }

        public void DisableFruit()
        {
            Rigidbody2D.bodyType = RigidbodyType2D.Static;
            CircleCollider2D.enabled = false;
        }

        public void Fall()
        {
            EnableFruit();

        }

        public float GetRadius()
        {
            return CircleCollider2D.radius;
        }

        #region Unity Function

        /// <summary>
        /// 碰撞检测函数
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 不是大西瓜，则合成
            if (this.FruitType != FruitSeriesType.BigWatermelon)
            {
                // 判断与之相碰的也是水果
                Fruit otherRuit = collision.gameObject.GetComponent<Fruit>();
                if (otherRuit != null)
                {
                    // 水果类型相同，才触发合成新瓜
                    if (otherRuit.FruitType == this.FruitType)
                    {
                        if (m_OnCompoundAction != null)
                        {
                            m_OnCompoundAction.Invoke(collision, this);
                        }

                    }
                }
            }
        }

        #endregion

    }
}
