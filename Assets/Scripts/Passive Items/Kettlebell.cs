using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettlebell : BasePassiveItem
{
    [SerializeField] private float strenght;

    public override void UpdatePlayerStats()
    {
        player.GetComponent<BaseCharacter>().StrenghtTotal += strenght;

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
                strenght = 0.1f;
                break;
            case 2:
                CurrentLevel = 2;
                strenght = 0.1f;
                break;
            case 3:
                CurrentLevel = 3;
                strenght = 0.1f;
                break;
            case 4:
                CurrentLevel = 4;
                strenght = 0.1f;
                break;
            case 5:
                CurrentLevel = 5;
                strenght = 0.1f;
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
                return "Raises inflicted damage by 10%.";
            case 2:
                return "Base damage up by 10%.";
            case 3:
                return "Base damage up by 10%.";
            case 4:
                return "Base damage up by 10%.";
            case 5:
                return "Base damage up by 10%.";

            default:
                return "";
        }
    }
}
