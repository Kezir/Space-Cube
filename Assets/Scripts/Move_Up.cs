using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Up : MonoBehaviour
{
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        


    }
}
