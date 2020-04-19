﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void GameRestarter()
    {
        SceneManager.LoadScene("Kevin_Scene");
    }
}
