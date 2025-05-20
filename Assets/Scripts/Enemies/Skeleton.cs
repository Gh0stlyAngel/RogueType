using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Skeleton, 1);
    }
}
