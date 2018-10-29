using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTarget
{
    public GameObject target;

}

[DisallowMultipleComponent]
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

    void Start()
    {
        camPivot = transform.parent.gameObject;
        player = camPivot.transform.parent.gameObject;

        ActorController ac = player.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;

        mainCamera = Camera.main.gameObject;

        Cursor.lockState = CursorLockMode.Locked;

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
        mainCamera.transform.LookAt(camPivot.transform);
    }

    public void ResetInputDevice(IPlayerInput playerInput)
    {
        pi = playerInput;
    }

}
