using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void StartScene()
    {
        SceneManager.LoadScene("Bike");
    }

    public void OptionScene()
    {
        SceneManager.LoadScene("Option");
    }
    public void ResultScene()
    {
        SceneManager.LoadScene("Result");
    }
    public void MenuScene() {
        SceneManager.LoadScene("Result");
    }
    public void Quit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}