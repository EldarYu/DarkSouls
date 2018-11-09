using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private ActorManager am;
    private void Awake()
    {
        am = transform.parent.GetComponent<ActorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            WeaponController targetWC = other.gameObject.GetComponentInParent<WeaponController>();

            GameObject attacker = targetWC.wm.am.gameObject;
            GameObject receiver = am.gameObject;

            Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
            Vector3 counterDir = attacker.transform.position - receiver.transform.position;

            float attackingAngle = Vector3.Angle(attacker.transform.forward, attackingDir);
            float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
            float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);

            bool attackValid = (attackingAngle < 45);
            bool counterValid = (counterAngle1 < 30 && Mathf.Abs(counterAngle2 - 180) < 30);


            am.TryDoDamage(other.gameObject.GetComponentInParent<WeaponController>(), attackValid, counterValid);
        }
    }
}
