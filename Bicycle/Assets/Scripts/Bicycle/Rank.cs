using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rank : MonoBehaviour
{
    private bool one = true;
    public struct forRank
    {
        public string name;
        public int rap;
        public float distance;
        public int nextTarget;

        public void RankArray(string name, int rap, int nextTarget, float distance) {
            this.name = name;
            this.rap = rap;
            this.distance = distance;
            this.nextTarget = nextTarget;
        }
    }

    public static Rank instance;

    public forRank[] ranks;
    forRank[] datas;


    [SerializeField]
    TextMeshProUGUI ranking;

    [SerializeField]
    TextMeshProUGUI playerRank;

    public AI [] ai;
    public AIS [] ais;
    public PlayerTrack player;

    private string[] rank;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        Debug.Log(ai.Length + ais.Length + 1);
        datas = new forRank[ai.Length + ais.Length + 1];
        ranks = new forRank[ai.Length + ais.Length + 1];
        ranks[0].RankArray("Player", player.rap, player.nextTarget, (player.target.position - player.transform.position).magnitude);
        
        int a = 0;

        for (int i = 0; i < ai.Length; i++)
            ranks[++a].RankArray(ai[i].Ai_name, ai[i].rap, ai[i].nextTarget, (ai[i].target.position - ai[i].transform.position).magnitude);

        
        for (int j = 0; j < ais.Length; j++)
            ranks[++a].RankArray(ais[j].Ai_name, ais[j].rap, ais[j].nextTarget, (ais[j].target.position - ais[j].transform.position).magnitude);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ranks[0].rap == 3 && one) {
            GameManager.Instance.gameoff = true;
            one = false;
        }
            
        DataCheck();
        RankdataCheck();
        RankSort();
        TextCheck();
    }

    

    void DataCheck() {
        int a = 0;
        for (int i = 0; i < ai.Length; i++)
            datas[a++].RankArray(ai[i].Ai_name, ai[i].rap, ai[i].nextTarget, (ai[i].target.position - ai[i].transform.position).magnitude);

        for (int j = 0; j < ais.Length; j++)
            datas[a++].RankArray(ais[j].Ai_name, ais[j].rap, ais[j].nextTarget, (ais[j].target.position - ais[j].transform.position).magnitude);

            datas[a++].RankArray("Player", player.rap, player.nextTarget, (player.target.position - player.transform.position).magnitude);
    }

    void RankdataCheck() {
        for (int i = 0; i < ranks.Length; i++) {
            for (int j = 0; j < ranks.Length; j++)
                if (datas[i].name == ranks[j].name) {
                    ranks[j].distance = datas[i].distance;
                    ranks[j].rap = datas[i].rap;
                    ranks[j].nextTarget = datas[i].nextTarget;
                }                       
        }
    }

    void TextCheck() {
        string a = "";
        string b = "";
        for (int i = 0; i < ranks.Length; i++) {
            a += ranks[i].name + "\n";
            Debug.Log(i+"번째 이름 : " + ranks[i].name);
        }
        ranking.text = a;
        for (int i = 0; i < ranks.Length; i++) {
            if (ranks[i].name == "Player") {
                i++;
                b = i.ToString();
            }
        }
        playerRank.text = b + " / " + ranks.Length;

    }

    

    void RankSort() {
        for(int i = 1; i < ranks.Length; i++) {
             {
                if (ranks[i - 1].rap < ranks[i].rap) {
                    Debug.Log("순위 변동 예정 : " + ranks[i-1].name + "rap : " + ranks[i-1].rap + " " + ranks[i].name + "rap : " + ranks[i].rap);
                    Swap(ref ranks[i - 1], ref ranks[i]);
                    Debug.Log("순위 내려간거 : " + ranks[i].name + " 순위 올라간거 : " + ranks[i-1].name);
                }
                else if (ranks[i - 1].rap == ranks[i].rap && ranks[i - 1].nextTarget < ranks[i].nextTarget) {
                    Debug.Log("순위 변동 예정2 : " + ranks[i - 1].name + "rap : " + ranks[i - 1].rap + "target :  "+ ranks[i - 1].nextTarget + " "+ ranks[i].name + "rap : " + ranks[i].rap +"target :  " + ranks[i].nextTarget);
                    Swap(ref ranks[i - 1], ref ranks[i]);
                    Debug.Log("순위 내려간거2 : " + ranks[i].name + " 순위 올라간거2 : " + ranks[i - 1].name);
                }
                else if (ranks[i - 1].rap == ranks[i].rap&& ranks[i - 1].nextTarget == ranks[i].nextTarget && ranks[i - 1].distance > ranks[i].distance) {
                    Debug.Log("순위 변동 예정3 : " + ranks[i - 1].name + "rap : " + ranks[i - 1].rap + "target :  " + ranks[i - 1].nextTarget + "distance : "  + ranks[i-1].distance + ranks[i].name + "rap : " + ranks[i].rap + "target :  " + ranks[i].nextTarget + "distance : " + ranks[i].distance);
                    Swap(ref ranks[i - 1], ref ranks[i]);
                    Debug.Log("순위 내려간거3 : " + ranks[i].name + " 순위 올라간거3 : " + ranks[i - 1].name);
                }
                else { }
            }
        }
    }

    void Swap(ref forRank a, ref forRank b) {
        forRank temp = a;

        a = b;
        b = temp;
    }
}
