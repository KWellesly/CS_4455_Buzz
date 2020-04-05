using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuzzRootMotion), typeof(Animator), typeof(Rigidbody))]
public class PowerupCollector : MonoBehaviour
{

    private bool hasLatte;
    public float latteSpeedMultiplier=1.5f;
    private bool startExpireLatte;
    public float latteDuration;

    private bool hasDonut;
    public float donutDuration;

    private bool hasWhiteClaw;
    public float whiteClawSpeedPenalty=2f;
    private bool startExpireWhiteClaw;
    public float whiteClawDuration;

    private bool hasHoney;
    private bool startExpireHoney;
    public float honeyDuration;

    private float startLatteTime;
    private float startWhiteClawTime;
    private float startHoneyTime;

    private BuzzRootMotion motion;

    private bool usedLatte;
    private bool usedDonut;
    private bool usedWhiteClaw;
    private bool usedHoney;
    
    // For throwing
    private Transform handHold;
    public Animator anim;

    // Bonebar
    public BoneBar boneBar;
    public int maxBoneCount = 10;
    public int numBoneFragments = 0;

    // Sounds
    public AudioClip pickupPowerUp;
    public AudioClip pickupBone;
    public AudioClip throwDonut;
    public AudioClip drinkLatte;
    public AudioClip drinkWhiteClaw;
    public AudioClip eatHoney;

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

    void Awake()
    {
        anim = GetComponent<Animator>();
        handHold = this.transform.Find("DummySkeleton/root/B-hips/B-spine/B-chest/B-upperChest/B-shoulder_L/B-upper_arm_L/B-forearm_L/B-hand_L/B-palm01_L");
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

        if (usedHoney)
        {
            if (!startExpireHoney)
            {
                InitHoneyInvincibility();
            }
            else
            {
                if(Time.time - startHoneyTime > honeyDuration)
                {
                    ExpireHoney();
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
        Rigidbody donutRb = donut.GetComponent<Rigidbody>();
        donut.GetComponent<DropDonutScript>().Drop(donutDuration, spawnPoint, donutRb, handHold);
        AudioSource.PlayClipAtPoint(throwDonut, this.gameObject.transform.position);
    }

    //CAN THIS CODE BE REMOVED? -Ben
    public void FixedUpdate() {
        //anim.SetBool("throw", hasDonut && Input.GetKeyDown(KeyCode.Alpha1));
    }

    public void ReceiveBoneFragment()
    {
        numBoneFragments++;
        AudioSource.PlayClipAtPoint(pickupBone, this.gameObject.transform.position);
    }

    public void ReceiveHoney()
    {
        hasHoney = true;
        AudioSource.PlayClipAtPoint(pickupPowerUp, this.gameObject.transform.position);
    }

    public void InitHoneyInvincibility()
    {
        startHoneyTime = Time.time;
        usedHoney = hasHoney = false;
        Debug.Log("Honey Activated!");
        AudioSource.PlayClipAtPoint(eatHoney, this.gameObject.transform.position);
    }

    public void ExpireHoney()
    {
        startExpireHoney = usedHoney = false;
    }

    //ally's code
    public bool HasDonut() {
        return hasDonut;
    }

    public bool HasWhiteClaw() {
        return hasWhiteClaw;
    }

    public bool IsWhiteClawActive()
    {
        return startExpireWhiteClaw;
    }

    public bool HasLatte() {
        return hasLatte;
    }

    public bool HasHoney()
    {
        return hasHoney;
    }

    //setters for when user clicks 1,2,3 on keyboard
    public void UseDonut() {
        usedDonut = true;
    }

    public void UseWhiteClaw() {
        usedWhiteClaw = true;
    }

    public void UseLatte() {
        usedLatte = true;
    }

    public void UseHoney()
    {
        usedHoney = true;
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
