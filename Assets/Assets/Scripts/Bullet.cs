using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float power;
    public PlayerMovement Player;
    public float lifeSteal = 0;
    public TrailRenderer trail;
    private float frequency = 20.0f;  
    private float magnitude = 0.5f;
    private int xDir;
    private Vector3 axis;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Blue"))
        {
            frequency = Random.Range(15.0f, 20.0f);
            magnitude = Random.Range(0.4f, 0.5f);
            speed = Random.Range(speed - 1.0f, speed);
            xDir = Random.Range(0, 2) == 0 ? -1 : 1;
            pos = transform.position;
            axis = new Vector3(xDir, 0, 0); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Blue"))
        {
            pos += transform.up * Time.deltaTime * speed;
            transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
           
        }
        if (transform.position.y < -6.0f || transform.position.y > 5.1f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy Dmg = collision.GetComponent<Enemy>();
            Dmg.TakeDmg(power);
            if(Player.lifeSteal != 0)
            {
                Player.Heal(Player.lifeSteal * power);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Boss"))
        {
            Enemy Dmg = collision.GetComponent<Enemy>();
            Dmg.TakeDmg(power);
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        if(trail != null)
        {
            trail.transform.parent = null;
            trail.autodestruct = true;
            trail.GetComponent<TrailRenderer>().startWidth = 0.03f;
            trail = null;
        }
        
    }
}
