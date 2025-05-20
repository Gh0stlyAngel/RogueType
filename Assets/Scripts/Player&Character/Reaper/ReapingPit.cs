using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapingPit : MonoBehaviour
{
    public float lifetime;
    public float damage;
    [SerializeField] private float damageDelay;
    public List<GameObject> AttackedEnemy;
    [SerializeField] private GameObject reapingPit;
    [SerializeField] private ParticleSystem reapingPitStars;
    [SerializeField] private ParticleSystem reapingPitRays;
    [SerializeField] private ParticleSystem reapingPitRunes;
    [SerializeField] private ParticleSystem reapingPitRunesSmall;
    


    private void Start()
    {
        Destroy(reapingPit, lifetime);
        StartCoroutine(FadingOut());
        StartCoroutine(ClearAttackedList());
    }

    IEnumerator FadingOut()
    {
        yield return new WaitForSeconds(lifetime - 1f);

        var emission = reapingPitStars.emission;
        emission.enabled = false;
        emission = reapingPitRays.emission;
        emission.enabled = false;
        emission = reapingPitRunes.emission;
        emission.enabled = false;
        emission = reapingPitRunesSmall.emission;
        emission.enabled = false;

    }

    IEnumerator ClearAttackedList()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageDelay);
            AttackedEnemy.Clear();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            if (!AttackedEnemy.Contains(collision.gameObject))
            {
                AttackedEnemy.Add(collision.gameObject);
                damagable.GetDamage(damage, 0, gameObject.transform.position);
            }
        }
    }
}
