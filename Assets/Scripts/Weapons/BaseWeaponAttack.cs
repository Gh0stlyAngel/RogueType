using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BaseWeaponAttack : MonoBehaviour
{
    protected List<IDamagable> attackedObjects;
    [SerializeField] protected float pushOutDuration;
    [SerializeField] protected float damage;
    [SerializeField] protected bool started;
    [SerializeField] protected float maxHits;
    [SerializeField] protected float currentHits;

    private void Start() => WeaponAttackStart();

    private void Update() => WeaponAttackUpdate();

    private void FixedUpdate() => WeaponAttackFixedUpdate();




    protected virtual void WeaponAttackStart()
    {
        attackedObjects = new();
    }

    protected virtual void WeaponAttackUpdate()
    {

    }

    protected virtual void WeaponAttackFixedUpdate()
    {
        if (started)
        {
            FixedUpdateCicle();
        }
    }

    protected virtual void FixedUpdateCicle()
    {

    }

    public virtual void Init(float damage, float pushOutDuration)
    {
        this.damage = damage;
        this.pushOutDuration = pushOutDuration;
        started = true;
    }

    protected virtual void DealDamage(IDamagable damagable)
    {
        damagable.GetDamage(damage, pushOutDuration, gameObject.transform.position);
    }

    protected virtual void CheckAttackedObjects(IDamagable damagable)
    {
        if (!attackedObjects.Contains(damagable))
        {
            DealDamage(damagable);
            attackedObjects.Add(damagable);
        }
    }

    protected virtual void CheckCurrentHits()
    {
        currentHits++;
        if (currentHits >= maxHits)
        {
            Destroy(gameObject);
        }
    }
}
