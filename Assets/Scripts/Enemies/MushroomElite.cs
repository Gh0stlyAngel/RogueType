using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomElite : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Mushroom, 10);
    }
}
