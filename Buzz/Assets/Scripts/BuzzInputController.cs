using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class BuzzInputController : MonoBehaviour
{
    public string Name = "Buzz";
    public Animator anim;

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;
    private SlapperScript slapScript;

    public Camera main_camera;
    public Camera reverse_camera;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public bool Action
    {
        get;
        private set;
    }

    void Awake() {
        anim = this.GetComponent<Animator>();
        slapScript = this.GetComponent<SlapperScript>();
    }

    void Update()
    {
        //GetAxisRaw() so we can do filtering here instead of the InputManager
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis

        if (InputMapToCircular)
        {
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);
        }

        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;

        // Forward speed limit to shift // Shift for running  
        if (Input.GetKey(KeyCode.LeftShift)) // DEBUG: Left Shift Not Working
        {
            forwardSpeedLimit = 1.0f;
        }
        else {
            forwardSpeedLimit = 0.6f; 
        }
        // Filter input and clamp speed 
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
            Time.deltaTime * turnInputFilter);
        

        // Final Variables For Control  
        Forward = filteredForwardInput;
        Turn = filteredTurnInput;
        Action = Input.GetButtonDown("Fire1");

        // Reverse camera if "R" is pressed
        if (Input.GetButtonDown("Reverse"))
        {
            main_camera.enabled = false;
            reverse_camera.enabled = true;
            

        }
        if (Input.GetButtonUp("Reverse"))
        {
            reverse_camera.enabled = false;
            main_camera.enabled = true;
        }

    }

    public void FixedUpdate()
    {
        anim.SetBool("Slap", slapScript.isSlappable && Input.GetButtonDown("Fire1"));
    }
}
