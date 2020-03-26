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

    /** Put your vending machine UI code */

    public void FixedUpdate()
    {
        if (player != null && (Vector3.Distance(player.transform.position, this.transform.position) > 2.2)) // Check for whether player is too far away to use
        {
            canBeUsed = false;
            // Code for removing vending machine UI goes here

        }

        if (player != null && canBeUsed && Input.GetKeyUp(KeyCode.Space))
        {
            PowerupCollector pc = player.gameObject.GetComponent<PowerupCollector>();
            // Code for pulling up the vending machine UI goes here

            // Use GivePowerUp method below to handle what happens when they choose a powerup
            if (pc.numBoneFragments > 0) {
                // RemoveBone(cost from UI, pc)
                // GivePowerUp(string from UI)
            }
        }
    }

    // As payment 
    public void RemoveBone(int cost, PowerupCollector pc) {
        if (pc.numBoneFragments - cost >= 0)
        {
            pc.numBoneFragments = pc.numBoneFragments - cost;
        }
        else {
            // Code for saying you don't have enough money 

        }
    }

    // Spawns a powerup on buzz for him to collect. "powerup" is a string that should be 
    public void GivePowerUp(string powerup) {
        Vector3 playerPos = this.gameObject.transform.position;
        GameObject testLatte = Instantiate(Resources.Load(powerup)) as GameObject;
        Vector3 spawnPoint = new Vector3(playerPos.x, playerPos.y + 0.5f, playerPos.z);
        testLatte.GetComponent<Collectible>().Drop(spawnPoint);
    }
}
