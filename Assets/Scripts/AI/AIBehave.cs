using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum Charachter{
    zero=0,
    two=2,
    three=3
}


public class AIBehave : MonoBehaviour
{
    public GameObject targetsParent;
    public List<GameObject> targets = new List<GameObject>();
    public List<GameObject> cubes = new List<GameObject>();
    public List<Transform> ropes = new List<Transform>();
    public float radius = 2;
    public Transform collectableMainObject;
    public GameObject prevObject;
    public Charachter charachterEnum;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool haveTarget = false;
    [HideInInspector]
    public Vector3 targetTransform;
    [HideInInspector]
    public string MaterialName;
    public AIManager aIManager;
    
    

    private void Start() {
        
        MaterialName = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.name.Substring(0,1);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        CollectTargetsToList();
    }

    private void Update() {
        
        if(aIManager.CanMove){
            AIActiveToMove();
        }
        
        
    }

    private void CollectTargetsToList(){
        for(int i = 0; i < targetsParent.transform.childCount;i++){
            targets.Add(targetsParent.transform.GetChild(i).gameObject);
        }
    }

    private void AIActiveToMove(){
        if(!haveTarget && targets.Count > 0){
            ChooseTarget();
        }
    }

    private void DesicionToCollectOrGoToBridge(){
        if(cubes.Count >= 5){
            
            int randomRope = Random.Range(0,ropes.Count);
            List<Transform> ropesNonActiveChild = new List<Transform>();
            
            foreach( Transform item in ropes[randomRope].GetChild(1)){
                if(!item.GetComponent<MeshRenderer>().enabled || item.GetComponent<MeshRenderer>().enabled && item.gameObject.tag == "Diz" + MaterialName){
                    ropesNonActiveChild.Add(item);
                    
                }
            }
            
            if(cubes.Count > ropesNonActiveChild.Count){
                try{
                    targetTransform = ropesNonActiveChild[ropesNonActiveChild.Count -1].position;
                }catch{
                    Debug.Log("RopesNonActiveChild overflowing!");
                }
            }else{

                try{
                    targetTransform =  ropesNonActiveChild[cubes.Count].position;
                }catch{
                    Debug.Log("RopesNonActiveChild 2 overflowing!");
                }
            }
            
            

        }else{
            Collider[] hitColliders = Physics.OverlapSphere(transform.position,radius);
            List <Vector3> ourColors= new List<Vector3>();
            
            for(int i=0; i<hitColliders.Length; i++){ 
                
                if(hitColliders[i].tag.StartsWith(MaterialName)){
                    ourColors.Add(hitColliders[i].transform.position);
                    
                }
            }
            if(ourColors.Count > 0){
                targetTransform = ourColors[0];
                
            }else{
                int random = Random.Range(0,targets.Count);
                targetTransform = targets[random].transform.position;
                
            }
        }
    }

    private void ChooseTarget(){
        
        DesicionToCollectOrGoToBridge();

        agent.SetDestination(targetTransform);
        
        if(!animator.GetBool("running")){
            animator.SetBool("running",true);
            
        }
        haveTarget = true;
        
    }

    private void CollectOrPut(Collider target){
        if(target.gameObject.tag.StartsWith(MaterialName)){
            target.transform.SetParent(collectableMainObject);
            Vector3 pos = prevObject.transform.localPosition;

            pos.y += 0.22f;
            pos.z = 0;
            pos.x = 0;
            
            target.transform.localRotation = new Quaternion(0,0,0,1f);

            target.transform.DOLocalMove(pos,0.2f);
            prevObject = target.gameObject;
            
            cubes.Add(target.gameObject);

            try{

                targets.Remove(target.gameObject);
            }catch{
                Debug.Log("Targets overflowed!");
            }
            
            
            target.tag = "Untagged";
            haveTarget= false;
            
        
        }
        else if(target.gameObject.tag == "DizR" || target.gameObject.tag != "Diz" + MaterialName && target.gameObject.tag.StartsWith("Diz")){
            if(cubes.Count > 1){
                GameObject myObject = cubes[cubes.Count - 1];
                cubes.RemoveAt(cubes.Count - 1);
                Destroy(myObject);
                
                target.GetComponent<MeshRenderer>().material = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                target.GetComponent<MeshRenderer>().enabled = true; 

                target.tag = "Diz" + MaterialName;
            }
            else{
                prevObject = cubes[0].gameObject;
                haveTarget = false;
            }
        }
    }

    private void OnTriggerEnter(Collider target) {
        CollectOrPut(target);
    }

    

    

}
