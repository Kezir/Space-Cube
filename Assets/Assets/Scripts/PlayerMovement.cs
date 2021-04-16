using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject[] ShotProjectile;
    public GameObject[] Weapons;
    public Image HealthImage;
    public int lives;
    float dirX;
    float dirY;
    float moveSpeed = 20f;
    public bool gameOver = false;
    public float timeDelay;
    private float timer = 0.0f;
    private Vector3 middle = new Vector3(0,0.7f);
    private Vector3 right = new Vector3(0.35f,1f);
    private Vector3 left = new Vector3(-0.35f,1f);
    private Vector3 left1 = new Vector3(-0.2f, 1.2f);
    private Vector3 left2 = new Vector3(-0.5f, 0.7f);
    private Vector3 right1 = new Vector3(0.2f, 1.2f);
    private Vector3 right2 = new Vector3(0.5f, 0.7f);
    public Text LivesLeft;
    public Text PowerText;
    public bool alive, targetAble;
    public float powerDmg;
    private float speedBullet;
    private float countLvl;
    private float maxHealth;
    public float health;
    public float baseDmg;
    public int Power_Blue;
    public int Power_Orange;
    public int Power_Purple;
    public int Power_Green;
    public int currentWeapon;
    public float lifeSteal = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        HealthImage = GameObject.FindGameObjectWithTag("Health").GetComponent<Image>();
        LivesLeft = GameObject.FindGameObjectWithTag("Lives_Text").GetComponent<Text>();
        PowerText = GameObject.FindGameObjectWithTag("Power_Text").GetComponent<Text>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedBullet = 8;
        currentWeapon = 0;
        Power_Blue = 0;
        Power_Orange = 1;
        Power_Purple = 0;
        Power_Green = 0;
        baseDmg = 0;
        lives = 2;
        LivesLeft.text = lives.ToString();
        PowerText.text = ReturnPowerOfWeapon(currentWeapon).ToString();
        PowerImageChange.Instance.ImageIndex = currentWeapon;
        maxHealth = 100.0f;
        alive = true;
        targetAble = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(alive == true)
        {
            if (Input.touchCount > 0 && timer > timeDelay)
            {
                timer = 0.0f;
                Shoot();

            }
            
            timer += Time.deltaTime;
            dirX = Input.acceleration.x * moveSpeed;
            dirY = Input.acceleration.y * moveSpeed + 12.0f;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), Mathf.Clamp(transform.position.y, -3.8f, 4.0f));
        }
        else
        {
            if (transform.position.y <= -3.5f)
            {
                transform.position += new Vector3(0,1,0) * Time.deltaTime;
            }
            else
            {
                alive = true;
            }
        }

        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY);

    }

    private int ReturnPowerOfWeapon(int index)
    {
        if (index == 0)
        {
            return Power_Orange;
        }
        else if (index == 1)
        {
            return Power_Purple;
        }
        else if (index == 2)
        {
            return Power_Blue;
        }
        else
        {
            return Power_Green;
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        HealthImage.fillAmount = health / maxHealth;
    }
    private void Shoot()
    {
        if(countLvl == 0)
        {
            GameObject bullet = Instantiate(Weapons[currentWeapon], transform.position + middle, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + middle, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet = bullet.GetComponent<Bullet>();
            _bullet.power = powerDmg + baseDmg;
            _bullet.speed = speedBullet;
            _bullet.Player = gameObject.GetComponent<PlayerMovement>();
            
        }
        else if(countLvl == 1)
        {           
            GameObject bullet2 = Instantiate(Weapons[currentWeapon], transform.position + left, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet2 = bullet2.GetComponent<Bullet>();
            _bullet2.power = powerDmg + baseDmg;
            _bullet2.speed = speedBullet;
            _bullet2.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet3 = Instantiate(Weapons[currentWeapon], transform.position + right, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet3 = bullet3.GetComponent<Bullet>();
            _bullet3.power = powerDmg + baseDmg;
            _bullet3.speed = speedBullet;
            _bullet3.Player = gameObject.GetComponent<PlayerMovement>();
        }
        else if(countLvl == 2)
        {
            GameObject bullet1 = Instantiate(Weapons[currentWeapon], transform.position + middle, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + middle, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet1 = bullet1.GetComponent<Bullet>();
            _bullet1.power = powerDmg + baseDmg;
            _bullet1.speed = speedBullet;
            _bullet1.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet2 = Instantiate(Weapons[currentWeapon], transform.position + left, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet2 = bullet2.GetComponent<Bullet>();
            _bullet2.power = powerDmg;
            _bullet2.speed = speedBullet;
            _bullet2.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet3 = Instantiate(Weapons[currentWeapon], transform.position + right, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet3 = bullet3.GetComponent<Bullet>();
            _bullet3.power = powerDmg + baseDmg;
            _bullet3.speed = speedBullet;
            _bullet3.Player = gameObject.GetComponent<PlayerMovement>();
        }
        else if (countLvl == 3)
        {
            GameObject bullet1 = Instantiate(Weapons[currentWeapon], transform.position + left1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet1 = bullet1.GetComponent<Bullet>();
            _bullet1.power = powerDmg + baseDmg;
            _bullet1.speed = speedBullet;
            _bullet1.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet2 = Instantiate(Weapons[currentWeapon], transform.position + left2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet2 = bullet2.GetComponent<Bullet>();
            _bullet2.power = powerDmg + baseDmg;
            _bullet2.speed = speedBullet;
            _bullet2.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet3 = Instantiate(Weapons[currentWeapon], transform.position + right1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet3 = bullet3.GetComponent<Bullet>();
            _bullet3.power = powerDmg + baseDmg;
            _bullet3.speed = speedBullet;
            _bullet3.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet4 = Instantiate(Weapons[currentWeapon], transform.position + right2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet4 = bullet4.GetComponent<Bullet>();
            _bullet4.power = powerDmg + baseDmg;
            _bullet4.speed = speedBullet;
            _bullet4.Player = gameObject.GetComponent<PlayerMovement>();
        }
        else if (countLvl == 4)
        {
            GameObject bullet1 = Instantiate(Weapons[currentWeapon], transform.position + left1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left1, ShotProjectile[currentWeapon].transform.rotation) ;
            Bullet _bullet1 = bullet1.GetComponent<Bullet>();
            _bullet1.power = powerDmg + baseDmg;
            _bullet1.speed = speedBullet;
            _bullet1.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet2 = Instantiate(Weapons[currentWeapon], transform.position + left2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet2 = bullet2.GetComponent<Bullet>();
            _bullet2.power = powerDmg + baseDmg;
            _bullet2.speed = speedBullet;
            _bullet2.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet3 = Instantiate(Weapons[currentWeapon], transform.position + right1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet3 = bullet3.GetComponent<Bullet>();
            _bullet3.power = powerDmg + baseDmg;
            _bullet3.speed = speedBullet;
            _bullet3.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet4 = Instantiate(Weapons[currentWeapon], transform.position + right2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet4 = bullet4.GetComponent<Bullet>();
            _bullet4.power = powerDmg + baseDmg;
            _bullet4.speed = speedBullet;
            _bullet4.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet5 = Instantiate(Weapons[currentWeapon], transform.position + middle, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + middle, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet5 = bullet5.GetComponent<Bullet>();
            _bullet5.power = powerDmg + baseDmg;
            _bullet5.speed = speedBullet;
            _bullet5.Player = gameObject.GetComponent<PlayerMovement>();
        }
        else if (countLvl == 5)
        {
            GameObject bullet1 = Instantiate(Weapons[currentWeapon], transform.position + left1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet1 = bullet1.GetComponent<Bullet>();
            _bullet1.power = powerDmg + baseDmg;
            _bullet1.speed = speedBullet;
            _bullet1.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet2 = Instantiate(Weapons[currentWeapon], transform.position + left2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet2 = bullet2.GetComponent<Bullet>();
            _bullet2.power = powerDmg + baseDmg;
            _bullet2.speed = speedBullet;
            _bullet2.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet3 = Instantiate(Weapons[currentWeapon], transform.position + right1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet3 = bullet3.GetComponent<Bullet>();
            _bullet3.power = powerDmg + baseDmg;
            _bullet3.speed = speedBullet;
            _bullet3.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet4 = Instantiate(Weapons[currentWeapon], transform.position + right2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet4 = bullet4.GetComponent<Bullet>();
            _bullet4.power = powerDmg + baseDmg;
            _bullet4.speed = speedBullet;
            _bullet4.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet5 = Instantiate(Weapons[currentWeapon], transform.position + left, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet5 = bullet5.GetComponent<Bullet>();
            _bullet5.power = powerDmg + baseDmg;
            _bullet5.speed = speedBullet;
            _bullet5.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet6 = Instantiate(Weapons[currentWeapon], transform.position + right, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet6 = bullet6.GetComponent<Bullet>();
            _bullet6.power = powerDmg + baseDmg;
            _bullet6.speed = speedBullet;
            _bullet6.Player = gameObject.GetComponent<PlayerMovement>();
        }
        else if (countLvl == 6)
        {
            GameObject bullet1 = Instantiate(Weapons[currentWeapon], transform.position + left1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet1 = bullet1.GetComponent<Bullet>();
            _bullet1.power = powerDmg + baseDmg;
            _bullet1.speed = speedBullet;
            _bullet1.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet2 = Instantiate(Weapons[currentWeapon], transform.position + left2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet2 = bullet2.GetComponent<Bullet>();
            _bullet2.power = powerDmg + baseDmg;
            _bullet2.speed = speedBullet;
            _bullet2.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet3 = Instantiate(Weapons[currentWeapon], transform.position + right1, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right1, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet3 = bullet3.GetComponent<Bullet>();
            _bullet3.power = powerDmg + baseDmg;
            _bullet3.speed = speedBullet;
            _bullet3.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet4 = Instantiate(Weapons[currentWeapon], transform.position + right2, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right2, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet4 = bullet4.GetComponent<Bullet>();
            _bullet4.power = powerDmg + baseDmg;
            _bullet4.speed = speedBullet;
            _bullet4.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet5 = Instantiate(Weapons[currentWeapon], transform.position + left, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + left, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet5 = bullet5.GetComponent<Bullet>();
            _bullet5.power = powerDmg + baseDmg;
            _bullet5.speed = speedBullet;
            _bullet5.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet6 = Instantiate(Weapons[currentWeapon], transform.position + right, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + right, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet6 = bullet6.GetComponent<Bullet>();
            _bullet6.power = powerDmg + baseDmg;
            _bullet6.speed = speedBullet;
            _bullet6.Player = gameObject.GetComponent<PlayerMovement>();

            GameObject bullet7 = Instantiate(Weapons[currentWeapon], transform.position + middle, Weapons[currentWeapon].transform.rotation);
            Instantiate(ShotProjectile[currentWeapon], transform.position + middle, ShotProjectile[currentWeapon].transform.rotation);
            Bullet _bullet7 = bullet7.GetComponent<Bullet>();
            _bullet7.power = powerDmg + baseDmg;
            _bullet7.speed = speedBullet;
            _bullet7.Player = gameObject.GetComponent<PlayerMovement>();
        }

    }

    public void PowerUp_Orange()
    {
        if(currentWeapon != 0)
        {
            lifeSteal = 0;
            currentWeapon = 0;
            PowerImageChange.Instance.ImageIndex = 0;
        }

        if(Power_Orange < 20)
        {
            Power_Orange += 1;
        }
        else
        {
            baseDmg += 5;
        }
        PowerText.text = Power_Orange.ToString();


        switch (Power_Orange)
        {
            case 1:
                powerDmg = 10;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                break;
            case 2:
                powerDmg = 15;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                break;
            case 3:
                powerDmg = 20;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 4:
                powerDmg = 25;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 5:
                powerDmg = 30;
                speedBullet = 10;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 6:
                powerDmg = 35;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 2;
                break;
            case 7:
                powerDmg = 40;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 2;
                break;
            case 8:
                powerDmg = 45;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 2;
                break;
            case 9:
                powerDmg = 50;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 3;
                break;
            case 10:
                powerDmg = 55;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 3;
                break;
            case 11:
                powerDmg = 60;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 3;
                break;
            case 12:
                powerDmg = 65;
                speedBullet = 12;
                timeDelay = 0.5f;
                countLvl = 4;
                break;
            case 13:
                powerDmg = 70;
                speedBullet = 14;
                timeDelay = 0.5f;
                countLvl = 4;
                break;
            case 14:
                powerDmg = 75;
                speedBullet = 14;
                timeDelay = 0.5f;
                countLvl = 4;
                break;
            case 15:
                powerDmg = 80;
                speedBullet = 14;
                timeDelay = 0.25f;
                countLvl = 5;
                break;
            case 16:
                powerDmg = 85;
                speedBullet = 14;
                timeDelay = 0.25f;
                countLvl = 5;
                break;
            case 17:
                powerDmg = 90;
                speedBullet = 15;
                timeDelay = 0.25f;
                countLvl = 5;
                break;
            case 18:
                powerDmg = 95;
                speedBullet = 15;
                timeDelay = 0.25f;
                countLvl = 6;
                break;
            case 19:
                powerDmg = 100;
                speedBullet = 15;
                timeDelay = 0.2f;
                countLvl = 6;
                break;
            case 20:
                powerDmg = 110;
                speedBullet = 15;
                timeDelay = 0.2f;
                countLvl = 6;
                break;
            default:
                break;
        }
    }

    public void PowerUp_Blue()
    {
        if (currentWeapon != 2)
        {
            lifeSteal = 0;
            currentWeapon = 2;
            PowerImageChange.Instance.ImageIndex = 2;
        }
        if (Power_Blue < 20)
        {
            Power_Blue += 1;
        }
        else
        {
            baseDmg += 5;
        }

        PowerText.text = Power_Blue.ToString();

        switch (Power_Blue)
        {
            case 1:
                powerDmg = 10;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                break;
            case 2:
                powerDmg = 10;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 1;
                break;
            case 3:
                powerDmg = 15;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 4:
                powerDmg = 15;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 5:
                powerDmg = 20;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 2;
                break;
            case 6:
                powerDmg = 20;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 2;
                break;
            case 7:
                powerDmg = 25;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 2;
                break;
            case 8:
                powerDmg = 25;
                speedBullet = 10;
                timeDelay = 0.75f;
                countLvl = 3;
                break;
            case 9:
                powerDmg = 30;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 3;
                break;
            case 10:
                powerDmg = 30;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 3;
                break;
            case 11:
                powerDmg = 35;
                speedBullet = 12;
                timeDelay = 0.5f;
                countLvl = 4;
                break;
            case 12:
                powerDmg = 35;
                speedBullet = 12;
                timeDelay = 0.5f;
                countLvl = 4;
                break;
            case 13:
                powerDmg = 40;
                speedBullet = 14;
                timeDelay = 0.5f;
                countLvl = 4;
                break;
            case 14:
                powerDmg = 40;
                speedBullet = 14;
                timeDelay = 0.25f;
                countLvl = 5;
                break;
            case 15:
                powerDmg = 45;
                speedBullet = 14;
                timeDelay = 0.25f;
                countLvl = 5;
                break;
            case 16:
                powerDmg = 45;
                speedBullet = 14;
                timeDelay = 0.25f;
                countLvl = 5;
                break;
            case 17:
                powerDmg = 50;
                speedBullet = 15;
                timeDelay = 0.2f;
                countLvl = 6;
                break;
            case 18:
                powerDmg = 50;
                speedBullet = 15;
                timeDelay = 0.2f;
                countLvl = 6;
                break;
            case 19:
                powerDmg = 55;
                speedBullet = 15;
                timeDelay = 0.1f;
                countLvl = 6;
                break;
            case 20:
                powerDmg = 55;
                speedBullet = 15;
                timeDelay = 0.1f;
                countLvl = 6;
                break;
            default:
                break;
        }
    }
    public void PowerUp_Purple()
    {
        if (currentWeapon != 1)
        {
            currentWeapon = 1;
            lifeSteal = 0;
            PowerImageChange.Instance.ImageIndex = 1;
        }
        if (Power_Purple < 20)
        {
            Power_Purple += 1;
        }
        else
        {
            baseDmg += 5;
        }

        PowerText.text = Power_Purple.ToString();

        switch (Power_Purple)
        {
            case 1:
                powerDmg = 30;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                break;
            case 2:
                powerDmg = 40;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                break;
            case 3:
                powerDmg = 50;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 0;
                break;
            case 4:
                powerDmg = 60;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 5:
                powerDmg = 70;
                speedBullet = 10;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 6:
                powerDmg = 80;
                speedBullet = 10;
                timeDelay = 1.25f;
                countLvl = 1;
                break;
            case 7:
                powerDmg = 90;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 1;
                break;
            case 8:
                powerDmg = 100;
                speedBullet = 10;
                timeDelay = 1f;
                countLvl = 1;
                break;
            case 9:
                powerDmg = 110;
                speedBullet = 12;
                timeDelay = 1f;
                countLvl = 1;
                break;
            case 10:
                powerDmg = 120;
                speedBullet = 12;
                timeDelay = 1f;
                countLvl = 1;
                break;
            case 11:
                powerDmg = 130;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 1;
                break;
            case 12:
                powerDmg = 140;
                speedBullet = 12;
                timeDelay = 0.75f;
                countLvl = 2;
                break;
            case 13:
                powerDmg = 150;
                speedBullet = 14;
                timeDelay = 0.75f;
                countLvl = 2;
                break;
            case 14:
                powerDmg = 160;
                speedBullet = 14;
                timeDelay = 0.75f;
                countLvl = 2;
                break;
            case 15:
                powerDmg = 170;
                speedBullet = 14;
                timeDelay = 0.5f;
                countLvl = 2;
                break;
            case 16:
                powerDmg = 180;
                speedBullet = 14;
                timeDelay = 0.5f;
                countLvl = 2;
                break;
            case 17:
                powerDmg = 190;
                speedBullet = 15;
                timeDelay = 0.5f;
                countLvl = 3;
                break;
            case 18:
                powerDmg = 200;
                speedBullet = 15;
                timeDelay = 0.35f;
                countLvl = 3;
                break;
            case 19:
                powerDmg = 210;
                speedBullet = 15;
                timeDelay = 0.35f;
                countLvl = 3;
                break;
            case 20:
                powerDmg = 220;
                speedBullet = 15;
                timeDelay = 0.35f;
                countLvl = 3;
                break;
            default:
                break;
        }
    }
    public void PowerUp_Green()
    {
        if (currentWeapon != 3)
        {
            currentWeapon = 3;
            PowerImageChange.Instance.ImageIndex = 3;
        }
        if (Power_Green < 20)
        {
            Power_Green += 1;
        }
        else
        {
            baseDmg += 5;
        }

        PowerText.text = Power_Green.ToString();

        switch (Power_Green)
        {
            case 1:
                powerDmg = 10;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                lifeSteal = 0;
                break;
            case 2:
                powerDmg = 15;
                speedBullet = 8;
                timeDelay = 1.5f;
                countLvl = 0;
                lifeSteal = 0;
                break;
            case 3:
                powerDmg = 15;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                lifeSteal = 0;
                break;
            case 4:
                powerDmg = 20;
                speedBullet = 8;
                timeDelay = 1.25f;
                countLvl = 1;
                lifeSteal = 0;
                break;
            case 5:
                powerDmg = 25;
                speedBullet = 10;
                timeDelay = 1.25f;
                countLvl = 1;
                lifeSteal = 0;
                break;
            case 6:
                powerDmg = 25;
                speedBullet = 10;
                timeDelay = 1.25f;
                countLvl = 2;
                lifeSteal = 0;
                break;
            case 7:
                powerDmg = 30;
                speedBullet = 10;
                timeDelay = 1.1f;
                countLvl = 2;
                lifeSteal = 0;
                break;
            case 8:
                powerDmg = 35;
                speedBullet = 10;
                timeDelay = 1.1f;
                countLvl = 2;
                lifeSteal = 0;
                break;
            case 9:
                powerDmg = 35;
                speedBullet = 12;
                timeDelay = 1f;
                countLvl = 3;
                lifeSteal = 0;
                break;
            case 10:
                powerDmg = 40;
                speedBullet = 12;
                timeDelay = 1f;
                countLvl = 3;
                lifeSteal = 0.01f;
                break;
            case 11:
                powerDmg = 45;
                speedBullet = 12;
                timeDelay = 1f;
                countLvl = 3;
                lifeSteal = 0.01f;
                break;
            case 12:
                powerDmg = 45;
                speedBullet = 12;
                timeDelay = 0.85f;
                countLvl = 4;
                lifeSteal = 0.01f;
                break;
            case 13:
                powerDmg = 50;
                speedBullet = 14;
                timeDelay = 0.85f;
                countLvl = 4;
                lifeSteal = 0.01f;
                break;
            case 14:
                powerDmg = 55;
                speedBullet = 14;
                timeDelay = 0.85f;
                countLvl = 4;
                lifeSteal = 0.01f;
                break;
            case 15:
                powerDmg = 55;
                speedBullet = 14;
                timeDelay = 0.75f;
                countLvl = 5;
                lifeSteal = 0.02f;
                break;
            case 16:
                powerDmg = 60;
                speedBullet = 14;
                timeDelay = 0.75f;
                countLvl = 5;
                lifeSteal = 0.02f;
                break;
            case 17:
                powerDmg = 65;
                speedBullet = 15;
                timeDelay = 0.75f;
                countLvl = 5;
                lifeSteal = 0.02f;
                break;
            case 18:
                powerDmg = 65;
                speedBullet = 15;
                timeDelay = 0.5f;
                countLvl = 6;
                lifeSteal = 0.02f;
                break;
            case 19:
                powerDmg = 70;
                speedBullet = 15;
                timeDelay = 0.5f;
                countLvl = 6;
                lifeSteal = 0.02f;
                break;
            case 20:
                powerDmg = 80;
                speedBullet = 15;
                timeDelay = 0.5f;
                countLvl = 6;
                lifeSteal = 0.03f;
                break;
            default:
                break;
        }
    }
    public void PowerUp_Power(int power)
    {
        baseDmg += 5;
    }

    void Death()
    {
        lives -= 1;
        health = maxHealth;
        LivesLeft.text = lives.ToString();
        Invoke("ChangeTargetAble", 5);
        alive = false;
        targetAble = false;
        transform.position = new Vector3(0, -6, 0);
        dirX = 0;
        dirY = 0;

    }

    private void ChangeTargetAble()
    {
        targetAble = true;
    }
    public void TakeDmg(float dmg)
    {
        if(targetAble == true)
        {
            health -= dmg;

            if (health <= 0f)
            {
                if (lives > 0)
                {
                    Death();
                }
                else
                {
                    Destroy(gameObject);
                }

            }
            HealthImage.fillAmount = health / maxHealth;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy Dmg = collision.GetComponent<Enemy>();
            if(Dmg.type == "Soldier" && alive == true)
            {
                Dmg.CollisionWithPlayer();
                TakeDmg((float)(maxHealth * 0.3));
            }
            else if(Dmg.type == "Boss" && alive == true)
            {
                TakeDmg(maxHealth);
            }
            
        }
        

    }
}
