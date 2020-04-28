using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPlayableGame : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void ClickGoButton()
    {
        GameObject introMusic = GameObject.Find("IntroMusic");
        if (introMusic != null)
        {
            Destroy(introMusic);
        }
        SceneManager.LoadScene("Kevin_Scene");
    }
}
