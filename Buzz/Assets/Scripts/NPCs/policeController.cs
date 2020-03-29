using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class policeController : MonoBehaviour
{
    private Animator anim;
    private Transform tr;
    public float MaxSpeed = 1;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject buzz; 
    private int detectionDist;
    private bool buzzCompletedBone;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        detectionDist = 0;
        buzzCompletedBone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null)
        {
            return;
        }
        PowerupCollector pc = buzz.gameObject.GetComponent<PowerupCollector>();
        buzzCompletedBone = pc.DidBuzzCompleteABone();

        SlapperScript ss = buzz.gameObject.GetComponent<SlapperScript>();
        detectionDist = 3 * ss.getWantedLevel();
        //I used distance btwn buzz and the cop, if buzz is close enough, pathfind to him
        //Implement donut chase lofic here maybe?
        Vector3 distVec = buzz.transform.position - tr.position;
        float dist = distVec.magnitude;

        // start with default values and override if donut in view
        float minDist = float.MaxValue;
        Vector3 minDistPos = agent.transform.position;
        if (dist < 15 + detectionDist || buzzCompletedBone)
        {
            GameObject[] droppedDonuts = GameObject.FindGameObjectsWithTag("DroppedDonut");
            foreach (GameObject donut in droppedDonuts)
            {
                float donutDist = (donut.transform.position - tr.position).magnitude;
                if (donutDist < 15 && donutDist < minDist) {
                    minDist = donutDist;
                    minDistPos = donut.transform.position;
                }
            }
            if (minDist.Equals(float.MaxValue))
            {
                agent.SetDestination(buzz.transform.position);
            } else
            {
                agent.SetDestination(minDistPos);
            }
            
            //print("BUZZ!!!!!!!!");
        }
        anim.SetFloat("VelY", agent.velocity.magnitude);
    }
    void OnTriggerEnter(Collider x)
    {
        //print("Collided" + x.gameObject.tag);
        if (x.gameObject.tag == "Player")
        {
            //TODO actually add in the game over screen for when the cop touches buzz, just call scene manager
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
