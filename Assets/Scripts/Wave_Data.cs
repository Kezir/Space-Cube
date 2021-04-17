using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Data 
{
    public GameObject[] enemy;
    public string type;
    public string way;

    public Wave_Data( GameObject[] enemy, string type, string way)
    {
        this.enemy = enemy;
        this.type = type;
        this.way = way;
    }
}
