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
	private bool Atacando = false;
	[SerializeField]
	private float MultiplicadorCaida = 3f;
	[SerializeField]
	private float MultiplicadorSaltoLento = 2f;
	[Space(20)]
	public Transform GOSalto;
	public GameObject GOAtaque;
	private Rigidbody2D rb;
	private float DistanciaAtaque = 1.21f;




	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> (); //inicializamos la variable del RigidBody
		velocidad = 4.5f;
		potenciaSalto = 8.85f;
		GOAtaque.GetComponent<BoxCollider2D> ().enabled = false;
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
			StartCoroutine (Ataca ());
		}
	}

	IEnumerator Ataca()
	{
		Atacando = true;
		GOAtaque.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (0.5f);
		GOAtaque.GetComponent<BoxCollider2D> ().enabled = false;
		Atacando = false;
	}

	void FixedUpdate ()
	{
		float dir = Input.GetAxis ("Horizontal");
		transform.Translate (dir * velocidad * Time.deltaTime, 0, 0);
		if (dir > 0)
			GOAtaque.transform.localPosition = new Vector3(DistanciaAtaque, 0, 0);
		else if (dir < 0)
			GOAtaque.transform.localPosition = new Vector3(DistanciaAtaque*-1, 0, 0);
	}
}
