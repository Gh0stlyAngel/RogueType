using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IceStaffProjectile : BaseWeaponAttack
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private BoxCollider2D stunCollider;
    [SerializeField] private float lifetime;
    private bool freezed = false;


    protected override void WeaponAttackStart()
    {
        base.WeaponAttackStart();
    }

    protected override void WeaponAttackFixedUpdate()
    {
        base.WeaponAttackFixedUpdate();
    }

    protected override void FixedUpdateCicle()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        if ((startPosition - transform.position).magnitude >= 5 && !freezed)
        {
            speed = 0;
            FreezeEnemy();
            Destroy(gameObject, lifetime);
        }
    }

    public void Init(int damage, float pushOutDuration, float angle, Vector3 startPosition, float duration, float speed)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        lifetime = 0.45f;

        stunCollider = GetComponent<BoxCollider2D>();

        this.startPosition = startPosition;
        this.duration = duration;
        this.speed = speed;
        base.Init(damage, pushOutDuration);
    }

    private void FreezeEnemy()
    {
        // stunCollider.offset - локальное значение, для OverlapBoxAll нужно мировое
        // значение, поэтому TransformVector ковернтирует из локального в мировой offset

        Vector2 worldOffset = gameObject.transform.TransformVector(stunCollider.offset);
        Vector2 worldCenter = (Vector2)transform.position + worldOffset;

        Vector3 lossyScale = gameObject.transform.lossyScale;
        Vector2 worldSize = new Vector2(stunCollider.size.x * lossyScale.x, stunCollider.size.y * lossyScale.y);




        var collisionList = Physics2D.OverlapBoxAll(worldCenter, worldSize, transform.eulerAngles.z);

        foreach (var collision in collisionList)
        {
            if (collision.gameObject.TryGetComponent<IStunable>(out var stunable))
            {
                stunable.GetStunned(duration);
            }
        }

        freezed = true;
    }
}
