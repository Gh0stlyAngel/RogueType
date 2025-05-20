using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Canvas UI;
    [SerializeField] GameObject devMenu;
    private bool IsDevOpened;
    private GameManager gameManager;

    private GameObject player;
    [Header("Weapons")]
    [SerializeField] private GameObject axeWeaponObject;
    [SerializeField] private GameObject bookWeaponObject;
    [SerializeField] private GameObject magicWandWeaponObject;
    [SerializeField] private GameObject daggerWeaponObject;
    [SerializeField] private GameObject IceStaffWeaponObject;
    [SerializeField] private GameObject WhipWeaponObject;

    [Header("Passive items")]
    [SerializeField] private GameObject duplicatorObject;
    [SerializeField] private GameObject hourglassObject;
    [SerializeField] private GameObject cuirassObject;
    [SerializeField] private GameObject kettlebellObject;
    [SerializeField] private GameObject amuletObject;
    [SerializeField] private GameObject magnetObject;

    public List<GameObject> possibleWeapons;
    public List<GameObject> possiblePassiveItems;

    public void Init(GameObject player, GameManager gameManager)
    {
        this.player = player;
        devMenu = UI.transform.Find("DevMenu").gameObject;
        this.gameManager = gameManager;

        possibleWeapons.Add(axeWeaponObject);
        possibleWeapons.Add(bookWeaponObject);
        possibleWeapons.Add(magicWandWeaponObject);
        possibleWeapons.Add(daggerWeaponObject);
        possibleWeapons.Add(IceStaffWeaponObject);
        possibleWeapons.Add(WhipWeaponObject);

        possiblePassiveItems.Add(duplicatorObject);
        possiblePassiveItems.Add(hourglassObject);
        possiblePassiveItems.Add(cuirassObject);
        possiblePassiveItems.Add(kettlebellObject);
        possiblePassiveItems.Add(amuletObject);
        possiblePassiveItems.Add(magnetObject);
    }

    public void DevMenuButton()
    {
        if (IsDevOpened)
        {
            gameManager.ResumeGame();
            devMenu.SetActive(false);
            IsDevOpened = false;
        }

        else
        {
            gameManager.PauseGame();
            devMenu.SetActive(true);
            IsDevOpened = true;
        }
    }

    public void AddLevelSlots(BaseItem baseItem)
    {
        float baseYOffset = 10f;

        if (baseItem.maxLevel <= 5)
        {

            for (int i = 0; i < baseItem.maxLevel; i++)
            {
                GameObject itemLevel = Instantiate(gameManager.levelMarker);
                itemLevel.transform.SetParent(baseItem.CurrentSlot.transform, false);
                itemLevel.name = gameManager.levelMarker.name + (i + 1);

                RectTransform rTransform = itemLevel.GetComponent<RectTransform>();
                float xOffset;
                if (baseItem.maxLevel % 2 == 1)
                    xOffset = 15 * (Mathf.Floor(baseItem.maxLevel / 2) - i);
                else
                    xOffset = 15 * (Mathf.Floor(baseItem.maxLevel / 2) - 1 - i) + 7.5f;

                rTransform.localPosition -= new Vector3(xOffset, baseYOffset, 0);
            }

        }
        else if (baseItem.maxLevel <= 10)
        {
            int maxRowLenght = 5;
            int currentLevel = 0;
            for (int i = 0; i < maxRowLenght; i++)
            {
                GameObject itemLevel = Instantiate(gameManager.levelMarker);
                itemLevel.transform.SetParent(baseItem.CurrentSlot.transform, false);
                itemLevel.name = gameManager.levelMarker.name + (i + 1);

                RectTransform rTransform = itemLevel.GetComponent<RectTransform>();
                float xOffset = 15 * (Mathf.Floor(maxRowLenght / 2) - i);
                rTransform.localPosition -= new Vector3(xOffset, baseYOffset, 0);

                currentLevel++;
            }

            for (int i = 0; i < baseItem.maxLevel - currentLevel; i++)
            {
                GameObject itemLevel = Instantiate(gameManager.levelMarker);
                itemLevel.transform.SetParent(baseItem.CurrentSlot.transform, false);
                itemLevel.name = gameManager.levelMarker.name + (i + 1 + maxRowLenght);

                RectTransform rTransform = itemLevel.GetComponent<RectTransform>();

                float xOffset;
                if ((baseItem.maxLevel - currentLevel) % 2 == 1)
                    xOffset = 15 * (Mathf.Floor((baseItem.maxLevel - currentLevel) / 2) - i);
                else
                    xOffset = 15 * (Mathf.Floor((baseItem.maxLevel - currentLevel) / 2) - 1 - i) + 7.5f;

                float yOffset = 15;
                rTransform.localPosition -= new Vector3(xOffset, yOffset + baseYOffset, 0);
            }
        }

    }

    public void SetLevelSlots(BaseItem item, int level)
    {
        for (int i = 0; i < level; i++)
        {
            foreach (Transform itemTransform in item.CurrentSlot.transform)
            {
                if (itemTransform.name.StartsWith(gameManager.levelMarker.name + (i + 1)))
                {
                    foreach (Transform fill in itemTransform)
                    {
                        fill.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    public void AddPassiveItem(GameObject item)
    {
        BaseItem baseItem;
        GameObject _baseItemObject;
        if (gameManager.PassiveList.Count < gameManager.MaxPassiveSlots)
        {
            _baseItemObject = Instantiate(item, player.transform.position, item.transform.rotation);
            _baseItemObject.name = item.name;
            _baseItemObject.transform.parent = player.transform;

            baseItem = _baseItemObject.GetComponent<BaseItem>();
            baseItem.SetStats(1);
            baseItem.InitItem();

            switch (gameManager.PassiveList.Count)
            {
                case 0:
                    gameManager.PassiveSlot1.sprite = baseItem.itemImage;
                    baseItem.CurrentSlot = gameManager.PassiveSlot1;
                    break;
                case 1:
                    gameManager.PassiveSlot2.sprite = baseItem.itemImage;
                    baseItem.CurrentSlot = gameManager.PassiveSlot2;
                    break;
                case 2:
                    gameManager.PassiveSlot3.sprite = baseItem.itemImage;
                    baseItem.CurrentSlot = gameManager.PassiveSlot3;
                    break;

            }

            AddLevelSlots(baseItem);
            SetLevelSlots(baseItem, baseItem.CurrentLevel);

            gameManager.PassiveList.Add(_baseItemObject);
            possiblePassiveItems.Remove(item);
            if(!IsDevOpened)
            {
                gameManager.ResumeGame();
            }
            gameManager.levelUpPanel.SetActive(false);

            player.GetComponent<BaseCharacter>().AddExp(0);
        }
    }
    public void AddWeapon(GameObject item)
    {
        BaseItem baseItem;
        GameObject _baseItemObject;
        if (gameManager.WeaponList.Count < gameManager.MaxWeaponSlots)
        {
            _baseItemObject = Instantiate(item, player.transform.position, item.transform.rotation);
            _baseItemObject.name = item.name;
            _baseItemObject.transform.parent = player.transform;
            


            baseItem = _baseItemObject.GetComponent<BaseItem>();
            baseItem.SetStats(1);
            baseItem.InitItem();

            switch (gameManager.WeaponList.Count)
            {
                case 0:
                    gameManager.WeaponSlot1.sprite = baseItem.itemImage;
                    baseItem.CurrentSlot = gameManager.WeaponSlot1;
                    break;
                case 1:
                    gameManager.WeaponSlot2.sprite = baseItem.itemImage;
                    baseItem.CurrentSlot = gameManager.WeaponSlot2;
                    break;     
                case 2:
                    gameManager.WeaponSlot3.sprite = baseItem.itemImage;
                    baseItem.CurrentSlot = gameManager.WeaponSlot3;
                    break;
            }

            AddLevelSlots(baseItem);
            SetLevelSlots(baseItem, baseItem.CurrentLevel);

            gameManager.WeaponList.Add(_baseItemObject);
            possibleWeapons.Remove(item);

            if (!IsDevOpened)
            {
                gameManager.ResumeGame();
            }
            gameManager.levelUpPanel.SetActive(false);

            player.GetComponent<BaseCharacter>().AddExp(0);
        }
        
    }

    public void BaseItemLvlUp(string itemName)
    {
        foreach (Transform item in player.transform)
        {
            if (item.gameObject.name.StartsWith(itemName))
            {
                BaseItem testedItem = item.gameObject.GetComponent<BaseItem>();
                int nextLevel = testedItem.CurrentLevel + 1;
                Type itemType = testedItem.GetType();
                if (itemType.IsSubclassOf(typeof(BasePassiveItem)))
                {
                    testedItem.LevelUp(testedItem.CurrentLevel, ref gameManager.PassiveList);
                }
                else
                {
                    testedItem.LevelUp(testedItem.CurrentLevel, ref gameManager.WeaponList);
                }
                SetLevelSlots(testedItem, nextLevel);
                break;
            }
        }

        if (!IsDevOpened)
        {
            gameManager.ResumeGame();
        }
        gameManager.levelUpPanel.SetActive(false);

        player.GetComponent<BaseCharacter>().AddExp(0);
    }

    public void AddAxe()
    {
        AddWeapon(axeWeaponObject);
    }

    public void AxelvlUp()
    {
        BaseItemLvlUp(axeWeaponObject.name);
    }

    public void AddBook()
    {
        AddWeapon(bookWeaponObject);
    }

    public void BookLvlUp()
    {
        BaseItemLvlUp(bookWeaponObject.name);
    }

    public void AddMagicWand()
    {
        AddWeapon(magicWandWeaponObject);
    }

    public void WandLvlUp()
    {
        BaseItemLvlUp(magicWandWeaponObject.name);
    }

    public void AddDagger()
    {
        AddWeapon(daggerWeaponObject);
    }

    public void DaggerLvlUp()
    {
        BaseItemLvlUp(daggerWeaponObject.name);
    }

    public void AddIceStaff()
    {
        AddWeapon(IceStaffWeaponObject);
    }

    public void IceStaffLvlUp()
    {
        BaseItemLvlUp(IceStaffWeaponObject.name);
    }

    public void AddWhip()
    {
        AddWeapon(WhipWeaponObject);
    }

    public void WhipLvlUp()
    {
        BaseItemLvlUp(WhipWeaponObject.name);
    }

    ///////////////////
    // Passive Items //
    ///////////////////

    public void AddDuplicator()
    {
        AddPassiveItem(duplicatorObject);
    }

    public void DuplicatorLvlUp()
    {
        BaseItemLvlUp(duplicatorObject.name);
    }

    public void AddHourglass()
    {
        AddPassiveItem(hourglassObject);   
    }

    public void HourglassLvlUp()
    {
        BaseItemLvlUp(hourglassObject.name);
    }

    public void AddCuirass()
    {
        AddPassiveItem(cuirassObject);
    }

    public void CuirassLvlUp()
    {
        BaseItemLvlUp(cuirassObject.name);
    }

    public void AddKettlebell()
    {
        AddPassiveItem(kettlebellObject);
    }

    public void KettlebellLvlUp()
    {
        BaseItemLvlUp(kettlebellObject.name);
    }

    public void AddAmulet()
    {
        AddPassiveItem(amuletObject);
    }

    public void AmuletLvlUp()
    {
        BaseItemLvlUp(amuletObject.name);
    }

    public void AddMagnet()
    {
        AddPassiveItem(magnetObject);
    }

    public void MagnetLvlUp()
    {
        BaseItemLvlUp(magnetObject.name);
    }
}
