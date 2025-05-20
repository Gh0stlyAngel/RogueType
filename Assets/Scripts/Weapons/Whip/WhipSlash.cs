using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WhipSlash : BaseWeaponAttack
{
    private Vector3 playerPos;
    private Vector3 targetScale;
    private float scaleStep;
    private float fadeoutStep;
    private SpriteRenderer spriteRenderer;
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
        if (transform.localScale.x < targetScale.x)
        {
            if (transform.localScale.x * scaleStep > targetScale.x)
            {
                transform.localScale = targetScale;
            }
            else
            {
                transform.localScale *= scaleStep;
            }
        }
        else
        {
            Color newColor = spriteRenderer.color;
            newColor.a -= fadeoutStep;
            spriteRenderer.color = newColor;

            if(spriteRenderer.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Init(float damage, float pushOutDuration, GameObject player, float startScale)
    {
        playerPos = player.transform.position;

        Vector3 newScale = transform.localScale;
        newScale *= startScale;
        transform.localScale = newScale;

        targetScale = newScale * 3;

        scaleStep = 1.35f;
        fadeoutStep = 0.22f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        base.Init(damage, pushOutDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            CheckAttackedObjects(damagable);
        }

    }

}
