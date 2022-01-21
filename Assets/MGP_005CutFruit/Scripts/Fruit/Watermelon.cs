using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGP_005CutFruit
{ 

	public class Watermelon : BaseFruit
	{
		public override FruitType FruitType => FruitType.Watermelon;

		public override FruitBrokenType FruitBrokenType => FruitBrokenType.Watermelon_Broken;

		public override Color FruitSplahColor => Color.red;

		public override int Score => GameConfig.FRUIT_WATERMELON_SCORE;
	}
}
