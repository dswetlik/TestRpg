using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        StartCoroutine(LoadStartScene("LoadScene", false));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadStartScene("LoadScene", true));
    }

    IEnumerator LoadStartScene(string sceneName, bool loadGame)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation newScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        newScene.allowSceneActivation = false;

        while (newScene.progress < 0.9f)
        {
            Debug.Log("Loading scene: " + newScene.progress);
            yield return null;
        }

        newScene.allowSceneActivation = true;

        while (!newScene.isDone)
        {
            yield return null;
        }

        Scene thisScene = SceneManager.GetSceneByName(sceneName);

        if (thisScene.IsValid())
        {
            Debug.Log("Scene is Valid");

            SceneManager.SetActiveScene(thisScene);
        }
        SceneManager.UnloadSceneAsync(currentScene);

        GameObject.Find("GameManager").GetComponent<Engine>().isLoadingGame = loadGame;
        GameObject.Find("GameManager").GetComponent<Engine>().InitializeGame();
    }
}
