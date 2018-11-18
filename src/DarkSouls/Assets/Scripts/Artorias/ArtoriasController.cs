using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtoriasController : IActorController
{
    public delegate void OnActionDelegate();
    public event OnActionDelegate OnActionPressed;
    void Awake()
    {
        pi = GetComponent<IPlayerInput>();
        this.enabled = !(pi == null || Init());
    }

    private void Update()
    {
        LocolMotion();
        Roll();
        //attack

        if (pi.Action)
            OnActionPressed.Invoke();
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrushVec;
        thrushVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }
}
