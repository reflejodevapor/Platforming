using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWin : MonoBehaviour 
{

	public Image win;
	public bool ganastes = false;

	void Start()
	{
		win.enabled = false;
	}

	void Update()
	{
		if (ganastes) 
		{
			if (Time.timeScale > 0.2f)
				Time.timeScale -= 0.01f;
			else if (Time.timeScale < 0.2f)
				Time.timeScale = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		win.enabled = true;
		ganastes = true;
	}
}
