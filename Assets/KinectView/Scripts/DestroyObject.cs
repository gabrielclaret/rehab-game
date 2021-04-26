using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		//print ("COLIDIU");
		//if (col.gameObject.name == "objCollider") {
        if(col.gameObject.tag == "Destroy") { 
			print ("Coordinates:");
            Debug.Log("player coordinates: " + transform.position.ToString() + " obj coordinates: " + col.transform.position.ToString());
			Destroy(col.gameObject);
		}
	}
}
