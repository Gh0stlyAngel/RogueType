using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private MainCamera mainCamera;

    [Header("Player")]
    private GameObject player;
    [SerializeField] private GameObject currentPlayerPrefab;

    [Header("Managers")]
    [SerializeField] private MapManager mapManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private ItemManager itemManager;
    [SerializeField] private GemCounter gemCounter;
    [SerializeField] private CollectableManager collectableManager;
    [SerializeField] private GameTimerManager gameTimerManager;
    private PausePanelController pausePanelController;

    [Header("Audio Managment")]
    [SerializeField] private SoundMixerManager soundMixerManager;
    [SerializeField] private SFXManager soundFXManager;
    [SerializeField] private AudioMixerGroup soundFXMixerGroup;
    [SerializeField] private AudioMixerGroup hurtSoundFXMixerGroup;

    [Header("Game State")]
    public bool IsGamePaused;
    private bool gameOver;

    [Header("UI")]
    public GameObject levelUpPanel;


    public float abilityCurrentCooldown;

    [Header("Item Slots Managment")]
    public Image WeaponSlot1;
    public Image WeaponSlot2;
    public Image WeaponSlot3;

    public Image PassiveSlot1;
    public Image PassiveSlot2;
    public Image PassiveSlot3;

    public GameObject levelMarker;
    public int MaxWeaponSlots { get; private set; }
    public List<GameObject> WeaponList;
    public int MaxPassiveSlots { get; private set; }
    public List<GameObject> PassiveList;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
        pausePanelController = UIManager.Instance.PausePanelController;

        //player = Instantiate(currentPlayerPrefab, currentPlayerPrefab.transform.position, currentPlayerPrefab.transform.rotation);

        var selectedPrefab = GameData.Instance.SelectedCharacter;
        player = Instantiate(selectedPrefab, selectedPrefab.transform.position, selectedPrefab.transform.rotation);

        WeaponList = new List<GameObject>();
        PassiveList = new List<GameObject>();

        MaxWeaponSlots = 3;
        MaxPassiveSlots = 3;

        mapManager.Init(player);
        spawnManager.Init(player);
        mainCamera.Init(player);
        itemManager.Init(player, this);
        gemCounter.Init(player);
        collectableManager.Init(player, player.GetComponent<BaseCharacter>().GetCollectRadius());

        gameOver = false;

        gameTimerManager.StartGameTimer();
    }

    public void DisableAttackInput()
    {
        player.GetComponent<PlayerController>().DisableAttackForOneFrame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameOver)
            {
                if (levelUpPanel.activeSelf && IsGamePaused)
                {
                    pausePanelController.TogglePanel();
                }
                else if (IsGamePaused)
                {

                    ResumeGame();
                    pausePanelController.HidePanel();
                }
                else
                {
                    PauseGame();
                    pausePanelController.ShowPanel();
                }
            }
            
        }


    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        SFXManager.instance.PauseActiveSFX();
        IsGamePaused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;

        SFXManager.instance.ResumeActiveSFX();
        IsGamePaused = false;
    }

    public void GameOver()
    {
        EndRunPanel(false);
    }

    public void Victory()
    {
        EndRunPanel(true);
    }

    private void EndRunPanel(bool winner)
    {

        StartCoroutine(EndRunCoroutine(winner));

        IEnumerator EndRunCoroutine(bool winner)
        {
            float startVolume = SFXManager.instance.BGMusicSource.volume;
            gameOver = true;
            if (winner)
                UIManager.Instance.GameOverController.SetVictoryPanel();
            else
                UIManager.Instance.GameOverController.SetGameOverPanel();

            UIManager.Instance.GameOverController.ShowPanel();
            while (Time.timeScale >= 0)
            {
                if (Time.timeScale - Time.fixedDeltaTime >= 0)
                {
                    Time.timeScale -= Time.fixedDeltaTime;
                }
                else
                {
                    SFXManager.instance.BGMusicSource.volume = 0;
                    Time.timeScale = 0;
                    break;
                }

                UIManager.Instance.GameOverController.FadeInPanel();
                SFXManager.instance.BGMusicSource.volume -= startVolume * (Time.fixedDeltaTime * 2.0f * (1 - (1 - startVolume)));

                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime * 2.0f);
            }
            UIManager.Instance.GameOverController.ShowQuitButton();
        }
    }



    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
