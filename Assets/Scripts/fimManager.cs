using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fimManager : MonoBehaviour {

    public GameObject Centena;
    public GameObject Dezena;
    public GameObject Unidade;

    private int length;
    //int first = 0;
    //int second = 0;
    //int third = 0;

    //botões do main menu
    public Button Back;
    public Button Retry;

    void Start()
    {
        Back.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("menuScene"); });
        Retry.GetComponent<Button>().onClick.AddListener(delegate {
            SceneManager.LoadScene("gameScene");
            PlayerPrefs.SetInt("Score", 0);
        });
        //PlayerPrefs.SetInt("Score", 992);
        updateScore(Centena, Dezena, Unidade, true);
    }

    public void updateScore(GameObject c, GameObject d, GameObject u, bool isScore)
    {
        int primeiro;
        int segundo;
        int terceiro;
        int score = 0;
        if (isScore)
            score = PlayerPrefs.GetInt("Score");
        else if (!isScore)
            score = PlayerPrefs.GetInt("Tamanho");
        
        primeiro = 0;
        segundo = 0;
        terceiro = 0;
        if (score < 10)
        {
            primeiro = score;
        }
        else if (score < 100 && score > 9)
        {
            primeiro = score/10;
            primeiro = score - (primeiro * 10);
            segundo = score/10;
        }
        else if (score > 99)
        {
            primeiro = score/100;
            segundo = (score - (primeiro*100))/10;
            terceiro = primeiro;
            primeiro = score - (terceiro * 100) - (segundo * 10);
        }

        for(int i = 0; i < 10; i++)
        {
            c.transform.GetChild(i).gameObject.SetActive(false);
            d.transform.GetChild(i).gameObject.SetActive(false);
            u.transform.GetChild(i).gameObject.SetActive(false);
        }

        c.transform.GetChild(terceiro).gameObject.SetActive(true);
        d.transform.GetChild(segundo).gameObject.SetActive(true);
        u.transform.GetChild(primeiro).gameObject.SetActive(true);
    }

}
