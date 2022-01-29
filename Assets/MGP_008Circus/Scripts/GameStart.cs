﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_008Circus { 

	public class GameStart : MonoBehaviour
	{
        private void Awake()
        {
            GameManager.Instance.Awake(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.Start();

        }

        // Update is called once per frame
        void Update()
        {
            GameManager.Instance.Update();
        }

        private void OnDestroy()
        {
            GameManager.Instance.Destroy();
        }
    }
}
