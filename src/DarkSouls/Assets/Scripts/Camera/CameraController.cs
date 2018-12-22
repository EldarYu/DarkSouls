using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockTarget
{
    public GameObject target;
    public float halfHeight;
    public ActorManager am;
}

public class CameraController : MonoBehaviour
{
    public bool isAI = false;
    [Header("Camera Settings")]
    public float cameraDampValue;
    public float horizontalSpeed;
    public float verticalSpeed;
    [Range(-60.0f, 100.0f)]
    public float maxEulerX;
    [Range(-60.0f, 100.0f)]
    public float minEulerX;

    [Header("Lock On Settings")]
    public float maxLockDistance = 10.0f;
    public LayerMask lockOnLayer;
    public Image lockDot;

    [HideInInspector]
    public bool lockState = false;
    private GameObject character;
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
        character = camPivot.transform.parent.gameObject;

        IActorController ac = character.GetComponent<IActorController>();
        model = ac.model;
        pi = ac.pi;

        if (!isAI)
        {
            mainCamera = Camera.main.gameObject;
        }

        if (camPivot == null || character == null || model == null || pi == null)
            this.enabled = false;
    }

    private void Update()
    {
        if (pi.LockOn)
            LockUnlock();

        if (lockTarget.target != null)
        {
            lockState = true;

            if (!isAI)
            {
                Vector3 woldPos = lockTarget.target.transform.position + Vector3.up * lockTarget.halfHeight;
                lockDot.transform.position = Camera.main.WorldToScreenPoint(woldPos);
            }

            if (Vector3.Distance(model.transform.position, lockTarget.target.transform.position) >= maxLockDistance)
                LockUnlock(null);

            if (lockTarget.am != null && lockTarget.am.StateM.isDie)
                LockUnlock(null);
        }
        else
        {
            lockState = false;
            lockDot.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (lockTarget.target == null)
        {
            Vector3 temp = model.transform.eulerAngles;

            character.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            eulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            eulerX = Mathf.Clamp(eulerX, minEulerX, maxEulerX);
            camPivot.transform.localEulerAngles = new Vector3(eulerX, 0, 0);

            model.transform.eulerAngles = temp;
        }
        else
        {
            Vector3 tempForward = lockTarget.target.transform.position - character.transform.position;
            tempForward.y = 0;
            character.transform.forward = tempForward;
        }

        if (!isAI)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position, ref currentVelocity, cameraDampValue);
            mainCamera.transform.LookAt(camPivot.transform);
        }
    }

    public void LockUnlock()
    {
        Vector3 boxCenter = model.transform.position + Vector3.up + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, lockOnLayer);

        if (cols.Length == 0)
        {
            LockUnlock(null);
        }
        else
        {
            Collider temp = cols[0];
            if (lockTarget.target == temp.gameObject)
            {
                LockUnlock(null);
                return;
            }

            LockUnlock(temp.gameObject, temp.bounds.extents.y, temp.gameObject.GetComponent<ActorManager>(), true);
        }
    }

    public void LockUnlock(GameObject target)
    {
        lockTarget.target = target;
        lockTarget.halfHeight = 0;
        lockTarget.am = null;
    }

    private void LockUnlock(GameObject target, float halfHeight = 0, ActorManager am = null, bool lookDotEnable = false)
    {
        lockTarget.target = target;
        lockTarget.halfHeight = halfHeight;
        lockTarget.am = am;
        if (!isAI)
            lockDot.gameObject.SetActive(lookDotEnable);
    }

    public void ResetInputDevice(IPlayerInput playerInput)
    {
        pi = playerInput;
    }
}
