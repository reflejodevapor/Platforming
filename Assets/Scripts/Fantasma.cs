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
		transform.position = Vector3.Lerp(origen, destino, Mathf.PingPong(Time.time, 1));
	}
}
