using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulet : BasePassiveItem
{
    [SerializeField] private float cooldown;

    public override void UpdatePlayerStats()
    {
        player.GetComponent<BaseCharacter>().CooldownTotal -= cooldown;

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
                cooldown = 0.08f;
                break;
            case 2:
                CurrentLevel = 2;
                cooldown = 0.08f;
                break;
            case 3:
                CurrentLevel = 3;
                cooldown = 0.08f;
                break;
            case 4:
                CurrentLevel = 4;
                cooldown = 0.08f;
                break;
            case 5:
                CurrentLevel = 5;
                cooldown = 0.08f;
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
                return "Reduces weapons cooldown by 8%.";
            case 2:
                return "Cooldown reduced by 8.0%.";
            case 3:
                return "Cooldown reduced by 8.0%.";
            case 4:
                return "Cooldown reduced by 8.0%.";
            case 5:
                return "Cooldown reduced by 8.0%.";

            default:
                return "";
        }
    }
}
