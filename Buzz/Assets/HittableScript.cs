using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HittableScript : MonoBehaviour
{
    public Animator anim;
    public Collider player = null;
    public bool isSlappable = false; 



    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c != null)
        {
            PowerupCollector pc = c.gameObject.GetComponent<PowerupCollector>(); // Only SomeDude_RootMotion has ball collector 
            if (pc != null)
            {
                player = c;
            }
            isSlappable = pc != null;
        }
    }
    void OnTriggerStay(Collider c)
    {
        if (c != null)
        {
            PowerupCollector pc = c.gameObject.GetComponent<PowerupCollector>(); // Only SomeDude_RootMotion has ball collector 
            if (pc != null)
            {
                player = c;
            }
            isSlappable = pc != null;
        }
    }

    public void FixedUpdate()
    {
        if (player != null && (Vector3.Distance(player.transform.position, this.transform.position) > 2.2))
        {
            isSlappable = false;
        }
        //anim.SetBool("isHungry", isHungry);
    }
}
