using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
	

	public float moveSpeed = 15;
	public float jumpForce = 1200;

	public AnimationCurve curve;

	public bool isDead = false;
	public AudioSource backgroundTarget;
	public AudioClip deadMusic;

	[Range(0,1)]
	public float sliding = 0.9f;

	IEnumerator bounce() {
		Vector2 pos = transform.position;

		for (float t = 0; t < curve.keys [curve.length - 1].time; t += Time.deltaTime) {

			transform.position = new Vector2 (pos.x, pos.y + curve.Evaluate (t) * 5);

			yield return null;
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {

		if (!isDead) {
			float h = Input.GetAxis ("Horizontal");

			Vector2 v = GetComponent<Rigidbody2D> ().velocity;

			if (h != 0) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (h * moveSpeed, v.y);
				transform.localScale = new Vector2 (Mathf.Sign (h), transform.localScale.y);
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (v.x * sliding, v.y);
			}
			GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs (h));


			if (Input.GetAxis ("Vertical") > 0 && IsGrounded ()) {
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
				GetComponent<AudioSource> ().Play ();
				GetComponent<Animator> ().SetBool ("Jumping", !IsGrounded ());
			}

			if (transform.position.y < -5.5) {
				backgroundTarget.Stop ();
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isDead = true;
				GetComponent<Rigidbody2D> ().isKinematic = true;
				GetComponent<AudioSource> ().PlayOneShot (deadMusic);
			}

		} else {

			StartCoroutine ("bounce");
			if (!GetComponent<AudioSource> ().isPlaying) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}			
	}


	bool IsGrounded() {
		Bounds bounds = GetComponent<Collider2D> ().bounds;
		float range = bounds.size.y * 0.1f;

		Vector2 v = new Vector2 (bounds.center.x, bounds.min.y - range);

		RaycastHit2D hit = Physics2D.Linecast (v, bounds.center);

		return (hit.collider.gameObject != gameObject);
	}
}
