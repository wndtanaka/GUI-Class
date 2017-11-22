using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    #region Variables
    [Header("Bools")]
    public bool showOptions;
    public bool muteToggle;
    [Header("Keys")]
    public KeyCode[] keys;
    //public KeysNames[] keyEnum;
    public string[] keyNames;
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode sprint;
    public KeyCode interact;
    public KeyCode holdingKey;

    [Header("GUI Text")]
    public Text forwardText;
    public Text backwardText;
    public Text leftText;
    public Text rightText;
    public Text jumpText;
    public Text crouchText;
    public Text sprintText;
    public Text interactText;

    [Header("Resolutions and Screen Elements")]
    public int index;
    public bool showRes;
    public bool fullScreenToggle;
    public int[] resX, resY;
    public float scrW, scrH;
    public Vector2 scrollPosRes;
    [Header("Other References")]
    public AudioSource music;
    public float volumeSlider, holdingVolume;
    public Light dirLight;
    public float brightnessSlider;
    //FindGUI
    [Header("Art")]
    public GUISkin menuSkin;
    public GUIStyle boxStyle;
    #endregion


    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        fullScreenToggle = true;

        //brightness
        dirLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        //mainMusic
        music = GameObject.Find("MenuMusic").GetComponent<AudioSource>();
        //mainMusic
        volumeSlider = music.volume;
        //brightness
        brightnessSlider = dirLight.intensity;
    }
    void Update()
    {
        if (music != null) // mainMusic
        {
            if (muteToggle == false)
            {
                // mainMusic
                if (music.volume != volumeSlider)
                {
                    holdingVolume = volumeSlider;
                    // mainMusic
                    music.volume = volumeSlider;
                }
            }
            else
            {
                volumeSlider = 0;
                // mainMusic
                music.volume = 0;
            }
        }

        if (dirLight != null)//brightness
        {                           //brightness
            if (brightnessSlider != dirLight.intensity)
            {  // brightness
                dirLight.intensity = brightnessSlider;
            }
        }

    }
    void OnGUI()
    {

        if (!showOptions)//if we are on our Main Menu and not our Options
        {
            //FindGUI
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", boxStyle);//background
            //FindGUI
            GUI.skin = menuSkin;
            GUI.Label(new Rect(2 * scrW, 0.75f * scrH, 12 * scrW, 2 * scrH), "The Ancient of Defense");//title
            //Buttons
            if (GUI.Button(new Rect(6 * scrW, 4 * scrH, 4 * scrW, scrH), "Play"))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect(6 * scrW, 5 * scrH, 4 * scrW, scrH), "Options"))
            {
                showOptions = true;
            }
            //FindGUI
            // GUI.skin = null;
            if (GUI.Button(new Rect(6 * scrW, 6 * scrH, 4 * scrW, scrH), "Exit"))
            {
                Application.Quit();
            }
        }
        else if (showOptions)//if we are on our Options Menu!!!!!
        {
            //set our aspect shiz if screen size changes
            if (scrW != Screen.width / 16)
            {
                scrW = Screen.width / 16;
                scrH = Screen.height / 9;
            }
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");//background
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Options");//title

            if (GUI.Button(new Rect(14.875f * scrW, 8.375f * scrH, scrW, 0.5f * scrH), "Back"))
            {
                showOptions = false;
            }
            #region KeyBinding
            /*
            GUI.Box(new Rect(8.75f * scrW, 1f * scrH, 6.25f * scrW, 1f * scrH), "Forward");
            GUI.Box(new Rect(8.75f * scrW, 2f * scrH, 6.25f * scrW, 1f * scrH), "Backward");
            GUI.Box(new Rect(8.75f * scrW, 3f * scrH, 6.25f * scrW, 1f * scrH), "Left");
            GUI.Box(new Rect(8.75f * scrW, 4f * scrH, 6.25f * scrW, 1f * scrH), "Right");
            GUI.Box(new Rect(8.75f * scrW, 5f * scrH, 6.25f * scrW, 1f * scrH), "Jump");
            GUI.Box(new Rect(8.75f * scrW, 6f * scrH, 6.25f * scrW, 1f * scrH), "Sprint");
            GUI.Box(new Rect(8.75f * scrW, 7f * scrH, 6.25f * scrW, 1f * scrH), "Crouch");
            GUI.Box(new Rect(8.75f * scrW, 8f * scrH, 6.25f * scrW, 1f * scrH), "Interact");
            */
            #endregion
            #region Brightness and Audio
            int i = 0;
            GUI.Box(new Rect(0.25f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Volume");//Label
            volumeSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 0.25f * scrH), volumeSlider, 0, 1);

            if (GUI.Button(new Rect(3.75f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 0.5f * scrW, 0.5f * scrH), "Mute"))//Label
            {
                ToggleVolume();
            }

            i++;
            GUI.Box(new Rect(0.25f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Brightness");//Label
            brightnessSlider = GUI.HorizontalSlider(new Rect(2f * scrW, 3.125f * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 0.25f * scrH), brightnessSlider, 0, 1);
            #endregion
            #region Resolution and Screen
            i++;
            i++;
            if (GUI.Button(new Rect(0.25f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Resolutions"))
            {
                showRes = !showRes;
            }
            if (GUI.Button(new Rect(2f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 0.5f * scrH), "Fullscreen"))
            {
                FullScreenToggle();
            }
            i++;
            if (showRes)
            {
                GUI.Box(new Rect(0.25f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 3.5f * scrH), "");

                scrollPosRes = GUI.BeginScrollView(new Rect(0.25f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1.75f * scrW, 3.5f * scrH), scrollPosRes, new Rect(0, 0, 1.75f * scrW, 3.5f * scrH));

                for (int resSize = 0; resSize < resX.Length; resSize++)
                {
                    if (GUI.Button(new Rect(0f * scrW, 0 * scrH + resSize * (scrH * 0.5f), 1.75f * scrW, 0.5f * scrH), resX[resSize].ToString() + "x" + resY[resSize].ToString()))
                    {
                        Screen.SetResolution(resX[resSize], resY[resSize], fullScreenToggle);
                        showRes = false;
                    }
                }
                GUI.EndScrollView();
            }
            if (GUI.Button(new Rect(8f * scrW, 1 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "W"))
            {

            }
            GUI.Box(new Rect(9f * scrW, 1 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Forward");//Label
            if (GUI.Button(new Rect(8f * scrW, 2 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "S"))
            {

            }
            GUI.Box(new Rect(9f * scrW, 2 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Backward");//Label
            if (GUI.Button(new Rect(8f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "A"))
            {

            }
            GUI.Box(new Rect(9f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Left");//Label
            if (GUI.Button(new Rect(8f * scrW, 4 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "D"))
            {

            }
            GUI.Box(new Rect(9f * scrW, 4 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Right");//Label
            if (GUI.Button(new Rect(11.25f * scrW, 1 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "Space"))
            {

            }
            GUI.Box(new Rect(13f * scrW, 1 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Jump");//Label
            if (GUI.Button(new Rect(11.25f * scrW, 2 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "Left Control"))
            {

            }
            GUI.Box(new Rect(13f * scrW, 2 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Crouch");//Label
            if (GUI.Button(new Rect(11.25f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "Left Shift"))
            {

            }
            GUI.Box(new Rect(13f * scrW, 3 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Sprint");//Label
            if (GUI.Button(new Rect(11.25f * scrW, 4 * scrH + (i * (scrH * 0.5f)), 1f * scrW, 1f * scrH), "E"))
            {

            }
            GUI.Box(new Rect(13f * scrW, 4 * scrH + (i * (scrH * 0.5f)), 2f * scrW, 1f * scrH), "Interact");//Label
            #endregion
            Event e = Event.current;
            if (forward == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                    {
                        //set forward to the new key
                        forward = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        forwardText.text = forward.ToString();
                    }
                    else
                    {
                        //set forward back to what the holding key is
                        forward = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set back to last Key
                        forwardText.text = forward.ToString();

                    }
                }
            }
            if (backward == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                    {
                        //set back to the new key
                        backward = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        backwardText.text = backward.ToString();
                    }
                    else
                    {
                        //set forward back to what the holding key is
                        backward = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set back to last Key
                        backwardText.text = backward.ToString();

                    }
                }
            }
            if (left == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                    {
                        //set left to the new key
                        left = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        leftText.text = left.ToString();
                    }
                    else
                    {
                        //set left back to what the holding key is
                        left = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set back to last Key
                        leftText.text = left.ToString();

                    }
                }
            }
            if (right == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                    {
                        //set right to the new key
                        right = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        rightText.text = right.ToString();
                    }
                    else
                    {
                        //set right back to what the holding key is
                        right = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set back to last Key
                        rightText.text = right.ToString();

                    }
                }
            }
            if (jump == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                    {
                        //set jump to the new key
                        jump = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        jumpText.text = jump.ToString();
                    }
                    else
                    {
                        //set jump back to what the holding key is
                        jump = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set back to last Key
                        jumpText.text = jump.ToString();

                    }
                }
            }
            if (crouch == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == interact))
                    {
                        //set crouch to the new key
                        crouch = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        crouchText.text = crouch.ToString();
                    }
                    else
                    {
                        //set jump crouch to what the holding key is
                        crouch = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set crouch to last Key
                        crouchText.text = crouch.ToString();

                    }
                }
            }
            if (sprint == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == interact))
                    {
                        //set sprint to the new key
                        sprint = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        sprintText.text = sprint.ToString();
                    }
                    else
                    {
                        //set jump sprint to what the holding key is
                        sprint = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set sprint to last Key
                        sprintText.text = sprint.ToString();

                    }
                }
            }
            if (interact == KeyCode.None)
            {
                //if an event is trigged by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    //if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint))
                    {
                        //set interact to the new key
                        interact = e.keyCode;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set to new Key
                        interactText.text = interact.ToString();
                    }
                    else
                    {
                        //set jump interact to what the holding key is
                        interact = holdingKey;
                        //set holding key to none
                        holdingKey = KeyCode.None;
                        //set interact to last Key
                        interactText.text = interact.ToString();

                    }
                }
            }

        }

    }
    #region Controls
    public void Forward()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = forward;
            // set this button to non allowing only this to be editable
            forward = KeyCode.None;
            // set the text to none
            forwardText.text = forward.ToString();
        }
    }
    public void Backward()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = backward;
            // set this button to non allowing only this to be editable
            backward = KeyCode.None;
            // set the text to none
            backwardText.text = backward.ToString();
        }
    }
    public void Left()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = left;
            // set this button to non allowing only this to be editable
            left = KeyCode.None;
            // set the text to none
            leftText.text = left.ToString();
        }
    }
    public void Right()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = right;
            // set this button to non allowing only this to be editable
            right = KeyCode.None;
            // set the text to none
            rightText.text = right.ToString();
        }
    }
    public void Jump()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = jump;
            // set this button to non allowing only this to be editable
            jump = KeyCode.None;
            // set the text to none
            jumpText.text = jump.ToString();
        }
    }
    public void Crouch()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = crouch;
            // set this button to non allowing only this to be editable
            crouch = KeyCode.None;
            // set the text to none
            crouchText.text = crouch.ToString();
        }
    }
    public void Sprint()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = sprint;
            // set this button to non allowing only this to be editable
            sprint = KeyCode.None;
            // set the text to none
            sprintText.text = sprint.ToString();
        }
    }
    public void Interact()
    {
        // if none of the other keys are blank
        // then we can edit this key
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
        {
            // set our holding key to the key of this button
            holdingKey = interact;
            // set this button to non allowing only this to be editable
            interact = KeyCode.None;
            // set the text to none
            interactText.text = interact.ToString();
        }
    }
    #endregion
    bool ToggleVolume()
    {
        if (muteToggle == true)
        {
            muteToggle = false;
            volumeSlider = holdingVolume;
            return false;
        }
        else
        {
            muteToggle = true;
            holdingVolume = volumeSlider;
            volumeSlider = 0;
            music.volume = 0;
            return true;
        }
    }

    bool FullScreenToggle()
    {
        if (fullScreenToggle)
        {
            fullScreenToggle = false;
            Screen.fullScreen = false;
            return false;
        }
        else
        {
            fullScreenToggle = true;
            Screen.fullScreen = true;
            return true;
        }
    }
}