using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject powerUpPanel;

    [SerializeField] private GameObject powerUpGeneralPanel;
    [SerializeField] private GameObject powerUpPersonalPanel;

    private void Start()
    {
        ButtonsData.Instance.SelectButton(PlayerPrefs.GetInt("LastSelectedChar", 0), ButtonsData.Instance.characterButtons, "LastSelectedChar");
    }

    public void CloseAll()
    {
        mainMenuPanel.SetActive(false);
        characterSelectionPanel.SetActive(false);
        optionsPanel.SetActive(false);
        powerUpPanel.SetActive(false);
    }

    public void OpenMainMenu()
    {
        CloseAll();
        mainMenuPanel.SetActive(true);
    }

    public void OpenCharacterSelection()
    {
        CloseAll();
        characterSelectionPanel.SetActive(true);
    }

    public void OpenOptionsPanel()
    {
        CloseAll();
        optionsPanel.SetActive(true);
    }

    public void OpenPowerUpPanel()
    {
        CloseAll();
        powerUpPanel.SetActive(true);
    }

    /// Power Up Panel

    public void SelectGeneralPowerUp()
    {
        ButtonsData.Instance.generalPersonal[0].interactable = false;
        ButtonsData.Instance.generalPersonal[1].interactable = true;
        ButtonsData.Instance.SelectButton(1, ButtonsData.Instance.generalPersonal, string.Empty);

        powerUpPersonalPanel.SetActive(false);
        powerUpGeneralPanel.SetActive(true);
    }

    public void SelectPersonalPowerUp()
    {
        ButtonsData.Instance.generalPersonal[1].interactable = false;
        ButtonsData.Instance.generalPersonal[0].interactable = true;
        ButtonsData.Instance.SelectButton(0, ButtonsData.Instance.generalPersonal, string.Empty);

        powerUpPersonalPanel.SetActive(true);
        powerUpGeneralPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
