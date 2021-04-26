using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shoulderCollision : MonoBehaviour {

    public AudioClip hitSound;
    AudioSource hitSource;

    void Start()
    {
        hitSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter (Collision col)
	{
        if(col.gameObject.tag == "shoulderObject" || col.gameObject.tag == "destroyObj")
        {
            PlayerPrefs.SetInt("Score", (PlayerPrefs.GetInt("Score") + 1));
            //hitSource.PlayOneShot(hitSound, 0.7F);
            //print ("Shoulder block HIT!");
            //Debug.Log("player coordinates: " + transform.position.ToString() + " obj coordinates: " + col.transform.position.ToString());
            if (col.gameObject.name == "endgame")
            {
                Debug.Log(Time.timeSinceLevelLoad);
                Destroy(col.gameObject);
                SceneManager.LoadScene("endScene");
            }
            
            Destroy(col.gameObject);
        }
	}
}
