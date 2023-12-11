
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] public Text gemsText;
    [SerializeField] public Text timerText;
    [SerializeField] public Text deathText;
    [SerializeField] public Text godModeText;

    [SerializeField] public Text itemText;
    [SerializeField] private GameObject GodMode;

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private GameObject collectedItemsDisplay;

    private float currentTime = 0f;

    public DoorControl door;
    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        door = FindObjectOfType<DoorControl>();
        collectedItemsDisplay.SetActive(false);
        GodMode.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
        if (!gameManager.pauseOn)
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
            deathText.text = gameManager.getDeathCountUI().ToString();

            currentTime += Time.deltaTime;

            
            timerText.text = FormatTimer(currentTime);

            if (playerControl.activeQuest)
            {
                collectedItemsDisplay.SetActive(true);
            }
        }
    }
    string FormatTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
    private void DisableMode()
    {
        GodMode.SetActive(false);
    }
}
