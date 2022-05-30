using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerControl : MonoBehaviour
{ 

    public Transform collectableMainObject;
    public GameObject prevObject;
    public List<GameObject> cubes = new List<GameObject>();



    private void OnTriggerEnter(Collider target) {
        
        Collect(target);

        Putting(target);
        
    }

    private void Putting(Collider target){
        if(cubes.Count > 1 &&  target.gameObject.tag == "DizR" || cubes.Count > 1 && target.gameObject.tag != "Diz" + transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0,1) && target.gameObject.tag.StartsWith("Diz")){
            
            GameObject myObject = cubes[cubes.Count - 1];
            cubes.RemoveAt(cubes.Count - 1);
            Destroy(myObject);
                
            target.GetComponent<MeshRenderer>().material = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            target.GetComponent<MeshRenderer>().enabled = true; 

            target.tag = "Diz" + transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0,1);

            prevObject = cubes[0].gameObject;
            
            
        }
    }

    private void Collect(Collider target){
        if(target.gameObject.tag.StartsWith(transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0,1))){
            target.transform.SetParent(collectableMainObject);
            Vector3 pos = prevObject.transform.localPosition;

            pos.y += 0.33f;
            pos.z = 0;
            pos.x = 0;

            target.transform.localRotation = new Quaternion(0,0,0,1);

            target.transform.DOLocalMove(pos,0.5f);
            prevObject = target.gameObject;
            cubes.Add(target.gameObject);

            
            target.tag = "Untagged";
            
            
        }
    }





}
