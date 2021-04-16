using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Type")]
    public string type;

    [Header("Boss")]
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public Animator[] cubesAnimation;
    public string currentBossColor = "Shambles";
    public string currentColor = "White";
    private bool setColor;
    public Image HealthImage;
    private Spawn spawnGameObj;
    public GameObject player;

    [Header("Soldier")]
    public GameObject Waypoint;
    public Transform[] points;

    [Header("Common")]
    public GameObject DestroyParticle;
    public GameObject[] powerUps;
    public GameObject projectilePrefab;
    public int scoreValue;
    public string movement;
    public float speed;
    public float powerDmg;
    public float bulletSpeed;
    public float health;
    public Vector2 targetPosition;
    private Vector3 middle = new Vector3(0, -0.5f);
    private bool reached = false;
    private float maxHealth;
    private int waypointIndex;
    private float shootRate, shootRateBoss;
    private float timer = 0.0f;
    private float Radius = 0.05f;
    private float spiralX;
    private float spiralY;
    private Vector3 pos;
    public float time;

    private int bulletCount = 0;
    private Vector2 _centre;
    private float _angle;


    // Start is called before the first frame update

    void Start()
    {
        maxHealth = health;
        shootRate = Random.Range(2.0f, 8.0f);
        shootRateBoss = Random.Range(1.0f, 3.0f);
        if (type == "Soldier")
        {
            if(movement == "Waypoints")
            {
                waypointIndex = 0;
                points = new Transform[Waypoint.transform.childCount];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = Waypoint.transform.GetChild(i);
                }
                targetPosition = points[0].transform.position;
            }
            else if(movement == "Spiral")
            {
                spiralX = -0.5f;
                spiralY = -0.8f;
                speed = 1.5f;
                pos = Waypoint.transform.position;
            }
            
        }
        else if(type == "Boss")
        {
 
            HealthImage = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Image>();
            HealthImage.fillAmount = health / maxHealth;
            targetPosition = GetRandomPos();
            spawnGameObj = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spawn>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(type == "Soldier")
        {
            if (movement == "Waypoints")
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, targetPosition) <= 0.6f)
                {
                    targetPosition = GetNextPos();
                }
            }
            else if (movement == "Wave")
            {
                if (reached == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                    if (Vector2.Distance(transform.position, targetPosition) <= 0.2f)
                    {
                        reached = true;
                        _centre = transform.position;
                    }
                }
                else
                {
                    _angle += speed * Time.deltaTime;

                    var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
                    transform.position = _centre + offset;
                }
            }
            else if (movement == "Spiral")
            {

                pos += new Vector3(spiralX, spiralY, 0f) * Time.deltaTime * speed;
                time -= Time.deltaTime;
                transform.position = pos + new Vector3(Mathf.Sin(time) * 1.5f, 0.0f, 0.0f);
                if (transform.position.x < -4 && transform.position.y < -2)
                {
                    pos = new Vector3(-Waypoint.transform.position.x, Waypoint.transform.position.y, 0);
                    spiralX = -spiralX;
                }

            }
            else if (movement == "Free Roam")
            {
                if ((Vector2)transform.position != targetPosition)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                }
                else
                {
                    targetPosition = GetRandomPos();
                }
            }
            else if (movement == "Random")
            {
                transform.position += Vector3.down * Time.deltaTime;
            }

            if (timer > shootRate)
            {
                timer = 0.0f;
                Shoot();
            }
        }
        else if(type == "Boss")
        {
            if ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                targetPosition = GetRandomPos();
                if( currentBossColor == "Shambles" && timer > 5f)
                {
                    timer = 0;
                    currentBossColor = "Color";
                }
                else if(currentBossColor == "Color" && timer > 15f)
                {
                    timer = 0;
                    currentBossColor = "Shambles";
                    setColor = false;
                }
                
            }

            if(currentBossColor == "Color" && setColor == false)
            {
                StopAllCoroutines();
                Invoke("ChangeColorBoss",0.5f);
                Invoke("ShootColor", 5f);
                setColor = true;
                
            }

            else if(currentBossColor == "Shambles")
            {
                WhiteColorBoss();
                ChangeColorBoss();
            }
            
        }
        

        
        timer += Time.deltaTime;

        if(transform.position.x > 5 || transform.position.x < -5 || transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

    }
    private void ShootColor()
    {
        if(currentColor == "White")
        {
            spawnGameObj.SpawnWaveBoss();
        }
        else if(currentColor == "Blue")
        {
            Debug.Log("Blue");
        }
        else if (currentColor == "Red")
        {
            Debug.Log("Red");
        }
        else if (currentColor == "Green")
        {
            health += 0.3f * maxHealth;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
            HealthImage.fillAmount = health / maxHealth;
            
        }
        else if (currentColor == "Yellow")
        {
            InvokeRepeating("ShootYellowColor", 0f, 1.5f);
        }
        else if (currentColor == "Orange")
        {
            Debug.Log("Orange");
        }

    }

    private void ShootYellowColor()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position + middle, projectilePrefab.transform.rotation);
        EnemyBullet _bullet = bullet.GetComponent<EnemyBullet>();
        _bullet.power = powerDmg;
        _bullet.speed = 1f;
        if (bulletCount < 10)
        {
            bulletCount++;
        }
        else
        {
            bulletCount = 0;
            CancelInvoke();
        }
    }
    private Vector2 GetRandomPos()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxX);
        return new Vector2(randomX, randomY);
    }

    private void ChangeColorBoss()
    {
        int randomColor = Random.Range(0, 6);
        if(currentBossColor == "Color")
        {
            switch (randomColor)
            {
                case 0:
                    currentColor = "White";
                    break;
                case 1:
                    currentColor = "Blue";
                    break;
                case 2:
                    currentColor = "Yellow";
                    break;
                case 3:
                    currentColor = "Red";
                    break;
                case 4:
                    currentColor = "Green";
                    break;
                case 5:
                    currentColor = "Orange";
                    break;
            }
        }
        
        for(int i = 0; i < cubesAnimation.Length; i++)
        {
            if(currentBossColor == "Color")
            {                
                StartCoroutine(ExecuteAfterTime(Random.Range(0f, 1f), i, randomColor));
                
            }
            else if(currentBossColor == "Shambles")
            {
                StartCoroutine(ExecuteAfterTime(Random.Range(0f, 0.5f), i, randomColor));
            }
            
            
        }
    }

    IEnumerator ExecuteAfterTime(float time, int number, int color)
    {
        yield return new WaitForSeconds(time);

        cubesAnimation[number].SetInteger("Color", color);
    }
    private void WhiteColorBoss()
    {
        for (int i = 0; i < cubesAnimation.Length; i++)
        {
            cubesAnimation[i].SetInteger("Color", 0);
        }
    }

    private Vector2 GetNextPos()
    {
        //Debug.Log(numberOfChildren);
        //Debug.Log(waypointIndex);
        if (waypointIndex >= points.Length - 1)
        {
            waypointIndex = 0;
        }
        else
        {
            waypointIndex++;
        }

        return points[waypointIndex].transform.position;
    }

    public void TakeDmg(float dmg)
    {
        health -= dmg;

        if (health <= 0f)
        {
            if(type == "Soldier")
            {
                GameManager.Instance.Score += scoreValue;
                int powerUp = Random.Range(0, 100);
                if (powerUp < 10)
                {
                    int index = Random.Range(0, 5);
                    Instantiate(powerUps[index], transform.position, powerUps[index].transform.rotation);
                }
                Instantiate(DestroyParticle, transform.position, DestroyParticle.transform.rotation);
                Destroy(gameObject);
            }
            else if(type == "Boss")
            {
                GameManager.Instance.Score += scoreValue;
                int index = Random.Range(0, 4);
                Instantiate(powerUps[index], transform.position, powerUps[index].transform.rotation);
                Instantiate(DestroyParticle, transform.position, DestroyParticle.transform.rotation);
                Destroy(gameObject);

            }
            
        }

        if(type == "Soldier")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        }
        else if (type == "Boss")
        {
            HealthImage.fillAmount = health / maxHealth;
        }

    }
    public void CollisionWithPlayer()
    {
        if(type == "Soldier")
        {
            Instantiate(DestroyParticle, transform.position, DestroyParticle.transform.rotation);
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
