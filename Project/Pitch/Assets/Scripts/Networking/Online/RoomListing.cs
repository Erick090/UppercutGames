using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomListing : MonoBehaviour {

    [SerializeField] private Text roomNameText;
    [SerializeField] private Text playerValueText;
    [SerializeField] private Text modeValueText;


    public string RoomName { get; private set; }
    public string PlayerValue { get; private set; }
    public string ModeValue { get; private set; }

    public bool Updated { get; set; }

    void Start ()
    {
		
	}

    public void SetRoomNameText(string roomName)
    {
        RoomName = roomName;
        roomNameText.text = RoomName;
    }

    public void SetPlayerValueText(string maxPlayer, string actualPlayer)
    {
        PlayerValue = actualPlayer + "/" + maxPlayer;
        playerValueText.text = PlayerValue;
    }

    public void SetModeValueText(string modeName)
    {
        ModeValue = modeName;
        modeValueText.text = ModeValue;
    }

    public void JoinRoom()
    {
        Text[] rooms = this.GetComponentsInChildren<Text>();

        foreach(Text r in rooms)
        {
            if(r.gameObject.name == "Rooms")
            {
                PhotonNetwork.JoinRoom(r.text);
            }
        }

    }


}
