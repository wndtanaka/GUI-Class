using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[AddComponentMenu("Character Set Up/Character Handler")]

public class CharacterHandler : MonoBehaviour
{
    [Header("Character")]
    #region Character 
    public bool alive;
    public CharacterController controller;
    #endregion
    [Header("Health, Mana, Stamina")]
    #region Health, Mana & Stamina
    public float maxHealth, curHealth;
    public float maxMana, curMana;
    public float maxStamina, curStamina;
    #endregion
    [Header("Race")]
    public string playerClass;
    [Header("Levels and Exp")]
    #region Level and Exp
    public int curLevel;
    public float maxExp, curExp;
    #endregion
    [Header("Camera Connection")]
    #region MiniMap
    public RenderTexture miniMap;
    #endregion
    public bool gameScene;
    #region Start
    void Start()
    {
        //set max health to 100
        maxHealth = 100f;
        //set current health to max
        curHealth = maxHealth;
        // set max mana to 100
        maxMana = 100f;
        curMana = maxMana;
        //set stamina same as other
        maxStamina = 100f;
        curStamina = maxStamina;
        // set class temporary
        playerClass = "Human";
        //make sure player is alive
        alive = true;
        //max exp starts at 60
        maxExp = 60;
        //connect the Character Controller to the controller variable
        controller = this.GetComponent<CharacterController>();
    }
    #endregion
    #region Update
    void Update()
    {
        //if our current experience is greater or equal to the maximum experience
        if (curExp >= maxExp)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            curExp -= maxExp;
            //our level goes up by one
            curLevel++;
            //the maximum amount of experience is increased by 50
            maxExp = 50;
        }

    }
    #endregion
    #region LateUpdate
    void LateUpdate()
    {
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            //then our current health is equal to the max health
            curHealth = maxHealth;
        }
        //if our current health is less than 0 or we are not alive
        if (curHealth <= 0)
        {
            //current health equals 0
            curHealth = 0;
        }
        //if the player is alive
        //and our health is less than or equal to 0
        if (alive == true && curHealth <= 0)
        {
            //alive is false
            alive = false;
            //controller is turned off
            controller.enabled = false;
        }
    }
    #endregion
    #region OnGUI
    void OnGUI()
    {
        if (!gameScene)
        { 
            //set up our aspect ratio for the GUI elements
            //scrW - 16
            //scrH - 9
            int scrW = Screen.width / 16;
            int scrH = Screen.height / 9;

            //GUI Box on screen for the healthbar background
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "");
            //GUI Box for current health that moves in same place as the background bar
            //current Health divided by the posistion on screen and timesed by the total max health
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "");
            //GUI Box on screen for the experience background
            GUI.Box(new Rect(6 * scrW, 0.75f * scrH, 4 * scrW, 0.5f * scrH), "");
            // Mana
            GUI.Box(new Rect(6 * scrW, 0.75f * scrH, curMana * (4 * scrW) / maxMana, 0.5f * scrH), "");
            // Stamina
            GUI.Box(new Rect(6 * scrW, 1.25f * scrH, 4 * scrW, 0.5f * scrH), "");
            GUI.Box(new Rect(6 * scrW, 1.25f * scrH, curStamina * (4 * scrW) / maxStamina, 0.5f * scrH), "");
            // Exp
            GUI.Box(new Rect(6 * scrW, 1.75f * scrH, 4 * scrW, 0.25f * scrH), "");
            GUI.Box(new Rect(6 * scrW, 1.75f * scrH, curExp * (4 * scrW) / maxExp, 0.25f * scrH), "");

            //GUI Draw Texture on the screen that has the mini map render texture attached
            GUI.DrawTexture(new Rect(13 * scrW, 0.25f * scrH, 2.925f * scrW, 2.5f * scrH), miniMap);
        }
    }
    #endregion
}









