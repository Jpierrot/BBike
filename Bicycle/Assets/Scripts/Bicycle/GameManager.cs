using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Transform [] target;
    public Transform[] targets;

    public AI [] ai;
    public AIS [] ais;

    private float minAiSpeed = 37.5f;
    private float maxAiSpeed = 55f;

    public PlayerTrack[] player;

    private void Awake() {
        if (instance == null)
            instance = this;
        SpeedSet();
    }

    void SpeedSet() {
        for(int i  = 0; i <= ai.Length; i++) {
            ai[i].carSpeed = Random.Range(minAiSpeed, maxAiSpeed);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
