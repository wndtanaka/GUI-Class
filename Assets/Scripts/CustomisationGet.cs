using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationGet : MonoBehaviour
{
    public Renderer character;
    [Header("Stats")]
    private int baseStrength;
    private int baseConstitution;
    private int baseDexterity;
    private int baseIntelligence;
    private int baseWisdom;
    private int baseCharisma;
    public string race;
    public string classJob;
    public int strength;
    public int constitution;
    public int dexterity;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public int bonusPoint;

    [Header("Bools and Values")]
    public bool showInfo;

    [Header("References and Locations")]
    public Vector2 scrollPos = Vector2.zero;
    public MouseLook mainCam, playerCam;
    public Movement playerMove;
    CustomisationGet getinfo;

    void Start()
    {
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        LoadTexture();

        mainCam = Camera.main.GetComponent<MouseLook>();
        playerCam = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        baseStrength = strength;
        baseConstitution = constitution;
        baseDexterity = dexterity;
        baseIntelligence = intelligence;
        baseWisdom = wisdom;
        baseCharisma = charisma;
    }
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

    public void LoadTexture()
    {
        if (!PlayerPrefs.HasKey("CharacterName"))
        {
            SceneManager.LoadScene("CustomSet");
        }
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Cloth", PlayerPrefs.GetInt("ClothIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        gameObject.name = PlayerPrefs.GetString("CharacterName");
        race = PlayerPrefs.GetString("Race");
        classJob = PlayerPrefs.GetString("Class");
        strength = PlayerPrefs.GetInt("Strength");
        constitution = PlayerPrefs.GetInt("Constitution");
        dexterity = PlayerPrefs.GetInt("Dexterity");
        intelligence = PlayerPrefs.GetInt("Intelligence");
        wisdom = PlayerPrefs.GetInt("Wisdom");
        charisma = PlayerPrefs.GetInt("Charisma");
    }
    public void SetTexture(string type, int dir)
    {
        Texture2D tex = null;
        int matIndex = 0;
        switch (type)
        {
            case "Skin":
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                matIndex = 1;
                break;
            case "Hair":
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                matIndex = 3;
                break;
            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                matIndex = 4;
                break;
            case "Cloth":
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                matIndex = 5;
                break;
            case "Armour":
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                matIndex = 6;
                break;
        }
        Material[] mats = character.materials;
        mats[matIndex].mainTexture = tex;
        character.materials = mats;
    }
    void OnGUI()
    {
        if (showInfo)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;
            int i = 0;
            int j = 0;

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Character Info");
            GUI.Box(new Rect(scrW * 7, scrH * 0.5f, scrW * 2, scrH * 0.5f), gameObject.name);

            #region Stats
            #region STR Stats
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Strength");
            if (bonusPoint < 10 && baseStrength < strength)
            {
                if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    strength--;
                    bonusPoint++;
                }
            }
            if (bonusPoint >= 10 || baseStrength == strength)
            {
                GUI.Box(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-");
            }
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), strength.ToString());
            if (bonusPoint > 0)
            {
                if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    strength++;
                    bonusPoint--;
                }
            }
            if (bonusPoint <= 0)
            {
                GUI.Box(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+");
            }
            j++;
            #endregion
            #region CON Stats
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Constitution");
            if (bonusPoint < 10 && baseConstitution < constitution)
            {
                if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    constitution--;
                    bonusPoint++;
                }
            }
            if (bonusPoint >= 10 || baseConstitution == constitution)
            {
                GUI.Box(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-");
            }
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), constitution.ToString());
            if (bonusPoint > 0)
            {
                if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    constitution++;
                    bonusPoint--;
                }
            }
            if (bonusPoint <= 0)
            {
                GUI.Box(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+");
            }
            j++;
            #endregion
            #region DEX Stats
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Dexterity");
            if (bonusPoint < 10 && baseDexterity < dexterity)
            {
                if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    dexterity--;
                    bonusPoint++;
                }
            }
            if (bonusPoint >= 10 || baseDexterity == dexterity)
            {
                GUI.Box(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-");
            }
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), dexterity.ToString());
            if (bonusPoint > 0)
            {
                if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    dexterity++;
                    bonusPoint--;
                }
            }
            if (bonusPoint <= 0)
            {
                GUI.Box(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+");
            }
            j++;
            #endregion
            #region INT Stats
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Intelligence");
            if (bonusPoint < 10 && baseIntelligence < intelligence)
            {
                if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    intelligence--;
                    bonusPoint++;
                }
            }
            if (bonusPoint >= 10 || baseIntelligence == intelligence)
            {
                GUI.Box(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-");
            }
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), intelligence.ToString());
            if (bonusPoint > 0)
            {
                if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    intelligence++;
                    bonusPoint--;
                }
            }
            if (bonusPoint <= 0)
            {
                GUI.Box(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+");
            }
            j++;
            #endregion
            #region WIS Stats
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Wisdom");
            if (bonusPoint < 10 && baseWisdom < wisdom)
            {
                if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    wisdom--;
                    bonusPoint++;
                }
            }
            if (bonusPoint >= 10 || baseWisdom == wisdom)
            {
                GUI.Box(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-");
            }
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), wisdom.ToString());
            if (bonusPoint > 0)
            {
                if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    wisdom++;
                    bonusPoint--;
                }
            }
            if (bonusPoint <= 0)
            {
                GUI.Box(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+");
            }
            j++;
            #endregion
            #region CHA Stats
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Charisma");
            if (bonusPoint < 10 && baseCharisma < charisma)
            {
                if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    charisma--;
                    bonusPoint++;
                }
            }
            if (bonusPoint >= 10 || baseCharisma == charisma)
            {
                GUI.Box(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-");
            }
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), charisma.ToString());
            if (bonusPoint > 0)
            {
                if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                {
                    charisma++;
                    bonusPoint--;
                }
            }
            if (bonusPoint <= 0)
            {
                GUI.Box(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+");
            }
            j++;
            #endregion
            GUI.Box(new Rect(11.25f * scrW, scrH * 2 + j * (0.5f * scrH), scrW * 2f, 0.5f * scrH), "Bonus Point");
            GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), bonusPoint.ToString());
            #endregion
        }
    }
}
