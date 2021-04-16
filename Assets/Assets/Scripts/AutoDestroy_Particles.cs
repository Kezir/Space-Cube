using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy_Particles : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
