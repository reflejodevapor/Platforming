using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Disparo : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		gameObject.SetActive (false);

	}
}
