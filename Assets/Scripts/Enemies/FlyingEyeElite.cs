using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeElite : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.FlyingEye, 10);
    }
}
