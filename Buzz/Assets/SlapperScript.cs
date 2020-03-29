using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SlapperScript : MonoBehaviour
{
    public Animator anim;
    public bool isSlappable = false;
    public GameObject target;

    public float initalMatchTargetsAnimTime = 0.25f;
    public float exitMatchTargetsAnimTime = 0.75f;

    public bool match = false; 
    private Collider targetStudent;
    private int wantedLevel;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (target != null && isSlappable && Input.GetButtonDown("Fire1")) {
            anim.SetBool("Slap", isSlappable);
            match = true; // Set for match target
        }
        /**
        if (match)
        {
            var t = target.transform;
            anim.MatchTarget(t.position, t.rotation, AvatarTarget.Root,
                    new MatchTargetWeightMask(new Vector3(1f, 0f, 1f), 1f), <-- Use mask to only set rotation
                       initalMatchTargetsAnimTime,
                       exitMatchTargetsAnimTime);
            match = false;
        // 1: use the root and matchTarget code 
        // 2: rotation with programatic (nonroot motion) 
        }*/
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
                Debug.Log(weight);
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

                    studentController sc = targetStudent.gameObject.GetComponent<studentController>();
                    sc.setRagdoll();
                    wantedLevel++;
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }
    public int getWantedLevel()
    {
        return wantedLevel;
    }
}
