using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonElite : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.Skeleton, 10);
    }
}
