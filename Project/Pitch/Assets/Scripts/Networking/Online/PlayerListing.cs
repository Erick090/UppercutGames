using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerListing : MonoBehaviour {

    public PhotonPlayer PhotonPlayer { get; private set; }

    [SerializeField] private Text playerName;

    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        playerName.text = "  " + photonPlayer.NickName;
    }
	
}
