using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class handCollision : MonoBehaviour {

    public AudioClip hitSound;
    AudioSource hitSource;


    void Start()
    {
        hitSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter (Collision col)
	{
        if (col.gameObject.tag == "handObject" || col.gameObject.tag == "destroyObj")
        {
            PlayerPrefs.SetInt("Score", (PlayerPrefs.GetInt("Score") + 1));
            //print("Hand block HIT!");
            //Debug.Log("player coordinates: " + transform.position.ToString() + " obj coordinates: " + col.transform.position.ToString());
            //hitSource.PlayOneShot(hitSound, 0.05f);
            Destroy(col.gameObject);
            if (col.gameObject.name == "endgame")
            {
                Debug.Log(Time.timeSinceLevelLoad);
                SceneManager.LoadScene("endScene");
            }
        }  
    }

}
