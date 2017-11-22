using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationGet : MonoBehaviour
{
    public Renderer character;
    // Use this for initialization
    public string race;
    public string classJob;
    public int strength;
    public int constitution;
    public int dexterity;
    public int intelligence;
    public int wisdom;
    public int charisma;

    void Start()
    {
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        LoadTexture();
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
}
