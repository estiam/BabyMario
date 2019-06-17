using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private bool taken = false;
	void OnTriggerEnter2D(Collider2D coll) {
		// Destroy the coin
		GetComponent<AudioSource>().Play();
		GetComponent<SpriteRenderer> ().enabled = false;
		taken = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (taken && !GetComponent<AudioSource>().isPlaying) {
			Destroy (gameObject);
		}
	}
}
