using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void GameStarter()
    {
        SceneManager.LoadScene("OpeningDialog");
    }
}
