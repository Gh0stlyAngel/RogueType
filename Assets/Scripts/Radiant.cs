using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Radiant : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    [SerializeField] private BaseCollectable foodPrefab;
    [SerializeField] private BaseCollectable speedUpPrefab;
    [SerializeField] private BaseCollectable magnetCollectablePrefab;
    [SerializeField] private AudioClip getHurt;

    //[SerializeField] private List<GameObject> collectablePrefabs = new List<GameObject>();
    [SerializeField] private Dictionary<BaseCollectable, float> collectableWeights = new Dictionary<BaseCollectable, float>();
    void Start()
    {
        collectableWeights.Add(speedUpPrefab, speedUpPrefab.Weight);
        collectableWeights.Add(foodPrefab, foodPrefab.Weight);
        collectableWeights.Add(magnetCollectablePrefab, magnetCollectablePrefab.Weight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage, float pushOutDuration, Vector3 target)
    {
        health -= damage;
        SFXManager.instance.PlaySoundFXClip(getHurt, gameObject.transform, 0.05f);
        if (health <= 0)
        {
            BaseCollectable randomItem = SpawnRandomItem();
            if (randomItem != null)
            {
                Instantiate(randomItem, gameObject.transform.position, randomItem.transform.rotation);
            }
            else
            {
                Instantiate(foodPrefab, gameObject.transform.position, foodPrefab.transform.rotation);
            }
            
            Destroy(gameObject);
        }
    }

    private BaseCollectable SpawnRandomItem()
    {
        float totalWeight = 0;
        foreach (var item in collectableWeights)
        {
            totalWeight += item.Value;
        }
        float cumulative = 0;
        float randomValue = Random.Range(0, totalWeight);

        Debug.Log("randomValue: " + randomValue);

        foreach (var item in collectableWeights)
        {
            cumulative += item.Value;

            if(randomValue <= cumulative)
            {
                return item.Key;
            }
        }
        return null;
    }
}
