using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockTarget
{
    public GameObject target;
    public float halfHeight;
    public LockTarget(GameObject target = null, float halfHeight = 0)
    {
        this.target = target;
        this.halfHeight = halfHeight;
    }
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

    public float maxLockDistance = 10.0f;
    public LayerMask lockOnLayer;
    public Image lockDot;
    public bool lockState = false;
    public bool isAI = false;

    private GameObject player;
    private GameObject camPivot;
    private GameObject model;
    private GameObject mainCamera;
    private IPlayerInput pi;

    private float eulerX;
    private Vector3 currentVelocity;
    private LockTarget lockTarget = new LockTarget();

    void Start()
    {
        camPivot = transform.parent.gameObject;
        player = camPivot.transform.parent.gameObject;

        ActorController ac = player.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;

        if (!isAI)
        {
            mainCamera = Camera.main.gameObject;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (camPivot == null || player == null || model == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        if (pi.LockOn)
            LockOnLock();

        if (lockTarget.target != null)
        {
            lockState = true;

            if (!isAI)
            {
                lockDot.enabled = true;
                Vector3 woldPos = lockTarget.target.transform.position + Vector3.up * lockTarget.halfHeight;
                lockDot.transform.position = Camera.main.WorldToScreenPoint(woldPos);
            }

            if (Vector3.Distance(model.transform.position, lockTarget.target.transform.position) >= maxLockDistance)
                lockTarget.target = null;
        }
        else
        {
            lockState = false;

            if (!isAI)
                lockDot.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (lockTarget.target == null)
        {
            Vector3 temp = model.transform.eulerAngles;

            player.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            eulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            eulerX = Mathf.Clamp(eulerX, minEulerX, maxEulerX);
            camPivot.transform.localEulerAngles = new Vector3(eulerX, 0, 0);

            model.transform.eulerAngles = temp;
        }
        else
        {
            Vector3 tempForward = lockTarget.target.transform.position - player.transform.position;
            tempForward.y = 0;
            player.transform.forward = tempForward;
        }

        if (!isAI)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref currentVelocity, cameraDampValue);
            mainCamera.transform.LookAt(camPivot.transform);
        }
    }

    public void LockOnLock()
    {
        Vector3 boxCenter = model.transform.position + Vector3.up + mainCamera.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), mainCamera.transform.rotation, lockOnLayer);

        if (cols.Length == 0)
        {
            lockTarget.target = null;
        }
        else
        {
            Collider temp = cols[0];
            if (lockTarget.target == temp.gameObject)
            {
                lockTarget.target = null;
                return;
            }

            lockTarget = new LockTarget(temp.gameObject, temp.bounds.extents.y);
        }
    }

    public void ResetInputDevice(IPlayerInput playerInput)
    {
        pi = playerInput;
    }

}
