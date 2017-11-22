using UnityEngine;

public class Stats
{
    public string _className;
    public int _strength;
    public int _constitution;
    public int _dexterity;
    public int _intelligence;
    public int _wisdom;
    public int _charisma;
    public ClassType _classType;

    public string ClassName
    {
        get { return _className; }
        set { _className= value; }
    }
    public int Strength
    {
        get { return _strength; }
        set { _strength = value; }
    }
    public int Constitution
    {
        get { return _constitution; }
        set { _constitution = value; }
    }
    public int Dexterity
    {
        get { return _dexterity; }
        set { _dexterity = value; }
    }
    public int Intelligence
    {
        get { return _intelligence; }
        set { _intelligence = value; }
    }
    public int Wisdom
    {
        get { return _wisdom; }
        set { _wisdom = value; }
    }
    public int Charisma
    {
        get { return _charisma; }
        set { _charisma = value; }
    }
    public ClassType Job
    {
        get { return _classType; }
        set { _classType= value; }
    }
}

#region Enums
public enum ClassType
{
    Warrior,
    Knight,
    Rogue,
    Archer,
    Mage,
    Cleric
}
#endregion
