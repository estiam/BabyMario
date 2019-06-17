using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 3;
	Vector2 dir = Vector2.right;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = dir * speed;

	}

	void OnTriggerEnter2D(Collider2D coll) {
		transform.localScale = new Vector2 (-1 * transform.localScale.x, transform.localScale.y);

		dir = new Vector2 (-1 * dir.x, dir.y);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// Collided with BabyMario?
		if (coll.gameObject.name == "BabyMario") {
			// Is the collision above?
			if (coll.contacts [0].point.y > transform.position.y) {
				// ToDo kill self
			} else {
				// ToDo kill BabyMario
			}
		}
	}
}
