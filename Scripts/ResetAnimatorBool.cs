using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResetAnimatorBool : StateMachineBehaviour
{
    public string[] Bool;
    public bool[] Status;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i < Bool.Length; i++) 
            animator.SetBool(Bool[i], Status[i]);  
    }
}
