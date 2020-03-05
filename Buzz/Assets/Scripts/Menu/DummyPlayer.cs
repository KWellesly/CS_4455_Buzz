using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
   
   //for the bonebar
    public int maxBoneCount = 10;
    public int currentBoneCount;
    public BoneBar boneBar;

    //booleans for the power ups initialized as false
    bool hasDonut = false;
    bool hasWhiteClaw = false;
    bool hasStarbucks = false;

    void Start() {
    	currentBoneCount = 0;

    	//sets max number of bones needed to collect
    	boneBar.SetMaxBoneCount(maxBoneCount);

    	//sets current bone bar to 0
    	boneBar.SetBoneCount(currentBoneCount);

    }

    void Update() {

    	//change this if-statement to onTriggerEnter
    	if (Input.GetKeyDown(KeyCode.Space)) {

    		if (currentBoneCount < maxBoneCount)
    			currentBoneCount++;

    		//updates the bar so that each time space is pressed, it increments
    		boneBar.SetBoneCount(currentBoneCount);
    	}

    }

    //public functions for bone bar script to use
    public int GetBoneCount() {
        return currentBoneCount;
    }

    public int GetMaxBoneCount() {
        return maxBoneCount;
    }



    //public functions for power-up menu script to use
    public bool HasDonut() {
        return hasDonut;
    }

    public bool HasWhiteClaw() {
        return hasWhiteClaw;
    }

    public bool HasStarbucks() {
        return hasStarbucks;
    }

    //setters for when user clicks 1,2,3 on keyboard
    public void SetHasDonut(bool v) {
        hasDonut = v;
    }

    public void SetHasWhiteClaw(bool v) {
        hasWhiteClaw = v;
    }

    public void SetHasStarbucks(bool v) {
        hasStarbucks = v;
    }
}
