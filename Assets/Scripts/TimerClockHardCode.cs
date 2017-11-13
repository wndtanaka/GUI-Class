using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerClockHardCode : MonoBehaviour
{
    public float timer; // set this to the time you want in seconds + 1 second for PC Load Start

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // count down this may take us below 0
        }
        else
        {
            timer = 0; // this sets us back to 0
        }
    }
    void LateUpdate()
    {
        if (timer < 0)
        {
            timer = 0; // this sets us back to 0
        }
    }
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer - mins * 60);

        string clockTime = string.Format("{0:0}:{1:00}", mins, secs);
        GUI.Box(new Rect(scrW, scrH,scrW,scrH),clockTime); // displaying our clock
    }
}
