﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDampValue;
    public float horizontalSpeed;
    public float verticalSpeed;
    [Range(-60.0f, 100.0f)]
    public float maxEulerX;
    [Range(-60.0f, 100.0f)]
    public float minEulerX;

    private GameObject player;
    private GameObject camPivot;
    private GameObject model;
    private GameObject mainCamera;
    private IPlayerInput pi;

    private float eulerX;
    private Vector3 currentVelocity;

    void Awake()
    {
        camPivot = transform.parent.gameObject;
        player = camPivot.transform.parent.gameObject;
        model = player.transform.GetChild(0).gameObject;
        pi = player.GetComponent<IPlayerInput>();
        mainCamera = Camera.main.gameObject;

        if (camPivot == null || player == null || model == null || pi == null || mainCamera == null)
            this.enabled = false;
    }

    void FixedUpdate()
    {
        Vector3 temp = model.transform.eulerAngles;

        player.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        eulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        eulerX = Mathf.Clamp(eulerX, minEulerX, maxEulerX);
        camPivot.transform.localEulerAngles = new Vector3(eulerX, 0, 0);

        model.transform.eulerAngles = temp;

        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref currentVelocity, cameraDampValue);
        mainCamera.transform.eulerAngles = transform.eulerAngles;
    }

}
