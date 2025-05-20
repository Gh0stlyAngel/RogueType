using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownController : MonoBehaviour
{
    [SerializeField] private Image abilityIcon;
    [SerializeField] private Image abilityCooldownMask;
    [SerializeField] private TextMeshProUGUI abilityCooldownText;

    public void SetAbilityIcon(Sprite icon)
    {
        abilityIcon.sprite = icon;
    }
    public void StartAbilityCooldown(float cooldown)
    {
        abilityCooldownMask.gameObject.SetActive(true);
        abilityCooldownText.gameObject.SetActive(true);

        UpdateAbilityCooldown(cooldown);
    }

    public void UpdateAbilityCooldown(float cooldown)
    {
        abilityCooldownText.text = cooldown.ToString("F1", CultureInfo.InvariantCulture);
    }

    public void EndAbilityCooldown()
    {
        abilityCooldownMask.gameObject.SetActive(false);
        abilityCooldownText.gameObject.SetActive(false);
    }
}
