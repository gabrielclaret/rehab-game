using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameSceneManager : MonoBehaviour {

    //botoes do pause
    public Button voltarButton;
    public Button resumeButton;

    fimManager func = new fimManager();

    //musicas
    public AudioSource[] audioSong = new AudioSource[2];

    public GameObject Sounds;
    public GameObject Song;
    public GameObject pausedMenu;
    public GameObject[] selects = new GameObject[2];

    //objetos para os numeros do score
    public GameObject Centena;
    public GameObject Dezena;
    public GameObject Unidade;

    public GameObject[] BeatMap = new GameObject[2]; //0 - assimetrico 

    //objeto para o fazer a troca de cena para o fim de jogo
    public GameObject endGame;

    private bool paused = false;
    
    void Start ()
    {
        voltarButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("menuScene"); });
        resumeButton.GetComponent<Button>().onClick.AddListener(delegate { paused = false; });
        endGame.transform.position = new Vector3(0,0, 10 + (PlayerPrefs.GetInt("Tamanho")) * 5);
        int simetria = PlayerPrefs.GetInt("Simetria");
        BeatMap[simetria].SetActive(true);
        Sounds.SetActive(true);
        //changeMap();
    }

    void Update()
    {
        int aux = PlayerPrefs.GetInt("Song");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < selects.Length; i++)
                selects[i].SetActive(false);

            if (!paused)
                paused = true;
            else
                paused = false;
        }
        if (!paused)
        {
            audioSong[aux].UnPause();
            Time.timeScale = 1;
            pausedMenu.SetActive(false);
        }
        else if (paused)
        {
            audioSong[aux].Pause();
            Time.timeScale = 0;
            pausedMenu.SetActive(true);
        }

        if (pausedMenu.activeSelf)
        {
            audioSong[aux].Pause();
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown("k")|| Input.GetKeyDown("j")||Input.GetKeyDown("f"))
        { 
            Debug.Log("time: " + Time.timeSinceLevelLoad);
        }
        func.updateScore(Centena, Dezena, Unidade, true);
    }
    /*
    void changeMap()
    {
        int musica = PlayerPrefs.GetInt("Song");
        int modo = PlayerPrefs.GetInt("Modo");
        int simetria = PlayerPrefs.GetInt("Simetria");
        int dificuldade = PlayerPrefs.GetInt("Dificuldade");

        Sounds[musica].SetActive(true);

        PlayerPrefs.SetInt("Score",0);

        Song.transform.GetChild(musica).gameObject.SetActive(true);
        Song.transform.GetChild(musica).GetChild(modo).gameObject.SetActive(true);

        if (dificuldade == 0)
        {
            Song.transform.GetChild(musica).GetChild(modo).GetChild(1).gameObject.SetActive(false);
            Song.transform.GetChild(musica).GetChild(modo).GetChild(2).gameObject.SetActive(false);
        }
        else if(dificuldade == 1)
        {
            Song.transform.GetChild(musica).GetChild(modo).GetChild(2).gameObject.SetActive(false);
        }

        Debug.Log(Song.transform.GetChild(musica).name);
        Debug.Log(Song.transform.GetChild(musica).GetChild(modo).name);

    }*/
}
