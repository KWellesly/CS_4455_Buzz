using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningLevelScript : MonoBehaviour
{

	public SlapperScript slapper;
	public Text wantedLevel;

    // Update is called once per frame
    void Update()
    {
        wantedLevel.text = "Wanted Level: " + slapper.getWantedLevel().ToString();
    }
}
