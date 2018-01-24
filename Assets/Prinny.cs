using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Prinny : MonoBehaviour 
{
	[Header("Variables privadas de Prinny")]
	[SerializeField]
	private float potenciaSalto = 5;
	[SerializeField]
	private float velocidad = 5;
	[SerializeField]
	private bool dobleSalto = true;
	[Space(20)]
	public Transform GOSalto;
	private Rigidbody2D rb;



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
		
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (dobleSalto || Physics2D.OverlapPoint (GOSalto.position)) // Manejador de saltos
			{
				rb.velocity = Vector2.zero;
				rb.AddForce (Vector2.up * potenciaSalto, ForceMode2D.Impulse);
				dobleSalto = false;
			}

		}
		if (Physics2D.OverlapPoint (GOSalto.position)) // cuando está tocando el piso activamos el salto doble
			dobleSalto = true;
		
		Vector2 S = GetComponent<SpriteRenderer> ().bounds.size;
		GetComponent<BoxCollider2D> ().size = S;
//		GetComponent<BoxCollider2D> ().offset;
	}

	void FixedUpdate()
	{
		float dir = Input.GetAxis ("Horizontal");
		transform.Translate (dir * velocidad * Time.deltaTime, 0, 0);
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.CompareTag("Walls"))
			rb.velocity = Vector2.zero;
	}

	void OnCollisionStay2D(Collision2D col) 
	{
		if (col.gameObject.CompareTag("Walls"))
			rb.velocity = Vector2.zero;
	}

}
