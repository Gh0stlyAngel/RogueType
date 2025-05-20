using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : BasePassiveItem
{
    [SerializeField] private float radiusMultiplier;

    public override void UpdatePlayerStats()
    {
        player.GetComponent<BaseCharacter>().UpdateCollectCollider(radiusMultiplier);

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
                radiusMultiplier = 1.5f;
                break;
            case 2:
                CurrentLevel = 2;
                radiusMultiplier = 1.33f;
                break;
            case 3:
                CurrentLevel = 3;
                radiusMultiplier = 1.25f;
                break;
            case 4:
                CurrentLevel = 4;
                radiusMultiplier = 1.20f;
                break;
            case 5:
                CurrentLevel = 5;
                radiusMultiplier = 1.33f;
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
                return "Character pickups items from further away.";
            case 2:
                return "Pickup range increased by 33%.";
            case 3:
                return "Pickup range increased by 25%.";
            case 4:
                return "Pickup range increased by 20%.";
            case 5:
                return "Pickup range increased by 33%.";

            default:
                return "";
        }
    }
}
