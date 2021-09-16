using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStart;
    public bool gameEnd;
    public bool gameoff;
    public bool playerIn;


    private static GameManager _instance;

    public static GameManager Instance {
        get {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해줌
            if (!_instance) {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public Transform [] target;
    public Transform[] targets;
    public Transform[] playerTargets;

    public AI [] ai;
    public AIS [] ais;

    private float minAiSpeed = 37.5f;
    private float maxAiSpeed = 55f;

    public PlayerTrack player;
    private PlayerTrack playerCheck;

    [SerializeField]
    TextMeshProUGUI counts;
    
    private void Awake() {

        if (_instance == null) {
            _instance = this;
        }

        else if (_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SpeedSet();
        gameEnd = false;
    }

    void SpeedSet() {
        for(int i  = 0; i < ai.Length; i++) {
            ai[i].carSpeed = Random.Range(minAiSpeed, maxAiSpeed);
        }
        for(int i = 0; i < ais.Length; i++) {
            ais[i].carSpeed = Random.Range(minAiSpeed, maxAiSpeed);
        }
    }

    void Ai_On() {

        for(int i = 0; i < ai.Length; i++) {
            ai[i].transform.gameObject.GetComponent<AI>().enabled = true;
        }
        for (int i = 0; i < ais.Length; i++) {
            ais[i].transform.gameObject.GetComponent<AIS>().enabled = true;
        }
    }

    void Ai_Off() {
        for (int i = 0; i < ai.Length; i++) {
            ai[i].transform.gameObject.GetComponent<AI>().enabled = false;
        }
        for (int i = 0; i < ais.Length; i++) {
            ais[i].transform.gameObject.GetComponent<AIS>().enabled = false;
        }
    }

    void PlayerSetOn() {
        player.gameObject.GetComponent<Bicycle_movement>().enabled = true;
        player.gameObject.GetComponent<PlayerTrack>().enabled = true;
    }

    void PlayerSetOff() {
        player.gameObject.GetComponent<Bicycle_movement>().enabled = false;
        player.gameObject.GetComponent<PlayerTrack>().enabled = false;
    }

    void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update() {
        if (gameoff) {
            StartCoroutine(GameEnd());
            gameoff = false;
        }
    }
    IEnumerator GameStart() {
        gameStart = true;
        Debug.Log("게임시작");
        counts.gameObject.SetActive(true);
        counts.text = "3";
        yield return new WaitForSeconds(1);
        counts.text = "2";
        yield return new WaitForSeconds(1);
        counts.text = "1";
        yield return new WaitForSeconds(1);
        counts.text = "Start!";
        yield return new WaitForSeconds(0.3f);
        counts.gameObject.SetActive(false);
        Ai_On();
        PlayerSetOn();
        yield return null;
    }

    IEnumerator GameEnd() {
        counts.gameObject.SetActive(true);
        if (playerIn) {
            counts.text = "WIN";
        }
        else 
        {
            for (int i = 10; i >= 0; i--) 
            {
                if (playerIn == false)
                    counts.text = i.ToString();
                 yield return new WaitForSeconds(1f);
            }
            if (playerIn == false)
                counts.text = "Game Over";
            else
                counts.text = "Game End";
            Ai_Off();
            PlayerSetOff();
            gameEnd = true;
            SceneManager.LoadScene("Result");
            StartCoroutine(GameResult());
        }
    }

    IEnumerator GameResult() {

        TextMeshProUGUI[] names = new TextMeshProUGUI[Rank.instance.ranks.Length];
        TextMeshProUGUI[] times = new TextMeshProUGUI[Rank.instance.ranks.Length];

        for(int i = 0; i < Rank.instance.ranks.Length; i++) {
            names[i] = GameObject.Find("PlayerNames").GetComponentInChildren<TextMeshProUGUI>();
            times[i] = GameObject.Find("Times").GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    

    

    

}

