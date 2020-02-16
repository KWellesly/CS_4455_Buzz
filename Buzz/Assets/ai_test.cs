using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_test : MonoBehaviour
{
    public Animator anim;

    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        tr.Translate(Vector3.forward * Time.deltaTime);
    }
}

