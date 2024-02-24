using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
public class RandomSeed : MonoBehaviour {
	public string gameSeed = "Default";
	public int currentSeed = 0;

	private void Awake() {
		currentSeed = gameSeed.GetHashCode();
		Random.InitState(currentSeed);
	}
}
