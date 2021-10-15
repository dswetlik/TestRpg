using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicAudio", 0.75f);
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicAudio", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicAudio", sliderValue);
    }
}
