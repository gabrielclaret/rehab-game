using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

    //botões do main menu
    public Button[] mainMenuBttn = new Button[4];

    //botões dentro do novo jogo
    public Button[] gameMenuBttn = new Button[10];

    //botao de voltar do menu de creditos
    public Button creditMenuBttn; 

    //objetos de marcação para o jogador saber quais botões foram selecionados
    public GameObject[] objClicked = new GameObject[9];

    //public Slider[] sideSlider = new Slider[2]; 

    //botão de jogar
    public Button playButton;

    //slider para escolher o tamanho da musica (segundos)
    public Slider tamSlider;

    //variavel para poder usar o metodo endmanager.updatescore
    fimManager func1 = new fimManager();

    float seconds;

    //objetos dos menus e player
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject creditMenu;
    public GameObject map;
    public GameObject player;

    //objetos dos numeros no tamanho da musica
    public GameObject Centena;
    public GameObject Dezena;
    public GameObject Unidade;

    //private float dirValue;
    //private float esqValue;

    void Start() {

        //botões do main menu 
        mainMenuBttn[0].GetComponent<Button>().onClick.AddListener(delegate { mainMenuOptions(1); }); //botão novo jogo
        mainMenuBttn[1].GetComponent<Button>().onClick.AddListener(delegate { mainMenuOptions(2); }); //botão creditos
        mainMenuBttn[2].GetComponent<Button>().onClick.AddListener(delegate { mainMenuOptions(3); }); //botao sair
        mainMenuBttn[3].GetComponent<Button>().onClick.AddListener(delegate { mainMenuOptions(4); }); //botao kinect

        //botões do menu de novo jogo
        gameMenuBttn[0].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(0); }); //song1
        gameMenuBttn[1].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(1); }); //song2
        gameMenuBttn[2].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(2); }); //assimetrico
        gameMenuBttn[3].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(3); }); //simetrico
        gameMenuBttn[4].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(4); }); //livre
        gameMenuBttn[5].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(5); }); //restrito
        gameMenuBttn[6].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(6); }); //pequena
        gameMenuBttn[7].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(7); }); //media
        gameMenuBttn[8].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(8); }); //grande   
        gameMenuBttn[9].GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(9); }); //botão voltar

        playButton.GetComponent<Button>().onClick.AddListener(delegate { newGameOptions(10); }); //botao jogar

        //botões do menu de opções
        creditMenuBttn.GetComponent<Button>().onClick.AddListener(delegate { creditOptions(0); }); //botão voltar
    }

    void Update()
    {
        seconds = (137 * tamSlider.value)/100;
        PlayerPrefs.SetInt("Tamanho", (int) seconds);
        //Debug.Log(seconds);
        func1.updateScore(Centena, Dezena, Unidade, false);
    }

    void mainMenuOptions(int option)
    {
        mainMenu.SetActive(false);
        switch (option)
        {
            case 1:
                Debug.Log("Novo Jogo");
                PlayerPrefs.SetInt("Score",0);
                player.SetActive(false);
                gameMenu.SetActive(true);
                break;
            case 2:
                Debug.Log("Creditos");
                player.SetActive(false);
                creditMenu.SetActive(true);
                break;
            case 3:
                Debug.Log("Sair");
                gameMenu.SetActive(false);
                Application.Quit();
                break;
            case 4:
                Debug.Log("Kinect");
                SceneManager.LoadScene("kinectScene");
                break;
        }
    }

    void newGameOptions(int option)
    {
        int count = 0;

        switch(option)
        {
            case 0:
                objClicked[1].SetActive(false);
                PlayerPrefs.SetInt("Song", 0);
                break;
            case 1: 
                objClicked[0].SetActive(false);
                PlayerPrefs.SetInt("Song", 1);
                break;
            case 2:
                objClicked[3].SetActive(false);
                PlayerPrefs.SetInt("Simetria", 0); //assimetrico
                break;
            case 3:
                objClicked[2].SetActive(false);
                PlayerPrefs.SetInt("Simetria", 1); //simetrico
                break;
            case 4:
                objClicked[5].SetActive(false);
                PlayerPrefs.SetInt("Modo", 0); //livre
                break;
            case 5:
                objClicked[4].SetActive(false);
                PlayerPrefs.SetInt("Modo", 1); //restrito
                break;
            case 6:
                objClicked[7].SetActive(false);
                objClicked[8].SetActive(false);
                PlayerPrefs.SetInt("Distancia", 0); //pequena 
                break;
            case 7:
                objClicked[6].SetActive(false);
                objClicked[8].SetActive(false);
                PlayerPrefs.SetInt("Distancia", 1); //medio
                break;
            case 8:
                objClicked[6].SetActive(false);
                objClicked[7].SetActive(false);
                PlayerPrefs.SetInt("Distancia", 2); //grande
                break;
            case 9:
                gameMenu.SetActive(false);
                SceneManager.LoadScene("menuScene");
                break;
            case 10:
                SceneManager.LoadScene("gameScene");
                /*dirValue = sideSlider[0].GetComponent<Slider>().value;
                Debug.Log(dirValue);
                esqValue = sideSlider[1].GetComponent<Slider>().value;
                Debug.Log(esqValue);*/
                break;
        }

        if(option < 9)
            objClicked[option].SetActive(true);

        for (int i = 0; i < objClicked.Length; i++)
            if (objClicked[i].activeSelf)
                count++;    

        if (count == 4)
            playButton.gameObject.SetActive(true);
    }

    void creditOptions(int option)
    {
        if (option == 0)
        {
            creditMenu.SetActive(false);
            SceneManager.LoadScene("menuScene");
        }
    }
}
