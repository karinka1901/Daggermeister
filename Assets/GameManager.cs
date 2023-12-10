using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool mainMenuOn;
    public bool pauseOn;

    [Header("Level Control")]
    public GameObject[] lockedLevelPictures;
    public GameObject[] levelButtons;
    static int levelsCompleted = 0;

    [Header("Timer/Score control")]
    public float secretTimer = 0f;
    public Text[] bestTimeText; 
    public static float[] levelTimeScores = {0,0,0,0,0,0};
    public static float[] newBest = { 0f, 0f, 0f, 0f, 0f, 0f };

    [Header("Death/Score control")]
    public static int[] playerDeathPerLvl = { 0, 0, 0, 0, 0, 0 };
    [SerializeField]static public int playerDeathCount = 0;
    static int playerDeathCountUI = 0;
    public Text[] deathCountText;
    public static bool nextLevel;




    void Start()
    {
        pauseMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name == "LevelSelect")
        {
            CheckUnlockedLevels();
            UpdateTimerInLevelSelect();
            DisplayDeathCount();
        }

        
    }


    void Update()
    {

        if (!pauseOn)
        {
            secretTimer += Time.deltaTime;
        }

        if (SceneManager.GetActiveScene().name == "Start")
        {
            mainMenuOn = true;
        }
        else
        {
            mainMenuOn = false;
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuOn)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            pauseOn = !pauseOn;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "LevelSelect")
        {
            SceneManager.LoadScene("Start");
        }
    }


    /// //////////////////PLAYER DEATH DISPLAY///////////////////////////////

    /// in ui /// ////////////
    public int getDeathCountUI()
    {
        return playerDeathCountUI;
    }
    public void AddDeathCountUI()
    {
        playerDeathCountUI++;
    }
    public void ResetDeathCountUI()
    {
        playerDeathCountUI = 0;
    }

    /// in level selection /// ////////////

    public int getDeathCount()
    {
        return playerDeathCount;
    }
    public void AddDeathCount()
    {
        playerDeathCount++;
    }

    public void ResetDeathCount()
    {
        playerDeathCount = 0;
    }

    public void UpdateDeath(int lvl, int death) //is called in DoorControl script
    {
        playerDeathPerLvl[lvl] = death;
    }


    public void DisplayDeathCount() 
    { 
    
        for (int i = 0; i < levelsCompleted; i++)
        {
            deathCountText[i].text = playerDeathPerLvl[i].ToString();
            Debug.Log(playerDeathPerLvl[i]);

            
        }
    }

   

    /// //////////////////LEVEL UNLOCK///////////////////////////////
    public int getCompletedLevels()
    {
        return levelsCompleted;
    }

    public void AddCompletedLevel()
    {
        levelsCompleted++;
    }

    void CheckUnlockedLevels()
    {
        for (int i = 0; i < levelsCompleted; i++)
        {
            
            lockedLevelPictures[i+1].SetActive(false);
            levelButtons[i+1].SetActive(true);

        }
    }

    /// //////////////////TIME SCORE///////////////////////////////

    public void UpdateTimer(int lvl, float time) //is called in DoorControl script
    {
        levelTimeScores[lvl] = time;
        UpdateBestTime(lvl);
    }

    void UpdateBestTime(int lvl)
    {
        if (levelTimeScores[lvl] < newBest[lvl] || newBest[lvl] == 0f)
        {
            newBest[lvl] = levelTimeScores[lvl];
            Debug.Log("did the thingy");
        }
        else {
            Debug.Log("didnt do the thingy");
                }
    }

    void UpdateTimerInLevelSelect()
    {
        for (int i = 0; i < levelTimeScores.Length; i++)
        {
            string currentTimeString = FormatTime(levelTimeScores[i]);
            string bestTimeString = FormatTime(newBest[i]);

            bestTimeText[i].text = "Completed "+ currentTimeString + "\n" + "Best Time " + bestTimeString + "\n"; ;
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }


    /// //////////////////BUTTONS CONTROL///////////////////////////////
    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
        mainMenuOn = false;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseOn = false;
        Debug.Log("Resume");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
        mainMenuOn = true;
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
        mainMenuOn = true;
    }


    public void LoadLevel(int lvl)
    {
        if (lvl < SceneManager.sceneCountInBuildSettings)
        {
                SceneManager.LoadScene(lvl);
            
        }
    }


}

