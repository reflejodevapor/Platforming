using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemigo : MonoBehaviour 
{
	public int vida = 8;
	[SerializeField]
	bool being_hitted = false;
	Color original, otro = Color.blue;

	// Use this for initialization
	void Start () 
	{
		gameObject.tag = "Enemigo";
		original = gameObject.GetComponent<SpriteRenderer> ().color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (being_hitted) 
		{
			Color lerped = Color.Lerp (original, otro, Mathf.PingPong(Time.time, 0.5f));
			gameObject.GetComponent<SpriteRenderer> ().color = lerped;
		}
	}

	public void RecibeDano()
	{
		vida--;
		StartCoroutine (HitAnimation ());
		if (vida < 1)
			Destroy (gameObject);
	}

	IEnumerator HitAnimation()
	{
		being_hitted = true;
		yield return new WaitForSeconds (1);
		being_hitted = false;
		gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
