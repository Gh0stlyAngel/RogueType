using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : BaseCollectable
{
    [SerializeField] private float HealingAmount;

    protected override void CollectableOnDestroy()
    {
        if (player != null)
            player.GetComponent<BaseCharacter>().GetHealing(HealingAmount);
    }


}
