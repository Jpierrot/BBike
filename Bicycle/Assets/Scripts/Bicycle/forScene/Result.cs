using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] nameText;
    [SerializeField]
    TextMeshProUGUI[] timeText;

    public static string []names = new string[5];
    public static float []times = new float[5];

    // Start is called before the first frame update

    private void Awake() {
        for (int i = 0; i < names.Length; i++) {
            nameText[i].text = names[i];
            timeText[i].text = times[i] == 3.3f ? "Retire" : times[i].ToString();
        }
    }
}
