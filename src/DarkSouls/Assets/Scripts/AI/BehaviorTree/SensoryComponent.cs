using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensoryComponent : MonoBehaviour
{
    public GameObject editorTest;
    public GameObject target;

    private MeshCollider meshCollider;

    private void Awake()
    {
#if UNITY_EDITOR
        editorTest.SetActive(true);
#endif
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
            target = other.gameObject;
    }

    public void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
