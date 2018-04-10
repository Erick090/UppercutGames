using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_CTRL : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
        // CursorLockMode.
    }

    // Update is called once per frame
    void Update () {
		
	}

    protected void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
