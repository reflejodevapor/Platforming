using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Prinny : MonoBehaviour 
{
	[SerializeField]
	private float potenciaSalto = 5;
	[SerializeField]
	private float velocidad = 5;
	private Rigidbody2D rb;
	public Transform GOSalto;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> (); //inicializamos la variable del RigidBody
		velocidad = 5;
		potenciaSalto = 6;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float dir = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown (KeyCode.Space) && Physics2D.OverlapPoint(GOSalto.position)) 
		{
			rb.AddForce (Vector2.up * potenciaSalto, ForceMode2D.Impulse);
		}
		transform.Translate (dir * velocidad * Time.deltaTime, 0, 0);
	}
}
