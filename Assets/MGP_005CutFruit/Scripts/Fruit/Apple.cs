using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class Apple : BaseFruit
	{
       

        public override FruitType FruitType => FruitType.Apple;

        public override FruitBrokenType FruitBrokenType => FruitBrokenType.Apple_Broken;

        public override Color FruitSplahColor => Color.red;

        public override int Score => GameConfig.FRUIT_APPLE_SCORE;
    }
}
