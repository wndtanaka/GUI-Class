using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceGUI : MonoBehaviour
{
    public int curHealth, maxHealth;
    public Vector3 pos;

    Camera mainCam;
    float distance;
    GameObject steve;

    private void Start()
    {
        mainCam = Camera.main;
        steve = GameObject.Find("Steve");
    }
    private void Update()
    {
        //Debug.Log(distance);
    }
    private void LateUpdate()
    {
        pos = Camera.main.WorldToScreenPoint(transform.position);
        distance = 100/Vector3.Distance(mainCam.transform.position, steve.transform.position);

        if (curHealth < 0)
        {
            curHealth = 0;
        }
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

    }
    private void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        float healthWidth = 1 / Mathf.Clamp(distance,0.01f, 1);
        float healthHeight = 1 / Mathf.Clamp(distance, 0.01f, 0.25f);

        Rect healthBar = new Rect(pos.x + scrW * -0.5f, -pos.y + scrH * 7.5f, curHealth * scrW / maxHealth / healthWidth, scrH / healthHeight);

        if (curHealth > 0 && distance < 50f)
        {
            GUI.Box(healthBar, "");
        }
    }
}
