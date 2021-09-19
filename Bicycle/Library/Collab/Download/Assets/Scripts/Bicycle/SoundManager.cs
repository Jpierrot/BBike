using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioMain;

    [SerializeField]
    AudioSource engine;

    [SerializeField]
    AudioSource counts;

    [SerializeField]
    AudioSource engineSound;

    [SerializeField]
    AudioSource booster;

    float time = 0;
    bool once = true;

    // Start is called before the first frame update

    void Start()
    {
        audioMain.volume = 0.42f;
        audioMain.pitch = 1.14f;

        engine.gameObject.SetActive(false);
        counts.gameObject.SetActive(false);
        engineSound.gameObject.SetActive(false);
        booster.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStart) {
            if (once) {
                StartCoroutine(Counts());
                engine.gameObject.SetActive(true);
                engineSound.gameObject.SetActive(true);
                once = false;
            }
        }
        
    }

    IEnumerator Counts() {
        counts.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        counts.gameObject.SetActive(false);
        booster.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        booster.gameObject.SetActive(false);


    }
}
