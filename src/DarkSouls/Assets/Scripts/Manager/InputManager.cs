using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool inputEnabled = true;

    [System.Serializable]
    public class KeyMap
    {
        public KeyCode moveForward;
        public KeyCode moveBack;
        public KeyCode moveLeft;
        public KeyCode moveRight;
        public KeyCode run;
    }

    public KeyMap keyMap;

    [System.Serializable]
    public class MouseSettings
    {
        public float mouseXSensitivity;
        public float mouseYsensitivity;
    }
    public MouseSettings mouseSettings;

    public PlayerController playerController;
    public CameraRig cameraRig;
    // Use this for initialization
    void Start()
    {
        if (playerController == null || cameraRig == null)
            this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inputEnabled)
            return;

        playerController.MoveMent(((Input.GetKey(keyMap.moveForward) ? 1.0f : 0) - (Input.GetKey(keyMap.moveBack) ? 1.0f : 0)),
            (Input.GetKey(keyMap.moveRight) ? 1.0f : 0) - (Input.GetKey(keyMap.moveLeft) ? 1.0f : 0));

        playerController.run = Input.GetKey(keyMap.run);

        //cameraRig.RotateCamera()
    }
}
