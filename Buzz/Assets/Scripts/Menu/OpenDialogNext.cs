using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogNext : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void ClickNextButton()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;

       
    }

}
