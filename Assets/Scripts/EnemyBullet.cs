using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float power;
    public string color;
    public Vector3 target;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        moveDir = (target - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(color == "Yellow")
        {
            transform.position += moveDir * speed * Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        

        if (transform.position.y < -6.0f || transform.position.y > 6.0f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement Dmg = collision.GetComponent<PlayerMovement>();
            Dmg.TakeDmg(power);
            Destroy(gameObject);
        }


    }
}
