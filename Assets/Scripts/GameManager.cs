
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

   // AudioManager music;
    SFXcontrol sfx;
   // public GameObject settings;

    public GameObject pauseMenu;
   // public GameObject pause;
    private GameObject settings;
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


    private void Awake()
    {
       // SceneManager.LoadScene("StartingScene");
        Debug.Log(SceneManager.GetActiveScene().name);
       // DontDestroyOnLoad(settings);
    }

    void Start()
    {

        pauseMenu.SetActive(false);
        mainMenuOn = true;
        ////////////////////MUSIC CONTROL//////////////////////////////////////////////////////////////////////////////////////////

        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>(); 
        settings = GameObject.FindGameObjectWithTag("SettingsMenu");

        /////////////////////////////LEVEL SELECTION MENU////////////////////////////////////////////////////////
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

        if (SceneManager.GetActiveScene().name == "Start" || SceneManager.GetActiveScene().name == "LevelSelect")
        {
            mainMenuOn = true;
        }
        else
        {
            mainMenuOn = false;
        }


        if (Input.GetKeyDown(KeyCode.Tab) && !mainMenuOn)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            pauseOn = !pauseOn;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && SceneManager.GetActiveScene().name == "LevelSelect" || Input.GetKeyDown(KeyCode.Tab) && SceneManager.GetActiveScene().name == "SettingsMenu")
        {
            SceneManager.LoadScene("Start");
         
        }

        if ((Input.anyKeyDown) && SceneManager.GetActiveScene().name == "StartingScene")
        {
            SceneManager.LoadScene("Start");

        }

        if ((Input.anyKeyDown) && SceneManager.GetActiveScene().name == "Level8")
        {
            SceneManager.LoadScene("Start");

        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && SceneManager.GetActiveScene().name == "LevelSelect" )
        {
            levelsCompleted = 6;
            SceneManager.LoadScene("LevelSelect");

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
            deathCountText[i].text = playerDeathPerLvl[i+1].ToString();
            Debug.Log(playerDeathPerLvl[i+1]);

            
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
            levelButtons[i].SetActive(true);

            

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
        for (int i = 1; i < levelTimeScores.Length; i++)
        {
            Debug.Log(levelTimeScores.Length);
            string currentTimeString = FormatTime(levelTimeScores[i]);
            string bestTimeString = FormatTime(newBest[i]);

            bestTimeText[i-1].text = "Completed "+ currentTimeString + "\n" + "Best Time " + bestTimeString + "\n"; ;
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
        sfx.PlaySFX(sfx.buttonPress);
    }

    public void Resume()
    {
        sfx.PlaySFX(sfx.buttonPress);
        pauseMenu.SetActive(false);
        pauseOn = false;
        Debug.Log("Resume");
    }

    public void VolumePauseMenu()
    {
        sfx.PlaySFX(sfx.buttonPress);
       // pauseMenu.SetActive(false);
        pauseOn =false;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(10);
    }

    public void MainMenu()
    {
        sfx.PlaySFX(sfx.buttonPress);
        SceneManager.LoadScene("Start");
        mainMenuOn = true;
    }
    public void LevelSelect()
    {
        sfx.PlaySFX(sfx.buttonPress);
        SceneManager.LoadScene("LevelSelect");
        mainMenuOn = true;
    }


    public void LockedButtons()
    {
     sfx.PlaySFX(sfx.lockedLvl);
    }

    public void LoadLevel(int lvl)
    {
        if (lvl < SceneManager.sceneCountInBuildSettings)
        {
           sfx.PlaySFX(sfx.buttonPress);
            SceneManager.LoadScene(lvl+1);
         

            Debug.Log(mainMenuOn);
            
        }
    }


}

