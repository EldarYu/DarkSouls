using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : IActorManager
{
    public IActorManager bossAm;
    public HudController hudController;
    private EventCasterManager em;
    private void Start()
    {
        em = GetComponentInChildren<EventCasterManager>();

        //
        StartCoroutine(TestFunc());
    }

    IEnumerator TestFunc()
    {
        yield return new WaitForSeconds(2);
        StartBossBattle(GameObject.FindGameObjectWithTag("Player"));
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
