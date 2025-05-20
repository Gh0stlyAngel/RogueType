using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePassiveItem : BaseItem
{

    protected override void ItemStart()
    {
        base.ItemStart();
        player = GameObject.FindWithTag("Player");
        UpdatePlayerStats();
    }

    public virtual void UpdatePlayerStats()
    {
        foreach (Transform item in player.transform)
        {
            if (item.TryGetComponent(out BaseWeapon weapon))
            {
                weapon.SetStats(weapon.CurrentLevel);
            }
        }
    }

    public override void LevelUp(int currentLevel, ref List<GameObject> currentItemsList)
    {
        base.LevelUp(currentLevel, ref currentItemsList);
    }

    public override void SetStats(int level)
    {
        base.SetStats(level);
    }

    public override void InitItem()
    {

    }
}
