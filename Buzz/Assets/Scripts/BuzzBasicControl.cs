using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(BuzzInputController))]
public class BuzzBasicControl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private BuzzInputController cinput;

    private Transform leftFoot;
    private Transform rightFoot;

    // Playback variables 
    public float forwardMaxSpeed = 2f;
    public float turnMaxSpeed = 40f;

    // Jump
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;
    private int groundContactCount = 0;
    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<BuzzInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }

    // Start is called before the first frame update
    void Start()
    {
        leftFoot = this.transform.Find("DummySkeleton/root/B-hips/B-thigh_L/B-shin_L/B-foot_L");
        rightFoot = this.transform.Find("DummySkeleton/root/B-hips/B-thigh_R/B-shin_R/B-foot_R");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");

        anim.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputForward = 0f;
        float inputTurn = 0f;

        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
        }

        //switch turn around if going backwards
        if (inputForward < 0f)
            inputTurn = -inputTurn;

        bool isGrounded = IsGrounded; //|| CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        rbody.MovePosition(rbody.position + this.transform.forward * inputForward * Time.deltaTime * forwardMaxSpeed);
        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));  

        //anim.SetFloat("velx", inputTurn); // disabled rotation input for animation 
        anim.SetFloat("vely", inputForward);
        anim.SetBool("isFalling", !isGrounded);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "ground")
        {
            ++groundContactCount;
            // Generate event for ground contact: sounds, whatever 
            //EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }
    }
}
