using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] UnityEvent OnGameStart;
    [SerializeField] UnityEvent OnGameFinish;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    public void StartGame(){
        OnGameStart.Invoke();
    }

    public void FinishGame(){
        OnGameFinish.Invoke();
    }
}
