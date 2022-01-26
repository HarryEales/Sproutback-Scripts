using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SlidersMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider zoomSlider;

    public Camera camera;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        //audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        //audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        camera.orthographicSize = PlayerPrefs.GetFloat("ZoomValue");
        zoomSlider.value = PlayerPrefs.GetFloat("ZoomValue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMusicLevelChanged()
    {
        audioMixer.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void OnSFXLevelChanged()
    {
        audioMixer.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void OnZoomLevelChanged()
    {
        camera.orthographicSize = zoomSlider.value;
        PlayerPrefs.SetFloat("ZoomValue", zoomSlider.value);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
