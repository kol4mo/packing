using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{

	[SerializeField] Transform cameraPos;

	private bool isInTruck = false;

	private void OnTriggerEnter2D(Collider2D collision) {
		cameraPos.position += new Vector3(0, 6, 0);
	}


}