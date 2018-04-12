using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlayer : MonoBehaviour
{
    [SerializeField] private GameObject spawnPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PhotonNetwork.Destroy(other.gameObject);
            GameObject Player = PhotonNetwork.Instantiate("Testo", spawnPos.transform.position, spawnPos.transform.rotation, 0);

        }
    }

}
