using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] playerScripts;

    private PhotonView photonView;

	// Use this for initialization
	void Start () 
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
	}
	
    private void Initialize()
    {
        if(!photonView.isMine)
        {
            playerCamera.SetActive(false);
            foreach(MonoBehaviour m in playerScripts)
            {
                m.enabled = false;            
            }
        }
    }

   
}
