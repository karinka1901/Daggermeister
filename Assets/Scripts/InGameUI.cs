using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] public Text gemsText;
    [SerializeField] public Text timerText;
    [SerializeField] public Text godModeText;

    [SerializeField] public Text itemText;
    [SerializeField] private GameObject GodMode;

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private GameObject collectedItemsDisplay;

    private float currentTime = 0f;

    void Start()
    {
        collectedItemsDisplay.SetActive(false);
        GodMode.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (playerControl.godMode)
        {
            GodMode.SetActive(true);
            godModeText.text = "NOOB MODE: ON";
            
        }
        else
        {
            godModeText.text = "NOOB MODE: OFF";
            Invoke("DisableMode", 0.5f);
        }

        gemsText.text = playerControl.collectedGems.ToString() + "/6";
        itemText.text = playerControl.collectedItem.ToString() + "/1";

        currentTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds/10);
        timerText.text = timeString;

        if (playerControl.activeQuest)
        {
            collectedItemsDisplay.SetActive(true);
        }
    }

    private void DisableMode()
    {
        GodMode.SetActive(false);
    }
}
