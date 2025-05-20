using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeBoss : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.FlyingEye, 250);
    }
}
