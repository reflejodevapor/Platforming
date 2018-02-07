using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : Enemigo {
	Vector3 origen, destino;
	float stTime, duracion;


	// Use this for initialization
	void Awake () {
		origen = transform.position;
		destino = origen + (Vector3.right * 3);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Set our position as a fraction of the distance between the markers.
		float t = Mathf.PingPong(Time.time, 1);
		if (t == 1)
			GetComponent<SpriteRenderer> ().flipX = false;
		else if (t == 0)
			GetComponent<SpriteRenderer> ().flipX = true;
		transform.position = Vector3.Lerp(origen, destino, t);

	}
}
