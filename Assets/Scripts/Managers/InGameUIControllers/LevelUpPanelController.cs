using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LevelUpPanelController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private List<Button> itemButtons;
    [SerializeField] private ItemManager itemManager;

    [Serializable]
    public class ButtonElements
    {
        public Image icon;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI descriptionText;
        public GameObject newLabel;
    }

    [SerializeField] private List<ButtonElements> buttonElements;

    [SerializeField] private float submitDelay = 0.5f;
    [SerializeField] private float menuOpenedTime;

    public void ShowPanel()
    {
        panel.SetActive(true);
        menuOpenedTime = Time.unscaledTime;
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    public void ClearButtons()
    {
        foreach (var button in itemButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }

    public void SetupButton(int index, BaseItem item, bool isLevelUp, Action onClick)
    {
        if (index < 0 || index >= itemButtons.Count)
            return;

        Button button = itemButtons[index];
        ButtonElements ui = buttonElements[index];

        button.gameObject.SetActive(true);
        ui.icon.sprite = item.ItemCardIdle;

        ui.newLabel.SetActive(!isLevelUp);
        ui.levelText.gameObject.SetActive(isLevelUp);
        ui.levelText.text = isLevelUp ? $"Level: {item.CurrentLevel + 1}" : "";

        ui.descriptionText.text = item.GetLevelDescription(isLevelUp ? item.CurrentLevel + 1 : item.CurrentLevel);

        SpriteState spriteState = new SpriteState
        {
            pressedSprite = item.ItemCardActive,
            selectedSprite = item.ItemCardActive
        };
        button.spriteState = spriteState;

        button.onClick.AddListener(() =>
        {
            float timeSinceOpen = Time.unscaledTime - menuOpenedTime;
            if (timeSinceOpen >= submitDelay)
            {
                onClick?.Invoke();
                GameManager.Instance.DisableAttackInput();
            }
                
        });
    }
}
