using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [HideInInspector]
    public bool CanMove = false;

    public void CanMoveSetTrue(){
        CanMove = true;
    }

    public void CanMoveSetFalse(){
        CanMove = false;
    }
}
