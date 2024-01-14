using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour
{
    [Header("Menus")]

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject MainMenu;


    [Header("Slider")]

    [SerializeField] private Slider loadingSlider;

    public void sceneloader(int levelId)
    {
        MainMenu.SetActive(false);
        
        loadingScreen.SetActive(true);

        StartCoroutine(Delayload(levelId));
    }

    IEnumerator Delayload(int levelId)
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(LoadLevelAsync(levelId));
    }


    IEnumerator LoadLevelAsync(int levelId)
    {
        AsyncOperation loadlevelOperation = SceneManager.LoadSceneAsync(levelId);

        while (!loadlevelOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadlevelOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
        
    }



}
