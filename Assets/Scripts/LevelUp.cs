using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    private ItemManager itemManager;
    private GameManager gameManager;

    private LevelUpPanelController levelUpPanelController;

    private void Start()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelUpPanelController = UIManager.Instance.LevelUpPanelController;
    }

    private bool IsWeaponsUpgraded()
    {
        int amountOfUpgradedItems = gameManager.MaxWeaponSlots;
        if (gameManager.WeaponList.Count >= gameManager.MaxWeaponSlots)
        {
            foreach (GameObject weapon in gameManager.WeaponList)
            {
                if (weapon.GetComponent<BaseItem>().AtMaxLevel)
                {
                    amountOfUpgradedItems--;
                }
            }
        }
        if (amountOfUpgradedItems <= 0) { return true; }

        return false;
    }

    private bool isPassiveItemsUpgraded()
    {
        int amountOfUpgradedItems = gameManager.MaxPassiveSlots;
        if (gameManager.PassiveList.Count >= gameManager.MaxPassiveSlots)
        {
            foreach (GameObject passiveItem in gameManager.PassiveList)
            {
                if (passiveItem.GetComponent<BaseItem>().AtMaxLevel)
                {
                    amountOfUpgradedItems--;
                }
            }
        }
        if (amountOfUpgradedItems <= 0) { return true; }

        return false;
    }

    private void passiveOrActive(ref List<GameObject> passive, ref List<GameObject> active, Transform button, bool isOwned, int buttonIndex)
    {
        GameObject randomItem;
        if (passive.Count == 0 && active.Count == 0)
        {
            button.gameObject.SetActive(false);
        }
        else if (IsWeaponsUpgraded() || active.Count == 0)
        {
            randomItem = passive[UnityEngine.Random.Range(0, passive.Count)];
            Action onClick;
            if (isOwned)
            {
                onClick = () => itemManager.BaseItemLvlUp(randomItem.name);
            }
            else
            {
                onClick = () => itemManager.AddPassiveItem(randomItem);
            }
            levelUpPanelController.SetupButton(buttonIndex, randomItem.GetComponent<BaseItem>(), isOwned, onClick);
            passive.Remove(randomItem);


        }
        else if (isPassiveItemsUpgraded() || passive.Count == 0)
        {
            randomItem = active[UnityEngine.Random.Range(0, active.Count)];
                Action onClick;
                if (isOwned)
                {
                    onClick = () => itemManager.BaseItemLvlUp(randomItem.name);
                }
                else
                {
                    onClick = () => itemManager.AddWeapon(randomItem);
                }
                levelUpPanelController.SetupButton(buttonIndex, randomItem.GetComponent<BaseItem>(), isOwned, onClick);
                active.Remove(randomItem);
        }
        else
        {
            if (UnityEngine.Random.Range(0, 2) == 0) // Passive items
            {
                randomItem = passive[UnityEngine.Random.Range(0, passive.Count)];
                Action onClick;
                if (isOwned)
                {
                    onClick = () => itemManager.BaseItemLvlUp(randomItem.name);
                }
                else
                {
                    onClick = () => itemManager.AddPassiveItem(randomItem);
                }
                levelUpPanelController.SetupButton(buttonIndex, randomItem.GetComponent<BaseItem>(), isOwned, onClick);
                passive.Remove(randomItem);
            }
            else  // Active items
            {
                randomItem = active[UnityEngine.Random.Range(0, active.Count)];
                Action onClick;
                if (isOwned)
                {
                    onClick = () => itemManager.BaseItemLvlUp(randomItem.name);
                }
                else
                {
                    onClick = () => itemManager.AddWeapon(randomItem);
                }
                levelUpPanelController.SetupButton(buttonIndex, randomItem.GetComponent<BaseItem>(), isOwned, onClick);
                active.Remove(randomItem);
            }
        }
    }

    public void OnCharacterLevelUp()
    {

        if (IsWeaponsUpgraded() && isPassiveItemsUpgraded())
        {

        }

        else
        {

            List<GameObject> tempCurrentWeapons = new();
            List<GameObject> tempCurrentPassiveItems = new();
            List<GameObject> tempPossibleWeapons = new(itemManager.possibleWeapons);
            List<GameObject> tempPossiblePassiveItems = new(itemManager.possiblePassiveItems);

            foreach (GameObject weapon in gameManager.WeaponList)
            {
                if (!weapon.GetComponent<BaseItem>().AtMaxLevel)
                {
                    tempCurrentWeapons.Add(weapon);
                }
            }

            foreach (GameObject passiveItem in gameManager.PassiveList)
            {
                if (!passiveItem.GetComponent<BaseItem>().AtMaxLevel)
                {
                    tempCurrentPassiveItems.Add(passiveItem);
                }
            }

            gameManager.PauseGame();
            levelUpPanelController.ClearButtons();
            //ClearButtonsDelegate();
            levelUpPanelController.ShowPanel();

            for (int i = 0; i < 3; i++)
            {
                foreach (Transform button in gameManager.levelUpPanel.transform)
                {
                    if (button.gameObject.name == $"Item{i + 1}")
                    {
                        if (gameManager.PassiveList.Count >= gameManager.MaxPassiveSlots)
                        {
                            tempPossiblePassiveItems.Clear();
                        }
                        if (gameManager.WeaponList.Count >= gameManager.MaxWeaponSlots)
                        {
                            tempPossibleWeapons.Clear();
                        }


                        if (gameManager.PassiveList.Count == 0 && gameManager.WeaponList.Count == 0)
                        {
                            // Random item
                            passiveOrActive(ref tempPossiblePassiveItems, ref tempPossibleWeapons, button, false, i);
                        }
                        else if (gameManager.PassiveList.Count >= gameManager.MaxPassiveSlots && gameManager.WeaponList.Count >= gameManager.MaxWeaponSlots)
                        {
                            // Owned item
                            passiveOrActive(ref tempCurrentPassiveItems, ref tempCurrentWeapons, button, true, i);
                        }
                        else
                        {
                            if (UnityEngine.Random.Range(0, 2) == 1 && (tempCurrentPassiveItems.Count != 0 || tempCurrentWeapons.Count != 0))
                            {
                                // Owned item
                                passiveOrActive(ref tempCurrentPassiveItems, ref tempCurrentWeapons, button, true, i);
                            }
                            else
                            {
                                // Random item
                                passiveOrActive(ref tempPossiblePassiveItems, ref tempPossibleWeapons, button, false, i);
                            }
                        }

                        if (button.gameObject.name == "Item1")
                        {
                            EventSystem.current.SetSelectedGameObject(button.gameObject);
                        }
                    }
                }

                
            }
        }

    }

    private void ClearButtonsDelegate()
    {
        foreach (Transform button in gameManager.levelUpPanel.transform)
        {
            button.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}
