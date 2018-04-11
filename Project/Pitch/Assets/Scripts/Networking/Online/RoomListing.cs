using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomListing : MonoBehaviour {

    [SerializeField] private Text roomNameText;

    public string RoomName { get; private set; }

    public bool Updated { get; set; }

    void Start ()
    {
		
	}

    public void SetRoomNameText(string text)
    {
        RoomName = text;
        roomNameText.text = RoomName;
    }

    public void JoinRoom()
    {
        string room;
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
