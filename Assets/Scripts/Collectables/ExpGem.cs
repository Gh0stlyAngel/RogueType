using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : BaseCollectable
{
    [SerializeField] public int ExpAmount;
    protected override void CollectableStart()
    {
        base.CollectableStart();
        GemCounter.Instance.AddGem(this);
        //GemCounter.Instance.RegisterGem(this);
    }

    /*protected override void CollectableUpdate()
    {
        base.CollectableUpdate();
    }*/

    protected override void CollectableOnDestroy()
    {
        if(player != null)
        {
            var baseCharacter = player.GetComponent<BaseCharacter>();
            if(baseCharacter.Health > 0)
            {
                baseCharacter.AddExp(ExpAmount);
            }
            
        }

        if (GemCounter.Instance != null)
        {
            GemCounter.Instance.RemoveGem(this);
            //GemCounter.Instance.UnregisterGem(this);
        }

    }

    public override void CollectableEffect(GameObject playerObject)
    {
        base.CollectableEffect(playerObject);
    }

}
