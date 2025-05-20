using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamagable, IStunable
{
    SpawnManager spawnManager;

    private Rigidbody2D rb;
    //[SerializeField] private float onDamageForce;
    [SerializeField] private float onDamageSpeed;
    [SerializeField] private float onAttackForce;

    [SerializeField] private bool isPushingOut;
    [SerializeField] private Vector3 pushOutTarget;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioClip getHurt;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private GameObject player;
    [SerializeField] private BaseCharacter playerCharacter;
    [SerializeField] private GameObject expGem;
    public bool alive { get; private set; }
    [SerializeField] private bool stunned;
    [SerializeField] private float currentStunDuration;
    [SerializeField] private float stunResist;
    private Color currentStunColor;
    private void Start() => EnemyStart();

    private void Update() => EnemyUpdate();
    private void FixedUpdate() => EnemyFixedUpdate();

    private void OnCollisionEnter2D(Collision2D collision) => EnemyOnCollisionEnter2D(collision);

    private void OnDestroy() => EnemyOnDestroy();




    protected virtual void EnemyStart()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerCharacter = player.GetComponent<BaseCharacter>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        stunResist = 0;
        currentStunColor = new Color(0, 0, 1);
        alive = true;

    }

    protected virtual void EnemyUpdate()
    {

    }

    protected virtual void EnemyFixedUpdate()
    {
        if(alive && !stunned)
        {
            if (isPushingOut)
            {
                Vector3 outOfTarget = (transform.position - pushOutTarget).normalized;
                transform.Translate(outOfTarget * onDamageSpeed * Time.fixedDeltaTime);
            }
            else
            {
                Vector3 toPlayer = (player.transform.position - transform.position).normalized;
                transform.Translate(toPlayer * speed * Time.fixedDeltaTime);
            }

            if (transform.position.x < player.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        
        
    }

    protected virtual void EnemyOnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !stunned)
        {
            DealDamage();
            StartCoroutine(PushOut(0.1f, player.transform.position));
        }
    }

    protected virtual void EnemyOnDestroy()
    {
        spawnManager.RemoveEnemyFromList(gameObject);
    }

    IEnumerator PushOut(float duration, Vector3 target)
    {
        pushOutTarget = target;
        isPushingOut = true;
        yield return new WaitForSeconds(duration);
        isPushingOut = false;

    }


    protected virtual void PlayHurtSound()
    {
        SFXManager.instance.PlayHurtSoundFXClip(gameObject.transform);
    }

    public virtual void GetDamage(float damage, float pushOutDuration, Vector3 target)
    {
        health -= damage;
        PlayHurtSound();

        if (stunned)
        {
            // В стане анимация не запускается - сразу проверяем смерть
            CheckOnDeath();
        }
        else
        {
            // Вызов проверки смерти происходит внутри анимации
            animator.SetTrigger("Take Hit");
        }
        
        PushOut(pushOutDuration, target);
        StartCoroutine(PushOut(pushOutDuration, target));
    }

    public virtual void GetStunned(float stunDuration)
    {
        if (currentStunDuration < stunDuration - (stunDuration * stunResist))
        {
            currentStunDuration = stunDuration - (stunDuration * stunResist);
            if (stunResist < 0.9f)
            {
                stunResist += 0.05f;
                if(stunResist - 0.65f > 0)
                {
                    currentStunColor = Color.Lerp(currentStunColor, Color.white, stunResist - 0.65f);
                }
                
                spriteRenderer.color = currentStunColor;
            }
        }
        
        if (!stunned)
            StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        stunned = true;

        spriteRenderer.color = currentStunColor;
        animator.speed = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            currentStunDuration -= 0.2f;
            if (currentStunDuration <= 0)
            {
                currentStunDuration = 0;
                animator.speed = 1;
                stunned = false;
                spriteRenderer.color = Color.white;
                break;
            }
        }
        
        
    }

    protected virtual void DealDamage()
    {
        playerCharacter.GetDamage(damage);
    }

    public void CheckOnDeath()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        Instantiate(expGem, transform.position, expGem.transform.rotation);
        Destroy(gameObject);
    }

    public static explicit operator BaseEnemy(GameObject v)
    {
        throw new NotImplementedException();
    }
}