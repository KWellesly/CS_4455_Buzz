﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SlapperScript : MonoBehaviour
{
    //This script also acts as the wanted level manager
    public Animator anim;
    public bool isSlappable = false;
    public GameObject target;

    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;

    public bool match = false; 
    private Collider targetStudent;
    private int wantedLevel;
    public GameObject policePrefab;
    private GameObject[] mapWaypoints;
    public GameObject buzz;
    public AudioSource mapleMusic;
    public AudioSource godOfWarMusic;
    public AudioSource winnieMusic;
    public AudioSource whiteClawMusic;
    private bool playGodofWar;
    private bool playWinnieMusic;
    private bool playWhiteClawMusic;
    private PowerupCollector pc;
    

    void Awake()
    {
        anim = GetComponent<Animator>();
        playGodofWar = playWhiteClawMusic = playWinnieMusic = false;
    }

    void Update() {

        if (pc == null)
        {
            pc = buzz.GetComponent<PowerupCollector>();
        }

        bool powerUpMusicActive = playWinnieMusic || playWhiteClawMusic;

        godOfWarMusic.mute = powerUpMusicActive;
        mapleMusic.mute = powerUpMusicActive;

        if ((wantedLevel > 20 || pc.DidBuzzCompleteABone()) && !playGodofWar && !(powerUpMusicActive))
        {
            playGodofWar = true;
            mapleMusic.Stop();
            godOfWarMusic.Play();
        }

        if (target != null)
        {
            Vector3 direction = target.transform.position - this.transform.position;
            float dotprod = Vector3.Dot(direction, this.transform.forward);

            if (isSlappable && Input.GetButtonDown("Fire1") && dotprod > 0)
            {
                anim.SetBool("Slap", isSlappable);
                match = true; // Set for match target
                //Debug.Log("target = " + target);
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c != null)
        {
            HittableScript hs = c.gameObject.GetComponent<HittableScript>(); // Only SomeDude_RootMotion has ball collector 
            if (hs != null)
            {
                target = c.gameObject;
            }
            isSlappable = hs != null;
        }
        // adding in win screen here since this file already has an OnTriggerMethod
        if (c != null)
        {
         if (c.gameObject.tag == "WinGate")
         {
             PowerupCollector pc = GetComponent<PowerupCollector>();
             bool isWin = pc.DidBuzzCompleteABone();
             if (isWin)
             {
                 SceneManager.LoadScene("GameWinScene");
             }
         }   
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c != null)
        {
            HittableScript hs = c.gameObject.GetComponent<HittableScript>(); // Only SomeDude_RootMotion has ball collector
            if (hs != null)
            {
                target = c.gameObject;
            }
            isSlappable = hs != null;
            targetStudent = c;
        }
    }

    public void FixedUpdate()
    {
        if (target != null && (Vector3.Distance(target.transform.position, this.transform.position) > 2.2))
        {
            //Debug.Log(Vector3.Distance(target.transform.position, this.transform.position));
            isSlappable = false;
        }
        //anim.SetBool("isHungry", isHungry);
    }

    // DEBUG: Rotation stops after match target 
    /**
    public void LateUpdate() {
        if (match) {
            Debug.Log("MATCH");
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
            float str = Mathf.Min(0.75f * Time.deltaTime, 1);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, str);
            match = false; // Only match once 
        // TRY: Rigid body move to position 
        }
    }*/
       
    void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(1);
            if (astate.IsName("Slapping"))
            {
                float weight = anim.GetFloat("SlapWeight");
                Transform headTransform = target.transform.Find("DummySkeleton/root/B-hips/B-spine/B-chest/B-upperChest/B-neck/B-head");
                // Set the look target position, if one has been assigned
                if (target != null)
                {
                    anim.SetLookAtWeight(weight);
                    anim.SetLookAtPosition(target.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
                    anim.SetIKPosition(AvatarIKGoal.RightHand,
                    headTransform.position);
                    // TODO: Ragdolling 
                    // This needs to be target, not targetStudent which is the collider!
                    studentController sc = target.gameObject.GetComponent<studentController>();
                    if (sc != null)
                    {
                        sc.setRagdoll(this);
                    }

                    // get position of slapped student
                    fleeFromSlappedStudent(target.transform.position);
                    policeSearchRadius(target.transform.position);


                    target.GetComponent<CapsuleCollider>().enabled = false;
                    target.layer = 4;
                    Transform[] transforms = target.GetComponentsInChildren<Transform>();
                    foreach (Transform transform in transforms)
                        transform.gameObject.layer = 4;

                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }

    void policeSearchRadius(Vector3 slapped_position)
    {
        int layerid = 13;
        int layermask = 1 << layerid;
        int vicinity_radius = 75;
        

        Collider[] hitColliders = Physics.OverlapSphere(slapped_position, vicinity_radius, layermask);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponentInParent<policeController>().pathTowardsSlappedStudent(slapped_position);
        }
    }

    // cause other students in the vicinity to flee from the area
    void fleeFromSlappedStudent(Vector3 slapped_position)
    {
        int fear_radius = 50;
        // first find all students in vicinity
        int layerid = 11;
        int layermask = 1 << layerid;

        Collider[] hitColliders = Physics.OverlapSphere(slapped_position, fear_radius, layermask);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponentInParent<studentController>())
            {
                // get vector of student to slapped_position, reverse it for their new path for a certain distance
                Vector3 student_pos = hitColliders[i].GetComponentInParent<Transform>().position;
                Vector3 away_slap_center = (student_pos - slapped_position).normalized;
                hitColliders[i].GetComponentInParent<studentController>().setRunawayWaypoint(student_pos + away_slap_center * fear_radius);

            }
        }
    }

    public int getWantedLevel()
    {
        return wantedLevel;
    }

    public void halfWantedLevel() {
        wantedLevel = wantedLevel/2;
    }

    public void increaseWantedLevel()
    {
        wantedLevel++;
        if (mapWaypoints == null)
        {
            GameObject student = GameObject.Find("student"); 
            studentController sc = targetStudent.gameObject.GetComponent<studentController>();
            mapWaypoints = sc.getWaypoints();
        }

        int randomIndex = (int) Random.Range(0, mapWaypoints.Length);
        GameObject newPolice = (GameObject) Instantiate(policePrefab, mapWaypoints[randomIndex].transform.position, Quaternion.identity);
        policeController pc = newPolice.gameObject.GetComponent<policeController>();
        pc.buzz = buzz;

        //proof pf concept code, spawns police at the first waypoint so you can find them easier
        //GameObject prPolice = (GameObject) Instantiate(policePrefab, mapWaypoints[0].transform.position, Quaternion.identity);
        //policeController pc1 = prPolice.gameObject.GetComponent<policeController>();
        //pc1.buzz = buzz;

    }

    public void startHoneyMusic()
    {
        if (playWhiteClawMusic)
        {
            whiteClawMusic.mute = true;
        }

        winnieMusic.Play();
        playWinnieMusic = true;
    }

    public void startWhiteClawMusic()
    {
        if (playWinnieMusic)
        {
            winnieMusic.mute = true;
        }

        whiteClawMusic.time = 43.5f;
        whiteClawMusic.Play();
        playWhiteClawMusic = true;
    }

    public void stopWinnieMusic()
    {
        winnieMusic.Stop();

        if (playWhiteClawMusic)
        {
            whiteClawMusic.mute = false;
        }

        playWinnieMusic = false;
    }

    public void stopWhiteClawMusic()
    {
        whiteClawMusic.Stop();

        if (playWinnieMusic)
        {
            winnieMusic.mute = false;
        }

        playWhiteClawMusic = false;
    }
}
