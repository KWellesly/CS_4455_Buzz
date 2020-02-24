using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollector : MonoBehaviour
{

    public bool hasDonut;
    public bool hasLatte;
    public bool hasWhiteClaw;
    public int numBoneFragments;

    // Start is called before the first frame update
    void Start()
    {
        hasDonut = hasLatte = hasWhiteClaw = false;
        numBoneFragments = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveDonut()
    {
        hasDonut = true;
    }

    public void ExpireDonut()
    {
        hasDonut = false;
    }

    public void ReceiveLatte()
    {
        hasLatte = true;
    }

    public void ExpireLatte()
    {
        hasLatte = false;
    }

    public void ReceiveWhiteClaw()
    {
        hasWhiteClaw = true;
    }

    public void ExpireWhiteClaw()
    {
        hasWhiteClaw = false;
    }

    public void ReceiveBoneFragment()
    {
        numBoneFragments++;
    }
}
