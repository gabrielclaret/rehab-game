using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class planeCollision : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "handObject"|| col.gameObject.tag == "shoulderObject" ||  col.gameObject.tag == "elbowObject" || col.gameObject.tag == "destroyObj")
        {

            Destroy(col.gameObject);
            if (col.gameObject.name == "endgame")
            {
                Debug.Log(Time.timeSinceLevelLoad);
                SceneManager.LoadScene("endScene");
            }

            //Debug.Log(Time.timeSinceLevelLoad);
        }
    }
}