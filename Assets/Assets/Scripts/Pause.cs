using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseClicked();
        }
    }
    public void PauseClicked()
    {

        if (Time.timeScale == 0f)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
       
    }
}
