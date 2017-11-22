﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<Item> inv = new List<Item>();
    public Item selectedItem;

    [Header("Bools and Values")]
    public bool showInv;
    public int money;

    [Header("References and Locations")]
    public Vector2 scrollPos = Vector2.zero;
    public MouseLook mainCam, playerCam;
    public Movement playerMove;
    public Transform wHandler, hHandler;
    public GameObject helm, weapon;

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main.GetComponent<MouseLook>();
        playerCam = GetComponent<MouseLook>();
        playerMove = GetComponent<Movement>();
        wHandler = GameObject.FindGameObjectWithTag("WeaponHandler").GetComponent<Transform>();
        hHandler = GameObject.FindGameObjectWithTag("HeadHandler").GetComponent<Transform>();

        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(1));
        inv.Add(ItemGen.CreateItem(100000));
        inv.Add(ItemGen.CreateItem(100));
        inv.Add(ItemGen.CreateItem(101));
        inv.Add(ItemGen.CreateItem(103));
        inv.Add(ItemGen.CreateItem(206));
        inv.Add(ItemGen.CreateItem(800));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            inv.Add(ItemGen.CreateItem(0));
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            inv.Remove(inv[inv.Count - 1]);
        }
    }
    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
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
            showInv = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainCam.enabled = false;
            playerCam.enabled = false;
            playerMove.enabled = false;
            return true;
        }
    }
    private void OnGUI()
    {
        if (showInv)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            // Background labeled Inventory
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
            // If we have less than or 35 items, then no scroll view
            if (inv.Count <= 35)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrW + i * (0.25f * scrH), 3 * scrW, 0.25f * scrH), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
            }
            // else, we can scroll
            else
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0, 6 * scrW, 9 * scrH), scrollPos, new Rect(0, 0, 0, 9 * scrH + ((inv.Count - 35) * 0.25f * scrH)), false, true);
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrW + i * (0.25f * scrH), 3 * scrW, 0.25f * scrH), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
                GUI.EndScrollView();
            }
            if (selectedItem != null)
            {
                if (selectedItem.Type == ItemType.Food)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: $" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.75f * scrH, scrW, 0.25f * scrH), "Eat"))
                    {
                        Debug.Log("Yummy " + selectedItem.Name);
                        inv.Remove(selectedItem);
                        selectedItem = null;
                    }
                }
                else if (selectedItem.Type == ItemType.Weapon)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: $" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.75f * scrH, scrW, 0.25f * scrH), "Equip"))
                    {
                        Debug.Log(selectedItem.Name + "Equipped");
                        if (weapon != null)
                        {
                            Destroy(weapon);
                        }
                        weapon = Instantiate(SpawnItem(selectedItem.Name),wHandler.position,wHandler.rotation,wHandler);
                        selectedItem = null;
                    }
                }
                else if (selectedItem.Type == ItemType.Apparel)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: $" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.75f * scrH, scrW, 0.25f * scrH), "Equip"))
                    {
                        Debug.Log(selectedItem.Name + "Equipped");
                        if (helm != null && selectedItem.Name == "Helmets")
                        {
                            Destroy(helm);
                        }
                        helm = Instantiate(SpawnItem(selectedItem.Name), hHandler.position, hHandler.rotation, hHandler);
                        selectedItem = null;
                    }
                }
                else if (selectedItem.Type == ItemType.Crafting)
                {

                }
                else if (selectedItem.Type == ItemType.Potions)
                {

                }
                else if (selectedItem.Type == ItemType.Scrolls)
                {

                }
                else
                {
                    Debug.Log("Item Error");
                }
            }
        }
    }
    public GameObject SpawnItem(string ItemName)
    {
        GameObject spawn = Resources.Load("Prefabs/" + selectedItem.Name) as GameObject;
        return spawn;
    }
}
