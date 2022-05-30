using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject FinishPanel;


    public void SetStartPanelActive(){
        StartPanel.SetActive(true);
    }
    public void SetStartPanelDeactive(){
        StartPanel.SetActive(false);
    }
    public void SetFinishPanelActive(){
        FinishPanel.SetActive(true);
    }

    public void GameRestart(){
        SceneManager.LoadScene(0);
    }

}
