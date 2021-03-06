﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInventory : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    public bool showInv;
    public List<Item> inventory = new List<Item>();
    public int slotX, slotY;
    private Rect inventorySize;

    [Header("Dragging")]
    public bool dragging;
    public Item draggedItem;
    public int draggedFrom;
    public GameObject droppedItem;

    [Header("Tool Tip")]
    public int toolTipItem;
    public bool showToolTip;
    private Rect toolTipRect;

    [Header("References and Locations")]
    public Movement playerMovement;
    public MouseLook mainCam, playerCam;
    private float scrW, scrH;
    CharacterHandler charH;
    #endregion
    #region Clamp to Screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region Add Item
    public void AddItem(int ID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name == null)
            {
                inventory[i] = ItemGen.CreateItem(ID);
                Debug.Log(inventory[i].Name + " was added");
                return;
            }
        }
    }
    #endregion
    #region Drop Item
    public void DropItem(int ID)
    {
        droppedItem = Resources.Load("Prefabs/" + ItemGen.CreateItem(ID).Mesh) as GameObject;
        Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        return;
    }
    #endregion
    #region Draw Item
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scrW * 0.5f, scrH * 0.5f), draggedItem.Icon);
        }
    }
    #endregion
    #region Tool Tip
    #region Tool Tip Content
    private string ToolTipContent(int ID)
    {
        string toolTipString =
            "Name: " + inventory[ID].Name +
            "\nDescription: " + inventory[ID].Description +
            "\nType: " + inventory[ID].Type +
            "\nID: " + inventory[ID].ID;
        return toolTipString;
    }
    #endregion
    #region Tool Tip Window
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scrW * 2, scrH * 3), ToolTipContent(toolTipItem));
    }
    #endregion
    #endregion
    #region Drag Inventory
    void InvetoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, 0.25f * scrH, 6 * scrW, 0.5f * scrH), "Banner");
        GUI.Box(new Rect(0, 4.25f * scrH, 6 * scrW, 0.5f * scrH), "Gold and Exp");
        showToolTip = false;

        #region Nested For Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(0.125f * scrW + x * (scrW * 0.75f), 0.75f * scrH + y * (scrH * 0.65f), scrW * 0.75f, scrH * 0.65f);
                GUI.Box(slotLocation, "");

                #region Pickup Item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !dragging && inventory[i].Name != null)
                {
                    draggedItem = inventory[i];
                    inventory[i] = new Item();
                    dragging = true;
                    draggedFrom = i;
                    Debug.Log("Dragging: " + draggedItem.Name);
                }
                #endregion
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping: " + draggedItem.Name + "With: " + inventory[i].Name);
                    inventory[draggedFrom] = inventory[i];
                    inventory[i] = draggedItem;
                    dragging = false;
                    draggedItem = new Item(); ;
                }
                #endregion
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name == null)
                {
                    Debug.Log("Place: " + draggedItem.Name + "Into: " + i);
                    inventory[i] = draggedItem;
                    dragging = false;
                    draggedItem = new Item(); ;
                }
                #endregion
                #region Return Item
                if (e.button == 0 && e.type == EventType.MouseUp && i == ((slotX * slotY) - 1) && dragging)
                {
                    Debug.Log("Return: " + draggedItem.Name + "Into: " + draggedFrom);
                    inventory[draggedFrom] = draggedItem;
                    dragging = false;
                    draggedItem = new Item(); ;
                }
                #endregion
                #region Draw Item Icon
                if (inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    #region Set ToolTip on Mouse
                    if (slotLocation.Contains(e.mousePosition) && !dragging & showInv)
                    {
                        toolTipItem = 1;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }

        #endregion
        #region Drag Window
        GUI.DragWindow(new Rect(0 * scrW, 0 * scrH, 6 * scrW, 0.5f * scrH));
        GUI.DragWindow(new Rect(0 * scrW, 0.5f * scrH, 0.25f * scrW, 3.5f * scrH));
        GUI.DragWindow(new Rect(5.5f * scrW, 0.5f * scrH, 0.25f * scrW, 3.5f * scrH));
        GUI.DragWindow(new Rect(0 * scrW, 4 * scrH, 0.25f * scrW, 3.5f * scrH));
        #endregion
    }
    #endregion
    #region Start
    void Start()
    {
        charH = GetComponent<CharacterHandler>();
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        playerMovement = GetComponent<Movement>();
        mainCam = Camera.main.GetComponent<MouseLook>();
        playerCam = GetComponent<MouseLook>();
        inventorySize = new Rect(scrW, scrH, 6 * scrW, 4.5f * scrH);
        for (int i = 0; i < (slotX * slotY); i++)
        {
            inventory.Add(new Item());
        }
        AddItem(0);
        AddItem(0);
        AddItem(2);
        AddItem(102);
        AddItem(206);
    }
    #endregion
    #region Toggle Inventory and Control
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
            playerMovement.enabled = true;
            charH.gameScene = true;
            return (false);
        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainCam.enabled = false;
            playerCam.enabled = false;
            playerMovement.enabled = false;
            charH.gameScene = false;
            return (true);
        }

    }
    #endregion
    #region Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
    }
    #endregion
    #region OnGUI
    void OnGUI()
    {
        Event e = Event.current;
        #region Draw Inventory if showInv is true
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InvetoryDrag, "My Inventory"));
            GUI.Box(new Rect(scrW * 8, scrH * 2, scrW * 4, scrH * 3), "Tutorial \n Click 'C' for Character Info \n Hit the box with E, to gain Exp");
        }
        #endregion
        #region Draw ToolTip
        if (showToolTip && showInv)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f,scrW * 2,scrH *3);
            GUI.Window(3, toolTipRect, DrawToolTip, "");
        }

        #endregion
        #region Drop Item on not show Inventory and Mouse is up
        if (e.button == 0 && e.type == EventType.MouseUp && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: "+ draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Incase inventory closes drop dragged item
        if (e.button == 0 && e.type == EventType.MouseUp && dragging && !showInv)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Draw Item on Mouse
        if (dragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scrW * 0.5f, scrH * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }
    #endregion
}
