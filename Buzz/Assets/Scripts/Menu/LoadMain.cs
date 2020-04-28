using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMain : MonoBehaviour
{
    // Start is called before the first frame update
    public void MainMenu()
    {
        GameObject introMusic = GameObject.Find("IntroMusic");
        if (introMusic != null)
        {
            Destroy(introMusic);
        }
        SceneManager.LoadScene("StartMenuNew");
    }
}

