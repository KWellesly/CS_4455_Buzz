using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDonutScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drop(float duration, Vector3 spawnPoint, Rigidbody donutRb, Transform handHold)
    {
        //Rigidbody player = this.GetComponent<GameObject>().GetComponent<Rigidbody>();
        /*
        donutRb.transform.position = spawnPoint;
        donutRb.isKinematic = false;
        donutRb.velocity = Vector3.zero;
        donutRb.angularVelocity = Vector3.zero; // for unity gravity bug 
        donutRb.AddForce(this.transform.forward * 5, ForceMode.VelocityChange);
        */

        donutRb.transform.position = spawnPoint; 
        Destroy(this.gameObject, duration);
    }
}
