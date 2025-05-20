using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCharacter: BaseCharacter
{
    [SerializeField] private bool isBlocking;
    [SerializeField] private float blockDuration;
    [SerializeField] private float blockSlowMultiplier;
    [SerializeField] private float blockDamageMultiplier;


    [SerializeField] private GameObject swordSwing;
    [SerializeField] private AudioClip swordSwingSound;
    private SwordSwingParameters swordSwingParameters;

    private Coroutine blockingRoutine;

    protected override void CharacterStart()
    {
        base.CharacterStart();

        swordSwingParameters = swordSwing.GetComponent<SwordSwingParameters>();

        StartCoroutine(OnStart());
    }

    IEnumerator OnStart()
    {
        blockSlowMultiplier = 1.5f;
        blockDamageMultiplier = .33f;
        blockDuration = 7f;
        playerController.abilityBaseCooldown = 15f;

        yield return new WaitUntil(() => playerController.IsInitialized);
        swordSwingParameters.damage += GameData.Instance.KnightBonusStats[0];
        blockDuration += GameData.Instance.KnightBonusStats[1];
        playerController.abilityBaseCooldown -= GameData.Instance.KnightBonusStats[2];
        yield break;
    }

    private IEnumerator SwordSwing()
    {
        swordSwingParameters.AttackedEnemy.Clear();

        if (GetRotationSign() == 1)
        {
            Vector3 startRotation = transform.eulerAngles;

            //startRotation.y = 35f * GetRotationSign();
            swordSwing.transform.rotation = Quaternion.Euler(startRotation);
        }

        else
        {
            Vector3 startRotation = transform.eulerAngles;
            startRotation.y = 180;
            swordSwing.transform.rotation = Quaternion.Euler(startRotation);
        }

        swordSwing.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        swordSwing.SetActive(false);

    }
    public void StartSwing()
    {
        if (isBlocking)
        {
            StopBlocking();
            animator.ResetTrigger("Block");
        }
        StartCoroutine(SwordSwing());
    }

    private IEnumerator Block()
    {
        isBlocking = true;
        playerController.changeMoveSpeed(1 / blockSlowMultiplier);

        yield return new WaitForSeconds(blockDuration);

        if (isBlocking)
        {
            animator.SetTrigger("EndBlock");
            StopBlocking();
        }
        
    }


    public void StartBlocking()
    {
        if (!isBlocking)
        {
            blockingRoutine = StartCoroutine(Block());
        }
    }
    public void StopBlocking()
    {
        if (isBlocking)
        {
            isBlocking = false;    
            playerController.changeMoveSpeed(blockSlowMultiplier);
            StopCoroutine(blockingRoutine);
        }
    }


    public void PlaySound()
    {
        SFXManager.instance.PlaySoundFXClip(swordSwingSound, transform, 0.07f);
    }

    public override void GetDamage(float amount)
    {
        if (isBlocking)
        {
            amount *= Mathf.FloorToInt(blockDamageMultiplier);
            animator.SetTrigger("Block");
        }

        base.GetDamage(amount);
    }
}
