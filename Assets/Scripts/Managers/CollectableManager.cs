using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance { get; private set; }
    public Dictionary<Vector2Int, List<BaseCollectable>> Grid;
    [SerializeField] private GameObject player;
    [SerializeField] private float collectRadius;

    private bool toAttractGems;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Init(GameObject player, float radius)
    {
        this.player = player;
        Grid = new Dictionary<Vector2Int, List<BaseCollectable>>();
        SetCollectRadius(radius);
        toAttractGems = false;
        StartCoroutine(AttractCollectables());
    }

    IEnumerator AttractCollectables()
    {
        while(true)
        {
            if(toAttractGems)
            {
                AttractAllGems();
                toAttractGems = false;
            }
            int cellRange = Mathf.CeilToInt(collectRadius);
            Vector2Int playerCoordinates = WorldToCell(player.transform.position);
            List<BaseCollectable> collectablesToAttract = new List<BaseCollectable>();

            for (int i = -cellRange; i <= cellRange; i++)
            {
                for (int j = -cellRange; j <= cellRange; j++)
                {
                    Vector2Int gridToCheck = new Vector2Int(
                        playerCoordinates.x - i,
                        playerCoordinates.y - j);

                    if (Grid.TryGetValue(gridToCheck, out var collectableList))
                    {
                        foreach (var collectable in collectableList)
                        {
                            float distanceToPlayer = (collectable.transform.position - player.transform.position).sqrMagnitude;
                            if (distanceToPlayer <= collectRadius * collectRadius)
                            {
                                collectablesToAttract.Add(collectable);
                            }
                        }
                    }
                }
            }

            foreach(var collectable in collectablesToAttract)
            {
                collectable.CollectableEffect(player);
                UnregisterCollectable(collectable.coordinate, collectable);
            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public void ToAttractGems()
    {
        toAttractGems = true;
    }

    private void AttractAllGems()
    {
        List<ExpGem> gems = GemCounter.Instance.gems;
        foreach(var gem in gems)
        {
            gem.CollectableEffect(player);
            UnregisterCollectable(gem.coordinate, gem);
        }
    }

    public void SetCollectRadius(float radius)
    {
        collectRadius = radius;
    }

    public Vector2Int WorldToCell(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x),
            Mathf.FloorToInt(position.y)
            );
    }

    public Vector2Int RegisterCollectable(BaseCollectable collectable)
    {
        Vector2Int coordinates = WorldToCell(collectable.transform.position);
        if (!Grid.ContainsKey(coordinates))
        {
            Grid[coordinates] = new List<BaseCollectable>()
            {
                collectable
            };
        }
        else
        {
            Grid[coordinates].Add(collectable);
        }
        return coordinates;
    }

    public void UnregisterCollectable(Vector2Int coordinates, BaseCollectable collectable)
    {
        if (!Grid.ContainsKey(coordinates))
        {

        }
        else
        {
            Grid[coordinates].Remove(collectable);

            if (Grid[coordinates].Count == 0)
            {
                Grid.Remove(coordinates);
            }
        }
    }
}
