using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GemCounter : MonoBehaviour
{
    public static GemCounter Instance { get; private set; }

    public TextMeshProUGUI gemCounter;

    public List<ExpGem> gems = new List<ExpGem>();

    [SerializeField] GameObject player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } 
        Instance = this;
    }

    public void Init(GameObject player)
    {
        this.player = player;
    }

    public void AddGem(ExpGem gem)
    {
        gems.Add(gem);

        gemCounter.text = gems.Count.ToString();
    }

    public void RemoveGem(ExpGem gem = null)
    {
        if (gem != null)
        {
            gems.Remove(gem);
            Destroy(gem);
        }
        else
        {
            ExpGem gemToDestroy = gems[0];
            gems.Remove(gemToDestroy);
            Destroy(gemToDestroy);
        }
        
        gemCounter.text = gems.Count.ToString();
    }

   
}
