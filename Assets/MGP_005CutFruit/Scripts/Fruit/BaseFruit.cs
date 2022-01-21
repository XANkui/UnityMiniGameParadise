using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{

    public abstract class BaseFruit : MonoBehaviour
    {
        protected FruitManager m_FruitManager;
        protected SplashManager m_SplashManager;
        protected DataModelManager m_DataModelManager;

        private bool m_IsRecycle = false;

        public abstract FruitType FruitType { get; }
        public abstract FruitBrokenType FruitBrokenType { get; }
        public abstract Color FruitSplahColor { get; }
        public abstract int Score { get; }

        

        public virtual void Init(params object[] objs) {
            m_FruitManager = objs[0] as FruitManager;
            m_SplashManager = objs[1] as SplashManager;
            m_DataModelManager = objs[2] as DataModelManager;
        }

        /// <summary>
        /// 产生破碎的水果抽象方法
        /// </summary>
        public virtual FruitBroken SpawnBroken() { return m_FruitManager.GetFruitBroken(FruitBrokenType); }
        /// <summary>
        /// 产生果汁特效抽象方法
        /// </summary>
        public virtual Splash SpawnSplash() { return m_SplashManager.GetSplash(); }

        private void Update()
        {
            transform.Rotate(Vector3.right, Random.Range(70, 150) * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.name.StartsWith( GameConfig.KNIFE_NAME))
            {
                //增加成绩
                m_DataModelManager.Score.Value += Score;

                for (int i = 0; i < 2; i++)
                {
                    //产生破碎的水果
                    FruitBroken fruitBroken = SpawnBroken();
                    fruitBroken.Init(FruitBrokenType,m_FruitManager);
                    GameObject temp = fruitBroken.gameObject;
                    temp.SetActive(true);
                    temp.transform.position = transform.position;
                    temp.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    //-3 -2  2 3

                    float x, y;
                    int ranX = Random.Range(0, 2);
                    if (ranX == 0)
                    {
                        x = Random.Range(-3.0f, -2.0f);
                    }
                    else
                    {
                        x = Random.Range(2.0f, 3.0f);
                    }

                    int ranY = Random.Range(0, 2);
                    if (ranY == 0)
                    {
                        y = Random.Range(-3.0f, -2.0f);
                    }
                    else
                    {
                        y = Random.Range(2.0f, 3.0f);
                    }

                    temp.GetComponent<Rigidbody>().velocity = new Vector2(x, y);

                }

                //产生果汁特效
                Splash splash = SpawnSplash();
                splash.transform.position = transform.position;
                splash.transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(-180,180));
                splash.Show(FruitSplahColor, m_SplashManager.RecycleSplah);
                

                //隐藏水果物体
                m_FruitManager.RecycleFruit(FruitType,this);
                m_IsRecycle = false;
            }
            if (other.tag == "BottomCollider")
            {
                gameObject.SetActive(false);
            }
        }

        private void OnBecameVisible()
        {
            m_IsRecycle = true;
        }

        private void OnBecameInvisible()
        {
            if (m_IsRecycle==true)
            {
                m_FruitManager.RecycleFruit(FruitType, this);
                m_IsRecycle = false;

            }
        }
    }
}
