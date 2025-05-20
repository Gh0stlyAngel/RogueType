using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("UI - HP Bar")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Image characterHealthFill;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthSlider.maxValue = maxHealth;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();
    }

    public void UpdateCharacterHealthBar(float maxHealth, float currentHealth)
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        characterHealthFill.fillAmount = currentHealth / maxHealth;
    }

    public void SetCharacterHealthFill(Image healthFill)
    {
        characterHealthFill = healthFill;
    }
}
