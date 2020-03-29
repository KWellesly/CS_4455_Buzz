using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVending : MonoBehaviour
{
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
