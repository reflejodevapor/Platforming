using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Prinny : MonoBehaviour 
{
	[Header("Variables privadas de Prinny")]
	[Range(0, 10)]
	[SerializeField]
	private float potenciaSalto = 5;
	[SerializeField]
	private float velocidad = 5;
	[SerializeField]
	private float MultiplicadorCaida = 3f;
	[SerializeField]
	private float MultiplicadorSaltoLento = 2f;
	[Space(20)]
	public Transform GOSalto;
	private Rigidbody2D rb;
	private float DistanciaAtaque = 1.29f;
	private Vector2 AreaAtaque;




	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> (); //inicializamos la variable del RigidBody
		velocidad = 4.5f;
		potenciaSalto = 8.85f;
		AreaAtaque = new Vector2 (0.93f, 1.72f);
		GetComponents<BoxCollider2D> () [1].size = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (Physics2D.OverlapPoint (GOSalto.position)) // Manejador de saltos
			{
				rb.velocity = Vector2.up * potenciaSalto;
			} //aplicamos el salto
		}
		if (rb.velocity.y < 0) 
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (MultiplicadorCaida - 1) * Time.deltaTime;
		} 
		else if (rb.velocity.y > 0 && !Input.GetKey (KeyCode.Space)) 
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (MultiplicadorSaltoLento - 1) * Time.deltaTime;
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			GetComponents<BoxCollider2D> () [1].size = AreaAtaque;
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			GetComponents<BoxCollider2D> () [1].size = Vector2.zero;
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Enemigo"))
			Debug.Log ("HIT " + other.gameObject.name);
	}

	void FixedUpdate ()
	{
		float dir = Input.GetAxis ("Horizontal");
		transform.Translate (dir * velocidad * Time.deltaTime, 0, 0);
		if (dir > 0) 
		{
			GetComponents<BoxCollider2D> () [1].offset = new Vector2 (DistanciaAtaque, 0);
		} else if (dir < 0) 
		{
			GetComponents<BoxCollider2D> () [1].offset = new Vector2 (DistanciaAtaque * -1, 0);
		}
	}
}
