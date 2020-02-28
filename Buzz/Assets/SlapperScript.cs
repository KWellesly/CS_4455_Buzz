using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class SlapperScript : MonoBehaviour
{
    public Animator anim;
    public Collider player = null;
    public bool isSlappable = false;
    public GameObject target; 

    private Rigidbody rbody;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (target != null && isSlappable && Input.GetButtonDown("Fire1")) {
            anim.SetBool("slap", isSlappable);
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
}
