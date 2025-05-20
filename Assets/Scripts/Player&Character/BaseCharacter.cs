using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BaseCharacter : MonoBehaviour
{
    public Sprite AbilityIcon;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private LevelUp levelUp;
    [SerializeField] private MapManager mapManager;
    [SerializeField] protected PlayerController playerController;
    public UnityEngine.UI.Image HealthFillImage;

    [SerializeField] protected Animator animator;

    [SerializeField] private CircleCollider2D collectCollider;
    [SerializeField] private Vector2 collectColliderOffset;
    [SerializeField] public float collectColliderRadius;

    [SerializeField] private int currentLevel;
    [SerializeField] private int currentExp;
    [SerializeField] private int expTillNexlLevel;

    public float Health;
    public int Revive;

    [Header("Max Health")]
    public float MaxHealthTotal;
    [SerializeField] private float characterMaxHealth;

    [Header("Regeneration")]
    public float RegenerationDelayTotal;
    [SerializeField] private float characterRegenerationDelay;
    [SerializeField] private float currentRegenerationDelay;

    [Header("Armor")]
    public float ArmorTotal;
    [SerializeField] private float characterArmor;

    [Header("Strenght")]
    public float StrenghtTotal;
    [SerializeField] private float characterStrenght;

    [Header("Duration")]
    public float DurationTotal;
    [SerializeField] private float characterDuration;

    [Header("Amount")]
    public int AmountTotal;
    [SerializeField] private int characterAmount;

    [Header("Cooldown")]
    public float CooldownTotal;
    [SerializeField] private float characterCooldown;

    [Header("Luck")]
    public float LuckTotal;
    [SerializeField] private float characterLuck;

    [Header("Revive")]
    public int MaxReviveTotal;
    [SerializeField] private int characterMaxRevive;

    private void Start() => CharacterStart();

    private void FixedUpdate()
    {
        currentRegenerationDelay -= Time.fixedDeltaTime;
        if (currentRegenerationDelay <= 0)
        {
            currentRegenerationDelay = RegenerationDelayTotal;
            if (Health < MaxHealthTotal)
            {
                Health += 1;
                UIManager.Instance.HealthBarController.UpdateHealthBar(MaxHealthTotal, Health);
                
            }
            
        }
    }

    protected virtual void CharacterFixedUpdate()
    {

    }

    protected virtual void CharacterStart()
    {
        UIManager.Instance.HealthBarController.SetCharacterHealthFill(HealthFillImage);
        playerController = GetComponent<PlayerController>();
        animator = playerController.animator;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        levelUp = GameObject.Find("ItemManager").GetComponent<LevelUp>();
        collectCollider = gameObject.AddComponent<CircleCollider2D>();
        collectCollider.offset = collectColliderOffset;
        collectCollider.radius = collectColliderRadius;
        collectCollider.isTrigger = true;

        //transform.position = new Vector2(mapManager.width / 2, mapManager.height / 2);
        transform.position = Vector2.zero;


        MaxHealthTotal           = characterMaxHealth;
        

        RegenerationDelayTotal   = characterRegenerationDelay;
        currentRegenerationDelay = RegenerationDelayTotal;
        

        ArmorTotal               = characterArmor;
        

        StrenghtTotal            = characterStrenght;
        

        DurationTotal            = characterDuration;
        

        AmountTotal              = characterAmount;
        

        CooldownTotal            = characterCooldown;


        try
        {
            MaxHealthTotal += GameData.Instance.GeneralBonusStats[0];
            currentRegenerationDelay -= GameData.Instance.GeneralBonusStats[1];
            ArmorTotal += GameData.Instance.GeneralBonusStats[2];
            StrenghtTotal += GameData.Instance.GeneralBonusStats[3];
            DurationTotal += GameData.Instance.GeneralBonusStats[4];
            AmountTotal += Convert.ToInt32(GameData.Instance.GeneralBonusStats[5]);
            CooldownTotal -= GameData.Instance.GeneralBonusStats[6];
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
        

        if (CooldownTotal <= 0.1f)
        {
            CooldownTotal = 0.1f;
        }

        LuckTotal                = characterLuck;

        MaxReviveTotal           = characterMaxRevive;

        Health                   = MaxHealthTotal;
        UIManager.Instance.HealthBarController.UpdateHealthBar(MaxHealthTotal, Health);
        UIManager.Instance.AbilityCooldownController.SetAbilityIcon(AbilityIcon);
    }

    public void AddExp(int amount)
    {

        currentExp += amount;

        if(currentExp >= expTillNexlLevel)
        {

            currentExp -= expTillNexlLevel;
            LevelUp();
        }

        UIManager.Instance.ExpBarController.UpdateExpBar(expTillNexlLevel, currentExp, currentLevel);
    }

    private void LevelUp()
    {
        currentLevel++;
        //currentExp = 0;
        expTillNexlLevel += 10;
        levelUp.OnCharacterLevelUp();
    }

    public virtual void UseAbility()
    {

    }

    public virtual void GetDamage(float amount)
    {
        float damage = amount - ArmorTotal;
        if (damage <= 1)
        {
            damage = 1;
        }
        Health -= damage;
        UIManager.Instance.HealthBarController.UpdateHealthBar(MaxHealthTotal, Health);
        UIManager.Instance.HealthBarController.UpdateCharacterHealthBar(MaxHealthTotal, Health);

        if (Health <= 0)
        {
            StartDeath();
        }
    }

    public virtual void GetHealing(float amount)
    {
        Health += amount;
        if (Health >= MaxHealthTotal)
        {
            Health = MaxHealthTotal;
        }
        UIManager.Instance.HealthBarController.UpdateHealthBar(MaxHealthTotal, Health);
        UIManager.Instance.HealthBarController.UpdateCharacterHealthBar(MaxHealthTotal, Health);
    }

    protected virtual void StartDeath()
    {
        Debug.Log("Death");
        playerController.Death();
    }

    public void UpdateCollectCollider(float multiplier)
    {
        collectColliderRadius *= multiplier;
        collectCollider.radius = collectColliderRadius;

        CollectableManager.Instance.SetCollectRadius(GetCollectRadius());
    }

    protected int GetRotationSign()
    {
        int rotationSign;

        if (playerController.faceRight)
        {
            rotationSign = 1;
        }
        else
        {
            rotationSign = -1;
        }

        if (!playerController.baseSpriteRightDirection)
            rotationSign *= -1;

        return rotationSign;
    }

    public float GetCollectRadius()
    {
        return collectColliderRadius * gameObject.transform.localScale.x;
    }



}
