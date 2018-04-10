using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManger : MonoBehaviour {

    private void Start()
    {
    }


    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene( scene , LoadSceneMode.Single);
    }

    public void OpenPanel()
    {
        
    }

    public void ClosePanel()
    {

    }
}
