using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimSet : MonoBehaviour
{
    public Animator animator;

    public void SetWinAnimation(){
        animator.SetBool("isWin",true);
    }
}
