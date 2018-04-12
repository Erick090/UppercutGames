using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove_Stream : MonoBehaviour {

    private PhotonView photonView;
    private Vector3 realPos = Vector3.zero;
    private Quaternion realRot = Quaternion.identity;

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
    }


    public void Update()
    {


        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, realPos, 0.3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRot, 0.3f);
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);


        }
        else
        {
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();
        }
    }
}

