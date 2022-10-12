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

    void Start()
    {
        GameManager.Instance.StartGame.AddListener(SoundInit);

        if (PlayerPrefs.HasKey("CurrentVolume"))
            audioMain.volume = PlayerPrefs.GetFloat("CurrentVolume");
        else
            audioMain.volume = 0.72f;
        audioMain.pitch = 1.14f;

        engine.gameObject.SetActive(false);
        counts.gameObject.SetActive(false);
        engineSound.gameObject.SetActive(false);
        booster.gameObject.SetActive(false);
    }

    void SoundInit()
    {
        if (GameManager.Instance.isgameStart)
        {
            StartCoroutine(StartCounts());
            engine.gameObject.SetActive(true);
            engineSound.gameObject.SetActive(true);
        }
    }

    public void SetVolume()
    {
        if (PlayerPrefs.HasKey("CurrentVolume"))
            audioMain.volume = PlayerPrefs.GetFloat("CurrentVolume");
    }

    IEnumerator StartCounts()
    {
        counts.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        counts.gameObject.SetActive(false);
        booster.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        booster.gameObject.SetActive(false);
    }
}
