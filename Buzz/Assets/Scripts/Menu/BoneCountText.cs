using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoneCountText : MonoBehaviour
{
	public PowerupCollector pc;
	public Text boneCount;

    // Update is called once per frame
    void Update()
    {
        boneCount.text = pc.GetBoneCount().ToString();
    }
}
