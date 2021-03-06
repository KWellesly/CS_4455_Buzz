﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeClawAlpha : MonoBehaviour
{
    
    Image image;
    Color c;
    public PowerupCollector player;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        c = image.color;
        c.a = 0.33f;
    }

    // Update is called once per frame
    void Update()
    {

    	//checks if player has collected a donut - this will be done in the player script (by ben)
    	if (!active && player.HasWhiteClaw()) {
    		c.a = 1f;
    		image.color = c;
            active = true;
    	}

        if (active && Input.GetKeyDown(KeyCode.Alpha2)) {
        	c.a = 0.33f;
        	image.color = c;
        	player.UseWhiteClaw();
            active = false;
    	}
    }
}
