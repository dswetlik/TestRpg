using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class MainMenu : MonoBehaviour
{
    bool isLoading = false;

    public AudioMixer mixer;

    GameObject UIPauseScreen;
    GameObject UIIAPScreen;
    GameObject UICreditsScreen;
    GameObject UILoadScreen;

    Button iapRemoveAdsBtn;
    Button iapDonateBtn;

    Text mainLoadingPercentTxt;
    Slider mainLoadingSlider;

    Slider musicSlider;
    Slider sfxSlider;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        Advertisements.Instance.Initialize();
        IAPManager.Instance.InitializeIAPManager(InitializeResultCallback);

        UIPauseScreen = GameObject.Find("UI Main Pause");
        UIIAPScreen = GameObject.Find("UI Main IAP");
        UICreditsScreen = GameObject.Find("UI Main Credits");
        UILoadScreen = GameObject.Find("UI Main Load");

        iapRemoveAdsBtn = GameObject.Find("IAPRemoveAdsBtn").GetComponent<Button>();
        iapDonateBtn = GameObject.Find("IAPDonateBtn").GetComponent<Button>();

        iapRemoveAdsBtn.onClick.AddListener(() => PurchaseRemoveAds());
        iapDonateBtn.onClick.AddListener(() => PurchaseDonate());

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

        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        UIPauseScreen.SetActive(false);
        UIIAPScreen.SetActive(false);
        UICreditsScreen.SetActive(false);
        UILoadScreen.SetActive(false);
    }

    public void NewGame()
    {
        isLoading = false;
        GameObject.Find("NewGameBtn").GetComponent<Button>().interactable = false;
        GameObject.Find("LoadBtn").GetComponent<Button>().interactable = false;
        Advertisements.Instance.HideBanner();

        Advertisements.Instance.ShowInterstitial();
        StartCoroutine(LoadStartScene("LoadScene", isLoading));

        UILoadScreen.SetActive(true);
    }

    public void LoadGame()
    {
        isLoading = true;
        GameObject.Find("NewGameBtn").GetComponent<Button>().interactable = false;
        GameObject.Find("LoadBtn").GetComponent<Button>().interactable = false;
        Advertisements.Instance.HideBanner();

        Advertisements.Instance.ShowInterstitial();
        StartCoroutine(LoadStartScene("LoadScene", isLoading));

        UILoadScreen.SetActive(true);
    }

    private void InterstitialClosed()
    {
        StartCoroutine(LoadStartScene("LoadScene", isLoading));
    }

    public void ActivatePauseScreen(bool x)
    {
        UIPauseScreen.SetActive(x);
        UIPauseScreen.GetComponent<Image>().raycastTarget = x;
    }

    public void ActivateIAPScreen(bool x)
    {
        UIIAPScreen.SetActive(x);
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

    public void ToGooglePlayListing()
    {
        Application.OpenURL("http://play.google.com/store/apps/details?id=com.XaericGameStudioLLC.Gladiatorial");
    }

    public void ToXaericWebsite()
    {
        Application.OpenURL("https://xaericgamestudio.wordpress.com/");
    }

    void PurchaseRemoveAds()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.RemoveAds, ProductBoughtCallback);
    }

    void PurchaseDonate()
    {
        IAPManager.Instance.BuyProduct(ShopProductNames.Donate, ProductBoughtCallback);
    }

    private void InitializeResultCallback(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
    {
        if (status == IAPOperationStatus.Success)
        {
            //IAP was successfully initialized
            //loop through all products
            for (int i = 0; i < shopProducts.Count; i++)
            {
                if (shopProducts[i].productName == "RemoveAds")
                {
                    //if active variable is true, means that user had bought that product
                    //so enable access
                    if (shopProducts[i].active)
                    {
                        iapRemoveAdsBtn.interactable = false;
                        Advertisements.Instance.RemoveAds(true);
                    }
                }
                else if (shopProducts[i].productName == "Donate")
                {
                    if(shopProducts[i].active)
                    {
                        iapDonateBtn.interactable = false;
                    }
                }
            }
        }
        else
        {

        }
    }

    // automatically called after one product is bought
    // this is an example of product bought callback
    private void ProductBoughtCallback(IAPOperationStatus status, string message, StoreProduct product)
    {
        if (status == IAPOperationStatus.Success)
        {
            //each consumable gives coins in this example
            if (product.productName == "RemoveAds")
            {
                iapRemoveAdsBtn.interactable = false;
                Advertisements.Instance.RemoveAds(true);
            }
            else if (product.productName == "Donate")
            {
                iapDonateBtn.interactable = false;
            }

        }
        else
        {
            //an error occurred in the buy process, log the message for more details
            Debug.Log("Buy product failed: " + message);
        }
    }
}
