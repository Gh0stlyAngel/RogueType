using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimerManager : MonoBehaviour
{
    public int CurrentMinute;
    public int CurrentSecond;
    [SerializeField] private TextMeshProUGUI timer;

    public void StartGameTimer()
    {
        CurrentMinute = 0;
        CurrentSecond = 0;
        StartCoroutine(GameTimer());
    } 

    IEnumerator GameTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CurrentSecond += 1;
            if (CurrentSecond == 60)
            {
                CurrentSecond = 0;
                CurrentMinute += 1;
            }
            string minuteText;
            string secondText;

            if (CurrentMinute < 10)
            {
                minuteText = "0" + CurrentMinute;
            }
            else
            {
                minuteText = $"{CurrentMinute}";
            }

            if (CurrentSecond < 10)
            {
                secondText = "0" + CurrentSecond;
            }
            else
            {
                secondText = $"{CurrentSecond}";
            }

            timer.text = $"{minuteText} : {secondText}";
        }
    }
}
