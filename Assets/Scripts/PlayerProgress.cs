using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public List<Upgrade> upgrades;
    public List<Upgrade> warriorUpgrades;
    public List<Upgrade> reaperUpgrades;
    public List<Upgrade> knightUpgrades;
    public int coins;
    public string version;
}

[System.Serializable]
public class Upgrade
{
    public string id;
    public int currentLevel;
}
