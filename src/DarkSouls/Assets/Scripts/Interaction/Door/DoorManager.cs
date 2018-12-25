using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : IActorManager
{
    public IActorManager bossAm;
    public HudController hudController;
    private EventCasterManager em;
    private MeshRenderer meshRenderer;
    private void Start()
    {
        em = GetComponentInChildren<EventCasterManager>();
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (bossAm.IsDie())
        {
            this.gameObject.SetActive(false);
        }
    }

    public override void StartBossBattle(GameObject obj)
    {
        bossAm.LockTarget(obj);
        hudController.bossStateController.SetBossInfo(bossAm);
    }
}
