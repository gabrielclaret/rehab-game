using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whenSelected : MonoBehaviour {

    public GameObject sel1;

    void Start()
    {
        sel1.SetActive(false);
    }

    void OnMouseEnter()
    {
        sel1.SetActive(true);
    }

    void OnMouseExit()
    {
        sel1.SetActive(false);   
    }

}
