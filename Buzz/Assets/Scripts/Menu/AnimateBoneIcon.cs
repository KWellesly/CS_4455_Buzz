using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBoneIcon : MonoBehaviour
{

	public DummyPlayer player;

	Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetBoneCount() >= player.GetMaxBoneCount()) {
            anim.SetBool("isFull", true);
        }
    }
}
