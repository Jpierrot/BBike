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
    public TextMeshProUGUI playerTime;

    private bool timeCheck = true;
    private float time;
   

    /// <summary>
    /// �� ���� °���� ��
    /// </summary>
    int rap;

    /// <summary>
    /// �÷��̾��� �� ������Ż�� �����ϸ鼭, üũ����Ʈ Ȯ��
    /// </summary>
    public int nextTarget;

    /// <summary>
    /// üũ����Ʈ
    /// </summary>
    [SerializeField]
    public Transform target;
    private void Start() {
        StartCoroutine(TrackCheck());
    }

    /// <summary>
    /// 3���� üũ����Ʈ�� ����
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
