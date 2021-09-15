using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTrack : MonoBehaviour
{

    /// <summary>
    /// 몇 바퀴 째인지 표시하는 텍스트
    /// </summary>
    public TextMeshProUGUI rapText;
    public TextMeshProUGUI playerTime;

    private bool timeCheck = true;
    private float time;
   

    /// <summary>
    /// 몇 바퀴 째인지 셈
    /// </summary>
    int rap;

    /// <summary>
    /// 플레이어의 맵 무단이탈을 방지하면서, 체크포인트 확인
    /// </summary>
    public int nextTarget;

    /// <summary>
    /// 체크포인트
    /// </summary>
    [SerializeField]
    public Transform target;
    private void Start() {
        StartCoroutine(TrackCheck());
    }

    /// <summary>
    /// 3개의 체크포인트를 관리
    /// </summary>
    /// <returns></returns>
    IEnumerator TrackCheck() { 

        while (true) {
            float dis = (target.position - transform.position).magnitude;

            if (rap >= 3)
                timeCheck = false;

            if (dis <= 50) {
                nextTarget += 1;
                if (nextTarget >= GameManager.instance.target.Length) {
                    nextTarget = 0;
                    rap++;
                }
                target = GameManager.instance.target[nextTarget];

            }
            yield return null;
        }

    }

    private void Update() {
        rapText.text = rap.ToString() + "  /  3";
        playerTime.text = string.Format("{0: 00} : {1 :00.00}",(int)(time/60%60), time % 60);

        if (timeCheck)
            time += Time.deltaTime;
        
    }
    
}
