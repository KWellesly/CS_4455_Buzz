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
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null)
        {
            return;
        }

        //I used distance btwn buzz and the cop, if buzz is close enough, pathfind to him
        //Implement donut chase lofic here maybe?
        Vector3 distVec = buzz.transform.position - tr.position;
        float dist = distVec.magnitude;
        if (dist < 15)
        {
            agent.SetDestination(buzz.transform.position);
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
            print("Game should end now");
        }
    }
}
