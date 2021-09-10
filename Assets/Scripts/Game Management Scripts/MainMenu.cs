using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    GameObject UILoadScreen;
    Text mainLoadingPercentTxt;
    Slider mainLoadingSlider;

    // Start is called before the first frame update
    void Start()
    {
        UILoadScreen = GameObject.Find("UI Main Load");
        mainLoadingPercentTxt = GameObject.Find("MainLoadingPercentTxt").GetComponent<Text>();
        mainLoadingSlider = GameObject.Find("MainLoadingSlider").GetComponent<Slider>();

        UILoadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    IEnumerator LoadStartScene(string sceneName, bool loadGame)
    {
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
            //Debug.Log("Scene is Valid");

            SceneManager.SetActiveScene(thisScene);
        }
        SceneManager.UnloadSceneAsync(currentScene);

        GameObject.Find("GameManager").GetComponent<Engine>().isLoadingGame = loadGame;
        GameObject.Find("GameManager").GetComponent<Engine>().InitializeGame();
    }
}
