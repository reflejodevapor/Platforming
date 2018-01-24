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
	[Space(20)]
	public Transform GOSalto;
	private Rigidbody2D rb;
	[SerializeField]
	private float MultiplicadorCaida = 2.5f;
	[SerializeField]
	private float MultiplicadorSaltoLento = 2f;



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
		//Vector2 S = GetComponent<SpriteRenderer> ().bounds.size;
		//GetComponent<BoxCollider2D> ().size = S; 
		//GetComponent<BoxCollider2D> ().offset;
	}

	void FixedUpdate ()
	{
		float dir = Input.GetAxis ("Horizontal");
		transform.Translate (dir * velocidad * Time.deltaTime, 0, 0);
	}
}
