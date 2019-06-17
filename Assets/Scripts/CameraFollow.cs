using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform target;

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {
		transform.position = new Vector3(target.position.x,
			target.position.y > 5 ? target.position.y : transform.position.y,
			transform.position.z);
	}
}
