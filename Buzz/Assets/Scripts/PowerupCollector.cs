using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuzzRootMotion))]
public class PowerupCollector : MonoBehaviour
{

    private bool hasLatte;
    public float latteSpeedMultiplier=2f;
    private bool startExpireLatte;
    public float latteDuration;

    private bool hasDonut;
    public float donutDuration;

    private bool hasWhiteClaw;
    public float whiteClawSpeedPenalty=2f;
    private bool startExpireWhiteClaw;
    public float whiteClawDuration;

    private float startLatteTime;
    private float startWhiteClawTime;

    private BuzzRootMotion motion;

    private bool usedLatte;
    private bool usedDonut;
    private bool usedWhiteClaw;

    //ally's bonebar
    public BoneBar boneBar;
    public int maxBoneCount = 10;
    public int numBoneFragments = 0;

    // sounds
    public AudioClip pickupPowerUp;
    public AudioClip pickupBone;
    public AudioClip throwDonut;
    public AudioClip drinkLatte;
    public AudioClip drinkWhiteClaw;

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

    void Update()
    {
        boneBar.SetBoneCount(numBoneFragments);

        if (usedDonut)
        {
            DropDonut();
        }

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
        AudioSource.PlayClipAtPoint(pickupPowerUp, this.gameObject.transform.position);
    }

    public void InitLatteSpeedup(float factor)
    {
        Debug.Log(factor);
        motion.rootMovementSpeed *= factor;
        motion.turnMaxSpeed *= factor;
        startLatteTime = Time.time;
        startExpireLatte = true;
        hasLatte = false;
        AudioSource.PlayClipAtPoint(drinkLatte, this.gameObject.transform.position);
    }
    public void ExpireLatte(float factor)
    {
        motion.rootMovementSpeed /= factor;
        motion.turnMaxSpeed /= factor;
        startExpireLatte = usedLatte = false;
    }

    public void ReceiveWhiteClaw()
    {
        hasWhiteClaw = true;
        AudioSource.PlayClipAtPoint(pickupPowerUp, this.gameObject.transform.position);
    }

    public void InitWhiteClawSlowdown(float factor)
    {
        motion.rootMovementSpeed /= factor;
        motion.turnMaxSpeed /= factor;
        startWhiteClawTime = Time.time;
        startExpireWhiteClaw = true;
        hasWhiteClaw = false;
        AudioSource.PlayClipAtPoint(drinkWhiteClaw, this.gameObject.transform.position);
    }

    public void ExpireWhiteClaw(float factor)
    {
        motion.rootMovementSpeed *= factor;
        motion.turnMaxSpeed *= factor;
        startExpireWhiteClaw = usedWhiteClaw = false;
    }


    public void ReceiveDonut()
    {
        hasDonut = true;
        AudioSource.PlayClipAtPoint(pickupPowerUp, this.gameObject.transform.position);
    }

    public void DropDonut()
    {
        usedDonut = hasDonut = false;
        GameObject donut = Instantiate(Resources.Load("Dropped Donut")) as GameObject;

        Vector3 playerPos = this.gameObject.transform.position;
        Vector3 spawnPoint = new Vector3(playerPos.x, playerPos.y + 0.5f, playerPos.z);
        donut.GetComponent<DropDonutScript>().Drop(donutDuration, spawnPoint);
        AudioSource.PlayClipAtPoint(throwDonut, this.gameObject.transform.position);
    }


    public void ReceiveBoneFragment()
    {
        numBoneFragments++;
        AudioSource.PlayClipAtPoint(pickupBone, this.gameObject.transform.position);
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
    public void UseDonut() {
        usedDonut = true;
        hasDonut = false;
    }

    public void UseWhiteClaw() {
        usedWhiteClaw = true;
    }

    public void UseLatte() {
        usedLatte = true;
    }

    //public functions for bone bar script to use
    public int GetBoneCount() {
        return numBoneFragments;
    }

    public int GetMaxBoneCount() {
        return maxBoneCount;
    }

    public void SetNumBoneFrag(int num) {
        numBoneFragments = num;
    }
    
    public bool DidBuzzCompleteABone()
    {
        return maxBoneCount == numBoneFragments;
    }
}
