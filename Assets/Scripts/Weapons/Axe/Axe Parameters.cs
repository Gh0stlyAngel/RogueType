using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeParameters : BaseWeaponAttack
{
    [SerializeField] private float startSpeed;
    [SerializeField] private float speed;

    [SerializeField] private Vector3 currentRotation;
    [SerializeField] private float rotationAngle;
    [SerializeField] private bool speedUpRotation;

    [SerializeField] private float xOffset;
    [SerializeField] private int throwDirection;
    protected override void WeaponAttackStart()
    {
        base.WeaponAttackStart();
        currentHits = 0;
        currentRotation = transform.eulerAngles;
        rotationAngle = 12f;
        startSpeed = 8;
        speed = startSpeed;
        xOffset = Random.Range(1f, 2.8f);
    }

    public void Init(float damage, float maxHits, float pushOutDuration, int throwDirection)
    {
        this.maxHits = maxHits;
        this.throwDirection = throwDirection;

        base.Init(damage, pushOutDuration);
    }

    protected override void WeaponAttackFixedUpdate()
    {
        base.WeaponAttackFixedUpdate();
    }

    protected override void FixedUpdateCicle()
    {
        Vector3 nextPos = Vector3.up * speed;
        nextPos.x += xOffset * throwDirection;

        transform.Translate(nextPos * Time.fixedDeltaTime, Space.World);
        transform.rotation = Quaternion.Euler(currentRotation);
        currentRotation.z -= rotationAngle * throwDirection;

        if (speed >= 0)
        {
            speed -= 0.2f;
        }
        else
        {
            speedUpRotation = true;
            speed -= 0.3f;
        }


        if (rotationAngle > 0)
        {

        }
        if (!speedUpRotation)
        {
            rotationAngle -= 0.2f;
        }
        else
        {
            rotationAngle += 0.4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            CheckAttackedObjects(damagable);

            CheckCurrentHits();
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SpawnManager"))
        {
            Destroy(gameObject);
        }
    }
}
