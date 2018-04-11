using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    delegate Vector3 Direction();

    public Vector3 EndPositionLeft;
    public Vector3 EndPositionRight;
    public Vector3 startPosition;

    [SerializeField]
    string shotButton = "Shot";

    Direction dirVec;

	// Use this for initialization
	void Start () {
        dirVec = new Direction(LerpShot);
	}
	
	// Update is called once per frame
	void Update () {
        dirVec();

        ChangeDirection();
	}

    public Vector3 LerpLeft()
    { 
        return this.transform.position = Vector3.Lerp(this.transform.position, EndPositionLeft, 0.5f);
    }

    public Vector3 LerpRight()
    {
        return this.transform.position = Vector3.Lerp(this.transform.position, EndPositionRight, 0.5f);
    }

    Vector3 LerpShot()
    {
        return this.transform.position = Vector3.Lerp(this.transform.position,
                                               EndPositionLeft,
                                               0.5f);
    }

    Vector3 LerpBack()
    {
        return this.transform.position = this.transform.position = Vector3.Lerp(this.transform.position,
                                               startPosition,
                                               0.5f);
    }

    void ChangeDirection()
    {
        if (Input.GetButtonDown(shotButton))
            //StartCoroutine(Shooting(20, 0.5f));
            dirVec = new Direction(LerpShot);

        if (Input.GetKeyDown(KeyCode.L))
            dirVec = new Direction(LerpBack);
    }

    IEnumerator Shooting(float range, float time)
    {
        Vector3 savedPosition = this.transform.position;


        yield return new WaitForSeconds(2);

        this.transform.position = Vector3.Lerp(this.transform.position, savedPosition, time);
    }
}
