using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationSet : MonoBehaviour
{
    #region Variables
    [Header("Texture List")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> cloth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<string> race = new List<string>();
    public List<string> classes = new List<string>();
    public List<Stats> status = new List<Stats>();
    public Stats stats;

    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int hairIndex;
    public int mouthIndex;
    public int eyesIndex;
    public int clothIndex;
    public int armourIndex;
    public int raceIndex;
    public int classIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;
    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int hairMax;
    public int mouthMax;
    public int eyesMax;
    public int clothMax;
    public int armourMax;
    public int raceMax;
    public int classMax;
    [Header("Character Name")]
    //name of our character that the user is making
    public string charName = "Adventurer";
    #endregion
    CharacterHandler charH;
    [Header("Stats")]
    private int baseStrength;
    private int baseConstitution;
    private int baseDexterity;
    private int baseIntelligence;
    private int baseWisdom;
    private int baseCharisma;
    public int strength;
    public int constitution;
    public int dexterity;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public int bonusPoint = 10;

    #region Start
    void Start()
    {
        charH = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHandler>();
        charH.gameScene = false;
        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the skin List
            skin.Add(temp);
        }

        //for loop looping from 0 to less than the max amount of hair textures we need
        for (int i = 0; i < hairMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
            Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the hair List
            hair.Add(temp);
        }

        //for loop looping from 0 to less than the max amount of mouth textures we need
        for (int i = 0; i < mouthMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the mouth List
            mouth.Add(temp);
        }


        //for loop looping from 0 to less than the max amount of eyes textures we need
        for (int i = 0; i < eyesMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the eyes List
            eyes.Add(temp);
        }
        for (int i = 0; i < clothMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the eyes List
            cloth.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Armour_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the eyes List
            armour.Add(temp);
        }
        race.Add("Human");
        race.Add("Orc");
        race.Add("Elf");
        race.Add("Undead");
        classes.Add("Warrior");
        classes.Add("Knight");
        classes.Add("Rogue");
        classes.Add("Archer");
        classes.Add("Mage");
        classes.Add("Cleric");

        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Cloth", 0);
        SetTexture("Armour", 0);
        SetRace("Race", 0);
        SetClass("Class", 0);
        SetStats("Warrior", 0);
        #endregion
    }
    #endregion

    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        //inside a switch statement that is swapped by the string name of our material
        #region Switch Material
        switch (type)
        {
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as our skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skin.ToArray();
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //now repeat for each material 
            //hair is 2
            case "Hair":
                //index is the same as our index
                index = hairIndex;
                //max is the same as our max
                max = hairMax;
                //textures is our list .ToArray()
                textures = hair.ToArray();
                //material index element number is 2
                matIndex = 2;
                //break
                break;
            //mouth is 3
            case "Mouth":
                //index is the same as our index
                index = mouthIndex;
                //max is the same as our max
                max = mouthMax;
                //textures is our list .ToArray()
                textures = mouth.ToArray();
                //material index element number is 3
                matIndex = 3;
                //break
                break;
            //eyes are 4
            case "Eyes":
                //index is the same as our index
                index = eyesIndex;
                //max is the same as our max
                max = eyesMax;
                //textures is our list .ToArray()
                textures = eyes.ToArray();
                //material index element number is 4
                matIndex = 4;
                //break
                break;
            case "Cloth":
                //index is the same as our index
                index = clothIndex;
                //max is the same as our max
                max = clothMax;
                //textures is our list .ToArray()
                textures = cloth.ToArray();
                //material index element number is 5
                matIndex = 5;
                //break
                break;
            case "Armour":
                //index is the same as our index
                index = armourIndex;
                //max is the same as our max
                max = armourMax;
                //textures is our list .ToArray()
                textures = armour.ToArray();
                //material index element number is 6
                matIndex = 6;
                //break
                break;
        }
        #endregion
        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = character.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        character.materials = mat;
        //create another switch that is goverened by the same string name of our material
        #endregion
        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            //case hair
            case "Hair":
                //index equals our index
                hairIndex = index;
                //break
                break;
            //case mouth
            case "Mouth":
                //index equals our index
                mouthIndex = index;
                //break
                break;
            //case eyes
            case "Eyes":
                //index equals our index
                eyesIndex = index;
                //break
                break;
            case "Cloth":
                //index equals our index
                clothIndex = index;
                //break
                break;
            case "Armour":
                //index equals our index
                armourIndex = index;
                //break
                break;
        }
        #endregion
    }
    #endregion
    #region SetRace
    void SetRace(string raceName, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        string[] raceType = new string[0];

        switch (raceName)
        {
            case "Race":
                index = raceIndex;
                max = raceMax;
                raceType = race.ToArray();
                matIndex = 1;
                break;
        }
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        raceType[matIndex] = raceType[index];
        switch (raceName)
        {
            case "Race":
                raceIndex = index;
                break;
        }
    }
    #endregion
    #region SetClass
    void SetClass(string className, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        string[] classType = new string[0];

        switch (className)
        {
            case "Class":
                index = classIndex;
                max = classMax;
                classType = classes.ToArray();
                matIndex = 1;
                break;
        }
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        classType[matIndex] = classType[index];
        switch (className)
        {
            case "Class":
                classIndex = index;
                break;
        }
    }
    #endregion
    #region SetStats
    void SetStats(string className, int dir)
    {
        int index = 0, max = 0;

        switch (className)
        {
            case "Warrior":
                strength = 17;
                constitution = 13;
                dexterity = 8;
                intelligence = 4;
                wisdom = 8;
                charisma = 10;
                break;
            case "Knight":
                strength = 12;
                constitution = 19;
                dexterity = 4;
                intelligence = 8;
                wisdom = 10;
                charisma = 7;
                break;
            case "Rogue":
                strength = 8;
                constitution = 8;
                dexterity = 18;
                intelligence = 8;
                wisdom = 8;
                charisma = 10;
                break;
            case "Archer":
                strength = 6;
                constitution = 6;
                dexterity = 20;
                intelligence = 10;
                wisdom = 8;
                charisma = 10;
                break;
            case "Mage":
                strength = 4;
                constitution = 4;
                dexterity = 6;
                intelligence = 20;
                wisdom = 12;
                charisma = 14;
                break;
            case "Cleric":
                strength = 4;
                constitution = 6;
                dexterity = 4;
                intelligence = 18;
                wisdom = 15;
                charisma = 13;
                break;
            default:
                strength = 17;
                constitution = 13;
                dexterity = 8;
                intelligence = 4;
                wisdom = 8;
                charisma = 10;
                break;
        }

        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        baseStrength = strength;
        baseConstitution = constitution;
        baseDexterity = dexterity;
        baseIntelligence = intelligence;
        baseWisdom = wisdom;
        baseCharisma = charisma;
    }
    #endregion


    #region Save
    //Function called Save this will allow us to save our indexes to PlayerPrefs
    void Save()
    {
        //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothIndex", clothIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        //SetString CharacterName, and stats
        PlayerPrefs.SetString("CharacterName", charName);
        PlayerPrefs.SetString("Race", race[raceIndex]);
        PlayerPrefs.SetString("Class", classes[classIndex]);
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Constitution", constitution);
        PlayerPrefs.SetInt("Dexterity", dexterity);
        PlayerPrefs.SetInt("Intelligence", intelligence);
        PlayerPrefs.SetInt("Wisdom", wisdom);
        PlayerPrefs.SetInt("Charisma", charisma);
    }
    #endregion

    #region OnGUI
    //Function for our GUI elements
    void OnGUI()
    {
        //create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        //create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        int j = 0;
        #region Skin
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Skin", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Skin");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Skin", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        //set up same things for Hair, Mouth and Eyes
        #region Hair
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Hair", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Hair");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Hair", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Mouth", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Mouth");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Mouth", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Eyes
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Eyes", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Eyes");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Eyes", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Cloth
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Cloth", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Cloth");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Cloth", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Armour
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            SetTexture("Armour", -1);
        }
        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Armour");
        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(2.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            SetTexture("Armour", 1);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Race
        if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            SetRace("Race", -1);
        }
        GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), race[raceIndex]);
        if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetRace("Race", 1);
        }
        j++;
        #endregion
        #region Class
        if (GUI.Button(new Rect(13.25f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            SetClass("Class", -1);
            SetStats(classes[classIndex], -1);
            bonusPoint = 10;
        }
        GUI.Box(new Rect(13.75f * scrW, scrH * 2 + j * (0.5f * scrH), scrW, 0.5f * scrH), classes[classIndex]);
        if (GUI.Button(new Rect(14.75f * scrW, scrH * 2 + j * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetClass("Class", 1);
            SetStats(classes[classIndex], 1);
            bonusPoint = 10;
        }
        j++;
        #endregion
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
        #region Random & Reset
        //create 2 buttons one Random and one Reset
        //Random will feed a random amount to the direction 
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Random"))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyesMax - 1));
            SetTexture("Cloth", Random.Range(0, clothMax - 1));
            SetTexture("Armour", Random.Range(0, armourMax - 1));
            SetRace("Race", Random.Range(0, raceMax - 1));
            SetClass("Class", Random.Range(0, classMax - 1));
            SetStats(classes[classIndex], 0);
        }
        //reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Cloth", clothIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetRace("Race", raceIndex = 0);
            SetClass("Class", classIndex = 0);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12);
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        //GUI Button called Save and Play
        if (GUI.Button(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
        {
            //this button will run the save function and also load into the game level
            Save();
            SceneManager.LoadScene("Game");
        }
        #endregion
    }
    #endregion
}
