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
        SceneManager.LoadScene("Kevin_Scene");
    }
}
