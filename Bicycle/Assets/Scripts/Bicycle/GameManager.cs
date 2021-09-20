using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    public bool gameStart;
    public bool gameEnd;
    public bool gameoff;
    public bool playerIn;

    [SerializeField]
    GameObject watercheck;


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

    public Transform[] target;
    public Transform[] targets;
    public Transform[] playerTargets;

    public AI[] ai;
    public AIS[] ais;

    private float minAiSpeed = 35f;
    private float maxAiSpeed = 52.5f;

    public PlayerTrack player;
    public Bicycle_movement playerCheck;

    [SerializeField]
    TextMeshProUGUI counts;

    private void Awake() {
        if(Time.timeScale == 0) {
            Time.timeScale = 1f;
        }

        if (_instance == null) {
            _instance = this;
        }

        else if (_instance != this) {
            Destroy(gameObject);
        }
    } 
    
    void SpeedSet() {
        for (int i = 0; i < ai.Length; i++) {
            ai[i].carSpeed = Random.Range(minAiSpeed, maxAiSpeed);
        }
        for (int i = 0; i < ais.Length; i++) {
            ais[i].carSpeed = Random.Range(minAiSpeed, maxAiSpeed);
        }
    }

    public void Ai_On() {

        for (int i = 0; i < ai.Length; i++) {
            ai[i].transform.gameObject.GetComponent<AI>().enabled = true;
        }
        for (int i = 0; i < ais.Length; i++) {
            ais[i].transform.gameObject.GetComponent<AIS>().enabled = true;
        }
    }

    public void Ai_Off() {
        for (int i = 0; i < ai.Length; i++) {
            ai[i].transform.gameObject.GetComponent<AI>().enabled = false;
        }
        for (int i = 0; i < ais.Length; i++) {
            ais[i].transform.gameObject.GetComponent<AIS>().enabled = false;
        }
    }

    public void PlayerSetOn() {
        player.gameObject.GetComponent<Bicycle_movement>().enabled = true;
        player.gameObject.GetComponent<PlayerTrack>().enabled = true;
    }

    public void PlayerSetOff() {
        player.gameObject.GetComponent<Bicycle_movement>().enabled = false;
        player.gameObject.GetComponent<PlayerTrack>().enabled = false;
    }

    void Start() {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update() {
        if (gameoff)
        {
            StartCoroutine(GameEnd());
            gameoff = false;
        }
    }

    
    void DataSend() {
        for (int i = 0; i < Rank.instance.ranks.Length; i++) {
            Result.names[i] = Rank.instance.ranks[i].name;
            Debug.Log(Result.names[i] + " : "  + Rank.instance.ranks[i].time);
            Result.times[i] = Rank.instance.ranks[i].rap < 3 ? 3.3f : Rank.instance.ranks[i].time;
        }
    }


    IEnumerator GameStart() {
        SpeedSet();
        gameEnd = false;
        watercheck.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        gameStart = true;
        watercheck.SetActive(true);
        Debug.Log("게임시작");
        counts.gameObject.SetActive(true);
        counts.text = "3";
        yield return new WaitForSeconds(1);
        counts.text = "2";
        yield return new WaitForSeconds(1);
        counts.text = "1";
        yield return new WaitForSeconds(1);
        counts.text = "Start!";
        yield return new WaitForSeconds(0.15f);
        counts.gameObject.SetActive(false);
        Ai_On();
        PlayerSetOn();
        playerCheck = GetComponent<Bicycle_movement>();
        yield break;
    }


    IEnumerator GameEnd() {
        
        counts.gameObject.SetActive(true);
        if (playerIn) {
            counts.text = "WIN";
        }
            for (int i = 10; i >= 0; i--) {
                if (playerIn == false)
                    counts.text = i.ToString();
                else {
                       counts.text = "Goal";
                }
                yield return new WaitForSeconds(1f);
            }
        if (playerIn == false)
            counts.text = "Retire";
        else
            counts.text = "Game End";
        yield return new WaitForSeconds(1f);
        counts.text = "Show Result";
        yield return new WaitForSeconds(0.75f);
        Ai_Off();
            PlayerSetOff();
            DataSend();
            SceneManager.LoadScene("Result");
            gameEnd = true;
        }
}
