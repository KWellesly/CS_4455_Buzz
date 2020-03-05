using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoneBar : MonoBehaviour
{
   
   public Slider slider;

   public void SetMaxBoneCount(int boneCount) {
   		slider.maxValue = boneCount;
   }

   public void SetBoneCount(int boneCount) 
   {
   		slider.value = boneCount;
   }

}
