using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class SlapperScript : MonoBehaviour
{
    public Animator anim;
    public bool isSlappable = false;
    public GameObject target; 

    private Rigidbody rbody;
    public bool match = false; 

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (target != null && isSlappable && Input.GetButtonDown("Fire1")) {
            anim.SetBool("Slap", isSlappable);
            match = true; // Set for match target
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            HittableScript hs = c.attachedRigidbody.gameObject.GetComponent<HittableScript>(); // Only SomeDude_RootMotion has ball collector 
            if (hs != null)
            {
                target = c.attachedRigidbody.gameObject;
            }
            isSlappable = hs != null;
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            HittableScript hs = c.attachedRigidbody.gameObject.GetComponent<HittableScript>(); // Only SomeDude_RootMotion has ball collector 
            if (hs != null)
            {
                target = c.attachedRigidbody.gameObject;
            }
            isSlappable = hs != null;
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
        }
    }*/
       
    void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(1);
            if (astate.IsName("Slapping"))
            {
                float weight = 0.5f;
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
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }
}
