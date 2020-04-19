using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    private void Start()
    {
       
    }

    public void HowToPlayGame()
    {
        SceneManager.LoadScene("ControlScene");
    }
}
