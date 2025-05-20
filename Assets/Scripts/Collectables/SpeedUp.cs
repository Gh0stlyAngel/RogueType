using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : BaseCollectable
{
    [SerializeField] private float speedUpDuration;
    [SerializeField] private float speedUpMultiplier;

    protected override void CollectableOnDestroy()
    {
        if (player != null)
            player.GetComponent<PlayerController>().SpeedUp(speedUpDuration, speedUpMultiplier);
    }
}
