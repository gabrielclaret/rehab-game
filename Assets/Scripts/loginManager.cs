using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loginManager : MonoBehaviour
{

    public Button Proximo;

    public GameObject fillObj;

    public InputField nome;
    public InputField age;

    // Use this for initialization
    void Start()
    {
        Proximo.GetComponent<Button>().onClick.AddListener(delegate { NextScene(); });
    }

    void NextScene()
    {
        if (nome.text == "" && age.text == "")
        {
            fillObj.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("Nome", nome.text);
            PlayerPrefs.SetString("Idade", age.text);
            SceneManager.LoadScene("menuScene");
        }
    }
}