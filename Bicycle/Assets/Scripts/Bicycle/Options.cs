using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public bool timePause;

    [SerializeField]
    public GameObject pausePanel;

    [SerializeField]
    public Slider ld;

    [SerializeField]
    public Toggle mb;

    [SerializeField]
    public Slider mVolume;


    private void Awake()
    {
        PlayerPrefs.SetFloat("MasterVolume", mVolume.value);
        PlayerPrefs.SetInt("MotionBlur", mb.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("LensDistortion", ld.value);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TimePause();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MasterVolume()
    {
        AudioListener.volume = mVolume.value;
    }

    public void MotionBlur()
    {
        PlayerPrefs.SetInt("MotionBlur", mb.isOn ? 1 : 0);
    }

    public void LensDistortion()
    {
         PlayerPrefs.SetFloat("LensDistortion", ld.value);
    }

    public void TimePause()
    {
        if (!timePause)
        {
            timePause = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    public void Continue()
    {
        if (timePause)
        {
            timePause = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
