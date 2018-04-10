using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotonNetworkManager : MonoBehaviour {

    [System.Serializable]
    public class UISettings
    {
        public Text connectingText;
        public Text pingText;
        public Image LobbyPanel;

    }

    public UISettings uiSettings = new UISettings();

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lobbyCam;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private RoomInfo[] rooms;
    [SerializeField] private string[] roomNames;


    void Start () 
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public virtual void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        


    }
    public virtual void OnJoinedLobby()
    {
        //refreshingRooms();

        Debug.Log(PhotonNetwork.lobby.Name);
        PhotonNetwork.CreateRoom("test", null, null);

        PhotonNetwork.JoinRoom("test");

        PhotonNetwork.JoinOrCreateRoom("New", null, null);

    }
    public virtual void OnJoinedRoom()
    {
        Debug.Log("Lobby Name: " + PhotonNetwork.room.Name);

        PhotonNetwork.Instantiate(player.name, spawnPos.position, spawnPos.rotation, 0);
        lobbyCam.SetActive(false);
    }

    public void refreshingRooms()
    {
        rooms = PhotonNetwork.GetRoomList();
        Debug.Log(rooms.Length);

        for (int i = 0; i < rooms.Length; i++)
        {
            roomNames[i] = rooms[i].Name;
            Debug.Log(rooms[i].Name);
        }
    }


    void Update () 
    {
        //NOTE .ToString() not in update to intence
        uiSettings.connectingText.text = PhotonNetwork.connectionStateDetailed.ToString();
        uiSettings.pingText.text = "Ping:" + PhotonNetwork.GetPing().ToString();

        Debug.Log(rooms.Length);

    }
}
