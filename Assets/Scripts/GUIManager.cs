using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    #region Variables
    [Header("Bools")]
    public bool pauseOptions;
    public bool showOptions;
    public bool isMute;
    public bool isFullScreen;
    public bool gameScene;
    public bool pause;

    [Header("Keys")]
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode sprint;
    public KeyCode interact;
    // this remembers the key code of a key we are trying to change
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

    [Header("GUI Elements")]
    public GameObject menu;
    public GameObject options;
    public GameObject pauseMenu;
    public Toggle fullScreenToggle;
    public Toggle muteToggle;

    [Header("Resolutions")]
    public Dropdown resolutionDropDown;
    public int[] resX, resY;
    public int index;

    [Header("References")]
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public AudioSource music;
    public Light dirLight;

    private int muteValue;
    #endregion
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>(); // tagging the music for easy access in both scene
        // checking if it game scene, to lock cursor and 'play' the game
        if (gameScene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        if (volumeSlider != null && music != null)
        {
            if (PlayerPrefs.HasKey("Volume"))
            {
                Load();
            }
            volumeSlider.value = music.volume;
        }
        if (brightnessSlider != null && dirLight != null)
        {
            if (PlayerPrefs.HasKey("Brightness"))
            {
                Load();
            }
            brightnessSlider.value = dirLight.intensity;
        }
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            muteToggle.isOn = true;
        }
        else
        {
            muteToggle.isOn = false;
        }
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
        #region SetUp Keys
        //Set out keys to the preset keys we may have saved, else set keys to default.
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Space", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

        forwardText.text = forward.ToString();
        backwardText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        crouchText.text = crouch.ToString();
        sprintText.text = sprint.ToString();
        interactText.text = interact.ToString();
        #endregion
    }
    void Update()
    {
        if (!gameScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        if (volumeSlider != null && music != null)
        {
            if (music.volume != volumeSlider.value)
            {
                music.volume = volumeSlider.value;
            }
        }
        if (brightnessSlider != null && dirLight != null)
        {
            if (brightnessSlider.value != dirLight.intensity)
            {
                dirLight.intensity = brightnessSlider.value;
            }
        }
    }
    #region PauseMenu
    public void ShowPauseMenu()
    {
        TogglePause();
    }
    public void ReturnGame()
    {
        TogglePause();
    }
    public bool TogglePause()
    {
        if (pause)
        {
            if (!showOptions)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                pause = false;
            }
            else
            {
                showOptions = false;
                options.SetActive(false);
                pauseMenu.SetActive(true);
            }
            return false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pause = true;
            pauseMenu.SetActive(true);
            return true;
        }
    }
    #endregion
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void ShowOptions()
    {
        ToggleOptions();
    }
    public bool ToggleOptions()
    {
        if (showOptions)
        {
            showOptions = false;
            menu.SetActive(true);
            options.SetActive(false);
            return false;
        }
        else
        {
            showOptions = true;
            menu.SetActive(false);
            options.SetActive(true);
            return true;
        }
    }
    public void PauseOptions()
    {
        TogglePauseOptions();
    }
    public bool TogglePauseOptions()
    {
        if (pauseOptions)
        {
            pauseOptions = false;
            pauseMenu.SetActive(true);
            options.SetActive(false);
            return false;
        }
        else
        {
            pauseOptions = true;
            pauseMenu.SetActive(false);
            options.SetActive(true);
            return true;
        }
    }
    // to save options
    public void Save()
    {
        PlayerPrefs.SetInt("Resolutions", resolutionDropDown.value);

        PlayerPrefs.SetFloat("Volume", music.volume);
        PlayerPrefs.SetFloat("Brightness", dirLight.intensity);

        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());

        if (muteToggle.isOn == true)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
        if (fullScreenToggle.isOn == true)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }

    }
    // to load saved options
    public void Load()
    {
        resolutionDropDown.value = PlayerPrefs.GetInt("Resolutions");

        music.volume = PlayerPrefs.GetFloat("Volume");
        dirLight.intensity = PlayerPrefs.GetFloat("Brightness");

        PlayerPrefs.GetString("Forward");
        PlayerPrefs.GetString("Backward");
        PlayerPrefs.GetString("Left");
        PlayerPrefs.GetString("Right");
        PlayerPrefs.GetString("Jump");
        PlayerPrefs.GetString("Crouch");
        PlayerPrefs.GetString("Sprint");
        PlayerPrefs.GetString("Interact");
    }
    // to put reset/ default choices
    public void Default()
    {
        resolutionDropDown.captionText.text = "1920*1080";
        Screen.SetResolution(resX[5], resY[5], true);
        fullScreenToggle.isOn = true;

        volumeSlider.value = 1;
        brightnessSlider.value = 1;
        if (muteToggle.isOn == true)
        {
            muteToggle.isOn = false;
        }

        #region Reset Keys
        //Set out keys to the preset keys we may have saved, else set keys to default.
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Space", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

        forwardText.text = forward.ToString();
        backwardText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        crouchText.text = crouch.ToString();
        sprintText.text = sprint.ToString();
        interactText.text = interact.ToString();
        #endregion
    }

    // to mute sounds
    public void MuteSounds()
    {
        isMute = !isMute;
        if (isMute)
        {
            music.mute = true;
            volumeSlider.value = 0;
            muteValue = 1; // mute true
            
        }
        else
        {
            music.mute = false;
            volumeSlider.value = 1;
            muteValue = 0; // mute false
        }

    }
    #region Key Press Event
    void OnGUI()
    {
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
    #endregion
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
    #region FullScreen Toggle and Resolutions
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void ResolutionDropDown()
    {
        index = resolutionDropDown.value;
        Screen.SetResolution(resX[index], resY[index], isFullScreen);
    }
}

#endregion

/* TODO
   Save and Load all option menu Data
   - volume
   - mute
   - brightness
   - keybinding
   - fullscreen
   - resolutions
   Set up Option Menu for both main menu scene and the game scene

   Pause Menu
   - Toggle on and off correctly
   - Cursor visibility and lock toggling

    All button on both scenes linking together
    
    Code comments

    carry over music
*/
