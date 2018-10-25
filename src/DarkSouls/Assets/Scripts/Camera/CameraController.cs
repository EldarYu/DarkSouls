using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDampValue;
    public float horizontalSpeed;
    public float verticalSpeed;
    [Range(-30.0f, 40f)]
    public float maxEulerX;
    [Range(-30.0f, 40f)]
    public float minEulerX;

    public Transform camPos;

    private IPlayerInput playerInput;
    private Transform camPivot;
    private Transform playerTrans;
    private Transform modelTrans;

    private float eulerX;
    private Vector3 currentVelocity;

    void Awake()
    {
        if (camPos == null)
            this.enabled = false;

        camPivot = camPos.parent;
        playerTrans = camPivot.parent;
        playerInput = playerTrans.GetComponent<IPlayerInput>();
        modelTrans = playerTrans.GetChild(0).transform;
    }

    void Update()
    {
        Vector3 temp = modelTrans.eulerAngles;

        playerTrans.Rotate(Vector3.up, playerInput.Jright * horizontalSpeed * Time.deltaTime);
        eulerX -= playerInput.Jup * verticalSpeed * Time.deltaTime;
        eulerX = Mathf.Clamp(eulerX, minEulerX, maxEulerX);
        camPivot.transform.localEulerAngles = new Vector3(eulerX, 0, 0);

        modelTrans.eulerAngles = temp;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, camPos.position, ref currentVelocity, cameraDampValue);
        transform.eulerAngles = camPos.eulerAngles;
    }
}
