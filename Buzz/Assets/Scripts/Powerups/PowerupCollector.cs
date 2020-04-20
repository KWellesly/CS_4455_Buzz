using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuzzRootMotion), typeof(Animator), typeof(Rigidbody))]
public class PowerupCollector : MonoBehaviour
{

    private bool hasLatte;
    public float latteSpeedMultiplier=2f;
    private bool startExpireLatte;
    public float latteDuration;

    private bool hasDonut;
    public float donutDuration;

    private bool hasWhiteClaw;
    private bool startExpireWhiteClaw;
    public float whiteClawDuration;

    private bool hasHoney;
    private bool startExpireHoney;
    public float honeyDuration;

    private float startLatteTime;
    private float startWhiteClawTime;
    private float startHoneyTime;

    private BuzzRootMotion motion;
    private SlapperScript ss;

    private bool usedLatte;
    private bool usedDonut;
    private bool usedWhiteClaw;
    private bool usedHoney;
    private bool drunk;

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
        ss = GetComponent<SlapperScript>();
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
                InitWhiteClawSlowdown();
            }
            else
            {
                if (Time.time - startWhiteClawTime > whiteClawDuration)
                {
                    ExpireWhiteClaw();
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

    public void InitWhiteClawSlowdown()
    {
        startWhiteClawTime = Time.time;
        startExpireWhiteClaw = true;
        hasWhiteClaw = false;
        anim.SetFloat("drunk", 1.0f);
        AudioSource.PlayClipAtPoint(drinkWhiteClaw, this.gameObject.transform.position);
    }

    public void ExpireWhiteClaw()
    {
        anim.SetFloat("drunk", 0.0f);
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
        hasHoney = false;
        startExpireHoney = true;
        if (ss != null)
        {
            ss.startHoneyMusic();
        }
    }

    public void ExpireHoney()
    {
        startExpireHoney = usedHoney = false;
        if (ss != null)
        {
            ss.resumeNormalMusic();
        }

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

    public bool IsBuzzInvincible()
    {
        return startExpireHoney;
    }
}
