﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyNetwork : MonoBehaviour {

    public static LobbyNetwork Instance;
    public string PlayerName { get; private set; }

    private PhotonView photonView;



    [SerializeField] private GameObject player;

    [SerializeField] private GameObject lobbyCam;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private Transform spawnPos;

    // Use this for initialization
    private void Awake()
    {
        Instance = this;

        PlayerName = "Will#" + Random.Range(9000, 9999);
    }

    void Start ()
    {

        photonView = GetComponent<PhotonView>();

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

    private void OnJoinedRoom()
    {
        Debug.Log("Joined room");
    }

    public void InitializPlayer()
    {
        GameObject Player = PhotonNetwork.Instantiate("Testo", spawnPos.position, spawnPos.rotation, 0);

        if (lobbyCam.GetPhotonView().isMine)
        {
            print("lobbyCam true");
            lobbyCam.SetActive(false);
        }
        else
        {
            lobbyCam.SetActive(false);
        }


        if (lobbyPanel.GetPhotonView().isMine)
        {
            print("lobbyPanel true");

            lobbyPanel.SetActive(false);
        }
        else
            lobbyPanel.SetActive(false);

    }
}
