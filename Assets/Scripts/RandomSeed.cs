using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
public class RandomSeed : MonoBehaviour {


	private void Awake() {
		Random.InitState((int)DateTime.Now.Ticks);
	}
}
