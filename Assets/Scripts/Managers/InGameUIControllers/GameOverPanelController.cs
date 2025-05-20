using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{
    

    private Image currentText;
    private Image currentPanel;
    private Button currentQuitButton;

    [Header("UI - Victory")]
    [SerializeField] private Image victoryPanel;
    [SerializeField] private Image victoryText;
    [SerializeField] private Button victoryQuitButton;

    [Header("UI - Game Over")]
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Image gameOverText;
    [SerializeField] private Button gameOverQuitButton;


    public void SetVictoryPanel()
    {
        currentPanel = victoryPanel;
        currentText = victoryText;
        currentQuitButton = victoryQuitButton;
    }

    public void SetGameOverPanel()
    {
        currentPanel = gameOverPanel;
        currentText = gameOverText;
        currentQuitButton = gameOverQuitButton;
    }

    public void ShowPanel()
    {
        if (currentPanel != null)
        {
            currentPanel.gameObject.SetActive(true);
        }
    }

    public void FadeInPanel()
    {
        if (currentPanel != null && currentText != null)
        {
            Color newColor = currentPanel.color;
            newColor.a = 1 - Time.timeScale - 0.5f;
            currentPanel.color = newColor;

            newColor = currentText.color;
            newColor.a = 1 - Time.timeScale;
            currentText.color = newColor;
        }
        
    }

    public void ShowQuitButton()
    {
        if (currentQuitButton != null)
        {
            currentQuitButton.gameObject.SetActive(true);
        }
        
    }
}
