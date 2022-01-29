using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus
{


    /// <summary>
    /// 对象池中的对象使用情况
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolContainer<T>
    {
        // 获取实例
        private T item;

        public T Item { get => item; set => item = value; }


        // 是否使用
        public bool Used { get; private set; }

        /// <summary>
        /// 使用该对象池的对象
        /// </summary>
        public void Consume()
        {
            Used = true;
        }

        /// <summary>
        /// 释放对象池中的对象
        /// </summary>
        public void Release()
        {

            Used = false;
        }
    }
}
