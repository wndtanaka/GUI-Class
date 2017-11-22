using UnityEngine;

public static class StatsGen
{
    public static Stats BaseStats(string className)
    {
        Stats temp = new Stats();
        int strength = 0;
        int constitution = 0;
        int dexterity = 0;
        int intelligence = 0;
        int wisdom = 0;
        int charisma = 0;
        ClassType classType = ClassType.Warrior;

        switch (className)
        {
            case "Warrior":
                strength = 17;
                constitution = 13;
                dexterity = 8;
                intelligence = 4;
                wisdom = 8;
                charisma = 10;
                classType = ClassType.Warrior;
                break;
            case "Knight":
                strength = 12;
                constitution = 19;
                dexterity = 4;
                intelligence = 8;
                wisdom = 10;
                charisma = 7;
                classType = ClassType.Knight;
                break;
            case "Rogue":
                strength = 8;
                constitution = 8;
                dexterity = 18;
                intelligence = 8;
                wisdom = 8;
                charisma = 10;
                classType = ClassType.Rogue;
                break;
            case "Archer":
                strength = 6;
                constitution = 6;
                dexterity = 20;
                intelligence = 10;
                wisdom = 8;
                charisma = 10;
                classType = ClassType.Archer;
                break;
            case "Mage":
                strength = 4;
                constitution = 4;
                dexterity = 6;
                intelligence = 20;
                wisdom = 12;
                charisma = 14;
                classType = ClassType.Mage;
                break;
            case "Cleric":
                strength = 4;
                constitution = 6;
                dexterity = 4;
                intelligence = 18;
                wisdom = 15;
                charisma = 13;
                classType = ClassType.Cleric;
                break;
            default:
                strength = 17;
                constitution = 13;
                dexterity = 8;
                intelligence = 4;
                wisdom = 8;
                charisma = 10;
                classType = ClassType.Warrior;
                break;
        }
        #region Temp Connect
        temp.ClassName = className;
        temp.Strength = strength;
        temp.Constitution= constitution;
        temp.Dexterity= dexterity;
        temp.Intelligence = intelligence;
        temp.Wisdom= wisdom;
        temp.Charisma = charisma;
        temp.Job = classType;
        #endregion
        return temp;
    }
}
