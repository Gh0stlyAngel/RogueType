using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperCharacter : BaseCharacter
{
    [SerializeField] private GameObject reapingPit;
    [SerializeField] private GameObject reapingSlash;

    [SerializeField] private AudioClip slashSound;
    [SerializeField] private AudioClip pitSound;

    private ReapingSlashParameters reapingSlashParameters;

    public void SpawnPit()
    {
        Vector3 pitPos = new Vector3(transform.position.x, transform.position.y, -10);
        Instantiate(reapingPit, pitPos, reapingPit.transform.rotation);
    }

    protected override void CharacterStart()
    {
        base.CharacterStart();

        


        StartCoroutine(OnStart());
    }

    IEnumerator OnStart()
    {
        yield return new WaitUntil(() => playerController.IsInitialized);

        reapingSlashParameters = reapingSlash.GetComponent<ReapingSlashParameters>();
        ReapingPit reapeingPitParameters = reapingPit.transform.GetChild(3).GetComponent<ReapingPit>();

        reapingSlashParameters.damage += GameData.Instance.ReaperBonusStats[0];
        reapeingPitParameters.damage = 9;
        reapeingPitParameters.lifetime = 4.5f;

        reapeingPitParameters.damage += GameData.Instance.ReaperBonusStats[1];
        reapeingPitParameters.lifetime += GameData.Instance.ReaperBonusStats[2];

        yield break;
    }

    private IEnumerator ReapingSlash()
    {
        reapingSlashParameters.AttackedEnemy.Clear();
        if (GetRotationSign() == 1)
        {
            Vector3 startRotation = transform.eulerAngles;

            //startRotation.y = 35f * GetRotationSign();
            reapingSlash.transform.rotation = Quaternion.Euler(startRotation);
        }

        else
        {
            Vector3 startRotation = transform.eulerAngles;
            startRotation.y = 180;
            reapingSlash.transform.rotation = Quaternion.Euler(startRotation);
        }

        reapingSlash.SetActive(true);
        yield return new WaitForSeconds(0.350f);
        reapingSlash.SetActive(false);

    }

    public void StartSlash()
    {
        StartCoroutine(ReapingSlash());
    }

    public void PlaySlashSound()
    {
        SFXManager.instance.PlaySoundFXClip(slashSound, transform, 0.1f);
    }

    public void PlayPitSound()
    {
        SFXManager.instance.PlaySoundFXClip(pitSound, transform, 0.1f);
    }
}
