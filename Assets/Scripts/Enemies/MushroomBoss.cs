using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBoss : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Mushroom, 100);
    }
}
