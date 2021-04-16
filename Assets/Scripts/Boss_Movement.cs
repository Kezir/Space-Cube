using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement : MonoBehaviour
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    Vector2 targetPosition;
    private Vector3 middle = new Vector3(0, -0.5f);
    private Vector3 right = new Vector3(-0.25f, -1f);
    private Vector3 left = new Vector3(0.25f, -1f);
    private Vector3 left1 = new Vector3(0.25f, -1f);
    private Vector3 left2 = new Vector3(0.45f,- 0.8f);
    private Vector3 right1 = new Vector3(-0.25f, -1f);
    private Vector3 right2 = new Vector3(-0.45f,- 0.8f);
    public float speed;
    public float powerDmg;
    public float bulletSpeed;
    public float health;
    public GameObject[] powerUps;
    public GameObject projectilePrefab;
    public float timeDelay;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GetRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        if((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetRandomPos();
        }

        if (timer > timeDelay)
        {
            timer = 0.0f;
            Shoot();

        }
        timer += Time.deltaTime;

    }

    Vector2 GetRandomPos()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxX);
        return new Vector2(randomX, randomY);
    }

    public void TakeDmg(float dmg)
    {
        health -= dmg;

        if (health <= 0f)
        {
            int powerUp = Random.Range(0, 100);
            if (powerUp < 10)
            {
                int index = Random.Range(0, 3);
                Instantiate(powerUps[index], transform.position, powerUps[index].transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position + middle, projectilePrefab.transform.rotation);
        EnemyBullet _bullet = bullet.GetComponent<EnemyBullet>();
        _bullet.power = powerDmg;
        _bullet.speed = bulletSpeed;
    }
}
