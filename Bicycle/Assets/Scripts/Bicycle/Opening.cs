using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Opening : MonoBehaviour
{

    public TextMeshProUGUI text;
    private string m_text = "- Get Space to Start -";

    private void Start() {
        text.text = "";
        StartCoroutine(typing());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator typing() {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i <= m_text.Length; i++) {
            text.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
