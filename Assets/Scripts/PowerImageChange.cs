using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerImageChange : Singletone<PowerImageChange>
{
    public Sprite[] WeaponsImage;
    private int imageIndex;
    public int ImageIndex
    {
        get => imageIndex;
        set
        {
            this.imageIndex = value;
            gameObject.GetComponent<Image>().sprite = WeaponsImage[imageIndex];
        }
    }
    
}
