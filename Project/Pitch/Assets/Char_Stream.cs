using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Stream : MonoBehaviour {
    
    private PhotonView photonView;
    private Vector3 realPos = Vector3.zero;
    private Vector3 realVelo = Vector3.zero;
    private Quaternion realRot = Quaternion.identity;
    private Rigidbody rigidBody;

	public void Start()
	{
        photonView = GetComponent<PhotonView>();
        rigidBody = GetComponent<Rigidbody>();
	}


	public  void Update()
    {
        

        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, realPos, 0.3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRot, 0.3f);
            rigidBody.velocity = realVelo;
        }
       
    }

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigidBody.velocity);


        }
        else
        {
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();
            realVelo = (Vector3)stream.ReceiveNext();
        }
    }
}
