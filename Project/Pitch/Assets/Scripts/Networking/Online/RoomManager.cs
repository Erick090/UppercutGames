using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomManager : MonoBehaviour
{

    [SerializeField] private Text roomName;
    [SerializeField] private Text maxPlayer;
    [SerializeField] private Text mode;
    private GameObject tableInput;

    private List<RoomListing> roomListingHole = new List<RoomListing>();
    [SerializeField] private GameObject Content;

    [SerializeField] private RoomInfo[] rooms;
    [SerializeField] private string[] roomNames;

    void Start()
    {
        tableInput = Resources.Load("Prefabs/LobbyManagement/RoomInput") as GameObject;
    }

    public void OnClick_CreateRoom()
    {
        int max = System.Convert.ToInt32("9");
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = System.Convert.ToByte(maxPlayer.text)};
        //roomOptions.CustomRoomProperties.
        //RoomInfo thisRoom = PhotonNetwork.get

        if (PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default))
        {
            Debug.Log("Create Room successfully sent.");
        }
        else
        {
            Debug.Log("Create Room failed.");
        }


        GameObject roomInput = Instantiate(tableInput, Content.transform);
        Text[] objectsInInput = roomInput.GetComponentsInChildren<Text>();

        foreach (Text element in objectsInInput)
        {
            if(element.name == "Rooms")
            {
                element.text = roomName.text;
            }
            else if(element.name == "PlayerValue")
            {
                element.text = "1/" + roomOptions.MaxPlayers.ToString();
            }
            else if (element.name == "Mode")
            {
                switch(mode.text)
                {
                    case "Deathmatch":
                        element.text = "DM";

                        break;

                    case "Team Deathmatch":
                        element.text = "TDM";

                        break;

                }
            }
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        Debug.Log("Failed reson " +  codeAndMessage[1]);

    }
    
    public virtual void OnCreatedRoom()
    {
        Debug.Log("Created Room: " + PhotonNetwork.room.Name);

    }
   
    private void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach(RoomInfo room in rooms)
        {
            RoomReived(room);
        }
        RemoveOldRooms();
    }

    private void RoomReived(RoomInfo room)
    {
        int index = roomListingHole.FindIndex(x => x.RoomName == room.Name);

        if(index == -1)
        {
            if (room.IsVisible)
            {
                GameObject roomInput = Instantiate(tableInput, Content.transform);

                RoomListing roomListing = roomInput.GetComponent<RoomListing>();
                roomListingHole.Add(roomListing);

                index = (roomListingHole.Count - 1);
            }
        }
        if(index != -1)
        {
            RoomListing roomListing = roomListingHole[index];
            roomListing.SetRoomNameText(room.Name);
            roomListing.SetPlayerValueText(room.MaxPlayers.ToString(), room.PlayerCount.ToString());
            //roomListing.SetModeValueText(room.Name);

            roomListing.Updated = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        foreach(RoomListing roomListing in roomListingHole)
        {
            if (!roomListing.Updated)
            {
                removeRooms.Add(roomListing);
            }
            else
            {
                roomListing.Updated = false;
            }
        }
        foreach(RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObject = roomListing.gameObject;
            roomListingHole.Remove(roomListing);
            Destroy(roomListing);
        }
    }
}
