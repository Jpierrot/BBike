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

    /// <summary>
    /// 모션 블러 할지 안할지
    /// 1이면 true, 0이면 false
    /// </summary>
    [SerializeField]
    public Toggle mb;

    [SerializeField]
    public Slider mVolume;

    [SerializeField]
    public Slider cVolume;


    private void Awake()
    {
        /*PlayerPrefs.SetFloat("MasterVolume", mVolume.value);
        PlayerPrefs.SetInt("MotionBlur", mb.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("LensDistortion", ld.value);
        PlayerPrefs.SetFloat("CurrentVolume", cVolume.value);
        PlayerPrefs.Save();*/

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!timePause)
                TimePause();
            else
                Continue();
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolume"))
            mVolume.value = PlayerPrefs.GetFloat("MasterVolume");
        else
            PlayerPrefs.SetFloat("MasterVolume", mVolume.value);

        if (PlayerPrefs.HasKey("MotionBlur")) {
            int mb_temp = PlayerPrefs.GetInt("MotionBlur");
            mb.isOn = mb_temp == 1 ? true : false;
        }
        else
            PlayerPrefs.SetInt("MotionBlur", mb.isOn ? 1 : 0);

        if(PlayerPrefs.HasKey("LensDistortion"))
            ld.value = PlayerPrefs.GetFloat("LensDistortion");
        else
            PlayerPrefs.SetFloat("LensDistortion", ld.value);

        if(PlayerPrefs.HasKey("CurrentVolume"))
            cVolume.value = PlayerPrefs.GetFloat("CurrentVolume");
        else
            PlayerPrefs.SetFloat("CurrentVolume", cVolume.value);
        PlayerPrefs.Save();


    }

    public void CurrentVolume() {
        PlayerPrefs.SetFloat("CurrentVolume", cVolume.value);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    public void MasterVolume()
    {
        AudioListener.volume = mVolume.value;
        PlayerPrefs.Save();
    }

    public void MotionBlur()
    {
        PlayerPrefs.SetInt("MotionBlur", mb.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LensDistortion()
    {
         PlayerPrefs.SetFloat("LensDistortion", ld.value);
        PlayerPrefs.Save();
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
        PlayerPrefs.Save();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
