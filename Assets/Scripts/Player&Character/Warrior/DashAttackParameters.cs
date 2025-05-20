using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackParameters : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float damage;
    [SerializeField] private float pushOutDuration;
    public List<GameObject> AttackedEnemy;

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
        {
            if (collision.gameObject.tag == "Enemy" && !AttackedEnemy.Contains(collision.gameObject))
            {
                AttackedEnemy.Add(collision.gameObject);
                damagable.GetDamage(damage, pushOutDuration, gameObject.transform.position);
            }
            else
            {
                damagable.GetDamage(damage, 0, gameObject.transform.position);
            }
        }
    }
}
