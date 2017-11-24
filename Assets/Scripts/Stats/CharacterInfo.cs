using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{

    [Header("Bools and Values")]
    public bool showInfo;

    [Header("References and Locations")]
    public Vector2 scrollPos = Vector2.zero;
    public MouseLook mainCam, playerCam;
    public Movement playerMove;

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main.GetComponent<MouseLook>();
        playerCam = GetComponent<MouseLook>();
        playerMove = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCharacterInfo();
        }
    }
    bool ToggleCharacterInfo()
    {
        if (showInfo)
        {
            showInfo = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainCam.enabled = true;
            playerCam.enabled = true;
            playerMove.enabled = true;
            return false;
        }
        else
        {
            showInfo = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainCam.enabled = false;
            playerCam.enabled = false;
            playerMove.enabled = false;
            return true;
        }
    }
    void OnGUI()
    {
        if (showInfo)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height),"Character Info");
        }
    }
}
