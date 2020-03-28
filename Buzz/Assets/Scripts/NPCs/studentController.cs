using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class studentController : MonoBehaviour
{
    public bool isRagdoll;
    private Animator anim;
    public float MaxSpeed = 1;
    public AudioClip slapNoise;

    public NavMeshAgent agent;

    // for student waypoints
    public GameObject[] waypoints;
    private int currWaypoint; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //for ragdolling
        SetKinematic(true);
        isRagdoll = false;

        currWaypoint = -1;
        setNextWaypoint();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRagdoll)
		{
			SetKinematic(false);
			GetComponent<Animator>().enabled=false;
            isRagdoll = true;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<HittableScript>().enabled = false;
            return;
		}
        if (anim == null)
        {
            return;
        }

        //keep this duplaicate code here, idk if it will break the animator on ragdoll -Eric :D
        if (isRagdoll)
		{
			SetKinematic(false);
			GetComponent<Animator>().enabled=false;
		}
        if (agent.remainingDistance == 0 && !agent.pathPending)
        {
            setNextWaypoint();
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

    void setNextWaypoint()
    {
        //currWaypoint = (currWaypoint + 1) % waypoints.Length;
        currWaypoint = (int) Random.Range(0, waypoints.Length);
        agent.SetDestination(waypoints[currWaypoint].transform.position);
    }
    public void setRagdoll()
    {
        if(isRagdoll)
        {
            return;
        }
        isRagdoll = true;
        DropPowerUp();
        AudioSource.PlayClipAtPoint(slapNoise, this.gameObject.transform.position);
    }

    private int getRandomPowerUpValue()
    {
        float rand = Random.value;
        if (rand <= .05f)
        {
            return 0;
        }
        else if (rand > .05f && rand <= .20f)
        {
            return 1;
        }
        else if (rand > .20f && rand <= .35f)
        {
            return 2;
        } 
        else if (rand > .35f && rand <= .50f)
        {
            return 3;
        }
        else
        {
            return -1;
        }
    }

    public void DropPowerUp()
    {
        int typeOfPowerUp = getRandomPowerUpValue();
        GameObject droppedPowerUp = null;

        switch(typeOfPowerUp)
        {
            case 0:
                droppedPowerUp = Instantiate(Resources.Load("Donut")) as GameObject;
                break;
            case 1:
                droppedPowerUp = Instantiate(Resources.Load("Latte")) as GameObject;
                break;
            case 2:
                droppedPowerUp = Instantiate(Resources.Load("WhiteClaw")) as GameObject;
                break;
            case 3:
                droppedPowerUp = Instantiate(Resources.Load("Bone Fragment")) as GameObject;
                break;
            default:
                // return no item
                return;
        }

        Vector3 studentPos = this.gameObject.transform.position;
        Vector3 spawnPoint = new Vector3(studentPos.x, studentPos.y + 0.5f, studentPos.z);
        if (droppedPowerUp != null)
        {
            droppedPowerUp.GetComponent<Collectible>().Drop(spawnPoint);
        }
    }
}
