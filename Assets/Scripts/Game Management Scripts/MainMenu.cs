using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class MainMenu : MonoBehaviour
{

    public AudioMixer mixer;

    GameObject UIPauseScreen;
    GameObject UICreditsScreen;
    GameObject UILoadScreen;

    Text mainLoadingPercentTxt;
    Slider mainLoadingSlider;

    Slider musicSlider;
    Slider sfxSlider;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        UIPauseScreen = GameObject.Find("UI Main Pause");
        UICreditsScreen = GameObject.Find("UI Main Credits");
        UILoadScreen = GameObject.Find("UI Main Load");

        mainLoadingPercentTxt = GameObject.Find("MainLoadingPercentTxt").GetComponent<Text>();
        mainLoadingSlider = GameObject.Find("MainLoadingSlider").GetComponent<Slider>();

        musicSlider = GameObject.Find("MusicVolumeSetting").GetComponentInChildren<Slider>();
        sfxSlider = GameObject.Find("SFXVolumeSetting").GetComponentInChildren<Slider>();

        musicSlider.value = PlayerPrefs.GetFloat("MusicAudio", 0.7f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXAudio", 0.7f);
        mixer.SetFloat("MusicAudio", PlayerPrefs.GetFloat("MusicAudio", 0.7f));
        mixer.SetFloat("SFXAudio", PlayerPrefs.GetFloat("SFXAudio", 0.7f));

        if (File.Exists(Application.persistentDataPath + "/save.tpg"))
            GameObject.Find("LoadBtn").GetComponent<Button>().interactable = true;
        else
            GameObject.Find("LoadBtn").GetComponent<Button>().interactable = false;


        UIPauseScreen.SetActive(false);
        UICreditsScreen.SetActive(false);
        UILoadScreen.SetActive(false);
    }

    public void NewGame()
    {
        StartCoroutine(LoadStartScene("LoadScene", false));
        UILoadScreen.SetActive(true);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadStartScene("LoadScene", true));
        UILoadScreen.SetActive(true);
    }

    public void ActivatePauseScreen(bool x)
    {
        UIPauseScreen.SetActive(x);
        UIPauseScreen.GetComponent<Image>().raycastTarget = x;
    }

    public void ActivateCreditsScreen(bool x)
    {
        UICreditsScreen.SetActive(x);
    }

    IEnumerator LoadStartScene(string sceneName, bool loadGame)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation newScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        newScene.allowSceneActivation = false;

        mainLoadingSlider.value = newScene.progress;
        mainLoadingPercentTxt.text = ((int)(newScene.progress * 100)) + "%";

        while (newScene.progress < 0.9f)
        {
            //mainLoadingSlider.value = newScene.progress;
            mainLoadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
            mainLoadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
            yield return new WaitForFixedUpdate();
        }

        mainLoadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
        mainLoadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {
            mainLoadingSlider.value = Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime);
            mainLoadingPercentTxt.text = ((int)Mathf.MoveTowards(newScene.progress * 100, 90.0f, 0.25f * Time.deltaTime)) + "%";
            yield return new WaitForFixedUpdate();
        }

        Scene thisScene = SceneManager.GetSceneByName(sceneName);

        if (thisScene.IsValid())
        {
            SceneManager.SetActiveScene(thisScene);
        }
        SceneManager.UnloadSceneAsync(currentScene);

        GameObject.Find("GameManager").GetComponent<Engine>().isLoadingGame = loadGame;
        GameObject.Find("GameManager").GetComponent<Engine>().InitializeGame();
    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicAudio", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicAudio", sliderValue);
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXAudio", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXAudio", sliderValue);
    }
}
