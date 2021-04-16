using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Guided : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        var actualTargetPosition = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z);
        var MT = Vector3.MoveTowards(transform.position, actualTargetPosition, 1f);

        transform.position = MT;
    }
}
