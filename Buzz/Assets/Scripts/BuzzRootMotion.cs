using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(BuzzInputController))]
public class BuzzRootMotion : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private BuzzInputController cinput;

    private Transform leftFoot;
    private Transform rightFoot;

    // Playback variables 
    public float animationSpeed = 1.2f;
    public float rootMovementSpeed = 2f;
    public float rootTurnSpeed = 30f;
    public float turnMaxSpeed = 80f;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Initial Setup
        float inputForward = 0f;
        float inputTurn = 0f;
        bool inputAction = false;

        anim.speed = animationSpeed;
        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
            inputAction = cinput.Action;
        }

        //switch turn around if going backwards
        if (inputForward < 0f)
            inputTurn = -inputTurn;

        // Jump
        bool isGrounded = IsGrounded;// || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));
        // Movement
        anim.SetFloat("velx", inputTurn);
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

    void OnAnimatorMove()
    {
        Vector3 newRootPosition;
        Quaternion newRootRotation;
        bool isGrounded = IsGrounded; //|| CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        if (isGrounded)
        {
            //use root motion as is if on the ground		
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }
        //use rotational root motion as is
        newRootRotation = anim.rootRotation;
        this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);//newRootPosition; 
        this.transform.rotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);//newRootRotation;
    }
}
