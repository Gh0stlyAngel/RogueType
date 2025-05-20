using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarController : MonoBehaviour
{
    [Header("UI - Exp Bar")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;

    public void UpdateExpBar(int maxExp, int currentExp, int currentLevel)
    {
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;
        expText.text = "Level: " + currentLevel;
    }
}
