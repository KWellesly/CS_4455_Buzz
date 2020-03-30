using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDonutScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drop(float duration, Vector3 spawnPoint)
    {
        this.gameObject.transform.position = spawnPoint;
        Destroy(this.gameObject, duration);
    }
}
