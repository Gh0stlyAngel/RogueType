using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlyingEye : BaseEnemy
{
    protected override void EnemyOnDestroy()
    {
        base.EnemyOnDestroy();
        StatisticManager.Instance.AddEnemyKills(EnemyType.FlyingEye, 1);
    }
}
