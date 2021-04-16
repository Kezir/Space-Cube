using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singletone<GameManager>
{
    public Text ScoreText;
    private int score;
    public int Score
    {
        get => score;
        set
        {
            this.score = value;
            this.ScoreText.text = value.ToString();
        }
    }

}
