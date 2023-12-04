using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI gemsText;
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public TextMeshProUGUI keyText;
    [SerializeField] public TextMeshProUGUI itemText;

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private GameObject collectedItemsDisplay;
    private float currentTime = 0f;

    void Start()
    {
        collectedItemsDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        gemsText.text = playerControl.collectedGems.ToString() + "/6";
        //keyText.text = playerControl.collectedKey.ToString() + "/1";
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
}
