using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCharacter : BaseCharacter
{
    [SerializeField] private GameObject swordSwing;
    [SerializeField] private GameObject dashAttack;
    [SerializeField] private AudioClip swordSwingFirstSound;
    [SerializeField] private AudioClip swordSwingSecondSound;
    SwordSwingParameters swordSwingParameters;
    DashAttackParameters dashAttackParameters;

    protected override void CharacterStart()
    {
        base.CharacterStart();

        swordSwingParameters = swordSwing.GetComponent<SwordSwingParameters>();
        
        dashAttackParameters = dashAttack.GetComponent<DashAttackParameters>();
        

        StartCoroutine(OnStart());
    }

    IEnumerator OnStart()
    {
        yield return new WaitUntil(() => playerController.IsInitialized);
        swordSwingParameters.damage += GameData.Instance.WarriorBonusStats[0];
        dashAttackParameters.damage += GameData.Instance.WarriorBonusStats[1];
        playerController.abilityBaseCooldown -= GameData.Instance.WarriorBonusStats[2];
        yield break;
    }
    public override void UseAbility()
    {

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
        yield return new WaitForSeconds(0.165f);
        swordSwing.SetActive(false);

    }

    private IEnumerator DashAttack()
    {
        dashAttackParameters.AttackedEnemy.Clear();

        if (GetRotationSign() == 1)
        {
            Vector3 startRotation = transform.eulerAngles;

            //startRotation.y = 35f * GetRotationSign();
            dashAttack.transform.rotation = Quaternion.Euler(startRotation);
        }

        else
        {
            Vector3 startRotation = transform.eulerAngles;
            startRotation.y = 180;
            dashAttack.transform.rotation = Quaternion.Euler(startRotation);
        }

        dashAttack.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        dashAttack.SetActive(false);
    }

    public void StartSwing()
    {
        StartCoroutine(SwordSwing());
    }

    public void StartDashAttack()
    {
        StartCoroutine(DashAttack());
    }


    public void PlaySoundOne()
    {
        SFXManager.instance.PlaySoundFXClip(swordSwingFirstSound, transform, 0.07f);
    }

    public void PlaySoundTwo()
    {
        SFXManager.instance.PlaySoundFXClip(swordSwingSecondSound, transform, 0.07f);
    }


}
