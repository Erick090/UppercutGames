using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotonNetworkManager : MonoBehaviour {

    public string playerName { get; private set; }

    [System.Serializable]
    public class UISettings
    {
        public Text connectingText;
        public Text pingText;
        public Image LobbyPanel;

    }

    public UISettings uiSettings = new UISettings();



    void Start()
    {
        PhotonNetwork.playerName = playerName;

        Debug.Log("Connecting to Server . . .");
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    private void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");

        


    }

    private void OnJoinedLobby()
    {

        Debug.Log("Joined lobby.");

    }


    void Update () 
    {
        //NOTE .ToString() not in update to intence
        uiSettings.connectingText.text = PhotonNetwork.connectionStateDetailed.ToString();
        uiSettings.pingText.text = "Ping:" + PhotonNetwork.GetPing().ToString();


    }
}
