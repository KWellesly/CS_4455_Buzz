using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuzzBasicControl))]
public class PowerupCollector : MonoBehaviour
{

    public bool hasLatte;
    public float latteSpeedMultiplier;
    public bool startExpireLatte;
    public float latteDuration;

    public bool hasDonut;

    public bool hasWhiteClaw;
    public float whiteClawSpeedPenalty;
    public bool startExpireWhiteClaw;
    public float whiteClawDuration;

    public int numBoneFragments = 0;

    private float startLatteTime;
    private float startWhiteClawTime;

    private BuzzBasicControl motion;

    // Start is called before the first frame update
    void Start()
    {
        hasDonut = hasWhiteClaw = hasLatte = false;
        startExpireLatte = startExpireWhiteClaw = false;
        motion = GetComponent<BuzzBasicControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (hasLatte)
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

        if (hasWhiteClaw)
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
        motion.forwardMaxSpeed *= factor;
        motion.turnMaxSpeed *= factor;
        startLatteTime = Time.time;
        startExpireLatte = true;
    }
    public void ExpireLatte(float factor)
    {
        motion.forwardMaxSpeed /= factor;
        motion.turnMaxSpeed /= factor;
        hasLatte = startExpireLatte = false;
    }

    public void ReceiveWhiteClaw()
    {
        hasWhiteClaw = true;
    }

    public void InitWhiteClawSlowdown(float factor)
    {
        motion.forwardMaxSpeed /= factor;
        motion.turnMaxSpeed /= factor;
        startWhiteClawTime = Time.time;
        startExpireWhiteClaw = true;
    }

    public void ExpireWhiteClaw(float factor)
    {
        motion.forwardMaxSpeed *= factor;
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
}
