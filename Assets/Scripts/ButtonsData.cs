using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsData : MonoBehaviour
{
    public static ButtonsData Instance;

    public GameObject[] characters;
    public Button[] characterButtons;

    public Button[] generalPersonal;
    public Button[] personalCharacters;
    public GameObject[] personalPanels;

    [SerializeField] private Button startGameButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void SelectButton(int buttonIndex, Button[] buttonArray, string prefsKey = "")
    {
        foreach (Button button in buttonArray)
        {
            if (button != buttonArray[buttonIndex])
            {
                EnableMask(button);
            }
            else
            {
                DisableMask(button);
            }
        }

        if (prefsKey != string.Empty)
        {
            GameData.Instance.SelectedCharacter = characters[buttonIndex];

            PlayerPrefs.SetInt(prefsKey, buttonIndex);
            PlayerPrefs.Save();
        }

    }

    public void EnableMask(Button button)
    {
        foreach (Transform t in button.transform)
        {
            if (t.gameObject.name == "Mask")
            {
                t.gameObject.SetActive(true);
            }
        }
    }

    public void DisableMask(Button button)
    {
        foreach (Transform t in button.transform)
        {
            if (t.gameObject.name == "Mask")
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    public void SelectPersonalChracter(int index)
    {
        SelectButton(index, ButtonsData.Instance.personalCharacters);

        foreach (Button button in ButtonsData.Instance.personalCharacters)
        {
            button.interactable = true;
        }
        foreach (GameObject personalPanel in ButtonsData.Instance.personalPanels)
        {
            personalPanel.SetActive(false);
        }

        ButtonsData.Instance.personalCharacters[index].interactable = false;
        ButtonsData.Instance.personalPanels[index].SetActive(true);
    }

    /// Character Selection Panel


    public void SelectCharacter(int index)
    {
        SelectButton(index, ButtonsData.Instance.characterButtons, "LastSelectedChar");
    }

    public void RunGame()
    {
        SceneManager.LoadScene("MainScene");
    }


}
