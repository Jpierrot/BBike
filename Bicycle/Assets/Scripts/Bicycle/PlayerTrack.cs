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


    IEnumerator TrackCheck() { 

        while (true) {
            float dis = (target.position - transform.position).magnitude;

            if (dis <= 10) {
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
        rapText.text = rap.ToString();
    }
}
