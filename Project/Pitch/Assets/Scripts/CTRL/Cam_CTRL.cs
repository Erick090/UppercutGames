using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cam_CTRL : Game_CTRL {

    [SerializeField] private float lookSensitivity = 5;
    [SerializeField] private float lookSmoothDamp = 0.1f;

    [Space(10, order = 1)]
    [SerializeField] private GameObject player;

    #region rotationProperties
    private float xRotation;
    private float yRotation;
    private float currentXRotation;
    private float currentYRotation;
    private float xRotationV;
    private float yRotationV;
    #endregion

    #region positionProperties
    Transform camPosStart;
#endregion

    private void Start()
    {
        camPosStart = this.transform;
    }

    void Update () 
    {
        if (player == null)
            return;

        CameraMovement();



    }

    private void CameraMovement()
    {
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation += Input.GetAxis("Mouse Y") * lookSensitivity;

        // use Formula from geogebra here = f(x) = x²/10
        this.transform.position += new Vector3(0, Input.GetAxis("Mouse Y") * lookSensitivity, 0);
        xRotation = Mathf.Clamp(Input.GetAxis("Mouse Y") * lookSensitivity, 0, 3);
        //this.transform.position = new Vector3(xRotation,Mathf.Clamp(transform.localPosition.y, 0, 3), 0);
        //currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        //currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, yRotation, player.transform.eulerAngles.z);
        //player.transform.eulerAngles = new Vector3(xRotation, 0, player.transform.eulerAngles.z);

        this.transform.LookAt(player.transform);
    }
}
