using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WagicWandProjectile : BaseWeaponAttack
{
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 direction;
    [SerializeField] private GameObject particle;
    [SerializeField] private float speed;
    [SerializeField] private List<GameObject> enemies;

    protected override void WeaponAttackStart()
    {
        base.WeaponAttackStart();
        enemies = spawnManager.Enemies;
    }

    protected override void WeaponAttackFixedUpdate()
    {
        base.WeaponAttackFixedUpdate();
    }

    protected override void FixedUpdateCicle()
    {
        //Vector3 direction = (target.position - transform.position).normalized * speed * Time.fixedDeltaTime;
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    public void Init(float damage, float pushOutDuration, float speed, int maxHits, SpawnManager spawnManager, Vector3 target)
    {
        this.speed = speed;
        this.maxHits = maxHits;
        currentHits = 0;
        this.spawnManager = spawnManager;
        this.target = target;
        particle.GetComponent<ProjectileParticlesRotation>().target = this.target;
        direction = (target - transform.position).normalized;

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
