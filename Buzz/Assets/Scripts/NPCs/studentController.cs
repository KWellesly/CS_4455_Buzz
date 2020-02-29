﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class studentController : MonoBehaviour
{
    public bool isRagdoll;
    private Animator anim;
    public float MaxSpeed = 1;

    public Camera cam;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //for ragdolling
        SetKinematic(true);
        isRagdoll = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || isRagdoll)
		{
			SetKinematic(false);
			GetComponent<Animator>().enabled=false;
            isRagdoll = true;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            return;
		}
        if (anim == null)
        {
            return;
        }
        /*
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        move(x, y);
        */
        if (Input.GetKeyDown(KeyCode.Space))
		{
			SetKinematic(false);
			GetComponent<Animator>().enabled=false;
		}
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);

            }
        }

        anim.SetFloat("VelY", agent.velocity.magnitude);
    }
    void move(float x, float y)
    {
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        transform.Rotate(0,x * 5,0);

    }
    void SetKinematic(bool newValue)
	{
		//Get an array of components that are of type Rigidbody
		Rigidbody[] bodies=GetComponentsInChildren<Rigidbody>();

		//For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
		foreach (Rigidbody rb in bodies)
		{
			rb.isKinematic=newValue;
		}
	}
}
