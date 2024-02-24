using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class PathFollower : MonoBehaviour {
	bool isConveyer = true;
	
	[SerializeField] public SplineContainer splineContainer;
	[Range(0,40)] public float speed = 1;
	[SerializeField] public List<Transform> positions;
	[Range(0,1)] public float Tdistance; // distance along spline (0-1)

	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("pickup")) {
			isConveyer = false;
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("pickup")) {
			isConveyer = false;
		}
	}
	//public float speed {  get; set; }

	public float length { get { return splineContainer.CalculateLength(); } }
	public float distance { 
		get { 
			return Tdistance * length;
		}
		set { Tdistance = value / length; 
		}
	}

	private void Start() {
		//speed = maxSpeed;
	}

	private void Update() {
		if (isConveyer) {
			distance += speed * Time.deltaTime;
			updateTransform(math.frac(Tdistance));
		}

		if (Tdistance >= 1) {
			Destroy(gameObject);
		}
	}

	private void updateTransform(float t) {
		Vector3 position = splineContainer.EvaluatePosition(t);
		Vector3 upVector = splineContainer.EvaluateUpVector(t);
		Vector3 forward = Vector3.Normalize(splineContainer.EvaluateTangent(t));
		Vector3 right = Vector3.Cross(upVector, forward);

		transform.position = position;
		//transform.rotation = Quaternion.LookRotation(forward, new Vector3(0,1,0));
	}
}
