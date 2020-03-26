using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VendingScript : MonoBehaviour
{
    public Animator anim;
    public Collider player = null;
    private bool canBeUsed = false; // Variable that tells you whether this vending machine is useable or not 

    void OnTriggerEnter(Collider c)
    {
        if (c != null)
        {
            PowerupCollector pc = c.gameObject.GetComponent<PowerupCollector>(); // Check for whether collider is player 
            if (pc != null)
            {
                player = c;
            }
            canBeUsed = pc != null;
        }
    }
    void OnTriggerStay(Collider c)
    {
        if (c != null)
        {
            PowerupCollector pc = c.gameObject.GetComponent<PowerupCollector>(); // Check for whether collider is player
            if (pc != null)
            {
                player = c;
            }
            canBeUsed = pc != null;
        }
    }
    public void FixedUpdate()
    {
        if (player != null && (Vector3.Distance(player.transform.position, this.transform.position) > 2.2)) // Check for whether player is too far away to use
        {
            canBeUsed = false;
        }
    }

}
