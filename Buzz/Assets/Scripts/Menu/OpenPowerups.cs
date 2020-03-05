using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class OpenPowerups : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
    	canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
               
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
               
            }
        }

    }

    private void Awake()
    {
        try
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        catch
        {
            Debug.LogError("CanvasGroup member not found.", canvasGroup);
        }


    }

}
