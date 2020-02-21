using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class studentController : MonoBehaviour
{
    private Animator anim;
    public float MaxSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        move(x, y);
    }
    void move(float x, float y)
    {
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
        //transform.position += (Vector3.forward * MaxSpeed) * y * Time.deltaTime;
        //transform.position += (Vector3.right * MaxSpeed) * x * Time.deltaTime;
        transform.Rotate(0,x * 5,0);

    }
}
