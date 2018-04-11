using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour {

    private GameObject playerInput;


    private List<PlayerListing> playerListings = new List<PlayerListing>();

    void Start()
    {
        playerInput = Resources.Load("Prefabs/LobbyManagement/PlayerInput") as GameObject;

    }

    private void OnJoinedRoom()
    {
        PhotonPlayer[] photonPlayer = PhotonNetwork.playerList;
        for(int i = 0; i< photonPlayer.Length; i++)
        {
            PlayerJoinedRoom(photonPlayer[i]);
        }
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if(photonPlayer == null)
        {
            return;
        }
        PlayerLeftRoom(photonPlayer);

        GameObject Input = Instantiate(playerInput, this.transform);

        PlayerListing playerListing = Input.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        playerListings.Add(playerListing);


    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer phothonPlayer)
    {
        PlayerLeftRoom(phothonPlayer);
    }

    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = playerListings.FindIndex(x => x.PhotonPlayer == photonPlayer);

        if(index != -1)
        {
            Destroy(playerListings[index].gameObject);
            playerListings.RemoveAt(index);
        }
    }
}
