using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_CTRL : MonoBehaviour {

    [SerializeField] private float lookSensitivity = 5;
    [SerializeField] private float lookSmoothDamp = 0.1f;
    [SerializeField] private GameObject player;


    private float xRotation;
    private float yRotation;
    private float currentXRotation;
    private float currentYRotation;
    private float xRotationV;
    private float yRotationV;


    void Update () 
    {

        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation += Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, yRotation, player.transform.eulerAngles.z);
        transform.eulerAngles = new Vector3(xRotation, player.transform.eulerAngles.y, player.transform.eulerAngles.z);



    }
}
