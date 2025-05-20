using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : BasePassiveItem
{
    [SerializeField] private int amount;

    public override void UpdatePlayerStats()
    {
        player.GetComponent<BaseCharacter>().AmountTotal += amount;

        base.UpdatePlayerStats();
    }

    public override void InitItem()
    {
        base.InitItem();
        maxLevel = 2;
    }

    public override void SetStats(int level)
    {
        switch (level)
        {
            case 0:
            case 1:
                CurrentLevel = 1;
                amount = 1;
                break;
            case 2:
                CurrentLevel = 2;
                amount = 1;
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
                return "Weapons fire more projectiles.";
            case 2:
                return "Fires 1 more projectile.";

            default:
                return "";
        }
    }
}
