using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTrack : MonoBehaviour
{

    /// <summary>
    /// �� ���� °���� ǥ���ϴ� �ؽ�Ʈ
    /// </summary>
    public TextMeshProUGUI rapText;
    public TextMeshProUGUI rapTime;
    public TextMeshProUGUI playerTime;

    bool timeCheck;

    public bool TimeCheck {
        get {
            return timeCheck;
        }
        set {
            timeCheck = value;
        }
    }

    float playTime;

    public float PlayTime {
        get {
            return playTime;
        }
        set {
            playTime = value;
        }
    }

    bool rapplus;


    /// <summary>
    /// �� ���� °���� ��
    /// </summary>
   public int rap;

    /// <summary>
    /// �÷��̾��� �� ������Ż�� �����ϸ鼭, üũ����Ʈ Ȯ��
    /// </summary>
    public int nextTarget;

    /// <summary>
    /// üũ����Ʈ
    /// </summary>
    [SerializeField]
    public Transform target;

    [SerializeField]
    private Bicycle_movement bike;


    private void Start() {
        bike = GetComponent<Bicycle_movement>();
        StartCoroutine(TrackCheck());
        timeCheck = true;
        playTime = 0;
        rapText.text = "00 : 00.00";
    }

    /// <summary>
    /// 3���� üũ����Ʈ�� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator TrackCheck() { 

        while (true) {
            float dis = (target.position - transform.position).magnitude;

            if (rap >= 3) {
                rapText.text = rap.ToString() + "  /  3";
                timeCheck = false;
                bike.moveSpeed -= Time.fixedDeltaTime * (bike.moveSpeed / 8);
                GameManager.Instance.playerIn = true;
            }
            if (dis <= 10) {
                
                nextTarget += 1;
                if (nextTarget >= GameManager.Instance.target.Length) {
                    rap++;
                    rapplus = true;
                    nextTarget = 0;
                    rapplus = true;
                    
                }
                target = GameManager.Instance.target[nextTarget];

            }
            yield return null;
        }

    }

    private void FixedUpdate() {
        rapText.text = rap.ToString() + "  /  3";
        if (rapplus) {
            rapTime.text = rap.ToString() + "rap  " + playerTime.text;
            rapplus = false;
        }
        playerTime.text =  string.Format("{0: 00} : {1 :00.00}",(int)(playTime/60%60), playTime % 60);

        if(timeCheck && GameManager.Instance.gameEnd == false)
            playTime += Time.deltaTime;
    }
    
}
