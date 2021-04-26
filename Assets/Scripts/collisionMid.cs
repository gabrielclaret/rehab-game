using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionMid : MonoBehaviour
{

	List<string> colisoes = new List<string> ();

	public GameObject elbowLeft;
	public GameObject elbowRight;

	void Update ()
	{
		
		if (elbowRight.transform.position.y > 0 && elbowRight.transform.position.y < 1.50 && elbowLeft.transform.position.y > 0 && elbowLeft.transform.position.y < 1.50) {
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
