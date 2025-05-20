using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollectable : BaseCollectable
{
    protected override void CollectableOnDestroy()
    {
        if (player != null)
            CollectableManager.Instance.ToAttractGems();
    }
}
