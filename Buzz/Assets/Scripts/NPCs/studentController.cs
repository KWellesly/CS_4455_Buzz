using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class studentController : MonoBehaviour
{
    private Animator anim;
    public float MaxSpeed = 1;
    public AudioClip slapNoise;
    public AudioSource punch_audio;

    public enum StudentState
    {
        ChaseBuzz,
        Wander,
        Ragdoll
    }
    private StudentState currentState;

    public NavMeshAgent agent;
    public GameObject buzz;
    public float whiteClawActivationDistance;

    // for student waypoints
    public GameObject[] waypoints;
    private int currWaypoint; 
    Vector3 nextSpawnPosition;
    public GameObject studentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentState = StudentState.Wander;

        anim = GetComponent<Animator>();

        //for ragdolling
        SetKinematic(true);

        currWaypoint = -1;
        setNextWaypoint();

        whiteClawActivationDistance = 30;
        buzz = GameObject.Find("Buzz_Root_Motion");
    }

    // Update is called once per frame
    void Update()
    {   

        if (currentState == StudentState.Ragdoll)
		{
			SetKinematic(false);
			GetComponent<Animator>().enabled=false;
            currentState = StudentState.Ragdoll;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<HittableScript>().enabled = false;
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform transform in transforms) {
                if (transform.gameObject.tag == "Icon")
                    transform.gameObject.layer = 16;
            }
            return;
		}
        if (anim == null)
        {
            return;
        }

        float distFromBuzz = float.MaxValue;
        bool whiteClawIsActive = false;

        if (buzz != null)
        {
            distFromBuzz = (buzz.transform.position - this.gameObject.transform.position).magnitude;
            whiteClawIsActive = buzz.GetComponent<PowerupCollector>().IsWhiteClawActive();
        }

        if (whiteClawIsActive && distFromBuzz < whiteClawActivationDistance)
        {
            ChaseBuzz();
        }
        else if ((currentState == StudentState.ChaseBuzz && !whiteClawIsActive) || (agent.remainingDistance == 0 && !agent.pathPending))
        {
            setNextWaypoint();
        }


        anim.SetFloat("VelY", agent.velocity.magnitude);
    }
    void forceMove(float x, float y)
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

    // overloaded method, in case we want to supply a next waypoint for when students run away
    // Ben: Maybe lets rename this method to not be confusing with the original setNextWayPoint
    public void setRunawayWaypoint(Vector3 next_pos)
    {
        MaxSpeed = 5;
        nextSpawnPosition = next_pos; // use this for the next spawn point if the current student gets slapped
        agent.SetDestination(next_pos);
        agent.speed = MaxSpeed;
    }

    public void setNextWaypoint()
    {
        MaxSpeed = 1;
        //currWaypoint = (currWaypoint + 1) % waypoints.Length;
        currWaypoint = (int)Random.Range(0, waypoints.Length);
        nextSpawnPosition = waypoints[currWaypoint].transform.position; // use this for the next spawn point if the current student gets slapped
        agent.SetDestination(waypoints[currWaypoint].transform.position);
        agent.speed = MaxSpeed;
        currentState = StudentState.Wander;
    }

    public void ChaseBuzz()
    {
        MaxSpeed = 3;
        agent.SetDestination(buzz.transform.position + new Vector3(1, 1, 0));
        currentState = StudentState.ChaseBuzz;
    }

    public void setRagdoll(SlapperScript sc)
    {
        if(currentState == StudentState.Ragdoll)
        {
            return;
        }
        currentState = StudentState.Ragdoll;
        punch_audio.PlayOneShot(slapNoise, 1.0f);
        //AudioSource.PlayOneShot(slapNoise, this.gameObject.transform.position);

        DropPowerUp();
        spawnNewStudent();
        sc.increaseWantedLevel();
    }
    private void spawnNewStudent()
    {
        GameObject newStudent = (GameObject) Instantiate(studentPrefab, nextSpawnPosition, Quaternion.identity);
        newStudent.GetComponent<HittableScript>().player = null;
        newStudent.GetComponent<Collider>().enabled = true;
        Transform[] transforms = newStudent.GetComponentsInChildren<Transform>();
        foreach (Transform transform in transforms) {
            transform.gameObject.layer = 11;
            
            if (transform.gameObject.tag == "Icon")
                transform.gameObject.layer = 15;
        }

        //Debug.Log("Spawneed new student: " + newStudent);
        //below code if a proof of concept for student spawning, uncommment it to play with it
        //it just spawns a new student on top of the one you slapped
        //Transform position = GetComponent<Transform>();
        //GameObject newStudent = (GameObject) Instantiate(studentPrefab, position.position, Quaternion.identity);
    }

    private int getRandomPowerUpValue()
    {
        float rand = Random.value;
        if (rand <= .1f)
        {
            return 0;
        }
        else if (rand > .1f && rand <= .2f)
        {
            return 1;
        }
        else if (rand > .2f && rand <= .35f)
        {
            return 2;
        }
        else if (rand > .35f && rand <= .5f)
        {
            return 3;
        } 
        else if (rand > .5f && rand <= .85f)
        {
            return 4;
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
                droppedPowerUp = Instantiate(Resources.Load("Honey")) as GameObject;
                break;
            case 2:
                droppedPowerUp = Instantiate(Resources.Load("Latte")) as GameObject;
                break;
            case 3:
                droppedPowerUp = Instantiate(Resources.Load("WhiteClaw")) as GameObject;
                break;
            case 4:
                droppedPowerUp = Instantiate(Resources.Load("Bone Fragment")) as GameObject;
                break;
            default:
                // return no item
                return;
        }

        Vector3 studentPos = this.gameObject.transform.position;
        Vector3 spawnPoint = new Vector3(studentPos.x + 0.2f, studentPos.y + 0.5f, studentPos.z + 0.2f);
        if (droppedPowerUp != null)
        {
            droppedPowerUp.GetComponent<Collectible>().Drop(spawnPoint);
        }
    }
    public GameObject[] getWaypoints()
    {
        return waypoints;
    }
}
