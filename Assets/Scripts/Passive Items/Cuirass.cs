using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuirass : BasePassiveItem
{
    [SerializeField] private float armor;

    public override void UpdatePlayerStats()
    {
        player.GetComponent<BaseCharacter>().ArmorTotal += armor;

        base.UpdatePlayerStats();
    }

    public override void InitItem()
    {
        base.InitItem();
        maxLevel = 5;
    }

    public override void SetStats(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                CurrentLevel = 1;
                armor = 1f;
                break;
            case 2:
                CurrentLevel = 2;
                armor = 1f;
                break;
            case 3:
                CurrentLevel = 3;
                armor = 1f;
                break;
            case 4:
                CurrentLevel = 4;
                armor = 1f;
                break;
            case 5:
                CurrentLevel = 5;
                armor = 1f;
                AtMaxLevel = true;
                break;
        }
    }

    public override string GetLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                return "Reduces incoming damage by 1.";
            case 2:
                return "Reduces incoming damage by 1.";
            case 3:
                return "Reduces incoming damage by 1.";
            case 4:
                return "Reduces incoming damage by 1.";
            case 5:
                return "Reduces incoming damage by 1.";

            default:
                return "";
        }
    }
}
