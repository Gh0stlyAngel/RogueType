using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : BasePassiveItem
{
    [SerializeField] private float duration;

    public override void UpdatePlayerStats()
    {
        player.GetComponent<BaseCharacter>().DurationTotal += duration;

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
                duration = 0.1f;
                break;
            case 2:
                CurrentLevel = 2;
                duration = 0.1f;
                break;
            case 3:
                CurrentLevel = 3;
                duration = 0.1f;
                break;
            case 4:
                CurrentLevel = 4;
                duration = 0.1f;
                break;
            case 5:
                CurrentLevel = 5;
                duration = 0.1f;
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
                return "Increases duration of weapon effects by 10%.";
            case 2:
                return "Effect lasts 10% longer.";
            case 3:
                return "Effect lasts 10% longer.";
            case 4:
                return "Effect lasts 10% longer.";
            case 5:
                return "Effect lasts 10% longer.";

            default:
                return "";
        }
    }
}
