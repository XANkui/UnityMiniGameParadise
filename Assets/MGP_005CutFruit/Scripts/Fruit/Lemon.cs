using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class Lemon : BaseFruit
	{
		public override FruitType FruitType => FruitType.Lemon;

		public override FruitBrokenType FruitBrokenType => FruitBrokenType.Lemon_Broken;

		public override Color FruitSplahColor => Color.yellow;

		public override int Score => GameConfig.FRUIT_LEMON_SCORE;
	}
}
