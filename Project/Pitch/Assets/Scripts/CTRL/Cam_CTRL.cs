using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cam_CTRL : Game_CTRL {

    [SerializeField] private float lookSensitivity = 5;
    [SerializeField] private float lookSmoothDamp = 0.1f;

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
    [Header("Camera values")]
    [Tooltip("Regulates local (trivia: dependent on position from parent) min height of camera")]
    [SerializeField] private float minCameraHeight = 0f;

    [Tooltip("Regulates local(trivia: dependent on position from parent) max height of camera")]
    [SerializeField] private float maxCameraHeight = 4.6f;
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
        yRotation = Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

        // use Formula from geogebra here = f(x) = x²/10
        //Vector3 playerVec = player.transform.position;
        //Vector3 cameraVec = this.transform.position;
        //playerVec.x = 0;
        //cameraVec.x = 0;

        //Vector3 dirVec = (playerVec - cameraVec).normalized;

        //this.transform.position = playerVec + (dirVec * 5);

        //        this.transform.RotateAround(player.transform.position, this.transform.right, yRotation);
        //      Mach eine if-Abfrage für einen float, dre in den max-Wert für Clamp kommt

        float tempFloat = xRotation + this.transform.localEulerAngles.x;

        print(xRotation);

        if (tempFloat <= maxCameraHeight && tempFloat >= minCameraHeight)
        {
            this.transform.RotateAround(this.transform.position, this.transform.right, xRotation);
        }
        //Mathf.Clamp(this.transform.rotation.x, 0, 10);
        //Vector3 temp = this.transform.localEulerAngles;
        //temp.x = Mathf.Clamp(temp.x, minCameraHeight, maxCameraHeight);
        //this.transform.localEulerAngles = temp;

        //if(xRotation > 0)
        //{
        //    this.transform.localPosition -= new Vector3(0, (Mathf.Pow(Input.GetAxis("Mouse Y"), 2)) * lookSensitivity, 0);
        //}

        //else
        //{
        //    this.transform.localPosition += new Vector3(0, (Mathf.Pow(Input.GetAxis("Mouse Y"), 2)) * lookSensitivity, 0);
        //}

        //this.transform.localPosition = new Vector3(this.transform.localPosition.x, Mathf.Clamp(this.transform.localPosition.y, minCameraHeight, maxCameraHeight), this.transform.localPosition.z);
        //xRotation = Mathf.Clamp(Input.GetAxis("Mouse Y") * lookSensitivity, minCameraHeight, maxCameraHeight);
        //this.transform.position = new Vector3(xRotation,Mathf.Clamp(transform.localPosition.y, 0, 3), 0);
        //currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        //currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        //player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, yRotation, player.transform.eulerAngles.z);
        //player.transform.eulerAngles = new Vector3(xRotation, 0, player.transform.eulerAngles.z);

        //this.transform.LookAt(player.transform);
    }
}
