using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI gemsText;
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public TextMeshProUGUI keyText;

    [SerializeField] private PlayerControl playerControl;
    private float currentTime = 0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        gemsText.text = playerControl.collectedGems.ToString() + "/5";
        keyText.text = playerControl.collectedKey.ToString() + "/1";

        currentTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds/10);
        timerText.text = timeString;
    }
}
