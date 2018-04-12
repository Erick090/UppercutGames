using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        WhichScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void WhichScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "MainMenu":
                Cursor.visible = true;
                break;
            case "Game":
                //Cursor.visible = false;
                break;
        }
    }

}
