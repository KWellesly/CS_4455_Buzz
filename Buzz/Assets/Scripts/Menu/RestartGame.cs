using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public AudioClip gameOverSound;
    private void Start()
    {
        Time.timeScale = 1f;
        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Vector3.zero);
        }
    }

    public void GameRestarter()
    {
        SceneManager.LoadScene("Kevin_Scene");
    }
}
