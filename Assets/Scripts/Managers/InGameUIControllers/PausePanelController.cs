using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PausePanelController : MonoBehaviour
{
    [SerializeField] private Image pausePanel;

    public void TogglePanel()
    {
        pausePanel.gameObject.SetActive(!pausePanel.isActiveAndEnabled);
    }

    public void ShowPanel()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        pausePanel.gameObject.SetActive(false);
    }
}
