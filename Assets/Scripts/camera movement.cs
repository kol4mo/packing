using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{

	[SerializeField] Transform cameraPos;
	[SerializeField] Transform player;
	private bool isInTruck = false;

	private void Update() {
		if (player.position.y > 3.25) {
			cameraPos.position = new Vector3(0, 6, 0);
		} else {
			cameraPos.position = new Vector3(0, 0, 0);
		}
	}



}
