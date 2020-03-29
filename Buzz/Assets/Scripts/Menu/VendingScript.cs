using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VendingScript : MonoBehaviour
{
    //public Animator anim;
    public Collider player = null;
    private bool canBeUsed = false; // Variable that tells you whether this vending machine is useable or not 

    public CanvasGroup vendingUI;

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

    public void Update()
    {
        if (player != null && (Vector3.Distance(player.transform.position, this.transform.position) <= 2.2)) // Check for whether player is too far away to use
        {
            if (Input.GetKeyUp(KeyCode.Space)) {
            	
            	if (vendingUI.interactable) {

            		vendingUI.interactable = false;
                    vendingUI.blocksRaycasts = false;
                    vendingUI.alpha = 0f;
					Time.timeScale = 1f;

            	} else {

					vendingUI.interactable = true;
					vendingUI.blocksRaycasts = true;
					vendingUI.alpha = 1f;
					Time.timeScale = 0f;

            	}
            }

        }
    }


//*** don't need these, handled on UI canvas***//

    // // As payment 
    // public void RemoveBone(int cost, PowerupCollector pc) {
    //     if (pc.numBoneFragments - cost >= 0)
    //     {
    //         pc.numBoneFragments = pc.numBoneFragments - cost;
    //     }
    //     else {
    //         // Code for saying you don't have enough money 

    //     }
    // }

    // // Spawns a powerup on buzz for him to collect. "powerup" is a string that should be 
    // public void GivePowerUp(string powerup) {
    //     Vector3 playerPos = this.gameObject.transform.position;
    //     GameObject testLatte = Instantiate(Resources.Load(powerup)) as GameObject;
    //     Vector3 spawnPoint = new Vector3(playerPos.x, playerPos.y + 0.5f, playerPos.z);
    //     testLatte.GetComponent<Collectible>().Drop(spawnPoint);
    // }

}
