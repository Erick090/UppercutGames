using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_CTRL : MonoBehaviour {

    delegate Vector3 Direction();

    public Vector3 EndPositionLeft;
    public Vector3 EndPositionRight;
    public Vector3 startPosition;

    bool isShooting = false;

    [SerializeField]
    string shotButton = "Shot";

    Direction dirVec;

	// Use this for initialization
	void Start () {
        //dirVec = new Direction(LerpShot);
	}
	
	// Update is called once per frame
	void Update () {

        ChangeDirection();

        if (isShooting)
        dirVec();



    }

    public Vector3 LerpLeft()
    { 
        return this.transform.position = Vector3.Lerp(this.transform.position, EndPositionLeft, 0.5f);
    }

    public Vector3 LerpRight()
    {
        return this.transform.position = Vector3.Lerp(this.transform.position, EndPositionRight, 0.5f);
    }

    Vector3 LerpShot(float range, float time)
    {
        return this.transform.position += Vector3.Lerp(this.transform.localPosition,
                                               new Vector3(this.transform.localPosition.x,
                                                           this.transform.localPosition.y,
                                                           this.transform.localPosition.z + range),
                                               0.5f);
    }

    Vector3 LerpBack(float time)
    {
        return this.transform.position = this.transform.position = Vector3.Lerp(this.transform.localPosition,
                                               startPosition,
                                               0.5f);
    }

    void ChangeDirection()
    {
        if (Input.GetButtonDown(shotButton))
        {
            //this.transform.position += new Vector3(0, 0, Time.deltaTime * 50 / 0.5f);
            StartCoroutine(Shooting(5f, 0.5f));
        }
        //StartCoroutine(Shooting(20, 0.5f));
        //dirVec = new Direction(LerpShot);

        //if (Input.GetKeyDown(KeyCode.L))
        //    dirVec = new Direction(LerpBack);
    }

    IEnumerator Shooting(float range, float time)
    {
        startPosition = this.transform.localPosition;

        isShooting = true;

        //dirVec = new Direction(LerpShot(5, 0.5f));

        yield return new WaitForSeconds(2);

        //dirVec = new Direction(LerpBack);

        isShooting = false;
        //this.transform.position = Vector3.Lerp(this.transform.position, savedPosition, time);
    }
}
