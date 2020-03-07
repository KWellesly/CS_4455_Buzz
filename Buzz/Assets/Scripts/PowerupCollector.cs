﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuzzRootMotion))]
public class PowerupCollector : MonoBehaviour
{

    public bool hasLatte;
    public float latteSpeedMultiplier=2;
    public bool startExpireLatte;
    public float latteDuration;

    public bool hasDonut;

    public bool hasWhiteClaw;
    public float whiteClawSpeedPenalty=2;
    public bool startExpireWhiteClaw;
    public float whiteClawDuration;

    private float startLatteTime;
    private float startWhiteClawTime;

    private BuzzRootMotion motion;

    //ally's
    public bool usedLatte;
    public bool usedDonut;
    public bool usedWhiteClaw;

    //ally's bonebar
    public BoneBar boneBar;
    public int maxBoneCount = 10;
    public int numBoneFragments = 0;

    // Start is called before the first frame update
    void Start()
    {
        numBoneFragments = 0;
        //sets max number of bones needed to collect
        boneBar.SetMaxBoneCount(maxBoneCount);

        //sets current bone bar to 0
        boneBar.SetBoneCount(numBoneFragments);

        hasDonut = hasWhiteClaw = hasLatte = false;
        usedLatte = usedWhiteClaw = usedDonut = false;
        startExpireLatte = startExpireWhiteClaw = false;
        motion = GetComponent<BuzzRootMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        boneBar.SetBoneCount(numBoneFragments);
    }

    void FixedUpdate()
    {
        if (usedLatte)
        {
            if (!startExpireLatte)
            {
                InitLatteSpeedup(latteSpeedMultiplier);
            } else
            {
                if (Time.time - startLatteTime > latteDuration)
                {
                    ExpireLatte(latteSpeedMultiplier);
                }
            }
            
        }

        if (usedWhiteClaw)
        {
            if (!startExpireWhiteClaw)
            {
                InitWhiteClawSlowdown(whiteClawSpeedPenalty);
            }
            else
            {
                if (Time.time - startWhiteClawTime > whiteClawDuration)
                {
                    ExpireWhiteClaw(whiteClawSpeedPenalty);
                }
            }
        }
    }

    public void ReceiveLatte()
    {
        hasLatte = true;
    }

    public void InitLatteSpeedup(float factor)
    {
        Debug.Log(factor);
        motion.rootMovementSpeed *= factor;
        motion.turnMaxSpeed *= factor;
        startLatteTime = Time.time;
        startExpireLatte = true;
    }
    public void ExpireLatte(float factor)
    {
        motion.rootMovementSpeed /= factor;
        motion.turnMaxSpeed /= factor;
        hasLatte = startExpireLatte = false;
    }

    public void ReceiveWhiteClaw()
    {
        hasWhiteClaw = true;
    }

    public void InitWhiteClawSlowdown(float factor)
    {
        motion.rootMovementSpeed /= factor;
        motion.turnMaxSpeed /= factor;
        startWhiteClawTime = Time.time;
        startExpireWhiteClaw = true;
    }

    public void ExpireWhiteClaw(float factor)
    {
        motion.rootMovementSpeed *= factor;
        motion.turnMaxSpeed *= factor;
        hasWhiteClaw = startExpireWhiteClaw = false;
    }


    public void ReceiveDonut()
    {
        hasDonut = true;
    }

    public void ExpireDonut()
    {
        hasDonut = false;
    }


    public void ReceiveBoneFragment()
    {
        numBoneFragments++;
    }


    //ally's code
    public bool HasDonut() {
        return hasDonut;
    }

    public bool HasWhiteClaw() {
        return hasWhiteClaw;
    }

    public bool HasLatte() {
        return hasLatte;
    }

    //setters for when user clicks 1,2,3 on keyboard
    public void SetHasDonut(bool v) {
        hasDonut = v;
        usedDonut = !v;
    }

    public void SetHasWhiteClaw(bool v) {
        hasWhiteClaw = v;
        usedWhiteClaw = !v;
    }

    public void SetHasLatte(bool v) {
        hasLatte = v;
        usedLatte = !v;
    }

    //public functions for bone bar script to use
    public int GetBoneCount() {
        return numBoneFragments;
    }

    public int GetMaxBoneCount() {
        return maxBoneCount;
    }
}
