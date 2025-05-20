using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReapingSlashParameters : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float damage;
    [SerializeField] private float pushOutDuration;

    public List<GameObject> AttackedEnemy;

    private void Start()
    {
        AttackedEnemy = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable) && !AttackedEnemy.Contains(collision.gameObject))
        {
            if (collision.gameObject.tag == "Enemy")
            {
                damagable.GetDamage(damage, pushOutDuration, gameObject.transform.position);
                AttackedEnemy.Add(collision.gameObject);
            }
            else
            {
                damagable.GetDamage(damage, 0, gameObject.transform.position);
            }
        }
    }
}
