using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    /*public GameObject[] characters;
    public Button[] characterButtons;

    public Button[] generalPersonal;
    public Button[] personalCharacters;
    public GameObject[] personalPanels;
*/
    //[SerializeField] private Button startGameButton;

    public GameObject SelectedCharacter;

    public List<float> GeneralBonusStats;

    public List<float> WarriorBonusStats;
    public List<float> ReaperBonusStats;
    public List<float> KnightBonusStats;

    public string saveFilePath;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        saveFilePath = Application.persistentDataPath + "/PlayerProgress.json";
        DontDestroyOnLoad(gameObject);
    }

    /*public void SelectButton(int buttonIndex, Button[] buttonArray, string prefsKey = "")
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
            SelectedCharacter = ButtonsData.Instance.characters[buttonIndex];

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

    /// Personal Power Up Panel
    
    public void SelectPersonalChracter(int index)
    {
        SelectButton(index, ButtonsData.Instance.personalCharacters);

        foreach (Button button in ButtonsData.Instance.personalCharacters)
        {
            button.interactable = true;
        }
        foreach(GameObject personalPanel in ButtonsData.Instance.personalPanels)
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
    }*/

}
