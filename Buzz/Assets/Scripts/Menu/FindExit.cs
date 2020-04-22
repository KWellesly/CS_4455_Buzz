using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FindExit : MonoBehaviour
{	
	public CanvasGroup canvasGroup;
	public PowerupCollector pc;
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
        if (pc.DidBuzzCompleteABone()) {
        	canvasGroup.interactable = true;
	        canvasGroup.blocksRaycasts = true;
	        canvasGroup.alpha = 1f;
        }
    }
}
