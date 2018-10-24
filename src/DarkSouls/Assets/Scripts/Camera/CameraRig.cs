using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    //private float newX;
    //private float newY;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void RotateCamera(Vector2 axis   float mouseX, float mouseY)
    //{
    //    newX += cameraSettings.mouseXSensitivity * mouseX;
    //    newY += cameraSettings.mouseYSensitivity * mouseY;

    //    Vector3 eulerAngleAxis = new Vector3();
    //    eulerAngleAxis.x = -newY;
    //    eulerAngleAxis.y = newX;

    //    newX = Mathf.Repeat(newX, 360);
    //    newY = Mathf.Clamp(newY, cameraSettings.minAngle, cameraSettings.maxAngle);

    //    Quaternion newRotation = Quaternion.Slerp(pivot.localRotation, Quaternion.Euler(eulerAngleAxis), Time.deltaTime * cameraSettings.rotationSpeed);
    //    pivot.localRotation = newRotation;
    //}
}
