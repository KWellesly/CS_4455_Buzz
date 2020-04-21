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
    private int numBones;
    private bool buzzCompletedBone;
    public AudioClip yell;
    private bool found;
    private float found_time;
    public AudioSource yell_audio;
    private SlapperScript ss;
    private PowerupCollector pc;
    public AudioClip gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        detectionDist = 0;
        buzzCompletedBone = false;
        found = false;
        found_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null)
        {
            return;
        }

        if (ss == null)
        {
            ss = buzz.gameObject.GetComponent<SlapperScript>();
        }

        if (pc == null)
        {
            pc = buzz.gameObject.GetComponent<PowerupCollector>();
        }

        int wantedLevel = ss.getWantedLevel();
        detectionDist = 3 * wantedLevel;
        buzzCompletedBone = pc.DidBuzzCompleteABone();

        //I used distance btwn buzz and the cop, if buzz is close enough, pathfind to him
        //Implement donut chase lofic here maybe?
        Vector3 distVec = buzz.transform.position - tr.position;
        float dist = distVec.magnitude;

        // start with default values and override if donut in view
        float minDist = float.MaxValue;
        Vector3 minDistPos = agent.transform.position;
        if ((wantedLevel > 0 && dist < 15 + detectionDist) || buzzCompletedBone)
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
            // increase speed with respect to wanted level
            agent.speed = 3 + ss.getWantedLevel()*0.3f;

            if (Time.time - found_time > 5)
            {
                found = false;
            }
            if (!found)
            {

                found = true;
                if (Random.Range(0.0f, 1.0f) > 0.6)
                {
                    yell_audio.PlayOneShot(yell, 1.0f);
                } 
                found_time = Time.time;
            }
            
            //print("BUZZ!!!!!!!!");
        } else
        {
            // police will randomly wander if they have nothing to do
            if (agent.remainingDistance == 0 && !agent.pathPending)
            {
                // wander around slowly. walking animation here?
                Vector3 wander_position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)) + agent.GetComponentInParent<Transform>().position;
                agent.SetDestination(wander_position);
                MaxSpeed = 1;
                agent.speed = MaxSpeed;
            }
                
        }
        anim.SetFloat("VelY", agent.velocity.magnitude);
    }

    public void pathTowardsSlappedStudent(Vector3 slapped_pos)
    {
        Vector3 search_area = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)) + slapped_pos;
        agent.SetDestination(search_area);
    }

    void OnTriggerEnter(Collider x)
    {
        //print("Collided" + x.gameObject.tag);
        if (x.gameObject.tag == "Player")
        {
            if (buzz != null && buzz.GetComponent<PowerupCollector>().IsBuzzInvincible())
            {
                // Buzz is invincible
            }
            else
            {
                //TODO actually add in the game over screen for when the cop touches buzz, just call scene manager
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
