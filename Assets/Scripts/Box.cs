using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public AnimationCurve curve;

	public GameObject spawnPrefab;

	public GameObject nextPrefab;

	private bool RunningAnim = false;

	public bool doDestroy = true;

	IEnumerator bounce() {
		Vector2 pos = transform.position;

		if (!RunningAnim) {
			RunningAnim = true;
			for (float t = 0; t < curve.keys [curve.length - 1].time; t += Time.deltaTime) {
				
				transform.position = new Vector2 (pos.x, pos.y + curve.Evaluate (t));

				yield return null;
			}
			RunningAnim = false;

		} else
			yield return null;

		if (spawnPrefab)
			Instantiate (spawnPrefab, transform.position + Vector3.up, Quaternion.identity);

		if (nextPrefab)
			Instantiate (nextPrefab, transform.position, Quaternion.identity);
		if(doDestroy)
			Destroy (gameObject);

	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.contacts [0].point.y < transform.position.y) {
			StartCoroutine ("bounce");
		}	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
