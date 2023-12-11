
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{

    public Slider musicSlider;
    private float musicVol;

    public Slider sfxSlider;
    private float sfxVol;

    public AudioManager music;
    public SFXcontrol sfx;
    public GameObject settings;





    private void Awake()
    {
       
        DontDestroyOnLoad(this);
        musicSlider.value = 0.3f;
        sfxSlider.value = 0.8f;
    }
    private void Start()
    {

    }

    private void Volume()
    {
        musicVol = musicSlider.value * 0.5f;
        Debug.Log("Slider Value: " + musicSlider.value);
        Debug.Log("Calculated Volume: " + musicVol);
        music.musicSource.volume = musicVol;

        sfxVol = sfxSlider.value * 0.5f;
        sfx.SFXSource.volume = sfxVol;  
    }

    // Update is called once per frame
    void Update()
    {
        Volume();

        if (SceneManager.GetActiveScene().buildIndex != 11)
        {
            settings.SetActive(false);

         
        }
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            settings.SetActive(true);
        }
    }
}
