using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnable : MonoBehaviour
{
    public void SetControllerScriptEnable(){
        GetComponent<PlayerMovement>().enabled = true;
    }

    public void SetControllerScriptDisable(){
        GetComponent<PlayerMovement>().enabled = false;
    }
}
