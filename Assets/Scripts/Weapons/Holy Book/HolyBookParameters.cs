using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBookParameters : BaseWeaponAttack
{
    [SerializeField] private GameObject player;

    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float angle; // 3.2f == 360deg

    [SerializeField] private float lifetime;

    protected override void WeaponAttackFixedUpdate()
    {
        base.WeaponAttackFixedUpdate();
    }

    protected override void FixedUpdateCicle()
    {
        float x = Mathf.Cos(angle * speed) * radius;
        float y = Mathf.Sin(angle * speed) * radius * -1;

        transform.Translate(player.transform.position - new Vector3(x, y) - transform.position);

        angle += Time.fixedDeltaTime;

        /*refreshAttackedDamage--;

        if (refreshAttackedDamage == 0)
        {
            // Clear list every 180deg
            attackedObjects.Clear();
            refreshAttackedDamage = 80;
        }*/
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(float damage, float pushOutDuration, float lifetime, float speed, float radius, float angle, List<IDamagable> attacked, GameObject player)
    { 
        this.lifetime = lifetime;
        this.speed = speed;
        this.radius = radius;
        this.angle = angle;
        this.player = player;
        attackedObjects = attacked;

        base.Init(damage, pushOutDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            CheckAttackedObjects(damagable);
        }
    }


}
