using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class kinectManager : MonoBehaviour {

    public Button Back;
	
	void Start () {
        Back.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("menuScene"); });
    }
}
