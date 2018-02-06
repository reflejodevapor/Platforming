using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Prinny : MonoBehaviour 
{
	[Header("Variables privadas de Prinny")]
	[Range(0, 10)]
	[SerializeField]
	private int vidas = 5;
	[Range(0, 10)]
	[SerializeField]
	private float potenciaSalto = 5;
	[SerializeField]
	private float velocidad = 5;
	[SerializeField]
	private float MultiplicadorCaida = 3f;
	[SerializeField]
	private float MultiplicadorSaltoLento = 2f;
	[SerializeField]
	private float FuerzaDisparo;
	[Space(20)]
	public Transform GOSalto;
	private Rigidbody2D rb;
	private float DistanciaAtaque = 1.29f;
	private Vector2 AreaAtaque;
	private bool AtaqueAereo = false;
	public Image img_vida;
	private List<GameObject> pool;
	private int nOndas = 4;
	private float dir;




	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> (); //inicializamos la variable del RigidBody
		velocidad = 4.5f;
		potenciaSalto = 8.85f;
		AreaAtaque = new Vector2 (0.93f, 1.72f);
		GetComponents<BoxCollider2D> () [1].size = Vector2.zero;
		pool = new List<GameObject> ();
		for (int i = 0; i < nOndas; i++) 
		{
			GameObject go = (GameObject)Instantiate (Resources.Load("Aereo"));
			go.SetActive (false);
			pool.Add (go);
		}
		FuerzaDisparo = 5;
	}

	GameObject obtenerGOPool()
	{
		for (int i = 0; i < pool.Count; i++) 
			if (!pool [i].activeInHierarchy)
				return pool [i];
		return null;
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
			if (rb.velocity.y != 0) { // o sea que esta en movimiento vertical
				StartCoroutine(PermaneceAire());
				StartCoroutine (LanzaOndas ());
			} 
			else 
			{
				GetComponents<BoxCollider2D> () [1].size = AreaAtaque;
			}
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			GetComponents<BoxCollider2D> () [1].size = Vector2.zero;
		}
		if (AtaqueAereo)
			rb.velocity -= rb.velocity;
	}
	IEnumerator LanzaOndas()
	{
		while (AtaqueAereo) 
		{
			GameObject onda = obtenerGOPool ();
			if (onda != null) {
				onda.transform.position = transform.position;
				onda.transform.rotation = transform.rotation;
				onda.SetActive (true);
				bool d = (GetComponents<BoxCollider2D> () [1].offset.x > 0);
				onda.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((d) ? FuerzaDisparo : FuerzaDisparo * -1, -5), ForceMode2D.Impulse);
				yield return new WaitForSeconds (0.2f);
			} 
			else 
			{
				yield return new WaitForSeconds (0f);
			}

		}
		yield return new WaitForSeconds (0);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Enemigo"))
			other.gameObject.GetComponent<Enemigo> ().RecibeDano ();
	}

	void FixedUpdate ()
	{
		if (AtaqueAereo)
			return;
		dir = Input.GetAxis ("Horizontal");
		transform.Translate (dir * velocidad * Time.deltaTime, 0, 0);
		if (dir > 0) 
		{
			GetComponents<BoxCollider2D> () [1].offset = new Vector2 (DistanciaAtaque, 0);
		} else if (dir < 0) 
		{
			GetComponents<BoxCollider2D> () [1].offset = new Vector2 (DistanciaAtaque * -1, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D _col)
	{
		if (_col.gameObject.CompareTag ("Enemigo")) 
		{
			vidas--;
			img_vida.fillAmount = ((float)vidas / 5);
		}
	}

	IEnumerator PermaneceAire()
	{
		AtaqueAereo = true;
		yield return new WaitForSeconds (0.7f);
		AtaqueAereo = false;
	}
}
