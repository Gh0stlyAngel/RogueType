using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade Config")]
public class UpgradeConfig : ScriptableObject
{
    public string id;
    public string displayName;
    public int maxLevel;
    public List<int> perLevelCost;
    public List<float> perLevelBonus;
}
