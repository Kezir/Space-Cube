using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Power : MonoBehaviour
{
    private int currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            currentWeapon = player.currentWeapon;
            
            if(currentWeapon == 0)
            {
                player.PowerUp_Orange();
            }
            else if(currentWeapon == 1)
            {
                player.PowerUp_Purple();
            }
            else if(currentWeapon == 2)
            {
                player.PowerUp_Blue();
            }
            else if(currentWeapon == 3)
            {
                player.PowerUp_Green();
            }
            Destroy(gameObject);
        }


    }
}
