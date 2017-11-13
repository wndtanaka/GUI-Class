using UnityEngine;

public class Item
{
    #region Private Variables
    private string _name;
    private string _description;
    private int _idNum;
    public int _value;
    public string _mesh;
    public Texture2D _icon;
    private ItemType _type;
    #endregion
    #region Constructors
    public void ItemConstructor(int itemID, string itemName, Texture2D itemIcon, ItemType itemType)
    {
        _idNum = itemID;
        _name = itemName;
        _icon = itemIcon;
        _type = itemType;
    }
    #endregion
    #region Public Variables
    public int ID
    {
        get { return  _idNum; }
        set { _idNum = value; }
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }
    public string Mesh
    {
        get { return _mesh; }
        set { _mesh = value; }
    }
    public ItemType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    #endregion
}
#region Enums
public enum ItemType
{
    Food,
    Weapon,
    Apparel,
    Crafting,
    Quest,
    Money,
    Ingredients,
    Potions,
    Scrolls
}
#endregion