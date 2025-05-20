using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private AbilityCooldownController abilityCooldownController;
    public AbilityCooldownController AbilityCooldownController => abilityCooldownController;


    [SerializeField] private GameOverPanelController gameOverController;
    public GameOverPanelController GameOverController => gameOverController;


    [SerializeField] private ExpBarController expBarController;
    public ExpBarController ExpBarController => expBarController;


    [SerializeField] private HealthBarController healthBarController;
    public HealthBarController HealthBarController => healthBarController;

    [SerializeField] private PausePanelController pausePanelController;
    public PausePanelController PausePanelController => pausePanelController;

    [SerializeField] private LevelUpPanelController levelUpPanelController;
    public LevelUpPanelController LevelUpPanelController => levelUpPanelController;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

}
