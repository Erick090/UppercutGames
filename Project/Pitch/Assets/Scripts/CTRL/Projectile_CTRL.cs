using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_CTRL : MonoBehaviour {

    delegate Vector3 Direction();

    [Header("Speed parameters for punch shot")]
    [Tooltip("Speed of shoot")]
    [SerializeField]
    float speedShot = 5f;

    [Tooltip("Speed of back move")]
    [SerializeField]
    float speedBack = 5f;

    [Space(5)]
    [Tooltip("Duration of shot")]
    [SerializeField]
    float timeShot = 2f;

    [HideInInspector]
    public bool isShooting = false;

    Vector3 savedLocalPosition;

    [Space(10)]
    [SerializeField]
    string shotButton = "Shot";

    Direction dirVec;
    PhotonView photonView;

    [SerializeField]
    Char_CTRL playerRBody;

    [HideInInspector]
    public Vector3 shootDir;


    float knockbackValue = 100f;

    //[SerializeField]
    //Char_CTRL player;

	// Use this for initialization
	void Start () {
        //dirVec = new Direction(LerpShot);
        photonView = new PhotonView();
        savedLocalPosition = this.transform.localPosition;
        print("Saved: " + savedLocalPosition);
        print("Actual: " + this.transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {

        //if (isShooting)
        //dirVec();

        if (Input.GetButtonDown(shotButton))
        {
            shootDir = playerRBody.transform.TransformDirection(new Vector3(knockbackValue, knockbackValue, knockbackValue));
            StartCoroutine(Shooting());
        }

        PunchShoot(speedShot, speedBack);

    }

    //public Vector3 LerpLeft()
    //{ 
    //    return this.transform.position = Vector3.Lerp(this.transform.position, EndPositionLeft, 0.5f);
    //}

    //public Vector3 LerpRight()
    //{
    //    return this.transform.position = Vector3.Lerp(this.transform.position, EndPositionRight, 0.5f);
    //}

    //Vector3 LerpShot()
    //{
    //    return this.transform.position += Vector3.Lerp(this.transform.localPosition,
    //                                           new Vector3(this.transform.localPosition.x,
    //                                                       this.transform.localPosition.y,
    //                                                       this.transform.localPosition.z + range),
    //                                           0.5f);
    //}

    //Vector3 LerpBack()
    //{
    //    return this.transform.position = this.transform.position = Vector3.Lerp(this.transform.localPosition,
    //                                           startPosition,
    //                                           0.5f);
    //}

    void PunchShoot(float outSpeed, float inSpeed)
    {
        if (isShooting)
            this.transform.localPosition += new Vector3(0, 0, Time.deltaTime * outSpeed);

        if (!isShooting && this.transform.localPosition != savedLocalPosition)
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, savedLocalPosition, Time.deltaTime * inSpeed);
    }

    public IEnumerator Shooting()
    {
        ////startPosition = this.transform.localPosition;

        ////isShooting = true;
        //Instantiate(punchProj, player.transform);
        //punchProj.transform.localPosition += new Vector3(0, 0, Time.deltaTime * range);

        ////dirVec = new Direction(LerpShot);

        //print("ShootStart");

        if(!isShooting)
        {
            isShooting = true;
        }

        yield return new WaitForSeconds(timeShot);

        if (isShooting)
        {
            isShooting = false;
        } 

        //print("ShootEnd");

        ////dirVec = new Direction(LerpBack);


        ////Vector3 savedTransform = punchProj.transform.position;
        ////punchProj.transform.position = Vector3.Lerp(punchProj.transform.position,
        ////                                             new Vector3(punchProj.transform.position.x,
        ////                                                         punchProj.transform.position.y,
        ////                                                         punchProj.transform.position.z + range),
        ////                                             time);

        //Destroy(punchProj);

        //isShooting = false;
        ////this.transform.position = Vector3.Lerp(this.transform.position, savedPosition, time);
    }
}
