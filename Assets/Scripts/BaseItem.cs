using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour
{
    public Sprite itemImage;
    public Sprite ItemCardActive;
    public Sprite ItemCardIdle;

    [SerializeField] protected GameObject baseItemPrefab;

    [SerializeField] protected GameObject player;

    public int CurrentLevel;

    public Image CurrentSlot;
    public int maxLevel;
    public bool AtMaxLevel;

    private void Start() => ItemStart();

    protected virtual void ItemStart()
    {

    }

    public virtual void LevelUp(int currentLevel, ref List<GameObject> currentItemsList)
    {
        if (currentLevel < maxLevel)
        {
            GameObject newLvlItem = Instantiate(baseItemPrefab, player.transform.position, baseItemPrefab.transform.rotation);
            newLvlItem.transform.parent = player.transform;
            newLvlItem.GetComponent<BaseItem>().SetStats(currentLevel + 1);
            newLvlItem.GetComponent<BaseItem>().InitItem();
            newLvlItem.name = baseItemPrefab.name;
            currentItemsList.Remove(gameObject);
            currentItemsList.Add(newLvlItem);
            Destroy(gameObject);
        }
        
    }

    public virtual string GetLevelDescription(int level)
    {
        return "TestString";
    }

    public virtual void SetStats(int level)
    {
        player = GameObject.FindWithTag("Player");
    }

    public virtual void InitItem()
    {

    }
}
