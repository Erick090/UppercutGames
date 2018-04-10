using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotonNetworkManager : MonoBehaviour {

    [SerializeField] private Text connectingText;
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
        //PhotonNetwork.JoinLobby();
        //PhotonNetwork.CreateRoom("New", null, null);
        


    }
    public virtual void OnJoinedLobby()
    {
        refreshingRooms();

        Debug.Log("You joined the Lobby");

        PhotonNetwork.JoinOrCreateRoom("New", null, null);
        
        
        //PhotonNetwork.JoinRoom("New");
    }
    public virtual void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(player.name, spawnPos.position, spawnPos.rotation, 0);
        lobbyCam.SetActive(false);
    }

    public void refreshingRooms()
    {
        rooms = PhotonNetwork.GetRoomList();
        for (int i = 0; i < rooms.Length; i++)
        {
            roomNames[i] = rooms[i].Name;
        }
    }


    void Update () 
    {
        //NOTE .ToString() not in update to intence
        connectingText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
