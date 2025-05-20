using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb;
    public bool baseSpriteRightDirection;
    public Vector3 direction = new(1, 0, 0);
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    [SerializeField] private bool UsingAbility;
    private bool dead;

    public float abilityCurrentCooldown;
    public float abilityBaseCooldown;

    [SerializeField] private float speedUpCurrentDuration;
    [SerializeField] private bool isSpeedUp;
    [SerializeField] private GameObject speedUpEffect;

    public bool faceRight = true;

    public bool IsInitialized { get; private set; }

    [Header("UI - Ability")]
    [SerializeField] private Image abilityIcon;
    [SerializeField] private Image abilityCooldownMask;
    [SerializeField] private TextMeshProUGUI abilityCooldownText;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dead = false;

        abilityCurrentCooldown = 0;
        IsInitialized = true;
    }

    public void SetUsingAbilityTrue()
    {
        UsingAbility = true;
    }

    public void SetUsingAbilityFalse()
    {
        UsingAbility = false;
    }

    public void DisableAttackForOneFrame()
    {
        StartCoroutine(DisableAttack());

        IEnumerator DisableAttack()
        {
            UsingAbility = true;
            yield return null;
            UsingAbility = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !UsingAbility && !gameManager.IsGamePaused)
        {

            animator.SetTrigger("Attack");
            OnStartAbility();

        }

        if (Input.GetKeyDown(KeyCode.E) && !UsingAbility && !gameManager.IsGamePaused)
        {
            if (abilityCurrentCooldown <= 0)
            {
                animator.SetTrigger("Ability");
                OnStartAbility();
                abilityCurrentCooldown += abilityBaseCooldown;
                UIManager.Instance.AbilityCooldownController.StartAbilityCooldown(abilityCurrentCooldown);
            }
        }
        if (abilityCurrentCooldown > 0)
        {
            abilityCurrentCooldown -= Time.deltaTime;
            UIManager.Instance.AbilityCooldownController.UpdateAbilityCooldown(abilityCurrentCooldown);
        }
        
        if (abilityCurrentCooldown <= 0)
        {
            abilityCurrentCooldown = 0;
            UIManager.Instance.AbilityCooldownController.EndAbilityCooldown();
        }

    }

    private void FixedUpdate()
    {
        GetMoveInput();
    }

    private void OnStartAbility()
    {
        //animator.SetTrigger("Ability");
        UsingAbility = true;
    }

    public void OnEndAbility()
    {
        UsingAbility = false;
    }

    private void GetMoveInput()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput > 0 && !dead)
            {
                faceRight = baseSpriteRightDirection;
                ReflectPlayer(faceRight);
            }
            else if (horizontalInput < 0 && !dead)
            {
                faceRight = !baseSpriteRightDirection;
                ReflectPlayer(faceRight);
            }

            if (horizontalInput != 0 && verticalInput != 0)
            {
                horizontalInput /= 1.4f;
                verticalInput /= 1.4f;
            }

        Vector3 newPosition = new Vector3(horizontalInput, verticalInput, 0) * Convert.ToInt32(!dead);
        if (newPosition != Vector3.zero)
            direction = newPosition;

        transform.Translate(newPosition * speed * Time.fixedDeltaTime, Space.World);

/*        Vector3 newPosition = new Vector3(horizontalInput, verticalInput) * speed;
        //rb.MovePosition(newPosition * speed * Time.fixedDeltaTime);
        rb.AddForce(newPosition, ForceMode2D.Impulse);*/


    }

    private void ReflectPlayer(bool faceRight)
    {
        spriteRenderer.flipX = !faceRight;
    }

    public void Death()
    {
        if (dead) { return; }
            
        speedUpCurrentDuration = 0;
        animator.SetBool("Dead", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (!dead)
        {
            gameManager.GameOver();
        }
        dead = true;
    }

    public void SpeedUp(float duration, float speedUpMultiplier)
    {
        if (isSpeedUp)
        {
            speedUpCurrentDuration = duration;
        }
        else
        {
            StartCoroutine(StartSpeedUp(duration, speedUpMultiplier));
        }
    }

    private IEnumerator StartSpeedUp(float duration, float speedUpMultiplier)
    {
        speedUpCurrentDuration = duration;

        isSpeedUp = true;
        animator.speed *= speedUpMultiplier;
        changeMoveSpeed(speedUpMultiplier);
        abilityBaseCooldown /= speedUpMultiplier;
        speedUpEffect.SetActive(true);

        while (speedUpCurrentDuration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            speedUpCurrentDuration -= 0.1f;
        }

        animator.speed /= speedUpMultiplier;
        changeMoveSpeed(1 / speedUpMultiplier);
        abilityBaseCooldown *= speedUpMultiplier;
        isSpeedUp = false;
        speedUpEffect.SetActive(false);

    }

    public void changeMoveSpeed(float changeMultiplier)
    {
        speed *= changeMultiplier;
    }
}
