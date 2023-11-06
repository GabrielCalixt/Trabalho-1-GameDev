using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]  Slider volumeSlider;



    public void Save()
    {
        PlayerPrefs.SetFloat("volumeValue", volumeSlider.value);

    }
    public void  Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    void changeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
