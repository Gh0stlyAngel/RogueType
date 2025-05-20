using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DaggerParameters : BaseWeaponAttack
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    protected override void WeaponAttackStart()
    {
        base.WeaponAttackStart();
    }

    protected override void WeaponAttackUpdate()
    {
        base.WeaponAttackUpdate();
    }

    protected override void WeaponAttackFixedUpdate()
    {
        base.WeaponAttackFixedUpdate();
    }

    protected override void FixedUpdateCicle()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
    }

    public void Init(float damage, float pushOutDuration, float speed, int maxHits, Vector3 direction)
    {
        this.speed = speed;
        this.maxHits = maxHits;
        this.direction = direction;

        Vector3 rotationDirection = (transform.position + direction) - transform.position;
        float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 45 - 90);

        base.Init(damage, pushOutDuration);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            CheckAttackedObjects(damagable);

            CheckCurrentHits();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpawnManager"))
        {
            Destroy(gameObject);
        }
    }
}
