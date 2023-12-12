
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField]public AudioSource musicSource;
    //[SerializeField] AudioSource SFXSource;


    [Header("Background Music")]
    public AudioClip menuBackground;
    public AudioClip gameBackground; 
    public AudioClip gameBackgroundTwo;
    public AudioClip gameBackgroundThree;


    public AudioClip lastScene;




    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
     //   musicSource.volume = 0.5f;

    }



    private void Update()
    {

        Debug.Log("Music Source: " + musicSource); 

        PlayMusic();
}

    public void PlayMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 11 || SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 10)
        {
            if (musicSource.clip != menuBackground)
            {
                // musicSource.Stop();
                musicSource.clip = menuBackground;
                musicSource.Play();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (musicSource.clip != gameBackground)
            {
                //  musicSource.Stop();    
                musicSource.clip = gameBackground;
                musicSource.Play();
            }

        }


        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (musicSource.clip != gameBackgroundTwo)
            {
                //  musicSource.Stop();    
                musicSource.clip = gameBackgroundTwo;
                musicSource.Play();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            if (musicSource.clip != gameBackgroundThree)
            {
                //  musicSource.Stop();    
                musicSource.clip = gameBackgroundThree;
                musicSource.Play();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            if (musicSource.clip != lastScene)
            {
                //  musicSource.Stop();    
                musicSource.clip = lastScene;
                musicSource.Play();
            }
        }
        Debug.Log("Playing Music: " + musicSource.clip.name);

        }
    

    public void StopMusic()
    {
        musicSource.Stop();
      
    }



    }
