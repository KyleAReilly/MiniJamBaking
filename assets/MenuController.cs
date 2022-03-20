using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public Slider volSlider;
   

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", .8f);
            Load();
        }
        else
        {
            Load();
        }
        
       
        
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volSlider.value;
    }
    private void Load()
    {
        volSlider.value = PlayerPrefs.GetFloat("musicVolume");

    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volSlider.value);

    }

   

    public void Restart()
    {
        SceneManager.LoadScene("Main");

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }


}
