using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_002FlyPin { 

	public class PinHead : MonoBehaviour
	{
       /// <summary>
       /// 监听两个针是否插在靠近位置相撞
       /// 相撞则发送游戏结束消息
       /// </summary>
       /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 两个 PinHead 是否碰撞
            if (collision.name.Equals(ConstStr.PIN_HEAD_NAME))
            {
                // 相撞则发送游戏结束消息
                SimpleMessageCenter.Instance.SendMsg(MsgType.GameOver);
            }
        }

     
    }
}
