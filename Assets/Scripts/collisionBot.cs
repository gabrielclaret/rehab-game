using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionBot : MonoBehaviour
{

	List<string> colisoes = new List<string> ();

	public GameObject handRight;
	public GameObject handLeft;

	void Update ()
	{
			
		if (handLeft.transform.position.y < 0 && handRight.transform.position.y < 0) {
			SpriteRenderer rend = GetComponent<SpriteRenderer> ();
			Color cor = new Color (0, 1, 0, 0.45f);
			rend.color = cor;
		} else {
			SpriteRenderer rend1 = GetComponent<SpriteRenderer> ();
			Color cor1 = new Color (1, 0, 0, 0.45f);
			rend1.color = cor1;
		}

	}
}
